var historySearch = false;
var selectedIds = "";

$(function () {
    initializeGrid();
});
function initializeGrid() {
    $('#filterAnchor').unbind();
    $('#filterAnchor').unbind("click");
    $("#filterAnchor").click(function () {
        $("#filterDiv").slideToggle("slow");
    });
    attachSearchGridEvents();
    //restoreSearchHistory();
}
//To maintain history for ajax calls in grid
window.onpopstate = function (event) {
    var historyElement = event.state;
    if (historyElement !== undefined && historyElement !== null) {
        var currentUrl = historyElement.url.split('?')[0];
        currentUrl = currentUrl + "?" + historyElement.data;
        if (historyElement.url.split('?').length > 1) {
            var pageNumber = historyElement.url.split('?')[1].split('&')[0];
            currentUrl = currentUrl + "&" + pageNumber;   
        }
        loadSearchView(currentUrl, historyElement.targetGridId, historyElement.type, null);
    }
};

function restoreSearchHistory() {
    if (historySearch === false) {
        var currentUrl = $(location).attr('href');
        var searchObj = JSON.parse(localStorage.getItem("searchHistoryList"));
        if (searchObj !== undefined && searchObj !== null) {
            try {
                var formElement = $(searchObj.targetGridId).closest("form");
                $(formElement).values(searchObj.data);
                loadSearchView(searchObj.url, searchObj.targetGridId, searchObj.type, searchObj.data);
            }
            catch (err) {
                // console.log(err);
            }
        }   
    }
    historySearch = true;
}

function initializeEnterkeyForFilter() {
    $('.filterInput').unbind();
    $('.filterInput').each(function (i, obj) {
        $(obj).keypress(function (e) {
            var key = e.which;
            if (key == 13)  // the enter key code
            {
                $('.btn-filter').click();
                return false;
            }
        });
    });
}

function attachSearchGridEvents() {
    $(".grid").find(".pagination a").unbind();
    $(".grid").find(".col-sort").unbind();
    $('.grid').each(function (i, obj) {
       
        var data = $(obj).closest("form").serialize();
        var url = $(obj).closest("form").attr('action');
        
        var callbackfuntion = $(obj).attr('callbackfuntion');
        $(obj).find(".pagination a").on("click", function (e) {
            e.preventDefault();
            var targetGridId = "#" + $(this).closest('.grid').attr('id');
            loadSearchView($(this).attr('href'), targetGridId, "GET", data,callbackfuntion);
            return false;
        });
        $(obj).find(".col-sort").on("click", function (e) {
            e.preventDefault();
            var targetGridId = "#" + $(this).closest('.grid').attr('id');
            loadSearchView($(this).attr('href'), targetGridId, "GET", data, callbackfuntion);
            return false;
        });

        var btnFilter = $(obj).closest("form").find(".btn-filter");
        $(btnFilter).unbind();
        $(btnFilter).on("click", function (e) {
            e.preventDefault();
            var targetGridId = "#" + $(this).attr('targetgridid');
            data = $(this).closest("form").serialize();
            url = url + "?=" + data;
            loadSearchView(url, targetGridId, "GET", data, callbackfuntion);
            return false;
        });
    });
    initializeEnterkeyForFilter();
};

var loadSearchView = function (url, targetGridId, type, data, functioncallback) {
    if (url !== undefined && url !== '#') {
        $.ajax({
            url: url,
            type: type,
            cache: false,
           
            success: function (response) {
                $(targetGridId).html("");
                $(targetGridId).html(response);
                $(targetGridId).animate({ scrollTop: $(targetGridId).scrollHeight }, 1000);
                attachSearchGridEvents();
                executeCallback(functioncallback);
            }
        });
        if (url !== null && data!=null) {
            var absoluteUrl = getAbsoluteUrl(url);
            var searchHistoryObject = { url: absoluteUrl, targetGridId: targetGridId, type: type, data: data, callbackfunc: functioncallback };
            localStorage.setItem("searchHistoryList", JSON.stringify(searchHistoryObject));
            history.pushState(searchHistoryObject, null, null);
        }
       
    }
};

var getAbsoluteUrl = (function () {
    var a;
    return function (url) {
        if (!a) a = document.createElement('a');
        a.href = url;

        return a.href;
    };
})();

function executeFunctionByName(functionName, context /*, args */) {
    var args = [].slice.call(arguments).splice(2);
    var namespaces = functionName.split(".");
    var func = namespaces.pop();
    for (var i = 0; i < namespaces.length; i++) {
        context = context[namespaces[i]];
    }
    return context[func].apply(context, args);
}

function filterByPageSize(newpagesize, searchstring, url, targetGridId) {
    targetGridId = "#" + targetGridId;
    url = url.split('?')[0];
    var data = $(targetGridId).closest("form").serialize();
    data = data.replace(/&(.)age(.)ize=(.*)/, "") + "&PageSize=" + newpagesize;
    $.ajax({
        url: url,
        type: "GET",
        data: data,
        success: function (response) {
            $(targetGridId).html("");
            $(targetGridId).html(response);
            attachSearchGridEvents();
            $('.grid').each(function (i, obj) {
                var callbackfuntion = $(obj).attr('callbackfuntion');
                executeCallback(callbackfuntion);
            });
        }
    });
}

function executeCallback(functioncallback) {
    try {
        if (typeof functioncallback == "function") {
            functioncallback();
        } else if (functioncallback !== undefined && functioncallback !== null && functioncallback !== '') {
            var callbackfunc = new Function(functioncallback);
            callbackfunc();
        }
    }
    catch (err) {// The callback method might not be available.
        console.log(err);
    }

  
}

function clearStorage(element) {
    localStorage.removeItem("searchHistoryList");
    var formValues = $(element).closest("form").serializeArray();
    try {
        $(element).closest("form").values(formValues);
    }
    catch (err) {
        console.log(err);
    }


}

$.fn.values = function (data) {
    var els = $(this).find(':input').get();
    if (typeof data != 'object') {
        var queryString = data.split('&');
        var formdata = [];
        $.each(queryString, function (i, query) {
            var objFormData = { name: '', val: '' };
            objFormData.name = query.split('=')[0];
            objFormData.val = query.split('=')[1];
            formdata.push(objFormData);
        });
        // return all data
        data = {};

        $.each(els, function () {
            if (this.name && !this.disabled && (this.checked
                            || /select|textarea/i.test(this.nodeName)
                            || /text|hidden|password/i.test(this.type))) {
                var control = this;
                $.each(formdata, function (i, element) {
                    if (control.name == element.name) {
                        var value = element.val.replace('%40', '@');
                        $(control).val(value);
                    }
                });
            }
        });
        return data;
    } else {
        //Clear all the values of the form
        $.each(els, function () {
            if (this.type == 'checkbox' || this.type == 'radio') {
                $(this).attr("checked", false);
            } else {
                $(this).val('');
            }
        });
        return $(this);
    }
};

function resetPageSize(newpagesize, searchstring, url, targetGridId, callbackfunction) {
    url = url.replace(/&PageSize=(.*)/, "");
    var testurl = url + "&filterText=" + searchstring + "&PageSize=" + newpagesize;
    $(".filterText").each(function (index, element) {
        testurl = testurl + "&" + element.id + "=" + $(element).val();
    });
    $.ajax({
        url: testurl,
        type: "GET",
        success: function (response) {
            $("#" + targetGridId).html("");
            $("#" + targetGridId).html(response);
            if (typeof callbackfunction == "function") callbackfunction();
        }
    });
}