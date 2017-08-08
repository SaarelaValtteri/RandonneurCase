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
public partial class RiderListPage : System.Web.UI.Page
{

    BrevetDAO brevetDao = new BrevetDAO();
    RiderDAO riderDao = new RiderDAO();



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
        brevetList = brevetDao.GetAllBrevets();
        DateTime myDate;
        string myString;

        for (int i = 0; i < brevetList.Count; i++)
        {
            myDate = brevetList[i].BrevetDate;
            myString = myDate.ToString("yyyy-MM-dd");
            listBoxClubs.Items.Add(new ListItem(brevetList[i].Distance.ToString() + ": " + myString + ", " +
            brevetList[i].Location, brevetList[i].BrevetId.ToString()));
        }


        if (brevetList == null)
        {
            listBoxClubs.Items.Add("No Brevets Found!");
        }


    }

    protected void listBoxClubs_SelectedIndexChanged(object sender, EventArgs e)
    {
  
        listBoxResult.Items.Clear();

        int brevetId = Convert.ToInt32(listBoxClubs.SelectedValue);

        List<Rider> riderList = new List<Rider>();
        riderList = riderDao.GetBrevetParticipant(brevetId);

        if (riderList == null)
        {
            listBoxClubs.Items.Add("Riders Not Found!");
        }

        else
        {

            foreach(Rider rider in riderList)
            {
                ListItem listitem = new ListItem(rider.FamilyName + ", " + rider.GivenName + ", " + rider.Club.Clubname);
                listBoxResult.Items.Add(listitem);
            }  
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
