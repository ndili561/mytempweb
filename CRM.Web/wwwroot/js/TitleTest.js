var titletest = (function () {

    var titleSearchFunc = {
        search: function () {
            $.ajax({
                cache: false,
                type: "GET",
                url: '/Home/TestGet',
                success: function (result) { $('#returnedtitles').html(result); }
            });
        }
    };
    return {
        loadPage: function () {
            titleSearchFunc.search();
        }
    }

})();