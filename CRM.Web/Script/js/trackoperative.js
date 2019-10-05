var map, infobox, directionsManager, directionWaypointLayer;
var directionsErrorEventObj;
var directionsUpdatedEventObj;
var sourceAndDestination = [];

function initializeMap() {
    var longitude = operativeRouteData.vanlongitude ;
    var latitude = operativeRouteData.vanlatitude;
    map = new Microsoft.Maps.Map(document.getElementById('myMap'),
        {
            credentials: "AiDDomU1M5MWQziR_RapoDaxmDLCRZg7zjgj3ZxuintD0ezv4XVyDbDoTIwUzAmW",
            zoom: 12,
            center: new Microsoft.Maps.Location(latitude, longitude)
        }
    );  
    //Create a layer for managing custom waypoints.
    directionWaypointLayer = new Microsoft.Maps.Layer();
    //Add mouse events for showing instruction when hovering pins in directions waypoint layer.
    Microsoft.Maps.Events.addHandler(directionWaypointLayer, 'mouseover', showInstruction);
    Microsoft.Maps.Events.addHandler(directionWaypointLayer, 'mouseout', hideInstruction);
    map.layers.insert(directionWaypointLayer);
    //Create a reusable infobox.
    infobox = new Microsoft.Maps.Infobox(map.getCenter(), {
        showCloseButton: false,
        visible: false
    });
    infobox.setMap(map);
}

function getMap() {
    initializeMap();
    createDirections();
}


function createDirections() {

    Microsoft.Maps.loadModule('Microsoft.Maps.Directions',function () {
        //Create an instance of the directions manager.
        directionsManager = new Microsoft.Maps.Directions.DirectionsManager(map);
        // Set Route Mode to driving
        directionsManager.setRequestOptions({ routeMode: Microsoft.Maps.Directions.RouteMode.driving });
        //Add event handlers to directions manager.
        Microsoft.Maps.Events.addHandler(directionsManager, 'directionsError', function(){});
        Microsoft.Maps.Events.addHandler(directionsManager, 'directionsUpdated', function () { });
        createDrivingRoute();
    });
}

function createDrivingRoute() {
    if (operativeRouteData) {

        var sourceLocation = new Microsoft.Maps.Location(operativeRouteData.vanlatitude, operativeRouteData.vanlongitude);
        var destinationLocation = new Microsoft.Maps.Location(operativeRouteData.propertylatitude, operativeRouteData.propertylongitude);
        directionsManager.setRenderOptions(
            {
                itineraryContainer: document.getElementById('directionsItinerary'),
                firstWaypointPushpinOptions: { visible: false },
                lastWaypointPushpinOptions: { visible: false },
                waypointPushpinOptions: { visible: false }

            });

        var sourceWaypoint = new Microsoft.Maps.Directions.Waypoint({ location: sourceLocation });
        directionsManager.addWaypoint(sourceWaypoint);
        var destinationWaypoint = new Microsoft.Maps.Directions.Waypoint({ location: destinationLocation });
        directionsManager.addWaypoint(destinationWaypoint);

        //Add event handlers to directions manager.
        Microsoft.Maps.Events.addHandler(directionsManager, 'directionsUpdated', directionsUpdated);
        directionsManager.calculateDirections();
    }
}

function directionsUpdated(e) {
    directionWaypointLayer.clear();
    map.entities.clear();
    var route = e.route[0];
    var waypointCnt = 0;
    var stepCount = 0;
    var waypointLabel = "ABCDEFGHIJKLMNOPQRSTYVWXYZ";
    var wp = [];
    var step;
    var isWaypoint;
    var waypointColor;
    for (var i = 0; i < route.routeLegs.length; i++) {
        for (var j = 0; j < route.routeLegs[i].itineraryItems.length; j++) {
            stepCount++;
            isWaypoint = true;
            step = route.routeLegs[i].itineraryItems[j];
            if (j == 0) {
                if (i == 0) {
                    //Start Endpoint, make it green.
                    waypointColor = '#008f09';
                } else {
                    //Midpoint Waypoint, make it gray,
                    waypointColor = '#737373';
                }
            } else if (i == route.routeLegs.length - 1 && j == route.routeLegs[i].itineraryItems.length - 1) {
                //End waypoint, make it red.
                waypointColor = '#d60000';

            } else {
                //Instruction step
                isWaypoint = false;
            }
            if (isWaypoint) {
                pin = new Microsoft.Maps.Pushpin(step.coordinate, {
                    icon: '<svg xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" width="52" height="49.4" viewBox="0 0 37 35" xml:space="preserve"><circle cx="32" cy="30" r="4" style="stroke-width:2;stroke:#ffffff;fill:#000000;"/><polygon style="fill:rgba(0,0,0,0.5)" points="18,1 32,30 18,18 18,1"/><rect x="2" y="2" width="15" height="15" style="stroke-width:2;stroke:#000000;fill:{color}"/><text x="9" y="13" style="font-size:11px;font-family:arial;fill:#ffffff;" text-anchor="middle">{text}</text></svg>',
                    anchor: new Microsoft.Maps.Point(42, 39),
                    color: waypointColor,
                    text: waypointLabel[waypointCnt]    //Give waypoints a letter as a label.
                });
                //Store the instruction information in the metadata.
                pin.metadata = {
                    description: step.formattedText,
                    infoboxOffset: new Microsoft.Maps.Point(-30, 25)
                };
                waypointCnt++;
            } else {
                //Instruction step, make it a red circle with its instruction index.
                pin = new Microsoft.Maps.Pushpin(step.coordinate, {
                    icon: '<svg xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" width="18" height="17" viewBox="0 0 36 34" xml:space="preserve"><circle cx="16" cy="16" r="14" style="fill:{color}" /><text x="16" y="21" style="font-size:16px;font-family:arial;fill:#ffffff;" text-anchor="middle">{text}</text></svg>',
                    anchor: new Microsoft.Maps.Point(9, 9),
                    color: '#d60000',
                    text: stepCount + ''
                });
                //Store the instruction information in the metadata.
                pin.metadata = {
                    description: step.formattedText,
                    infoboxOffset: new Microsoft.Maps.Point(0, 0)
                };
            }

            wp.push(pin);
        }
    }
    //Reverse the order of the pins so that when rendered the last waypoints in the route are on top.
    wp.reverse();
    //Add the pins to the map. 
    directionWaypointLayer.add(wp);
    ////Get the all the waypoints used to calculate the route, this includes any via waypoints created by dragging.
    var waypoints = directionsManager.getAllWaypoints();
    //Display the waypoint information as a table.
    var html = ['<table><tr><td>Address</td><td>Location</td><td>IsViapoint</td></tr>'];
    for (var i = 0, len = waypoints.length; i < len; i++) {
        html.push('<tr><td>', waypoints[i].getAddress(), '</td><td>', waypoints[i].getLocation(), '</td><td>', waypoints[i].isViapoint(), '</td></tr>');
    }
    html.push('</table>');
    document.getElementById('output').innerHTML = html.join('');
    updateRouteValueToDom();
}

function updateRouteValueToDom() {
    sourceAndDestination = [];
    $("#output table tr").each(function () {
        sourceAndDestination.push($(this).find("td:first").text()); //put elements into array
    });
    $("#StartLocation").val(sourceAndDestination[1]);
    $("#FinalDestination").val(sourceAndDestination[sourceAndDestination.length -1]);
}

function removeViaRoute(deleteViaRoute) {
    var viaLocations = [];
    var url = "/Van/DeleteViaRoute";
    var startLocation = $("#StartLocation").val();
    var finalDestination = $("#FinalDestination").val();
    $("#divViaLocation input").each(function () {
        viaLocations.push($(this).val()); //put elements into array
    });
    var model = { DeleteViaRoute: deleteViaRoute, StartLocation: startLocation, FinalDestination: finalDestination, ViaLocations: viaLocations };
    $.ajax({
        url: url,
        data: { model: model },
        type: "POST",
        success: function (data) {
            $('#divRouteDetails').html(data);
        }
    });  
}

function addViaRoute() {
    var viaLocations = [];
    var url = "/Van/AddViaRoute";
    var startLocation = $("#StartLocation").val();
    var finalDestination = $("#FinalDestination").val();
    $("#divViaLocation input").each(function () {
        viaLocations.push($(this).val()); //put elements into array
    });
    var model = {StartLocation: startLocation, FinalDestination: finalDestination, ViaLocations: viaLocations};
    $.ajax({
        url: url,
        data: { model: model},
        type: "POST",
        success: function (data) {
            $('#divRouteDetails').html(data);
        }
    }); 
}

function getDirections() {
    //Clear any previously calculated directions.
    directionsManager.clearAll();
    directionsManager.clearDisplay();
    //Create waypoints to route between.
    var start = new Microsoft.Maps.Directions.Waypoint({ address: $('#StartLocation').val() });
    directionsManager.addWaypoint(start);
    $("#divViaLocation input").each(function () {
        if ($(this).val() !== undefined && $(this).val() !== null && $(this).val() !== "") {
            var via = new Microsoft.Maps.Directions.Waypoint({ address: $(this).val() });
            directionsManager.addWaypoint(via);  
        }
    });
   
    var end = new Microsoft.Maps.Directions.Waypoint({ address: $('#FinalDestination').val() });
    directionsManager.addWaypoint(end);
    //Calculate directions.
    directionsManager.calculateDirections();
}

function showInstruction(e) {
    infobox.setOptions({
        location: e.target.getLocation(),
        description: e.target.metadata.description,
        offset: e.target.metadata.infoboxOffset,
        visible: true
    });
}
function hideInstruction() {
    infobox.setOptions({ visible: false });
}

function confirmSaveRoute() {
    var viaLocations = [];
    var url = "/Van/SaveRoute";
    var startLocation = $("#StartLocation").val();
    var finalDestination = $("#FinalDestination").val();
    $("#divViaLocation input").each(function () {
        viaLocations.push($(this).val()); //put elements into array
    });
    var model = { StartLocation: startLocation, FinalDestination: finalDestination, ViaLocations: viaLocations };
    bootbox.dialog({
        size: 'small',
        message: "Are you sure you want to save?",
        buttons: {
            success: {
                label: "Ok",
                className: "btn-success",
                callback: function () {
                    $.ajax({
                        type: 'POST',
                        url: url,
                        data: { model: model },
                        success: function (returnResult) {
                            window.location.reload(true);
                            displaySuccessMessage(returnResult.message);
                        }
                    });
                }
            },
            danger: {
                label: "Cancel",
                className: "btn-danger",
                callback: function () {
                    return true;
                }
            }
        }
    });
    $(".modal-content").css("width", "400px");
    $(".modal-dialog").css("min-width", "1%");
}