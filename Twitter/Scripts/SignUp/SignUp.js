var userIDDuplicate = false;
var emailIDDuplicate = false;
$(function () {
	$('#txtUserName').change(function () {
		validateUserName(this.value);
	});
	$('#txtEmail').change(function () {
		validateEmailID(this.value);
	});
});

function validateUserName(userName) {
	var input = JSON.stringify({ userName: userName });
	$.ajax({

		url: '/Pages/SignUp.aspx/ValidateUserName',

		type: "POST",

		dataType: "json",

		data: input,

		contentType: "application/json; charset=utf-8",

		success: function (data) {

			if (data.d) {
				userIDDuplicate = true;
				$('#lblUserNameEror').text('Duplicate User ID.');
			}
			else {
				userIDDuplicate = false;
				$('#lblUserNameEror').text('');
			}
			changeButtonStatus(emailIDDuplicate || userIDDuplicate);
		}

	});
}

function validateEmailID(emailID) {
	var input = JSON.stringify({ email: emailID });
	$.ajax({

		url: '/Pages/SignUp.aspx/ValidateUserEmail',

		type: "POST",

		dataType: "json",

		data: input,

		contentType: "application/json; charset=utf-8",

		success: function (data) {

			if (data.d) {
				emailIDDuplicate = true;
				$('#lblEmailError').text('Duplicate email.');
			}
			else {
				emailIDDuplicate = false;
				$('#lblEmailError').text('');
			}
			changeButtonStatus(emailIDDuplicate || userIDDuplicate);
		}
	});
}

function changeButtonStatus(status) {
	$('#btnSignUp').prop('disabled', status);
}