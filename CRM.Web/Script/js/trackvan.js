var map, dataLayer, infobox;
var infoBoxes = [];
var pushpinFrameHTML = '<div class="Infobox title"><a class="infobox_close" href="javascript:closeInfobox()"><img src="/images/close.png"/></a><divontent">{content}</div></div><div class="infobox_pointer"><img src="/images/pointer_shadow.png"></div;';


function GetMap() {
    map = new Microsoft.Maps.Map('#track-map', {
        credentials: "AiDDomU1M5MWQziR_RapoDaxmDLCRZg7zjgj3ZxuintD0ezv4XVyDbDoTIwUzAmW",
        center: new Microsoft.Maps.Location(van.Latitude, van.Longitude),
        mapTypeId: Microsoft.Maps.MapTypeId.automatic,
        showDashboard: false,
        enableSearchLogo: false,
        zoom: 12
    });
    dataLayer = new Microsoft.Maps.EntityCollection();
    map.entities.push(dataLayer);

    var infoboxLayer = new Microsoft.Maps.EntityCollection();
    map.entities.push(infoboxLayer);
    infobox = new Microsoft.Maps.Infobox(new Microsoft.Maps.Location(0, 0), { visible: false });
    infoboxLayer.push(infobox);
    infobox.setMap(map);
    updateLocationOfVan();
    Microsoft.Maps.Events.addHandler(map, 'viewchange', function () {
        infobox.setOptions({ visible: false });
    });

}

function updateLocationOfVan(result) {
    $.each(vans, function (key, value) {
        var centerPoint = new Microsoft.Maps.Location(value.Latitude, value.Longitude);
        AddPushPin(centerPoint, value.Name, value.RegistrationNumber);
    });
}

function addInfoBox(title, description, location) {
    var infoBox = new Microsoft.Maps.Infobox(location, {
        title: title,
        description: description
    });
    infoBox.setMap(map);
    infoBoxes.push(infoBox);
}

function AddPushPin(center, driveName, registrationNumber) {

    var pin = new Microsoft.Maps.Pushpin(center, {
        icon: '/images/truck_32_orange.png',
        width: 10,
        height: 10,
        anchor: new Microsoft.Maps.Point(16, 20),
        title: driveName,
        subTitle: registrationNumber
    });
    Microsoft.Maps.Events.addHandler(pin, 'click', function (e) { displayInfobox(e, driveName, registrationNumber); });
    map.entities.push(pin);
}

function displayInfobox(e, driveName, registrationNumber) {
    if (e.targetType == 'pushpin') {
        var pin = e.target;
        var vanTrackUrl = '/Stock/Tracking/Index';
        // var voidUrl = 'stock/Tracking?voidId=' + driveName;
        var html = "";
        if (registrationNumber !== "") {
            html = "<span>DriverName = <a target='_blank' style='color:green;' href='" + vanTrackUrl + "'>" + driveName + "</a></span>";
            html += "<br><span>Registration Number = <a target='_blank' style='color:green;' href='" + vanTrackUrl + "'>" + registrationNumber + "</a></span>";
        }
        infobox.setOptions({
            visible: true,
            offset: new Microsoft.Maps.Point(-33, 20),

            htmlContent: pushpinFrameHTML.replace('{content}', html)
        });
        infobox.setLocation(e.target.getLocation());
        infobox.setMap(map);
    }
}

function closeInfobox() {
    infobox.setOptions({ visible: false });
}