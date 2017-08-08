﻿/* **************************************************************************
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
/// DepartmentManagementPage - Code behind part for the ASPX Page 
/// <remarks>Kari Silpiö 2014 
///          Modified by: Valtteri Saarela 15.5.2016</remarks>
/// </summary>
public partial class BrevetManagementPage : System.Web.UI.Page
{
    private BrevetDAO brevetDAO = new BrevetDAO();

    protected void Page_Load(object sender, EventArgs e)
    {
        checkLogin(true); // true = login is required for accessing this page

        if (this.IsPostBack == false)
        {
            viewStateNew();
            createBrevetList(); // Populate Department List for the first time
        }

        addButtonScripts();
    }

    private void addButtonScripts()
    {
        btDelete.Attributes.Add("onclick",
          "return confirm('Are you sure you want to delete the data?');");
    }

    private void createBrevetList()
    {
        List<Brevet> brevetList = brevetDAO.GetAllBrevetsOrderedByName();

        listBoxClubs.Items.Clear();
        if (brevetList == null)
        {
            showErrorMessage("DATABASE TEMPORARILY OUT OF USE (see Database.log)");
        }
        else
        {
            foreach (Brevet brevet in brevetList)
            {
              //  String text = club.Clubname;

                ListItem listItem = new ListItem(brevet.BrevetId.ToString(), brevet.BrevetId.ToString());
                listBoxClubs.Items.Add(listItem);
            }
        }
    }

    protected void btAdd_Click(object sender, EventArgs e)
    {
        Brevet brevet = screenToModel();
        int insertOk = brevetDAO.InsertBrevet(brevet);

        if (insertOk == 0) // Insert succeeded
        {
            createBrevetList();
            listBoxClubs.SelectedValue = brevet.BrevetId.ToString();
            viewStateDetailsDisplayed();
            showNoMessage();
        }
        else if (insertOk == 1)
        {
            showErrorMessage("Brevet id " + brevet.BrevetId +
              " is already in use. No record inserted into the database.");
            tbBrevetId.Focus();
        }
        else
        {
            showErrorMessage("No record inserted into the database. " +
              "THE DATABASE IS TEMPORARILY OUT OF USE.");
        }
    }

    protected void btDelete_Click(object sender, EventArgs e)
    {
        int brevetId = Convert.ToInt32(listBoxClubs.SelectedValue);
        int deleteOk = brevetDAO.DeleteBrevet(brevetId);

        if (deleteOk == 0) // Delete succeeded
        {
            createBrevetList();
            viewStateNew();
            showNoMessage();
        }
        else if (deleteOk == 1)
        {
            showErrorMessage("No record deleted. " +
              "Please delete the brevet riders first.");
        }
        else
        {
            showErrorMessage("No record deleted. " +
             "THE DATABASE IS TEMPORARILY OUT OF USE.");
        }
    }

    protected void btNew_Click(object sender, EventArgs e)
    {
        viewStateNew();
    }

    protected void btUpdate_Click(object sender, EventArgs e)
    {
        Brevet brevet = screenToModel();
        int updateOk = brevetDAO.UpdateBrevet(brevet);

        if (updateOk == 0) // Update succeeded
        {
            String selectedValue = listBoxClubs.SelectedValue;

            createBrevetList();
            listBoxClubs.SelectedValue = selectedValue;
            showNoMessage();
        }
        else
        {
            showErrorMessage("No record updated. " +
              "THE DATABASE IS TEMPORARILY OUT OF USE.");
        }
    }

    protected void listBoxClubs_SelectedIndexChanged(object sender, EventArgs e)
    {
        int brevetId = Convert.ToInt32(listBoxClubs.SelectedValue);
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
        tbBrevetId.Text = "" + brevet.BrevetId;
        tbDistance.Text = "" + brevet.Distance;
        tbDate.Text = "" + brevet.BrevetDate;
        tbLocation.Text = "" + brevet.Location;
        tbClimbing.Text = "" + brevet.Climbing.ToString();
    }

    private void resetForm()
    {
        tbBrevetId.Text = "";
        tbDistance.Text = "";
        tbDate.Text = "";
        tbLocation.Text = "";
        tbClimbing.Text = "";
    }

    private Brevet screenToModel()
    {
        Brevet brevet = new Brevet();

        brevet.BrevetId = Convert.ToInt32(tbBrevetId.Text.Trim());
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

    private void viewStateDetailsDisplayed()
    {
        tbBrevetId.Enabled = false;

        btAdd.Enabled = false;
        btDelete.Enabled = true;
        btNew.Enabled = true;
        btUpdate.Enabled = true;
    }

    private void viewStateNew()
    {
        tbBrevetId.Enabled = true;
        tbBrevetId.Focus();

        btAdd.Enabled = true;
        btDelete.Enabled = false;
        btNew.Enabled = true;
        btUpdate.Enabled = false;

        resetForm();
        listBoxClubs.SelectedIndex = -1;
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
