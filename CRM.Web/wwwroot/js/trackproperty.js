var map, dataLayer, infobox;
var infoBoxes = [];
var pushpinFrameHTML = '<div class="Infobox title"><a class="infobox_close" href="javascript:closeInfobox()"><img src="/images/close.png"/></a><divontent">{content}</div></div><div class="infobox_pointer"><img src="/images/pointer_shadow.png"></div;';


function GetMap() {
    map = new Microsoft.Maps.Map('#track-property', {
        credentials: "AqGdUTIZpeNjc44ZEMkZ3xGqf8pm9PBaVEj3PDMcf4JJ2JO2YJ_Z9EzqD7FD3n5L",
        center: new Microsoft.Maps.Location(property.Latitude, property.Longitude),
        mapTypeId: Microsoft.Maps.MapTypeId.automatic,
        showDashboard: false,
        enableSearchLogo: false,
        zoom: 16
    });
    trackProperty();
    $("#track-property").css("position", "absolute");
}

function trackProperty() {
    var centerPoint = new Microsoft.Maps.Location(property.Latitude, property.Longitude);
    AddPushPin(centerPoint, property.Code);
}

function AddPushPin(center, propertyCode) {
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
