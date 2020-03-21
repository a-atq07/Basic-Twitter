<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SignUp.aspx.cs" Inherits="Twitter.Pages.SignUp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<link href="../Content/bootstrap.min.css" rel="stylesheet" />
	<link href="../Styles/SignUp.css" rel="stylesheet" />
	<script src="../Scripts/jquery-3.3.1.min.js"></script>
	<script src="../Scripts/bootstrap.min.js"></script>
	<script src="../Scripts/SignUp/SignUp.js"></script>
	<title>Twitter | Sign Up</title>
</head>
<body>
	<form id="formSignUp" runat="server">
		<table id="formContainer">
			<tr>
				<td>
					<h1>Sign Up</h1>
				</td>
			</tr>
			<tr>
				<td>
					<div class="form-group">
						<label class="col-sm-2" for="pwd">Name</label>
						<div class="col-sm-5">
							<asp:TextBox runat="server" ID="txtName" CssClass="form-control input-sm" />
						</div>
						<div class="col-sm-5">
							<asp:RequiredFieldValidator ControlToValidate="txtName" Text="Name is required." ForeColor="Red" runat="server" />
						</div>
					</div>
				</td>
			</tr>
			<tr>
				<td>
					<div class="form-group">
						<label class="col-sm-2" for="pwd">Username</label>
						<div class="col-sm-5">
							<asp:TextBox runat="server" ID="txtUserName" CssClass="form-control input-sm" />
						</div>
						<div class="col-sm-5">
							<asp:Label ID="lblUserNameEror" EnableViewState="false" runat="server" ForeColor="Red" />
							<asp:RequiredFieldValidator ControlToValidate="txtUserName" Text="Username is required." ForeColor="Red" runat="server" />
						</div>
					</div>
				</td>
			</tr>
			<tr>
				<td>
					<div class="form-group">
						<label class="col-sm-2" for="pwd">Email</label>
						<div class="col-sm-5">
							<asp:TextBox runat="server" ID="txtEmail" CssClass="form-control input-sm" />
						</div>
						<div class="col-sm-5">
							<asp:Label ID="lblEmailError" EnableViewState="false" runat="server" ForeColor="Red" />
							<asp:RequiredFieldValidator ControlToValidate="txtEmail" Text="Email is required." ForeColor="Red" runat="server" />
							<asp:RegularExpressionValidator ControlToValidate="txtEmail" Text="Email is invalid."
								ValidationExpression="^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$" ForeColor="Red" runat="server" Style="margin-left: -110px" />
						</div>
					</div>

				</td>
			</tr>
			<tr>
				<td>
					<div class="form-group">
						<label class="col-sm-2" for="pwd">Address</label>
						<div class="col-sm-5">
							<asp:TextBox runat="server" ID="txtAddress" TextMode="MultiLine" CssClass="form-control input-sm" />
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
						<label class="col-sm-2" for="pwd">Confirm Password</label>
						<div class="col-sm-5">
							<asp:TextBox runat="server" ID="txtConfirmPassword" TextMode="Password" CssClass="form-control input-sm" />
						</div>
						<div class="col-sm-5">
							<asp:RequiredFieldValidator ControlToValidate="txtConfirmPassword" Text="Confirm Password." ForeColor="Red" runat="server" />
							<asp:CompareValidator ControlToValidate="txtConfirmPassword" ControlToCompare="txtPassword" Text="Passwords don't match." ForeColor="Red" runat="server" Style="margin-left: -110px" />
						</div>
					</div>
				</td>
			</tr>
			<tr>
				<td>
					<div class="form-group">
						<asp:Button runat="server" Text="Sign Up" ID="btnSignUp" OnClick="btnSignUp_Click" CssClass="btn btn-primary col-sm-2" />
						&nbsp; &nbsp; Already have an account?
						<asp:HyperLink NavigateUrl="~/Pages/Login.aspx" runat="server" Text="Login here" />.
					</div>
				</td>
			</tr>
			<tr>
				<td>
					<div class="form-group">
						<asp:Label ID="lblErrorMessage" EnableViewState="false"  runat="server" ForeColor="Red" />
					</div>
				</td>
			</tr>
		</table>
	</form>
</body>
</html>
