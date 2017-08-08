using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for BrevetRider
/// </summary>
public class BrevetRider
{

    int riderId;
    int brevetId;
    string isCompleted;
    string finishingTime;
    Rider rider;


    public BrevetRider()
    {
        riderId = 0;
        brevetId = 0;
        isCompleted = "";
        finishingTime = "";
    }

    public int RiderId
    {
        get { return riderId; }
        set { riderId = value; }
    }

    public int BrevetId
    {
        get { return brevetId; }
        set { brevetId = value; }
    }

    public string IsCompleted
    {
        get { return isCompleted; }
        set { isCompleted = value; }
    }

    public string FinishingTime
    {
        get { return finishingTime; }
        set { finishingTime = value; }
    }

    public Rider Rider
    {
        get
        {
            return rider;
        }

        set
        {
            rider = value;
        }
    }
}