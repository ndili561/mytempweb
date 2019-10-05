var map, dataLayer, infobox;
var infoBoxes = [];
var pushpinFrameHTML = '<div class="Infobox title"><a class="infobox_close" href="javascript:closeInfobox()"><img src="/images/close.png"/></a><divontent">{content}</div></div><div class="infobox_pointer"><img src="/images/pointer_shadow.png"></div;';


function GetMap() {
    map = new Microsoft.Maps.Map('#track-property', {
        credentials: "AiDDomU1M5MWQziR_RapoDaxmDLCRZg7zjgj3ZxuintD0ezv4XVyDbDoTIwUzAmW",
        center: new Microsoft.Maps.Location(property.Latitude, property.Longitude),
        mapTypeId: Microsoft.Maps.MapTypeId.automatic,
        showDashboard: false,
        enableSearchLogo: false,
        zoom: 16
    });
    trackProperty();
}

function trackProperty() {
    var centerPoint = new Microsoft.Maps.Location(property.Latitude, property.Longitude);
    AddPushPin(centerPoint, property.Code);
}

function AddPushPin(center, propertyCode) {
    debugger;
    var pin = new Microsoft.Maps.Pushpin(center, {
        icon: '/images/home.png',
        width: 10,
        height: 10,
        anchor: new Microsoft.Maps.Point(16, 20),
        title: propertyCode,
        subTitle: propertyCode
    });
    map.entities.push(pin);
}
