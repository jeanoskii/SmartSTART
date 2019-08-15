<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="CAPRES.index" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <!-- The above 3 meta tags *must* come first in the head; any other head content must come *after* these tags -->
    <title>SmartSTART</title>
    <!-- Bootstrap -->
    <link href="assets/css/bootstrap.min.css?parameter=1" rel="stylesheet" />
    <!-- Custom CSS -->
    <link href="assets/css/styles.css" rel="stylesheet" />
    <!-- HTML5 shim and Respond.js for IE8 support of HTML5 elements and media queries -->
    <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
    <!--[if lt IE 9]>
      <script src="https://oss.maxcdn.com/html5shiv/3.7.2/html5shiv.min.js"></script>
      <script src="https://oss.maxcdn.com/respond/1.4.2/respond.min.js"></script>
    <![endif]-->
</head>
<body id="login-body-bg">
    <form id="form1" runat="server">
    <div>
        <div id="login-container">
            <div class="center-block">
                <div>
                    <img src="assets/images/smartstart.png" alt="SmartSTART" class="img-responsive"/>
                </div>
                <div id="login-well" class="well center-block">
                    <div class="text-center">
                        <asp:Label ID="lblError" runat="server" Text="" CssClass="error-label"></asp:Label>
                    </div>
                    <div class="form-group form-inline">
                        <label for="txtUsername" class="login-label h4">USERNAME:</label>
                        <asp:TextBox ID="txtUsername" runat="server" CssClass="login-textbox form-control" required></asp:TextBox>
                    </div>
                    <div class="form-group form-inline">
                        <label for="txtPassword" class="login-label h4">PASSWORD:</label>
                        <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="login-textbox form-control"
                            required></asp:TextBox>
                    </div>
                    <div class="form-group form-inline">
                    <asp:CheckBox ID="chkRemember" runat="server" Text="Remember me."
                        class="login-label checkbox-inline" />
                    <asp:Button ID="btnLogin" runat="server" Text="LOG IN" CssClass="btn btn-primary pull-right"
                        onclick="btnLogin_Click"/>
                    </div>
                </div>
            </div>
        </div>
        <div>
            <img src="assets/images/smartcareers.png" alt="SmartCareers" class="img-responsive pull-right" style="max-width:20%; max-height:auto; position:fixed; bottom:0; right:0" />
        </div>
    </div>
    </form>
</body>
</html>
