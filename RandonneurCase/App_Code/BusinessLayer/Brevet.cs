using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Brevet
/// </summary>
public class Brevet
{

    int brevetId;
    int distance;
    DateTime brevetDate;
    string location;
    int climbing;

    public Brevet()
    {
        brevetId = 0;
        distance = 0;    
        location = "";
        climbing = 0;
    }

    public int BrevetId
    {
        get { return brevetId; }
        set { brevetId = value; }
    }

    public int Distance
    {
        get { return distance; }
        set { distance = value; }
    }

    public DateTime BrevetDate
    {
        get { return brevetDate; }
        set { brevetDate = value; }
    }

    public string Location
    {
        get { return location; }
        set { location = value; }
    }

    public int Climbing
    {
        get { return climbing; }
        set { climbing = value; }
    }

}