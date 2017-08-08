using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Username
/// </summary>
public class Username
{
    private string username;

    public Username()
    {
        username = "";
    }

    public string UserName
    {
        get
        {
            return username;
        }

        set
        {
            username = value;
        }
    }

}