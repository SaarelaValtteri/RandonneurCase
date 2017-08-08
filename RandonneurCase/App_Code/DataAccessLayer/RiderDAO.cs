using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Karis.DatabaseLibrary;
using System.Data;

/// <summary>
/// Summary description for BrevetDAO
/// </summary>
public class RiderDAO
{

    // (2) A reference variable for a Database object
    private Database myDatabase;

    // (3) Connection string for connecting to the database   I USED MSSQLLocalDB instead of v11.0 because it is used by Visual Studio 2015. 
    private string myConnectionString =
        @"Data Source=(LocalDB)\MSSQLLocalDB;                                      
          AttachDbFilename=|DataDirectory|\RandonneurDatabase.mdf;
          Integrated Security=True;Connect Timeout=40";

    IDataReader resultSet;
    String sqlText;

    public RiderDAO()
    {
        myDatabase = new Database();
    }

    public List<Brevet> GetAllBrevets()
    {
        myDatabase = new Database();

        try
        {

            // (5) Open a connection to the database
            myDatabase.Open(myConnectionString);
            List<Brevet> brevetList = new List<Brevet>();
            // (6) Construct a SELECT statement
            sqlText =
          "SELECT DISTINCT brevetId " +
          "FROM Brevet";

            // (7) Execute the SELECT statement
            resultSet = myDatabase.ExecuteQuery(sqlText);

            // (8) Process the multiple rows in the result set one by one 
            while (resultSet.Read() == true)  // 8.1 Move to the next available row 
            {                                 //     true = row available
                                              //     false = no more rows

                Brevet brevet = new Brevet();

                // 8.2 Retrieve column values from the current row in the result set
                brevet.BrevetId = (int)resultSet["brevetId"];
                brevetList.Add(brevet);
            }

            
            // (9) Close the result sethttp://localhost:60574/App_Data/
            resultSet.Close();
            return brevetList;
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



    public List<Rider> GetBrevetParticipant(int brevetId)
    {
        try
        {
            // (5) Open a connection to the database
            myDatabase.Open(myConnectionString);
            List<Rider> riderList = new List<Rider>();
            // (6) Construct a SELECT statement
            sqlText =

              "SELECT Rider.familyName, Rider.givenName, Club.clubName " +
               "FROM Rider JOIN Brevet_Rider ON(Rider.riderId = Brevet_Rider.riderId) JOIN Club ON (Club.clubId = Rider.clubId) " +
               "WHERE Brevet_Rider.brevetId = '" + brevetId + "' ORDER BY familyName ASC, givenName ASC, Club.clubName ASC";

            // (7) Execute the SELECT statement
            resultSet = myDatabase.ExecuteQuery(sqlText);

            // (8) Process the multiple rows in the result set one by one 
            while (resultSet.Read() == true)  // 8.1 Move to the next available row 
            {                                 //     true = row available
                                              //     false = no more rows
                Rider rider = new Rider();
                Club club = new Club();

                // 8.2 Retrieve column values from the current row in the result set
                rider.FamilyName = (string)resultSet["familyName"];
                rider.GivenName = (string)resultSet["givenName"];
             
                club.Clubname = (string)resultSet["clubName"];

                rider.Club = club;
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


    public int GetRiderIdByUsername(string Username)
    {
        try
        {
            myDatabase.Open(myConnectionString);

            String sqlText = String.Format(
              @"SELECT riderId
            FROM Rider
            WHERE username = '" + Username + "'");

            resultSet = myDatabase.ExecuteQuery(sqlText);

            if (resultSet.Read() == true)
            {

                int RiderId = (int)resultSet["riderId"];
                resultSet.Close();

                return RiderId;
            }

            else
            {
                return 0; // Not found
            }
        }
        

        catch (Exception)
        {
            return -1;  // An error occurred
        }

        finally
        {
            myDatabase.Close();
        }    
    }

}