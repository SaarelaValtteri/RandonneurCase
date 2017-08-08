using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Karis.DatabaseLibrary;
using System.Data;

/// <summary>
/// Summary description for BrevetRiderDAO
/// </summary>
public class BrevetRiderDAO
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

    public BrevetRiderDAO()
    {
        myDatabase = new Database();
    }
    

    public List<BrevetRider> GetBrevetResults(int distance, DateTime year, string location)
    {

        try
        {
            // (5) Open a connection to the database
            myDatabase.Open(myConnectionString);
            List<BrevetRider> brevetRiderList = new List<BrevetRider>();

            // (6) Construct a SELECT statement
            sqlText =
              "SELECT Brevet_Rider.riderId, Brevet_Rider.brevetId, Rider.givenName, Rider.familyName, isCompleted, finishingTime " +
              "FROM Brevet_Rider INNER JOIN Brevet ON(Brevet_Rider.brevetId = Brevet.brevetId) JOIN Rider ON(Brevet_Rider.riderId = Rider.riderId) " +
              "WHERE Brevet.distance = '" + distance + "' AND Brevet.brevetDate = '" + year.ToString("yyyy-MM-dd") + "'" +
              " AND Brevet.location = '" + location + "' AND isCompleted = 'Y'";

            // (7) Execute the SELECT statement
            resultSet = myDatabase.ExecuteQuery(sqlText);

            // (8) Process the multiple rows in the result set one by one 
            while (resultSet.Read() == true)  // 8.1 Move to the next available row 
            {                                 //     true = row available
                                              //     false = no more rows
                BrevetRider brevetRider = new BrevetRider();
                Rider rider = new Rider();

                // 8.2 Retrieve column values from the current row in the result set

                brevetRider.RiderId = (int)resultSet["riderId"];
                brevetRider.BrevetId = (int)resultSet["brevetId"];
                brevetRider.IsCompleted = (string)resultSet["isCompleted"];
                brevetRider.FinishingTime = (string)resultSet["finishingTime"];

                rider.FamilyName = (string)resultSet["familyName"];
                rider.GivenName = (string)resultSet["givenName"];

                brevetRider.Rider = rider;

                brevetRiderList.Add(brevetRider);

            }
            // (9) Close the result set
            resultSet.Close();
            return brevetRiderList;

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

    public List<BrevetRider> GetBrevetResult(int brevetId)
    {

        try
        {
            // (5) Open a connection to the database
            myDatabase.Open(myConnectionString);
            List<BrevetRider> brevetRiderList = new List<BrevetRider>();

            // (6) Construct a SELECT statement
            sqlText =
              "SELECT Brevet_Rider.riderId, Brevet_Rider.brevetId, Rider.familyName, Rider.givenName, isCompleted, finishingTime " +
              "FROM Brevet_Rider INNER JOIN Brevet ON(Brevet_Rider.brevetId = Brevet.brevetId) JOIN Rider ON(Brevet_Rider.riderId = Rider.riderId) " +
              "WHERE Brevet.brevetId = '" + brevetId + "' AND isCompleted = 'Y'";
         

            // (7) Execute the SELECT statement
            resultSet = myDatabase.ExecuteQuery(sqlText);

            // (8) Process the multiple rows in the result set one by one 
            while (resultSet.Read() == true)  // 8.1 Move to the next available row 
            {                                 //     true = row available
                                              //     false = no more rows
                BrevetRider brevetRider = new BrevetRider();
                Rider rider = new Rider();
                // 8.2 Retrieve column values from the current row in the result set

                brevetRider.RiderId = (int)resultSet["riderId"];
                brevetRider.BrevetId = (int)resultSet["brevetId"];
                brevetRider.IsCompleted = (string)resultSet["isCompleted"];
                brevetRider.FinishingTime = (string)resultSet["finishingTime"];

                rider.FamilyName = (string)resultSet["familyName"];
                rider.GivenName = (string)resultSet["givenName"];

                brevetRider.Rider = rider;

                brevetRiderList.Add(brevetRider);

            }
            // (9) Close the result set
            resultSet.Close();
            return brevetRiderList;

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

}