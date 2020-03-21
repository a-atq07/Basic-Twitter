<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Twitter.Pages.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<link href="../Content/bootstrap.min.css" rel="stylesheet" />
	<link href="../Styles/SignUp.css" rel="stylesheet" />
	<script src="../Scripts/jquery-3.3.1.min.js"></script>
	<script src="../Scripts/bootstrap.min.js"></script>
	<title>Twitter | Login</title>
</head>
<body>
	<form id="formLogin" runat="server">
		<table id="formContainer">
			<tr>
				<td>
					<asp:Label runat="server" ID="lblSuccessMessage" ForeColor="Green" EnableViewState="false" />
				</td>
			</tr>
			<tr>
				<td>
					<h1>Login</h1>
				</td>
			</tr>
			<tr>
				<td>
					<div class="form-group">
						<label class="col-sm-2" for="pwd">Username or Email</label>
						<div class="col-sm-5">
							<asp:TextBox runat="server" ID="txtUserName" CssClass="form-control input-sm" />
						</div>
						<div class="col-sm-5">
							<asp:RequiredFieldValidator ControlToValidate="txtUserName" Text="Username or email is required." ForeColor="Red" runat="server" />
						</div>
					</div>
				</td>
			</tr>
			<tr>
				<td>
					<div class="form-group">
						<label class="col-sm-2" for="pwd">Password</label>
						<div class="col-sm-5">
							<asp:TextBox runat="server" ID="txtPassword" TextMode="Password" CssClass="form-control input-sm" />
						</div>
						<div class="col-sm-5">
							<asp:RequiredFieldValidator ControlToValidate="txtPassword" Text="Password is required." ForeColor="Red" runat="server" />
						</div>
					</div>
				</td>
			</tr>
			<tr>
				<td>
					<div class="form-group">
						<asp:Button runat="server" Text="Login" ID="btnLogin" OnClick="btnLogin_Click" CssClass="btn btn-primary col-sm-2" />
						&nbsp; &nbsp; Don't have an account?
						<asp:HyperLink NavigateUrl="~/Pages/SignUp.aspx" runat="server" Text="Sign up here" />.
					</div>
				</td>
			</tr>
			<tr>
				<td>
					<div class="form-group">
						<asp:Label ID="lblErrorMessage" EnableViewState="false" runat="server" ForeColor="Red" />
					</div>
				</td>
			</tr>
		</table>
	</form>
</body>
</html>
