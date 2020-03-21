<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="Twitter.Pages.Home" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<link href="https://maxcdn.bootstrapcdn.com/font-awesome/4.2.0/css/font-awesome.min.css" rel="stylesheet" />
	<link href="../Content/bootstrap.min.css" rel="stylesheet" />
	<script src="../Scripts/jquery-3.3.1.min.js"></script>
	<script src="../Scripts/bootstrap.min.js"></script>
	<link href="../Styles/Home.css" rel="stylesheet" />
	<script src="../Scripts/Home/Home.js"></script>
	<title>Twitter | Home</title>
</head>
<body>
	<form id="form1" runat="server">
		<asp:HiddenField runat="server" ID="hdnUserID" />
		<div class="header" id="myHeader">
			<div class="left">
				<a id="lnkMyProfile">
					<span class="fa fa-user fa-2x"></span>
					<asp:Label ID="lblUserName" style="cursor: pointer" EnableViewState="true" runat="server" />
				</a>
			</div>
			<div class="right">
				<asp:HyperLink runat="server" NavigateUrl="~/Pages/Home.aspx"><i class="fa fa-home fa-2x"></i></asp:HyperLink>
				<button type="button" class="btn btn-primary" style="margin-left: 10px" data-toggle="modal" data-target="#myModal"><i class="fa fa-plus-square"></i> Tweet</button>
				<asp:Button ID="btnLogout" runat="server" CssClass="btn btn-default" Text="Logout" OnClick="btnLogout_Click" UseSubmitBehavior="false" />
			</div>
			<div class="centerBlock">
				<table>
					<tr>
						<td>
							<asp:TextBox runat="server" ID="txtSearch" CssClass="form-control" AutoPostBack="false"
								Style="width: 250px; margin-left: -150px; margin-right: 3px;" placeholder="search here" />
						</td>
						<td>
							<asp:DropDownList runat="server" ID="ddlSearchType" CssClass="form-control"
								Style="width: 100px; margin-right: 3px;" AutoPostBack="false" EnableViewState="false">
								<asp:ListItem Text="Tweets" Value="0" Selected="True" />
								<asp:ListItem Text="Users" Value="1" />
							</asp:DropDownList>
						</td>
						<td>
							<button id="btnSearch" class="btn btn-default">
								<span aria-hidden="true" class="fa fa-search"></span>
							</button>
						</td>
					</tr>
				</table>
			</div>
		</div>
		<div class="content">
			<div class="center" id="usersContainer">
				<%-- Users will populate here --%>
				
			</div>
			<div class="center" id="tweetsFeed">
				<%-- tweets will populate here --%>

			</div>
		</div>
	</form>

	<!-- New Tweet Modal -->
	<div class="modal fade" id="myModal" role="dialog">
		<div class="modal-dialog">

			<!-- Modal content-->
			<div class="modal-content">
				<div class="modal-header">
					<button type="button" id="btnCloseModal" class="close" data-dismiss="modal"><i class="fa fa-close"></i></button>
					<h3 class="modal-title">New Tweet</h3>
				</div>
				<div class="modal-body">
					<textarea id="tweetContent" placeholder="What's happening" class="form-control" rows="7"></textarea>
				</div>
				<div class="modal-footer">
					<span class="left text-danger" id="lblError"></span>
					<button type="button" id="btnTweet" class="btn btn-primary" data-dismiss="modal">Tweet</button>
					<%--<button type="button" class="btn btn-danger" data-dismiss="modal">Close</button>--%>
				</div>
			</div>

		</div>
	</div>
</body>
</html>
