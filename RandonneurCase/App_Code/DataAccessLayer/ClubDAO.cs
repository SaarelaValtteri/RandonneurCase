/* *************************************************************************
 * DepartmentDAO.cs   Original version: Kari Silpiö 20.3.2014 v1.0
 *                    Modified by     : Valtteri Saarela 24.4.2016
 * ------------------------------------------------------------------------
 *  Application: Model Case
 *  Layer:       Data Access Layer
 *  Class:       SQL Server specific DAO class for Department entity objects
 * -------------------------------------------------------------------------
 * NOTICE: This is an over-simplified example for an introductory course. 
 * - Error processing is not robust (some error are not handled)
 * - No multi-user considerations, no transaction programming 
 * - No protection for attacks of type 'SQL injection'
 * -------------------------------------------------------------------------
 * NOTE: This file can be included in your solution.
 *   If you modify this file, write your name & date after "Modified by:"
 *   DO NOT REMOVE THIS COMMENT.
 ************************************************************************* */
using System;
using System.Collections.Generic;

using System.Data;
using Karis.DatabaseLibrary;

/// <summary>
/// DepartmentDAO - Data Access Layer interface class. Accesses the data storage.
/// <remarks>Original version: Kari Silpiö 2014
///          Modified by: Valtteri Saarela 24.4.2016</remarks>
/// </summary>
public class ClubDAO
{
    private Database myDatabase;
    private string myConnectionString =
          @"Data Source=(LocalDB)\MSSQLLocalDB;                                      
          AttachDbFilename=|DataDirectory|\RandonneurDatabase.mdf;
          Integrated Security=True;Connect Timeout=40";

    public ClubDAO()
    {      
        myDatabase = new Database();
    }

    IDataReader resultSet;
    String sqlText;

    /// <summary>
    /// Deletes a single Department row by DepartmentId from the database.
    /// </summary>
    /// <param name="ClubId"></param>
    /// <returns>0 = OK, 1 = delete not allowed, -1 = error</returns>
    public int DeleteClub(int clubId)
    {
        try
        {
            myDatabase.Open(myConnectionString);

            if (employeeExistsForClub(clubId) == true)
            {
                return 1;
            }

            String sqlText = String.Format(
              @"DELETE FROM Club
                 WHERE clubId = {0}", clubId);                               

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
    public List<Club> GetAllClubsOrderedByName()
    {
        List<Club> clubList = new List<Club>();
 
        try
        {
            myDatabase.Open(myConnectionString);

            String sqlText = 
              @"SELECT clubId, clubName, city, email    
                  FROM Club 
              ORDER BY clubName ASC";                                                  

            resultSet = myDatabase.ExecuteQuery(sqlText); 
            while (resultSet.Read() == true)
            {
                Club club = new Club();

                club.Clubid = (int)resultSet["clubId"];
                club.Clubname = (String)resultSet["clubName"];
                club.City = (String)resultSet["city"];
                club.Email = (String)resultSet["email"];                           

                clubList.Add(club);
            }

            resultSet.Close();

            return clubList;
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

    /// <summary>
    /// Retrieves a single Department row by DepartmentId from the database.
    /// </summary>
    /// <param name="ClubId"></param>
    /// <returns>A single Department object</returns>
    public Club GetClubByClubId(int clubno)
    {

        try
        {
            myDatabase.Open(myConnectionString);

            String sqlText = String.Format(
              @"SELECT clubId, clubName, city, email  
                 FROM Club 
                 WHERE clubId = {0}", clubno);                                          

            resultSet = myDatabase.ExecuteQuery(sqlText);

            if (resultSet.Read() == true)
            {
                Club club = new Club();

                club.Clubid = (int)resultSet["clubId"];
                club.Clubname = (String)resultSet["clubName"];
                club.City = (String)resultSet["city"];
                club.Email = (String)resultSet["email"];                                          
                resultSet.Close();

                return club;
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
    /// <param name="Club"></param>
    /// <returns>0 = OK, 1 = insert not allowed, -1 = error</returns>
    public int InsertClub(Club club)
    {
        try
        {
            myDatabase.Open(myConnectionString);

            if (clubExists(club.Clubid) == true)
            {
                return 1;
            }

            String sqlText = String.Format(
              @"INSERT INTO Club (clubId, clubName, city, email)
                VALUES ({0}, '{1}', '{2}', '{3}')",                                       
                club.Clubid,
                club.Clubname,
                club.City,
                club.Email);                                                                

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
    /// <param name="Club"></param>
    /// <returns>0 = OK, -1 = error</returns>
    public int UpdateClub(Club club)
    {
        try
        {
            myDatabase.Open(myConnectionString);

            String sqlText = String.Format(
              @"UPDATE Club
                SET clubName      = '{0}', 
                    city          = '{1}',
                    email         = '{2}'
                    WHERE clubId  =  {3}",
               club.Clubname,
                club.City,
                club.Email,
                club.Clubid);                                                                  

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
    /// <param name="clubId"></param>
    /// <returns>true = row exists, otherwise false</returns>
    private bool clubExists(int clubId)
    {

        bool rowFound;

        String sqlText = String.Format(
          @"SELECT clubId 
              FROM Club 
             WHERE clubId = {0}", clubId);                                                  //Correct?

        resultSet = myDatabase.ExecuteQuery(sqlText);
        rowFound = resultSet.Read();
        resultSet.Close();

        return rowFound;   // true = row exists, otherwise false
    }

    /// <summary>
    /// Checks if the Department row is being referenced by another row. 
    /// </summary>
    /// <param name="clubId"></param>
    /// <returns>true = a child row exists, otherwise false</returns>
    private bool employeeExistsForClub(int clubId)
    {
        bool rowFound;

        String sqlText = String.Format(
          @"SELECT clubId
             FROM Rider
             WHERE clubId = {0}", clubId);                                                          

        resultSet = myDatabase.ExecuteQuery(sqlText);
        rowFound = resultSet.Read();
        resultSet.Close();

        return rowFound;   // true = row exists, otherwise false
    }

}
// End
