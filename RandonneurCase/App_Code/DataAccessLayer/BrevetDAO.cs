using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Karis.DatabaseLibrary;
using System.Data;

/// <summary>
/// Summary description for BrevetDAO
/// </summary>
public class BrevetDAO
{

    // (2) A reference variable for a Database object
    private Database myDatabase;

    // (3) Connection string for connecting to the database   I USED MSSQLLocalDB instead of v11.0 because it is used by the server.
    private string myConnectionString =
        @"Data Source=(LocalDB)\MSSQLLocalDB;                                      
          AttachDbFilename=|DataDirectory|\RandonneurDatabase.mdf;
          Integrated Security=True;Connect Timeout=40";
                                                                                
    IDataReader resultSet;
    String sqlText;

    public BrevetDAO()
    {

        myDatabase = new Database();
    }

    public List<Brevet> GetAllBrevets()
    {
        myDatabase = new Database();
        List<Brevet> brevetList = new List<Brevet>();


        try
        {
            // (5) Open a connection to the database
            myDatabase.Open(myConnectionString);

            // (6) Construct a SELECT statement
            sqlText =
              "SELECT brevetId, distance, brevetDate, location, climbing " +
              "FROM Brevet ORDER BY distance ASC, brevetDate ASC, location ASC";

            // (7) Execute the SELECT statement
            resultSet = myDatabase.ExecuteQuery(sqlText);

            // (8) Process the multiple rows in the result set one by one 
            while (resultSet.Read() == true)  // 8.1 Move to the next available row 
            {                                 //     true = row available
                                              //     false = no more rows

                Brevet brevet = new Brevet();

                // 8.2 Retrieve column values from the current row in the result set
                brevet.BrevetId = (int)resultSet["brevetId"];
                brevet.Distance = (int)resultSet["distance"];
                brevet.BrevetDate = (DateTime)resultSet["brevetDate"];
                brevet.Location = (string)resultSet["location"];
                brevet.Climbing = (int)resultSet["climbing"];
                brevetList.Add(brevet);
            }

            // (9) Close the result set
            resultSet.Close();
        }

        catch (Exception)
        {
            return null;
        }

        // (10) Close the database connection
        finally
        {
            myDatabase.Close();
        }

        return brevetList;
    }



    public List<Rider>GetBrevetParticipant(int brevetId)
    {

        try
        {
            List<Rider> riderList = new List<Rider>();
            // (5) Open a connection to the database
            myDatabase.Open(myConnectionString);

            // (6) Construct a SELECT statement
            sqlText =
              "SELECT Rider.riderId, Rider.familyName, Rider.givenName, Rider.gender, Rider.phone, Rider.email, Rider.clubId, " +
              "Rider.username, Rider.password, Rider.role " +
               "FROM Rider INNER JOIN Brevet_Rider ON(Rider.riderId = Brevet_Rider.riderId) " +
               "WHERE Brevet_Rider.brevetId = '" + brevetId + "'";

            // (7) Execute the SELECT statement
            resultSet = myDatabase.ExecuteQuery(sqlText);

            // (8) Process the multiple rows in the result set one by one 
            while (resultSet.Read() == true)  // 8.1 Move to the next available row 
            {                                 //     true = row available
                                              //     false = no more rows
                Rider rider = new Rider();


                // 8.2 Retrieve column values from the current row in the result set

                rider.RiderId = (int)resultSet["riderId"];
                rider.FamilyName = (string)resultSet["familyName"];
                rider.GivenName = (string)resultSet["givenName"];
                rider.Gender = (string)resultSet["gender"];
                rider.Phone = (string)resultSet["phone"];
                rider.Email = (string)resultSet["email"];
                rider.ClubId = (int)resultSet["clubId"];
                rider.Username = (string)resultSet["username"];
                rider.Password = (string)resultSet["password"];
                rider.Role = (string)resultSet["role"];


                riderList.Add(rider);
            }

            // (9) Close the result set
            resultSet.Close();
            return riderList;
        }

        catch (Exception)
        {
            return null;
        }

        finally
        {
            // (10) Close the database connection
            myDatabase.Close();
        }

    }
                                                                                       

/// <summary>
/// Deletes a single Department row by DepartmentId from the database.
/// </summary>
/// <param name="ClubId"></param>
/// <returns>0 = OK, 1 = delete not allowed, -1 = error</returns>
public int DeleteBrevet(int brevetId)
{
    try
    {
        myDatabase.Open(myConnectionString);

        if (riderExistsForBrevet(brevetId) == true)                    //CHANGE
        {
            return 1;
        }

        String sqlText = String.Format(
          @"DELETE FROM Brevet
                 WHERE brevetId = {0}", brevetId);                               //CORRECT?

        myDatabase.ExecuteUpdate(sqlText);

        return 0;   // OK
    }
    catch (Exception)
    {
        return -1; // An error occurred
    }
    finally
    {
        myDatabase.Close();
    }
}

/// <summary>
/// Retrieves all Department rows in alphabetical order by Department name from the database.
/// </summary>
/// <returns>A List of Departments</returns>
public List<Brevet> GetAllBrevetsOrderedByName()
{
    List<Brevet> brevetList = new List<Brevet>();
    IDataReader resultSet;

    try
    {
        myDatabase.Open(myConnectionString);

        String sqlText =
          @"SELECT brevetId, distance, brevetDate, location, climbing 
                  FROM Brevet ORDER BY distance ASC, brevetDate ASC, location ASC";                                                  

        resultSet = myDatabase.ExecuteQuery(sqlText);
        while (resultSet.Read() == true)
        {
            Brevet brevet = new Brevet();

            brevet.BrevetId = (int)resultSet["brevetId"];
            brevet.Distance = (int)resultSet["distance"];
            brevet.BrevetDate = (DateTime)resultSet["brevetDate"];
            brevet.Location = (String)resultSet["location"];                           
            brevet.Climbing = (int)resultSet["climbing"];
            brevetList.Add(brevet);
        }

        resultSet.Close();

        return brevetList;
    }
    catch (Exception)
    {
        return null; // An error occured
    }
    finally
    {
        myDatabase.Close();
    }                                                                               
}

    //Inserts a new rider into Brevet_Rider 
    public int InsertRider(BrevetRider rider)
    {

        try
        {
            // (5) Open a connection to the database
            myDatabase.Open(myConnectionString);

            int RiderId = rider.RiderId;
            int BrevetId = rider.BrevetId;


            // (6) Construct a SELECT statement
            sqlText =
              "INSERT INTO Brevet_Rider (riderId, brevetId, isCompleted, finishingTime ) " +
              "VALUES('" + RiderId + "', '" + BrevetId + "', '" + 'N' + "', " + '0' + ")";  

            // (7) Execute the SELECT statement
            myDatabase.ExecuteUpdate(sqlText);

            return 0;  //OK
        }

        catch
        {
            return -1;
        }


        finally
        {
            // (10) Close the database connection
            myDatabase.Close();
        }

    }


    /// <summary>
    /// Retrieves a single Department row by DepartmentId from the database.
    /// </summary>
    /// <param name="BrevetId"></param>
    /// <returns>A single Department object</returns>
    public Brevet GetBrevetByBrevetId(int brevetId)
{
    IDataReader resultSet;

    try
    {
        myDatabase.Open(myConnectionString);

        String sqlText = String.Format(
          @"SELECT brevetId, distance, brevetDate, location, climbing 
            FROM Brevet
            WHERE brevetId = {0}", brevetId);                                         

        resultSet = myDatabase.ExecuteQuery(sqlText);

        if (resultSet.Read() == true)
        {
            Brevet brevet = new Brevet();

                brevet.BrevetId = (int)resultSet["brevetId"];
                brevet.Distance = (int)resultSet["distance"];
                brevet.BrevetDate = (DateTime)resultSet["brevetDate"];
                brevet.Location = (String)resultSet["location"];
                brevet.Climbing = (int)resultSet["climbing"];                           
                resultSet.Close();

                return brevet;
        }
        else
        {
            return null; // Not found
        }
    }
    catch (Exception)
    {
        return null;  // An error occurred
    }
    finally
    {
        myDatabase.Close();
    }
}

/// <summary>
/// Inserts a single Department row into the database.
/// </summary>
/// <param name="Brevet"></param>
/// <returns>0 = OK, 1 = insert not allowed, -1 = error</returns>
public int InsertBrevet(Brevet brevet)
{
    try
    {
        myDatabase.Open(myConnectionString);

        if (brevetExists(brevet.BrevetId) == true)
        {
            return 1;
        }

        String sqlText = String.Format(
          @"INSERT INTO Brevet (distance, brevetDate, location, climbing, brevetId)
            VALUES ({0}, '{1}', '{2}', '{3}', '{4}')",                                         
            brevet.Distance,
            brevet.BrevetDate,
            brevet.Location,
            brevet.Climbing,
            brevet.BrevetId);                                                               

        myDatabase.ExecuteUpdate(sqlText);

        return 0;  // OK
    }
    catch (Exception)
    {
        return -1; // An error occurred
    }
    finally
    {
        myDatabase.Close();
    }
}

/// <summary>
/// Updates an existing Department row in the database.
/// </summary>
/// <param name="Brevet"></param>
/// <returns>0 = OK, -1 = error</returns>
public int UpdateBrevet(Brevet brevet)
{
    try
    {
        myDatabase.Open(myConnectionString);

        String sqlText = String.Format(
          @"UPDATE Brevet
                SET distance  = '{0}', 
                    brevetDate = '{1}',
                    location    =  '{2}',
                    climbing    =  '{3}'
                    WHERE brevetId  =  {4}",
            brevet.Distance,
            brevet.BrevetDate,
            brevet.Location,
            brevet.Climbing,
            brevet.BrevetId);                                                                  

        myDatabase.ExecuteUpdate(sqlText);

        return 0;  // OK
    }
    catch (Exception)
    {
        return -1; // An error occurred
    }
    finally
    {
        myDatabase.Close();
    }
}

    


    /// <summary>
    /// Checks if a Department row with the given Department id exists in the database.
    /// </summary>
    /// <param name="brevetId"></param>
    /// <returns>true = row exists, otherwise false</returns>
    private bool brevetExists(int brevetId)
{
 
    bool rowFound;

    String sqlText = String.Format(
      @"SELECT brevetId 
              FROM Brevet 
             WHERE brevetId = {0}", brevetId);                                                  //Correct?

    resultSet = myDatabase.ExecuteQuery(sqlText);
    rowFound = resultSet.Read();
    resultSet.Close();

    return rowFound;   // true = row exists, otherwise false
}

/// <summary>
/// Checks if the Department row is being referenced by another row. 
/// </summary>
/// <param name="brevetId"></param>
/// <returns>true = a child row exists, otherwise false</returns>
private bool riderExistsForBrevet(int brevetId)
{
  
    bool rowFound;

    String sqlText = String.Format(
      @"SELECT riderId
             FROM Brevet_Rider
             WHERE brevetId = {0}", brevetId);                                                          //EL CORRECTO?

    resultSet = myDatabase.ExecuteQuery(sqlText);
    rowFound = resultSet.Read();
    resultSet.Close();

    return rowFound;   // true = row exists, otherwise false
}

}
               
