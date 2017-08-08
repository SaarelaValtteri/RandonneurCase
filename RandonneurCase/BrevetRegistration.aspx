<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BrevetRegistration.aspx.cs" Inherits="BrevetRegistration" %>

<!DOCTYPE html>

<<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta content="text/html; charset=iso-8859-1" http-equiv="content-type" />
    <link href="CSS/ModelCaseStyleSheet.css" rel="stylesheet" type="text/css" />
    <title>Brevet Registration - DWA Model Case</title>
</head>

<body>
    <form id="form1" runat="server">
        <div id="div_CONTAINER">


            <div id="div_HEADER">
                <div id="div_header_TEXT">
                    <h1>Brevet Registration</h1>
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
                    <br />

                </div>
            </div>



            <div id="div_CENTER">
                <div class="div_center_HEADER">
                    Brevets
                </div>

                <div id="div_center_LISTBOX">
                    <asp:ListBox ID="listBoxBrevets" runat="server" AutoPostBack="True" OnSelectedIndexChanged="listBoxBrevets_SelectedIndexChanged" CssClass="listbox_main"></asp:ListBox>
                </div>

                <div id="div_center_IMAGE">
                    <img id="main_image" src="images/brevet.png" alt="Mountain image" />
                </div>
            </div>



            <div id="div_RIGHT">
                <div id="div_right_HEADER">
                    Brevet Details
                </div>

                <div id="div_right_DETAILS">


                    <div class="div_right_details_ROW">
                        <asp:Label ID="lbDistance" runat="server" Text="Distance:" CssClass="detail_label"></asp:Label>
                        <asp:TextBox ID="tbDistance" runat="server" CssClass="detail_textbox" MaxLength="100"></asp:TextBox>

                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorDistance" runat="server"
                            ErrorMessage="Required!" SetFocusOnError="True"
                            ControlToValidate="tbDistance" CssClass="validatorMessage"></asp:RequiredFieldValidator>
                    </div>

                    <div class="div_right_details_ROW">
                        <asp:Label ID="lbDate" runat="server" Text="Date:" CssClass="detail_label"></asp:Label>
                        <asp:TextBox ID="tbDate" runat="server" CssClass="detail_textbox" MaxLength="50"></asp:TextBox>

                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorDate" runat="server"
                            ErrorMessage="Required!" SetFocusOnError="True"
                            ControlToValidate="tbDate" CssClass="validatorMessage">
                        </asp:RequiredFieldValidator>
                    </div>

                    <div class="div_right_details_ROW">
                        <asp:Label ID="lbLocation" runat="server" Text="Location:" CssClass="detail_label"></asp:Label>
                        <asp:TextBox ID="tbLocation" runat="server" CssClass="detail_textbox" MaxLength="50"></asp:TextBox>

                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorLocation" runat="server"
                            ErrorMessage="Required!" SetFocusOnError="True"
                            ControlToValidate="tbLocation" CssClass="validatorMessage">
                        </asp:RequiredFieldValidator>
                    </div>

                    
                    <div class="div_right_details_ROW">
                        <asp:Label ID="lbClimbing" runat="server" Text="Climbing(m):" CssClass="detail_label"></asp:Label>
                        <asp:TextBox ID="tbClimbing" runat="server" CssClass="detail_textbox" MaxLength="100"></asp:TextBox>

                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorClimbing" runat="server"
                            ErrorMessage="Required!" SetFocusOnError="True"
                            ControlToValidate="tbClimbing" CssClass="validatorMessage"></asp:RequiredFieldValidator>
                    </div>

                </div>
                <!-- End of div_right_DETAILS -->


                <div id="div_right_BUTTONS">
                    <asp:Button ID="btRegister" runat="server" Text="Click here to register" OnClick="btRegister_Click" CausesValidation="False" CssClass="div_right_buttons_button" Width="310px" />
                </div>


                <div id="div_right_VALIDATORS">
                    <div>
                        <asp:Label ID="lbMessage" runat="server" Text=""></asp:Label>
                    </div>

          
                    <br />


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
                    Student Name 2014 v1.0
                </div>
            </div>


        </div>
        <!-- End of div_CONTAINER -->
    </form>
</body>
</html>
