/* **************************************************************************
 * DepartmentManagementPage.cs  Original version: Kari Silpiö 18.3.2014 v1.0
 *                              Modified by     : Valtteri Saarela 15.5.2016 
 * -------------------------------------------------------------------------
 *  Application: DWA Model Case
 *  Class:       Code-behind class for DepartmentManagementPage.aspx
 * -------------------------------------------------------------------------
 * NOTE: This file can be included in your solution.
 *   If you modify this file, write your name & date after "Modified by:"
 *   DO NOT REMOVE THIS COMMENT.
 ************************************************************************** */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Karis.DatabaseLibrary;
using System.Data;

/// <summary>
/// DepartmentManagementPage - Code behind part for the ASPX Page 
/// <remarks>Kari Silpiö 2014 
///          Modified by: Valtteri Saarela 15.5.2016</remarks>
/// </summary>
public partial class BrevetResults : System.Web.UI.Page
{

                                                 
    private ClubDAO clubDAO = new ClubDAO();


    protected void Page_Load(object sender, EventArgs e)
    {
        checkLogin(true); // true = login is required for accessing this page

        if (this.IsPostBack == false)
        {
            viewStateNew();
        }

    }

    private void viewStateNew()
    {
        List<Brevet> brevetList = new List<Brevet>();
        BrevetDAO brevetDao = new BrevetDAO();
        brevetList = brevetDao.GetAllBrevets();
        DateTime myDate;
        string myString;

        for (int i = 0; i < brevetList.Count; i++)
        {
            myDate = brevetList[i].BrevetDate;
            myString = myDate.ToString("yyyy-MM-dd");
            
            listBoxClubs.Items.Add(new ListItem(brevetList[i].Distance.ToString() + ": " + myString + ", " +
            brevetList[i].Location, brevetList[i].BrevetId.ToString()));

            ddlDistance.Items.Add(brevetList[i].Distance.ToString());
            ddlYear.Items.Add(myString);
            ddlLocation.Items.Add(brevetList[i].Location);
        }


        if (brevetList == null)
        {
            listBoxClubs.Items.Add("No Brevets Found!");
        }


    }

    protected void btSearch_Click(object sender, EventArgs e)
    {
        listBoxResult.Items.Clear();
        BrevetRiderDAO brevetRiderDAO = new BrevetRiderDAO();
        List<BrevetRider> brevetRiderList = new List<BrevetRider>();
        int distance = Convert.ToInt32(ddlDistance.SelectedValue);
        DateTime dateParsed = DateParse(ddlYear.SelectedValue);    
        string location = ddlLocation.SelectedValue;

        brevetRiderList = brevetRiderDAO.GetBrevetResults(distance, dateParsed, location);

        for (int i = 0; i < brevetRiderList.Count; i++)
        {
            listBoxResult.Items.Add("Rider: " + brevetRiderList[i].Rider.GivenName + " " + brevetRiderList[i].Rider.FamilyName + ", Brevet ID: " + brevetRiderList[i].BrevetId + ", Completed: " +
            brevetRiderList[i].IsCompleted + ", Finishing time: " + brevetRiderList[i].FinishingTime);
        }

    }

    public static DateTime DateParse(string date)
    {
        date = date.Trim();
        if (!string.IsNullOrEmpty(date))
            return DateTime.Parse(date, new System.Globalization.CultureInfo("en-GB"));    //DISCLAIMER: THIS PIECE OF CODE (DateParse()) IS A FIX I FOUND ONLINE. I MERELY ALTERED IT TO WORK IN THIS PROJECT!
        return new DateTime();
    }   

    protected void listBoxClubs_SelectedIndexChanged(object sender, EventArgs e)
    {

        listBoxResult.Items.Clear();
        BrevetRiderDAO brevetRiderDAO = new BrevetRiderDAO();
        List<BrevetRider> brevetRiderList = new List<BrevetRider>();
        int riderId = Convert.ToInt32(listBoxClubs.SelectedValue);

        brevetRiderList = brevetRiderDAO.GetBrevetResult(riderId);

        for (int i = 0; i < brevetRiderList.Count; i++)
        {
            listBoxResult.Items.Add("Rider: " + brevetRiderList[i].Rider.GivenName + " " + brevetRiderList[i].Rider.FamilyName + ", Brevet ID: " + brevetRiderList[i].BrevetId + ", Completed: " +
            brevetRiderList[i].IsCompleted + ", Finishing time: " + brevetRiderList[i].FinishingTime);
        }

    }



    /* **********************************************************************
    * LOGIN MANAGEMENT CODE 
    * - This is the special code to be used on your ASPX pages.
    * - DO NOT change anything else but the HyperLink controls here!
    *   HyperLink controls are managed under comments (1), (2), and (3)
    *********************************************************************** */
    private void checkLogin(bool loginRequired)
    {
        Response.Cache.SetNoStore();    // Should disable browser's Back Button

        // (1) Hide all hyperlinks that are available for autenthicated users only
        hyperLinkClubManagementPage.Visible = false;
        hyperLinkBrevetManagementPage.Visible = false;
        hyperLinkBrevetRegistrationPage.Visible = false;

        if (Session["username"] == null)
        {
            lbLoginInfo.Text = "You are not logged in";
            btLogout.Visible = false;

        }

        if (Session["username"] != null)
        {
            lbLoginInfo.Text = "You are logged in as " + Session["username"];
            btLogout.Visible = true;

            // (2) Show all hyperlinks that are available for autenthicated users only

            hyperLinkBrevetRegistrationPage.Visible = true;
        }

        if (Session["administrator"] != null)
        {
            // (3) In addition, show all hyperlinks that are available for administrators only
            hyperLinkClubManagementPage.Visible = true;
            hyperLinkBrevetManagementPage.Visible = true;
        }
    }

    protected void btLogout_Click(object sender, EventArgs e)
    {
        Session["username"] = null;
        Session["administrator"] = null;
        Page.Response.Redirect("HomePage.aspx");
    }
    /* LOGIN MANAGEMENT code ends here  */

   
}
// End
