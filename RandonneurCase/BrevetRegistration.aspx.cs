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

/// <summary>
/// BrevetRegistrationPage - Code behind part for the ASPX Page 
/// <remarks>Kari Silpiö 2014 
///          Modified by: Valtteri Saarela 15.5.2016</remarks>
/// </summary>
public partial class BrevetRegistration : System.Web.UI.Page
{
    private BrevetDAO brevetDAO = new BrevetDAO();
    private RiderDAO riderDAO = new RiderDAO();


    protected void Page_Load(object sender, EventArgs e)
    {
        checkLogin(true); // true = login is required for accessing this page

        if (this.IsPostBack == false)
        {
            viewStateNew();
            createBrevetList(); // Populate Department List for the first time
        }
      
    }

    private void createBrevetList()
    {
        List<Brevet> brevetList = brevetDAO.GetAllBrevetsOrderedByName();

        listBoxBrevets.Items.Clear();
        if (brevetList == null)
        {
            showErrorMessage("DATABASE TEMPORARILY OUT OF USE (see Database.log)");
        }
        else
        {
            foreach (Brevet brevet in brevetList)
            {
                ListItem listItem = new ListItem(brevet.BrevetId.ToString(), brevet.BrevetId.ToString());
                listBoxBrevets.Items.Add(listItem);
            }
        }
    }

    protected void btRegister_Click(object sender, EventArgs e)
    {
        Brevet brevet = screenToModel();
        BrevetRider rider = new BrevetRider();

        rider.BrevetId = Convert.ToInt32(listBoxBrevets.SelectedValue);
        rider.RiderId =  Convert.ToInt32(riderDAO.GetRiderIdByUsername((string)Session["username"]));
          
        int insertOk = brevetDAO.InsertRider(rider);                  

        if (insertOk == 0) // Insert succeeded
        {
            viewStateDetailsDisplayed();
            showMessage();
        }
        else if (insertOk == 1)
        {
            showErrorMessage("Brevet id " + brevet.BrevetId +
              " is already in use. No record inserted into the database.");
        }
        else
        {
            showErrorMessage("No record inserted into the database. " +
              "THE DATABASE IS TEMPORARILY OUT OF USE.");
        }                                   
    }


    protected void listBoxBrevets_SelectedIndexChanged(object sender, EventArgs e)
    {
        int brevetId = Convert.ToInt32(listBoxBrevets.SelectedValue);
        Brevet brevet = brevetDAO.GetBrevetByBrevetId(brevetId);                                          

        if (brevet != null)
        {
            modelToScreen(brevet);
            viewStateDetailsDisplayed();
            showNoMessage();
        }
    }

    private void modelToScreen(Brevet brevet)
    {
        tbDistance.Text = "" + brevet.Distance;
        tbDate.Text = "" + brevet.BrevetDate;
        tbLocation.Text = "" + brevet.Location;
        tbClimbing.Text = "" + brevet.Climbing.ToString();
    }

    private void resetForm()
    {
        tbDistance.Text = "";
        tbDate.Text = "";
        tbLocation.Text = "";
        tbClimbing.Text = "";
    }

    private Brevet screenToModel()
    {
        Brevet brevet = new Brevet();

        brevet.Distance = Convert.ToInt32(tbDistance.Text.Trim());
        brevet.BrevetDate = Convert.ToDateTime(tbDate.Text.Trim());
        brevet.Location = tbLocation.Text.Trim();
        brevet.Climbing = Convert.ToInt32(tbClimbing.Text.Trim());
        return brevet;
    }

    private void showErrorMessage(String message)
    {
        lbMessage.Text = message;
        lbMessage.ForeColor = System.Drawing.Color.Red;
    }

    private void showNoMessage()
    {
        lbMessage.Text = "";
        lbMessage.ForeColor = System.Drawing.Color.Black;
    }

    private void showMessage()
    {
        lbMessage.Text = "Registration successful";
    }

    private void viewStateDetailsDisplayed()
    {
      
        btRegister.Enabled = true;

    }

    private void viewStateNew()
    {
        btRegister.Enabled = true;
        tbDistance.Focus();


        resetForm();
        listBoxBrevets.SelectedIndex = -1;
        showNoMessage();
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
