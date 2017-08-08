/* *************************************************************************
 * LoginDAO.cs   Original version: Kari Silpiö 20.3.2014 v1.0
 *               Modified by     : Valtteri Saarela 10.5.2016
 * ------------------------------------------------------------------------
 *  Application: Model Case
 *  Layer:       Data Access Layer
 *  Class:       DAO class for database services for login functionality
 * -------------------------------------------------------------------------
 * NOTICE: This is an over-simplified example for an introductory course. 
 * - Error processing is not robust (some error are not handled)
 * - No multi-user considerations, no transaction programming 
 * - No protection for attacks of type 'SQL injection'
 * - No password security etc.
 * -------------------------------------------------------------------------
 * NOTE: This file can be included in your solution.
 *   If you modify this file, write your name & date after "Modified by:"
 *   DO NOT REMOVE THIS COMMENT.
 ************************************************************************* */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data;
using Karis.DatabaseLibrary;

/// <summary>
/// LoginDAO - Data Access Layer interface class. Accesses the data storage.
/// <remarks>Original version: Kari Silpiö 2014
///          Modified by: Valtteri Saarela 10.5.2016</remarks>
/// </summary>>
public class LoginDAO
{
    private Database myDatabase;
    private String myConnectionString;

    public LoginDAO()
    {
        myConnectionString =
        @"Data Source=(LocalDB)\MSSQLLocalDB;                                      
          AttachDbFilename=|DataDirectory|\RandonneurDatabase.mdf;
          Integrated Security=True;Connect Timeout=40";

    myDatabase = new Database();
    }

    public LoginRole GetLoginRole(string username, string password)
    {
        LoginRole loginRole = new LoginRole();
                             
        // ----------------------------------------------------------
        // TO DO: The login role must be retrieved from the database
        //
        // The below is here for testing purposes ONLY.
        // ----------------------------------------------------------
        loginRole.Role = null;

        IDataReader resultSet;

            myDatabase.Open(myConnectionString);

            String sqlText =
              @"SELECT role    
                  FROM Rider
              WHERE username = '" + username + "' AND password = '" + password + "'";                                                   //Correct?

            resultSet = myDatabase.ExecuteQuery(sqlText);
            while (resultSet.Read() == true)
            {

                loginRole.Role = (String)resultSet["role"];                           //Correct pl0x?
            }

            resultSet.Close();
            myDatabase.Close();

        return loginRole;
    }
}
// End
