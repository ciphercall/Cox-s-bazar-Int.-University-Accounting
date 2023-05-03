<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="AlchemyAccounting.login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <meta name="description" content="" />
    <meta name="author" content="" />
    <title>Admin Login</title>
    <link href="~/css2/bootstrap.min.css" rel="stylesheet" />
    <style type="text/css">
        body {
            /*background-color: white;
              background-image:url(../../img/logon/8.jpg);*/
            background: url(../../img/logon/8.jpg) no-repeat center center fixed;
            -webkit-background-size: cover;
            -moz-background-size: cover;
            -o-background-size: cover;
            background-size: cover;
        }

        .form-signin {
            max-width: 330px;
            padding: 15px;
            margin: 0 auto;
        }

            .form-signin .form-signin-heading, .form-signin .checkbox {
                margin-bottom: 10px;
            }

            .form-signin .checkbox {
                font-weight: normal;
            }

            .form-signin .form-control {
                position: relative;
                font-size: 16px;
                height: auto;
                padding: 10px;
                -webkit-box-sizing: border-box;
                -moz-box-sizing: border-box;
                box-sizing: border-box;
            }

                .form-signin .form-control:focus {
                    z-index: 2;
                }

            .form-signin input[type="text"] {
                margin-bottom: -1px;
                border-bottom-left-radius: 0;
                border-bottom-right-radius: 0;
            }

            .form-signin input[type="password"] {
                margin-bottom: 10px;
                border-top-left-radius: 0;
                border-top-right-radius: 0;
            }

        .account-wall {
            border-radius: 20px;
            -moz-border-radius-topleft: 20px;
            -moz-border-radius-topright: 20px;
            -moz-border-radius-bottomright: 20px;
            -moz-border-radius-bottomleft: 20px;
            margin-top: 20px;
            padding: 40px 10px 20px 10px;
            background-color: #FFFFFF;
            -moz-box-shadow: 0px 2px 2px rgba(0, 0, 0, 0.3);
            -webkit-box-shadow: 0px 2px 2px rgba(0, 0, 0, 0.3);
            box-shadow: 0px 2px 2px rgba(0, 0, 0, 0.3);
            margin-bottom: 10px;
        }

        .login-title {
            color: #0000CC;
            font-size: 18px;
            font-weight: 700;
            display: block;
            border-radius: 20px;
            -moz-border-radius-topleft: 20px;
            -moz-border-radius-topright: 20px;
            -moz-border-radius-bottomright: 20px;
            -moz-border-radius-bottomleft: 20px;
            margin-top: 20px;
            padding: 20px 10px 20px 10px;
            background-color: #FFFFFF;
            -moz-box-shadow: 0px 2px 2px rgba(0, 0, 0, 0.3);
            -webkit-box-shadow: 0px 2px 2px rgba(0, 0, 0, 0.3);
            box-shadow: 0px 2px 2px rgba(0, 0, 0, 0.3);
        }

        .profile-img {
            width: 96px;
            height: 96px;
            margin: 0 auto 10px;
            display: block;
            -moz-border-radius: 50%;
            -webkit-border-radius: 50%;
            border-radius: 50%;
        }

        .need-help {
            margin-top: 10px;
        }

        .new-account {
            display: block;
            margin-top: 10px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <br />
            <br />
            <br />
            <div class="container">
                <div class="row">
                    <div class="col-sm-6 col-md-4 col-md-offset-4">
                        <h5 class="text-center login-title">Sign in to continue to
                            <br />
                            Cox's Bazar International University 
                        </h5>
                        <div class="account-wall">
                            <img class="profile-img" src="https://lh5.googleusercontent.com/-b0-k99FZlyE/AAAAAAAAAAI/AAAAAAAAAAA/eu7opA4byxI/photo.jpg?sz=120"
                                alt="" />
                            <asp:TextBox ID="txtUserName" runat="server" CssClass="form-control input-group-sm"
                                placeholder="User Name" TabIndex="1"
                                OnTextChanged="txtUserName_TextChanged"></asp:TextBox>
                            <asp:TextBox ID="txtPassword" TextMode="Password" runat="server" CssClass="form-control input-group-sm"
                                placeholder="Password" TabIndex="2"
                                OnTextChanged="txtPassword_TextChanged"></asp:TextBox>
                            <asp:Label ID="lblErrmsg" runat="server" BackColor="White" ForeColor="Red"
                                Text="Label" Visible="False"></asp:Label>
                            <br />

                            <asp:Button ID="loginButton" runat="server" Text="Sign in" TabIndex="3" CssClass="btn btn-lg btn-primary btn-block"
                                OnClick="loginButton_Click" BorderColor="Black" BorderWidth="2px" ForeColor="Black" />
                            <br />

                            <a target="_blank" href="http://alchemy-bd.com/" class="pull-right need-help">Powered By- Alchemy Software </a><span class="clearfix"></span>
                        </div>
                    </div>
                </div>
            </div>
            <br />
            <%--  <div class="container">
            <div class="col-md-6 col-md-offset-3">
                <h3>
                    Log in</h3>
                <div class="row">
                    <div class="col-md-10">
                        <div class="col-md-4 mrgnTop5">
                            <asp:Label ID="Label4" runat="server" CssClass="" AssociatedControlID="txtUserName">User name</asp:Label>
                        </div>
                        <div class="col-md-8 form-control-container">
                            <asp:TextBox ID="txtUserName" runat="server" CssClass="form-control input-group-sm" TabIndex="1"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-10">
                        <div class="col-md-4 mrgnTop5">
                            <asp:Label ID="Label1" runat="server" AssociatedControlID="txtPassword" TextMode="Password">Password</asp:Label>
                        </div>
                        <div class="col-md-8 form-control-container">
                           <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control input-group-sm" TabIndex="2"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-10">
                        <div class="col-md-4 mrgnTop5">
                            <asp:Label runat="server" ID="lblmsg" CssClass="label label-danger"></asp:Label>
                        </div>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-md-10">
                        <div class="col-md-5 form-control-container">
                          <asp:Button ID="loginButton" runat="server" Text="Button" TabIndex="3" CssClass="btn btn-info"
        onclick="loginButton_Click" />

                            <asp:Label ID="lblBrID" runat="server" Visible="false"></asp:Label>
                           </div>
                    </div>
                </div>
            </div>
        </div>--%>
        </div>
    </form>
    <!-- jQuery Version 1.11.0 -->
    <script src="js/jquery-1.11.0.js"></script>
    <!-- Bootstrap Core JavaScript -->
    <script src="js/bootstrap.min.js"></script>
</body>
</html>
