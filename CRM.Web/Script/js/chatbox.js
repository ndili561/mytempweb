//this function can remove a array element.
Array.remove = function (array, from, to) {
    var rest = array.slice((to || from) + 1 || array.length);
    array.length = from < 0 ? array.length + from : from;
    return array.push.apply(array, rest);
};

//this variable represents the total number of popups can be displayed according to the viewport width
var total_popups = 0;

//arrays of popups ids
var popups = [];

//this is used to close a popup
function close_popup(id) {
    for (var iii = 0; iii < popups.length; iii++) {
        if (id === popups[iii]) {
            Array.remove(popups, iii);

            document.getElementById(id).style.display = "none";

            calculate_popups();

            return;
        }
    }
}

//displays the popups. Displays based on the maximum number of popups that can be displayed on the current viewport width
function display_popups() {
    var right = 300;

    var iii = 0;
    for (iii; iii < total_popups; iii++) {
        if (popups[iii] != undefined) {
            var chatelement = document.getElementById(popups[iii]);
            chatelement.style.right = right + "px";
            right = right + 250;
            chatelement.style.display = "block";
        }
    }

    for (var jjj = iii; jjj < popups.length; jjj++) {
        var element = document.getElementById(popups[jjj]);
        element.style.display = "none";
    }
}

//creates markup for a new popup. Adds the id to popups array.
function register_popup(id, name) {

    for (var iii = 0; iii < popups.length; iii++) {
        //already registered. Bring it to front.
        if (id === popups[iii]) {
            Array.remove(popups, iii);

            popups.unshift(id);

            calculate_popups();


            return;
        }
    }

    var element = ' <div class="small-chat-box fadeInRight animated" id="' + id + '">';
    element = element + '<div class="heading" draggable="true"> ';
    element = element + '<small class="chat-date text-center"> 02.01.2018</small>' + name + '';
    element = element + '<a class="chat-date pull-right" onclick="close_popup(\'' + id + '\');">&#10005;</a></div>';
    element = element + ' <div class="content"></div>';
    element = element + '<div class="form-chat">';
    element = element + '<div class="input-group input-group-sm">';
    element = element + '<input type="text" class="form-control"> <span class="input-group-btn">';
    element = element + '<button class="btn btn-primary" type="button"> Send';

    element = element + '</button>';
    element = element + '</span>';
    element = element + '</div></div></div>';

    document.getElementsByTagName("body")[0].innerHTML = document.getElementsByTagName("body")[0].innerHTML + element;

    popups.unshift(id);

    calculate_popups();

}

//calculate the total number of popups suitable and then populate the toatal_popups variable.
function calculate_popups() {
    var width = window.innerWidth;
    if (width < 540) {
        total_popups = 0;
    }
    else {
        width = width - 200;
        //320 is width of a single popup box
        total_popups = parseInt(width / 320);
    }

    display_popups();

}

//recalculate when window is loaded and also when window is resized.
window.addEventListener("resize", calculate_popups);
window.addEventListener("load", calculate_popups);

// Open close small chat
function toggleChatbox(element) {
    $(element).children().toggleClass('fa-comments').toggleClass('fa-remove');
    $('.small-chat-box').toggleClass('active');
};