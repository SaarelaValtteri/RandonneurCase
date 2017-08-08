<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RiderListPage.aspx.cs" Inherits="RiderListPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta content="text/html; charset=iso-8859-1" http-equiv="content-type" />
    <link href="CSS/ModelCaseStyleSheet.css" rel="stylesheet" type="text/css" />
    <title>Rider List - DWA Model Case</title>
    <style type="text/css">
        .listbox_main {}
    </style>
</head>

<body>
    <form id="form1" runat="server">
        <div id="div_CONTAINER">


            <div id="div_HEADER">
                <div id="div_header_TEXT">
                    <h1>Rider List</h1>
                </div>

                <div id="div_header_LOGIN_STATUS">
                    <asp:Label ID="lbLoginInfo" runat="server"></asp:Label>.<br />
                    <asp:LinkButton ID="btLogout" runat="server" CssClass="logout_link" OnClick="btLogout_Click" CausesValidation="False">LOGOUT</asp:LinkButton>
                </div>
            </div>



            <div id="div_LEFT">
                <div id="div_NAV">
                    <asp:HyperLink ID="hyperLinkHomePage" runat="server" CssClass="current_page_link" NavigateUrl="~/HomePage.aspx">Home</asp:HyperLink><br />
                    <asp:HyperLink ID="hyperLinkRiderListPage" runat="server" CssClass="other_page_link" NavigateUrl="~/RiderListPage.aspx">Rider Lists</asp:HyperLink><br />
                    <asp:HyperLink ID="hyperLinkBrevetResultsPage" runat="server" CssClass="other_page_link" NavigateUrl="~/BrevetResults.aspx">Brevet Results</asp:HyperLink><br />
                    <br />
                    <asp:HyperLink ID="hyperLinkBrevetRegistrationPage" runat="server" CssClass="other_page_link" NavigateUrl="~/BrevetRegistration.aspx">Brevet Registration</asp:HyperLink><br />
                    <br />
                    <asp:HyperLink ID="hyperLinkBrevetManagementPage" runat="server" CssClass="other_page_link" NavigateUrl="~/BrevetManagementPage.aspx">Brevet Management</asp:HyperLink><br />
                    <asp:HyperLink ID="hyperLinkClubManagementPage" runat="server" CssClass="other_page_link" NavigateUrl="~/ClubManagementPage.aspx">Club Management</asp:HyperLink><br />

                </div>
            </div>



            <div id="div_CENTER">
                <div class="div_center_HEADER">
                    Select a Brevet
                </div>

                <div id="div_center_LISTBOX">
                    <asp:ListBox ID="listBoxClubs" runat="server" AutoPostBack="True" OnSelectedIndexChanged="listBoxClubs_SelectedIndexChanged" CssClass="listbox_main"></asp:ListBox>
                </div>

                <div id="div_center_IMAGE">
                    <img id="main_image" src="images/brevet.png" alt="mountain image" />
                </div>
            </div>



            <div id="div_RIGHT">
                <div id="div_right_HEADER">
                    Rider List
                </div>

                <div id="div_right_DETAILS">

                    <div class="div_right_details_ROW">
                    <asp:ListBox ID="listBoxResult" runat="server" AutoPostBack="True" OnSelectedIndexChanged="listBoxClubs_SelectedIndexChanged" CssClass="listbox_main" Height="430px" Width="433px"></asp:ListBox>

                    </div>

                    <div class="div_right_details_ROW">
                
                    </div>

                    <div class="div_right_details_ROW">

                    </div>


                    <div class="div_right_details_ROW">
                    </div>
                </div>
                <!-- End of div_right_DETAILS -->


                <div id="div_right_BUTTONS">
                </div>


                <div id="div_right_VALIDATORS">
                    <div>

                    </div>

                </div>
                <!-- End of div_right_VALIDATORS -->
            </div>
            <!-- End of div_RIGHT -->



            <div id="div_FOOTER">
                <div id="div_footer_W3C_ICONS">
                    <a href="http://validator.w3.org/check?uri=referer">
                        <img class="w3c_icon" src="images/valid-xhtml10.png" alt="Valid XHTML 1.0 Transitional" /></a>
                    <a href="http://jigsaw.w3.org/css-validator/">
                        <img class="w3c_icon" src="images/vcss.png" alt="Valid CSS!" /></a>
                </div>

                <div id="div_footer_AUTHOR">
                    Valtteri Saarela 2016 v1.0
                </div>
            </div>


        </div>
        <!-- End of div_CONTAINER -->
    </form>
</body>
</html>
