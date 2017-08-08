using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Rider
/// </summary>
public class Rider
{

  int riderId;
  string familyName;
  string givenName;
  string gender;
  string phone;
  string email;
  int clubId;
  string username;
  string password;
  string role;
    Club club;

    public Rider()
    {
        riderId = 0;
        familyName = "";
        givenName = "";
        gender = "";
        phone = "";
        email = "";
        clubId = 0;
        username = "";
        password = "";
        role = "";
    }



    public int RiderId
    {
        get { return riderId; }
        set { riderId = value; }
    }

    public string FamilyName
    {
        get { return familyName; }
        set { familyName = value; }
    }

    public string GivenName
    {
        get { return givenName; }
        set { givenName = value; }
    }

    public string Gender
    {
        get { return gender; }
        set { gender = value; }
    }

    public string Phone
    {
        get { return phone; }
        set { phone = value; }
    }

    public string Email
    {
        get { return email; }
        set { email = value; }
    }

    public int ClubId
    {
        get { return clubId; }
        set { clubId = value; }
    }
    public string Username
    {
        get { return username; }
        set { username = value; }
    }
    public string Password
    {
        get { return password; }
        set { password = value; }
    }

    public string Role
    {
        get { return role; }
        set { role = value; }
    }

    public Club Club
    {
        get
        {
            return club;
        }

        set
        {
            club = value;
        }
    }
}