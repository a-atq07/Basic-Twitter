let CURRENT_USER_ID;

$(function () {
	CURRENT_USER_ID = Number($('#hdnUserID').val());

	$('#txtSearch').keypress(enterButtonHandler);
	$('#btnSearch').click(searchHandler);
	$('#btnTweet').click(tweetHandler);
	$('#btnCloseModal').click(closeModalHandler);
	$('#lnkMyProfile').click(() => { loadUserProfile(CURRENT_USER_ID); });
	loadTweets();
});


function loadTweets() {
	$.ajax({
		url: "/Services/TweetService.asmx/GetTweets",
		type: 'GET',
		contentType: 'application/json; charset=utf-8',
		success: onLoadTweetsSuccess,
		error: onLoadTweetsFailure
	});
}

function searchUsers(searchStr) {
	let input = JSON.stringify({
		searchKey: searchStr
	});

	$.ajax({
		url: "/Services/UserService.asmx/SearchUsers",
		data: input,
		type: 'POST',
		dataType: 'json',
		contentType: 'application/json; charset=utf-8',
		success: onSearchUsersSuccess,
		error: onSearchUsersFailure
	});
}

function searchTweets(searchStr) {
	let input = JSON.stringify({
		searchKey: searchStr
	});

	$.ajax({
		url: "/Services/TweetService.asmx/SearchTweets",
		data: input,
		type: 'POST',
		dataType: 'json',
		contentType: 'application/json; charset=utf-8',
		success: onLoadTweetsSuccess,
		error: onLoadTweetsFailure
	});
}

function tweetHandler() {
	let content = $('#tweetContent').val();
	if (content) {
		disableTweetButton();

		let input = JSON.stringify({
			tweetContent: content
		});

		$.ajax({
			url: "/Services/TweetService.asmx/AddTweet",
			data: input,
			type: 'POST',
			contentType: 'application/json; charset=utf-8',
			success: onTweetSuccess,
			error: onTweetFailure
		});
	} else {
		return false;
	}
}

function onSearchUsersSuccess(response) {
	let users = response.d;
	clearContainer();
	if (users) {
		for (let user of users) {
			populateUser(user);
		}
	}
}

function onSearchUsersFailure(response) {
	console.log(response);
}

function onLoadTweetsSuccess(response) {
	let tweets = response.d;
	clearContainer();
	if (tweets) {
		for (let tweet of tweets) {
			tweet.TweetedOn = formateDate(tweet.TweetedOn);
			populateInFeedBottom(tweet);
		}
	}
}

function onLoadTweetsFailure(response) {
	console.log(response)
}


function onTweetSuccess(response) {
	let tweet = response.d;
	tweet.TweetedOn = formateDate(tweet.TweetedOn)
	//populate tweet in feed
	console.log(tweet);
	//populateInFeedTop(tweet);

	$('#btnCloseModal').click();
	location.reload();
}

function onTweetFailure(response) {
	enableTweetButton();
	$('#lblError').html('Could not tweet. Please try again.');
	return false;
}

function populateInFeedTop(tweet) {
	let tweetElement = createTweetUIElement(tweet);
	$('#tweetsFeed').prepend(tweetElement);
}


function populateInFeedBottom(tweet) {
	let tweetElement = createTweetUIElement(tweet);
	$('#tweetsFeed').append(tweetElement);
}

function populateUser(user) {
	let userElement = createUserUIElement(user);
	$('#usersContainer').append(userElement);
}

function closeModalHandler() {
	enableTweetButton();
	$('#lblError').html('');
	$('#tweetContent').val('');
}

function enableTweetButton() {
	$('#btnTweet').html("Tweet");
	$('#btnTweet').prop('disabled', false);
}

function disableTweetButton() {
	$('#btnTweet').html(`<i class="fa fa-spinner fa-spin"></i> Tweeting`);
	$('#btnTweet').prop('disabled', true);
}

function formateDate(dateString) {
	let date = new Date(parseInt(dateString.substr(6)));
	return date.toLocaleDateString('default', {
		day: '2-digit', month: 'short', year: 'numeric', hour: '2-digit', minute: '2-digit'
	});
}

function createTweetUIElement(tweet) {
	let tweetTemplate = `<div class="tweetCotainer">
							<a onclick="return loadUserProfile(${tweet.TweetedBy})"><h4 class="visible-lg-inline">${tweet.TweetedByName}</h4>
							</a><span class="pull-right small text-light">${tweet.TweetedOn}</span></br>
							${tweet.Content}
						</div>`
	return tweetTemplate;
}

function createUserUIElement(user) {
	if (user.ID != CURRENT_USER_ID) {
		let userTemplate = `<div class="tweetCotainer">
							<a onclick="return loadUserProfile(${user.ID})"><h4 class="visible-lg-inline">${user.Name}</h4></a>
							<span class="text-light">&commat;${user.Handle}</span>
							<button onclick="return toggleFollowing(${user.ID})" id="btnFollow_${user.ID}" class="btn btn-default btn-sm pull-right">
								${user.IsFollowed ? '<i class="fa fa-check"></i> Following' : '<i class="fa fa-plus-square"></i> Follow'}</button>
							</br>
							<span>${user.FollowerCount} Followers</span> |
							<span>${user.FollowingCount} Following</span>
							<br /><span>${user.Address}</span>
						</div >`;
		return userTemplate;
	} else {
		let userTemplate = `<div class="tweetCotainer">
							<a onclick="return loadUserProfile(${user.ID})"><h4 class="visible-lg-inline">${user.Name}</h4></a>
							<span class="text-light">&commat;${user.Handle}</span>
							</br>
							<a onclick="return loadFollowerUsers()"><span>${user.FollowerCount} Followers</span></a> |
							<a onclick="return loadFollowingUsers()"><span>${user.FollowingCount} Following</span></a>
							<br /><span>${user.Address}</span>
						</div >`;
		return userTemplate;
	}

}

function clearTweetsFeed() {
	$('#tweetsFeed').html('');
}

function clearUsersContainer() {
	$('#usersContainer').html('');
}

function clearContainer() {
	clearTweetsFeed();
	clearUsersContainer();
}

function enterButtonHandler(event) {
	if (event.keyCode == 13) {
		return false;
	}
}

function searchHandler() {
	if ($('#txtSearch').val()) {
		if ($('#ddlSearchType').val() == '0') {
			searchTweets($('#txtSearch').val());
		} else {
			searchUsers($('#txtSearch').val());
		}
	}
	return false;
}

function toggleFollowing(id) {
	let input = JSON.stringify({
		userID: id
	});

	$.ajax({
		url: "/Services/UserService.asmx/ToggleFollowing",
		data: input,
		type: 'POST',
		dataType: 'json',
		contentType: 'application/json; charset=utf-8',
		success: (response) => { onToggleFollowingSuccess(response, id); },
		error: onToggleFollowingFailure
	});
	return false;
}

function onToggleFollowingSuccess(response, id) {
	let btnID = '#btnFollow_' + id;
	if (response.d) {
		setButtonToFollowing(btnID);
	} else {
		setButtonToFollow(btnID);
	}
}

function onToggleFollowingFailure(response) {
	console.log(response);
}

function setButtonToFollowing(btnID) {
	$(btnID).html(`<i class="fa fa-check"></i> Following`);
}

function setButtonToFollow(btnID) {
	$(btnID).html(`<i class="fa fa-plus-square"></i> Follow`);
}

function loadUserProfile(id) {
	let input = JSON.stringify({
		userID: id
	});

	$.ajax({
		url: "/Services/UserService.asmx/UserProfile",
		data: input,
		type: 'POST',
		dataType: 'json',
		contentType: 'application/json; charset=utf-8',
		success: onLoadUserProfileSuccess,
		error: onLoadUserProfileFailure
	});
	return false;
}

function onLoadUserProfileSuccess(response) {
	let tweets = response.d.Tweets;
	let user = response.d.User;

	clearContainer();

	if (user) {
		populateUser(user);
	}

	if (tweets) {
		for (let tweet of tweets) {
			tweet.TweetedOn = formateDate(tweet.TweetedOn);
			populateInFeedBottom(tweet);
		}
	}
}

function onLoadUserProfileFailure(response) {
	console.log(response);
}

function loadFollowerUsers() {
	$.ajax({
		url: "/Services/UserService.asmx/GetFollowers",
		type: 'GET',
		dataType: 'json',
		contentType: 'application/json; charset=utf-8',
		success: onSearchUsersSuccess,
		error: onSearchUsersFailure
	});
	return false;
}

function loadFollowingUsers() {
	$.ajax({
		url: "/Services/UserService.asmx/GetUsersFollowedBy",
		type: 'GET',
		dataType: 'json',
		contentType: 'application/json; charset=utf-8',
		success: onSearchUsersSuccess,
		error: onSearchUsersFailure
	});
	return false;
}