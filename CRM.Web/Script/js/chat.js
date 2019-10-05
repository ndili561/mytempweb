var chatHub = $.connection.chatHub;
var myContactId =0;
var activeChatUser = [];
$(function () {
    $("#chat-send").unbind();
    $("#chat-exit").unbind();
    $.connection.hub.start()
        .done(function () {
            chatHub.server.getConnectionId(email);
        })
        .fail(function () { chatRefreshState(false); });

    // This function is called every time start chat is called in the table
    $("#onlineUsers").on({
        click: function (e) {
            e.preventDefault();
            var contactId = $(this).data("id");
            chatHub.server.engageChatWithUser(contactId);
        }
    }, ".engage-user");
    $("#onlineUsers").on({
        click: function (e) {
            e.preventDefault();
            var contactId = $(this).data("id");
            var message = $("#message_" + contactId).val();
            chatHub.server.sendCasualMessageToUser(contactId, message);
        }
    }, ".message-user");
});

chatHub.client.chatHistoryMessage = function (message, guestContactId) {
    $("#chatList-" + guestContactId).append(message);
}

chatHub.client.displayMessage = function (message) {
    displaySuccessMessage(message);
}

chatHub.client.casualMessageFromUser = function (message) {
    $.niftyNoty({
        floating: {
            position: 'top-right',
            animationIn: 'flash',
            animationOut: 'rollOut'
        },
        type: 'danger',
        icon: 'fa fa-check',
        message: message,
        container: 'page',
        timer: 0,
        focus: true,
        closeBtn: true
    });
}

chatHub.client.addMessage = function (contactId, from, value) {
    var dt = new Date();
    var time = dt.getHours() + ":" + dt.getMinutes() + ":" + dt.getSeconds();
    var myChatMessage = "";
    if (from === "me") {
        myChatMessage = '<li class="mar-btm">' +
            '    <div class="media-right">' +
            '        <img src="/Content/Images/Users/' + myContactId + '.png" class="img-circle img-sm" alt="Profile Picture">' +
            '    </div>' +
            '    <div class="media-body pad-hor speech-right">' +
            '        <div class="speech">' +
            '            <a href="#" class="media-heading">' + from + '</a>' +
            '            <p>' + value + '</p>' +
            '            <p class="speech-time">' +
            '                <i class="demo-pli-clock icon-fw"></i>' + time +
            '            </p>' +
            '        </div>' +
            '    </div>' +
            '</li>';
    } else {
        myChatMessage = '<li class="mar-btm">' +
            '    <div class="media-left">' +
            '        <img src="/Content/Images/Users/' + contactId + '.png" class="img-circle img-sm" alt="Profile Picture">' +
            '    </div>' +
            '    <div class="media-body pad-hor">' +
            '        <div class="speech">' +
            '            <a href="#" class="media-heading">' + from + '</a>' +
            '            <p>' + value + '</p>' +
            '            <p class="speech-time">' +
            '                <i class="demo-pli-clock icon-fw"></i>' + time +
            '            </p>' +
            '        </div>' +
            '    </div>' +
            '</li>';
    }
    setStoredChat(contactId, $("#chatList-" + contactId).html());
    $("#chatList-" + contactId).append(myChatMessage);
    var chatDiv = $("#chat-box-msg-" + contactId);
    var height = chatDiv[0].scrollHeight;
    chatDiv.scrollTop(height);
    $("#abbr_" + contactId).text("now");
};
// This function will be invoke by the server every time the status of the user is change
chatHub.client.onlineUser = function (name, userContactId, connectionId, status, onlineTimestamp) {
    if (contactId !== undefined && userContactId !== undefined && contactId !== userContactId) {
        // var d = new Date();
        var element = $("#tr" + userContactId);
        var statusElement = $("#tdStatus_" + userContactId);
        var onlineSince = $("#abbr_" + userContactId);
        var htmlText = '<tr connectionId="' + connectionId + '" id="tr' + userContactId + '"><td><abbr id="abbr_' + userContactId + '" class="timeago" title="' + onlineTimestamp + '">' + onlineTimestamp + '</abbr></td>' +
            '<td>' + name + '</td>' +
            '<td id="tdStatus_' + userContactId + '">' + status + '</td>' +
            '<td><a href="#" class="engage-user text-warning text-lg text-bold" data-id="' + userContactId + '">Start Chat</a></td>' +
            '<td><textarea class="col-md-10" id="message_' + userContactId + '" ></textarea><a  href="#" class="message-user col-md-2 text-info text-lg text-bold" data-id="' + userContactId + '">Send</a></td>' +
            +'</tr>';
        if (name === undefined || name === null || status === undefined || status === null) {
            $(element).replaceWith("");
        } else {
            if (element.length === 0) {
                $("#onlineUsers > tbody").prepend(htmlText);
            } else {
                $(statusElement).text(status);
                $(onlineSince).text("now");
            }
        }
        $("#onlineUsers > tbody:first").find("abbr.timeago").timeago();
    }
};

chatHub.client.leave = function () {
    chatHub.server.leaveChat(myContactId);
};

chatHub.client.startChat = function (guestContactId, guestName) {
    IncommunitiesChat.init(guestName, guestContactId);
    activeChatUser.push(guestContactId);
    requestChat = true;
    setChatId(guestName, guestContactId);
    chatRefreshState(guestContactId, guestName);
    if (!$("#chat-box-" + guestContactId).hasClass("chat-open")) {
        toggleChatBox(guestContactId);
    }
    var charHistory = getStoredChat(guestContactId);
    if (charHistory !== undefined && charHistory !== null) {
        $("#chatList-" + guestContactId).append(charHistory);
    }
};

chatHub.client.endChat = function (guestContactId, guestName) {
    displayErrorMessage("Chat is closed by " + guestName);
    if ($("#chat-box-" + guestContactId).hasClass("chat-open")) {
        toggleChatBox(guestContactId);
    }
    $("#chat-box-header-" + guestContactId).hide();
}

chatHub.client.emailResult = function (state) {
    if (!state) {
        $("#chat-box-" + guestContactId).html(options.emailFailed);
    }
};

function hasStorage() {
    return typeof (Storage) !== 'undefined';
}

function setChatId(guestName, guestChatId) {
    if (hasStorage()) {
        sessionStorage.setItem(guestName, guestChatId);
    }
}

function getExistingChatId(guestContactId) {
    if (hasStorage()) {
        return sessionStorage.getItem(guestContactId);
    }
    return 0;
}

chatHub.client.clientContactId = function (hostEmail, hostContactId) {
    if (email === hostEmail) {
        contactId = hostContactId;
    }
    chatHub.server.getOnlineUser(email);
}

function hasStorage() {
    return typeof (Storage) !== 'undefined';
}

function setStoredChat(contactId, data) {
    if (hasStorage()) {
        var chatHistory = getStoredChat(contactId);
        chatHistory += data;
        localStorage.setItem(contactId, chatHistory);
    }
}

function getStoredChat(contactId) {
    if (hasStorage()) {
        return localStorage.getItem(contactId);
    }
}

function clearChat(contactId) {
    if (hasStorage()) {
        localStorage.removeItem(contactId);
    }
}

function chatRefreshState(contactId, guestName) {

    $("#chat-box-" + contactId).html(
        '<div id="chat-box-msg-' + contactId + '" class="nano-content pad-all" style="height:330px;overflow:auto;"><ul id="chatList-' + contactId + '" class="list-unstyled media-block"></ul></div>' +
        '<div id="chat-box-input-' + contactId + '" style="height:50px;"><textarea id="chat-box-textinput-' + contactId + '" style="width:100%;height: 50px;border:1px solid #0354cb;border-radius: 3px;" /></div>' +
        '<p class="pad-top pad-btm">' +
        '<input class="btn btn-info pull-left" type="button" id="chat-send-' + contactId + '" value="Send" />' +
        '<input class="btn btn-success pull-left col-md-offset-1" type="button" id="chat-save-' + contactId + '" value="Save" />' +
        '<input class="btn btn-purple pull-left col-md-offset-1" type="button" id="chat-history-' + contactId + '" value="History" />' +
        '<input class="btn btn-danger pull-left col-md-offset-1" type="button" id="chat-clear-' + contactId + '" value="Clear" />'
    );
    $("#headerTitle-" + contactId).text("Chat with " + guestName);
    $("#chat-box-header-" + contactId).show();

}

function toggleChatBox(contactId) {
    var elm = $("#chat-box-header-" + contactId);
    if ($("#chat-box-" + contactId).hasClass("chat-open")) {
        $("#chat-box-" + contactId).removeClass("chat-open");
        elm.css("bottom", "0px");
    } else {
        var y = 451 + elm.height();
        $("#chat-box-" + contactId).addClass("chat-open");
        elm.css("bottom", y);
    }
    $("#chat-box-" + contactId).slideToggle();
}

var IncommunitiesChat = function () {

    var requestChat = false;
    var chatId = '';
    var chatEditing = false;
    var options = [];

    function setDefaults() {
        options.position = 'fixed';
        options.placement = 'bottom-right';
        options.zIndex = 10000;

        options.headerPaddings = '3px 10px 3px 10px';
        options.headerBackgroundColor = '#50C7A7';
        options.headerTextColor = 'white';
        options.headerBorderColor = '#50C7A7';
        options.headerGradientStart = '#50C7A7';
        options.headerGradientEnd = '#50C7A7';
        options.headerFontSize = '15px';

        options.boxBorderColor = '#0376ee';
        options.boxBackgroundColor = '#fff';
        options.width = 350;

        options.offlineTitle = 'Contact us!';
        options.onlineTitle = 'Chat with us!';

        options.emailSent = 'Your email was sent, thanks we\'ll get back to you asap.';
        options.emailFailed = 'Doh! The email could not be sent at the moment.<br /><br />Sorry about that.';

    }

    function config(args) {
        setDefaults();
    }

    function getPlacement() {
        var right = (activeChatUser.length * options.width) + 10;
        if (options.placement == 'bottom-right') {
            return "bottom:50px;right:" + right+"px;";
        }
        return '';
    }

    function init(guestname,guestcontactId) {
        if (guestcontactId === undefined || guestcontactId === null || guestcontactId === 0) {
            return;
        }
        $('body').append(
            '<div data-guestname="' + guestname + '" id = "chat-main-window-' + guestcontactId + '">' +
            '<div id = "chat-box-header-' + guestcontactId + '" style="z-index: 3;display: none;position:' + options.position + ';' + getPlacement() + 'width:' + options.width + 'px;padding:' + options.headerPaddings + ';color:' + options.headerTextColor + ';font-size:' + options.headerFontSize + ';cursor:pointer;background:' + options.headerBackgroundColor + ';filter: progid:DXImageTransform.Microsoft.gradient(startColorstr=\'' + options.headerGradientStart + '\', endColorstr=\'' + options.headerGradientEnd + '\');background: -webkit-gradient(linear, left top, left bottom, from(' + options.headerGradientStart + '), to(' + options.headerGradientEnd + '));background: -moz-linear-gradient(top,  ' + options.headerGradientStart + ',  ' + options.headerGradientEnd + ');border:1px solid ' + options.headerBorderColor + ';box-shadow:inset 0 0 7px #0354cb;-webkit-box-shadow:inset 0 0 7px #0354cb;border-top-left-radius: 5px;border-top-right-radius: 5px;"><span id="headerTitle-' + guestcontactId + '"></span><input class="btn btn-info pull-right" type="button" id="chat-minimize-' + guestcontactId +'" value="Toggle" /><input class="btn btn-danger pull-right" type="button" id="chat-exit-' + guestcontactId +'" value="Exit" /></div>' +
            '<div id = "chat-box-'+guestcontactId +'" style="display: none;position:' + options.position + ';' + getPlacement() + 'width:' + options.width + 'px;height:450px;margin-bottom:0px;padding: 10px 10px 10px 10px;border: 1px solid ' + options.boxBorderColor + ';background-color:' + options.boxBackgroundColor + ';opacity: 0.8;font-size:14px !important;color: black !important;"></div>'
        +'</div>'
            );
        $("#chat-box-" + guestcontactId).unbind();
        $("#chat-box-" + guestcontactId).on({
            keydown: function (e) {
                var msg = $(this).val();
                if (e.keyCode == 13 && msg != '') {
                    e.preventDefault();
                    e.stopPropagation();
                    if (guestcontactId == null || guestcontactId == '') {
                        displayErrorMessage('The user is not logged in');
                    } else {
                        chatHub.server.send(msg, guestcontactId);
                    }
                    $("#chat-box-textinput-" + guestcontactId).val('');
                }
            }
        }, "#chat-box-textinput-" + guestcontactId);

        $("#chat-send-" + guestcontactId).unbind();
        $("#chat-box-" + guestcontactId).on({
            click: function () {
                var msg = $("#chat-box-textinput-" + guestcontactId).val();
                if (msg !== undefined && msg !== null  && msg !== '' && myContactId > 0 && guestcontactId > 0 && myContactId !== guestcontactId) {
                    chatHub.server.send(msg, guestcontactId);
                    $("#chat-box-textinput-" + guestcontactId).val('');
                }
            }
        }, "#chat-send-" + guestcontactId);

        $("#chat-history-" + guestcontactId).unbind();
        $("#chat-box-" + guestcontactId).on({
            click: function () {
                if (guestcontactId > 0 && myContactId !== guestcontactId) {
                    chatHub.server.chatHistoryMessage(myContactId, guestcontactId);
                }

            }
        }, "#chat-history-" + guestcontactId);

        $("#chat-save-" + guestcontactId).unbind();
        $("#chat-box-" + guestcontactId).on({
            click: function () {
                var msg = getStoredChat(guestcontactId);;
                if (msg !== undefined && myContactId > 0 && guestcontactId > 0 && myContactId !== guestcontactId) {
                    chatHub.server.saveMessage(msg, myContactId, guestcontactId);    
                }
               
            }
        }, "#chat-save-" + guestcontactId);

        $("#chat-clear-" + guestcontactId).unbind();
        $("#chat-box-" + guestcontactId).on({
            click: function () {
                clearChat(guestcontactId);
                $("#chatList-" + guestcontactId).html(''); 
                displaySuccessMessage("The chat has been clear from your local browser.");
            }
        }, "#chat-clear-" + guestcontactId);

        $("#chat-box-header-" + guestcontactId).unbind();
        $("#chat-box-header-" + guestcontactId).on({
            click: function () {
                chatHub.server.closeChat(guestcontactId);
                activeChatUser = $.grep(activeChatUser, function (value) {
                    return value != guestcontactId;
                });
                $("#chat-box-" + guestcontactId).hide();
                $("#chat-box-" + guestcontactId).removeClass("chat-open");
                $("#chat-box-header-" + guestcontactId).hide();
            }
        }, "#chat-exit-" + guestcontactId);
        $("#chat-minimize-" + guestcontactId).unbind();
        $("#chat-box-header-" + guestcontactId).on({
            click: function () {
                toggleChatBox(guestcontactId);
            }
        }, "#chat-minimize-" + guestcontactId);

        $("#chat-box-" + guestcontactId).on({
            click: function () {
                chatHub.server.sendEmail($("#chat-box-email").val(), $("#chat-box-cmt").val());
                $("#chat-box").html(options.emailSent);
                chatEditing = false;
            }
        }, "#chat-box-send-" + guestcontactId);
    }
    return {
        config: config,
        init: init
    }
}();

$(function () {
    IncommunitiesChat.config();
    IncommunitiesChat.init();
});
