function initializeTypeahedPersonSearch(elementId) {
    var mapPersonObject = {};
    var personArray = [];

    var fixPersonTypeaheadPos = _.debounce(function () {
        var box = $(elementId)[0];
        var newPos = elPos(box);
        $(box).parent().find("ul.typeahead").css({ top: newPos.y + 32, left: newPos.x });
    },
        10,
        true);

    var searchPerson = _.debounce(function (query, process) {
        //get the data to populate the typeahead (plus an id value)
        $.get('/Person/GetPersonForAutoComplete',
            { query: encodeURIComponent(query) },
            function (data) {
                debugger;
                //reset these containers every time the user searches
                //because we're potentially getting entirely different results from the api
                mapPersonObject = {};
                personArray = [];
                //Using underscore.js for a functional approach at looping over the returned data.
                _.each(data,
                    function (item, ix, list) {

                        //for each iteration of this loop the "item" argument contains
                        //1 person object from the array in our json, such as:
                        // { "id":7, "name":"Test User" }
                        if (!_.contains(personArray, item)) {
                            //item.forename = item.id +
                            //    ' #' +
                            //    item.forename +
                            //    ' #' +
                            //    item.surname;
                            //add the label to the display array
                            personArray.push(item.id + " " + item.forename + " " + item.surname + " " + item.dateofbirth);
                            //also store a hashmap so that when bootstrap gives us the selected
                            //name we can map that back to an id value
                            mapPersonObject[item.id + " " + item.forename + " " + item.surname + " " + item.dateofbirth] = item;
                        }
                    });
                //send the array of results to bootstrap for display
                process(personArray);
            });
    },
        300);

    $(".typeahead").typeahead({
        minLength: 3,
        items: 15,
        matcher: function () {
            fixPersonTypeaheadPos();
            return true;
        },
        source: function (query, process) {
            //here we pass the query (search) and process callback arguments to the throttled function
            searchPerson(query, process);
        },
        updater: function (item) {
            var p = mapPersonObject[item];
            //save the id value into the hidden field
            $(elementId).val(p.id);
            //return the string you want to go into the textbox (e.g. name)
            return item;
        },
        highlighter: function (item) {
            var p = mapPersonObject[item];
            var itm = '' +
                "<div class='typeahead_wrapper'>" +
                "<div class='typeahead_labels'>" +
                "<div class='typeahead_primary'>id: " +
                p.id + " | Name: " + p.forename + " " + p.surname + " | DoB: " + p.dateofbirth +
                "</div>" +
                //"<div class='typeahead_secondary'>" +
                //"Name: " + p.forename +" " + p.surname +
                //"</div>" +
                "</div>" +
                "</div>";
            return itm;
        }
    });

    function elPos(element) {
        var position = { x: element.offsetLeft, y: element.offsetTop };
        while (element = element.offsetParent) {
            position.x += element.offsetLeft;
            position.y += element.offsetTop;
        };
        return position;
    }
}
function initializeDropzone(formId, elementId, paramName, validateInputId) {
    $(formId).dropzone({
        //parameter name value
        paramName: paramName,
        //clickable div id
        clickable: '#previews',
        //preview files container Id
        previewsContainer: "#previewFiles",
        autoProcessQueue: false,
        //uploadMultiple: true,
        parallelUploads: 100,
        maxFiles: 100,
        //  url:"/", // url here to save file
        maxFilesize: 100, //max file size in MB,
        addRemoveLinks: true,
        dictResponseError: 'Server not Configured',
        acceptedFiles: ".ics", // use this to restrict file type
        init: function () {
            var self = this;
            // config
            self.options.addRemoveLinks = true;
            self.options.dictRemoveFile = "Delete";
            //New file added
            self.on("addedfile",
                function (file) {
                    //  alert('new file added ');
                    console.log('new file added ', file);
                    $('.dz-success-mark').hide();
                    $('.dz-error-mark').hide();
                });
            // Send file starts
            self.on("sending",
                function (file, xhr, formData) {
                    //alert('sending');
                    formData.append('Id', $(validateInputId).val());
                    formData.append('FileName', file.name);
                    formData.append('UploadedFile', file.UploadedFile);
                    console.log('upload started', file);
                    $('.meter').show();
                });

            // File upload Progress
            self.on("totaluploadprogress",
                function (progress) {
                    // alert('progress start');
                    console.log("progress ", progress);
                    $('.roller').width(progress + '%');
                    //alert('progress start');
                });

            self.on("queuecomplete",
                function (progress) {
                    // alert('queuecomplete');
                    $('.meter').delay(999).slideUp(999);
                });

            // On removing file
            self.on("removedfile",
                function (file) {
                    // alert('removedfile');
                    console.log(file);
                });

            $(elementId).on("click",
                function (e) {
                    e.preventDefault();
                    e.stopPropagation();
                    // Validate form here if needed
                    if ($(validateInputId).val().length > 0 && $(validateInputId).val() > 0) {
                        if (self.getQueuedFiles().length > 0) {
                            self.processQueue();
                        } else {
                            self.uploadFiles([]);
                            $(elementId).submit();
                        }
                    } else {
                        displayErrorMessage("Please select employee");
                        $(validateInputId).focus();
                    }


                });


            self.on("successmultiple",
                function (files, response) {
                    // Gets triggered when the files have successfully been sent.
                    // Redirect user or notify of success.

                });
        }
    });
}

function openApp(url) {
    window.open(url);
}
