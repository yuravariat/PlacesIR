var GlobalFunctions = {};
var Search = {};
Search.CurrentLocations = null;
Search.SearchFromValidationRules = null;
Search.PlaceItemTemplate = null;
Search.GoogleMapMarkers = {};

GlobalFunctions.JqueryAjax = function(options) {
    var settings = $.extend({
        url: '',
        data: {},
        successfun: false,
        errorfunc: false,
        synchronious: false,
        crossDomain: false,
        method: 'POST'
    }, options);

    $.ajax({
        async: !settings.synchronious,
        type: settings.method,
        url: settings.url,
        contentType: "application/json; charset=utf-8",
        dataType: (!settings.crossDomain) ? "json" : "jsonp",
        data: settings.data,
        timeout: 600000,
        success: settings.successfun,
        error: settings.errorfunc
    });
}

//#region Loading window
var LoadingDiv = null;
LoadingImg = new Image();
LoadingImg.src = 'Content/images/loading.gif';
function InitLoading() {
    var globalContainer = $('body');
    if (globalContainer.length > 0) {
        var loadingDivContent = '<div id="loading-container" class="loading-container whole-width hidden">\
                                    <div class="background whole-width">\
                                    </div>\
                                    <div class="loading-window whole-width white dark-font">\
                                        <div class="close-btn"></div>\
                                        <div class="image"><img src="' + LoadingImg.src + '" /></div>\
                                        <div class="text" id="loading-text-div">Please wait ...</div>\
                                        <div id="loading-text-div2"></div>\
                                    </div>\
                                </div>';
        LoadingDiv = $(loadingDivContent);
        globalContainer.append(LoadingDiv);
    }
    return false;
}
function ShowLoading(msg1, msg2, resultsSearch) {
    if (typeof LoadingDiv == 'undefined' || LoadingDiv == null) {
        InitLoading();
    }
    var html1 = '', html2 = '';
    if (typeof LoadingDiv != 'undefined' && LoadingDiv != null && !LoadingDiv.hasClass('hidden')) {
        return false;
    }
    if (typeof msg1 == "undefined" || msg1 == '')
        html1 = 'Please wait ...';
    else
        html1 = msg1;
    $("#loading-text-div", LoadingDiv).html(html1);
    if (typeof msg2 == "undefined" || msg2 == '')
        html2 = '';
    else
        html2 = msg2;
    $("#loading-text-div2", LoadingDiv).html(html2);
    LoadingDiv.removeClass('hidden');
    // Center
    $('.loading-window', LoadingDiv).css('margin-left', Math.floor(((1 - ($('.loading-window', LoadingDiv).width() / $(window).width())) / 2) * 100) + '%');
}
function HideLoading() {
    if (typeof LoadingDiv != 'undefined' && LoadingDiv != null) {
        LoadingDiv.addClass('hidden');
    }
}
$(window).unbind('Search.ShowLoading').bind('Search.ShowLoading', function (e, data) {
    var msg1 = typeof data != "undefined" ? data.msg1 : '';
    var msg2 = typeof data != "undefined" ? data.msg2 : '';
    var resultsSearch = typeof data != "undefined" ? data.resultsSearch : false;
    ShowLoading(msg1, msg2, resultsSearch);
});
$(window).unbind('Search.HideLoading').bind('Search.HideLoading', function (e) {
    HideLoading();
});
$(window).unload(function () {
    HideLoading();
});
//#endregion

Search.FindLocationsAction = function () {

    var isValid = Search.SearchFromValidationRules.ValidateAll();
    if (!isValid) {
        return false;
    }

    var data = {};
    data.place = $('#inp-place-name').val();
    data.keywords = $('#inp-keywords').val();
    data.distance = Number($('#inp-distance').val()) * 1000;
    data.rankby = $('#inp-rank-by').val();

    $(window).trigger('Search.ShowLoading');
    GlobalFunctions.JqueryAjax({
        url: 'local-api/places/nearby',
        data: JSON.stringify(data),
        successfun: function (msg) {
            $(window).trigger('Search.HideLoading');
            var container = $('#places-results');
            if (msg.IsValid) {
                $('#places-panel').slideDown();
                Search.CurrentLocations = msg.NearByPlaces;
                if (Search.CurrentLocations.length > 0) {
                    $('#map-panel').slideDown();
                    $('#places-panel .panel-footer').show();
                    Search.DrawGoogleMap(msg.CenterPlace, Search.CurrentLocations, data.distance);
                    container.empty();
                    var ul = $('<ul>');
                    for (var i in Search.CurrentLocations) {
                        var li = $('<li>' + Search.PlaceItemTemplate + '</li>');
                        $('.name', li).html(Search.CurrentLocations[i].name);
                        $('.logo', li).attr('src', Search.CurrentLocations[i].icon);
                        li.attr("rel", Search.CurrentLocations[i].place_id);
                        li.click(function (e, frommap) {
                            if (!$(this).hasClass('active')) {
                                $(this).siblings().removeClass('active');
                                $(this).addClass('active');
                                if (frommap) {
                                    var offsetTop = $('#places-panel .panel-body').scrollTop() + $('#places-results>ul>li.active').offset().top - $('#places-panel .panel-body').offset().top;
                                    $('#places-panel .panel-body').scrollTop(offsetTop - ($('#places-panel .panel-body').height() / 2) - 30);
                                }
                                google.maps.event.trigger(Search.GoogleMapMarkers[$(this).attr("rel")], 'click');
                            }
                        });
                        ul.append(li);
                    }
                    container.append(ul);
                }
                else {
                    container.html('No places found.');
                    $('#places-panel').slideDown();
                    $('#map-panel').slideUp();
                }
            }
            else {
                container.html('No places found.');
                var message = '';
                for (var key in msg.Errors) {
                    message += msg.Errors[key] + ', ';
                }
                alert(message);
            }
            return false;
        },
        errorfunc: function (msg) {
            $(window).trigger('Search.HideLoading');
            alert('error');
            $('#places-results').html('No places found.');
            $('#places-panel').slideDown();
            $('#map-panel').slideUp();
            return false;
        }
    });
}
Search.DrawGoogleMap = function (centerPlace,nearByPlaces,distance) {
	if(typeof centerPlace!='undefined'){
	    var mapOptions = {
	        center: { lat: centerPlace.geometry.location.lat, lng: centerPlace.geometry.location.lng },
	        zoom: Search.ComputeZoomLevel(distance/1000)
	    };
	    var map = new google.maps.Map(document.getElementById('google-map'), mapOptions);
	    
	    //setting markers
	    $.each(nearByPlaces,function( index, place ) {
	        var contentString = '<div id="content">'+
		        '<div class="place-popup">'+
		        '<h4>' + place.name + '</h4>'+
		        '<div id="bodyContent">'+
		        place.name+
		        '</div>'+
		        '</div>';
	
		    var infowindow = new google.maps.InfoWindow({
		        content: contentString
		    });
	        
	        var pinIcon = new google.maps.MarkerImage(
	            place.icon,
	            null, /* size is determined at runtime */
	            null, /* origin is 0,0 */
	            null, /* anchor is bottom center of the scaled image */
	            new google.maps.Size(20, 20)
	        )
	        var marker = new google.maps.Marker({
	            id: place.place_id,
	            position: new google.maps.LatLng(place.geometry.location.lat, place.geometry.location.lng),
	            map: map,
	            title: place.name,
	            animation: google.maps.Animation.DROP,
	            icon: index == 0 ? null : pinIcon
	        });
	        google.maps.event.addListener(marker, 'click', function(e) {
	            var item = $('#places-results>ul>li[rel=' + marker.id + ']');
	            if (item.length > 0 && !item.hasClass('active')) {
	                item.trigger("click", true);
	            }
	            else {
	                for (var v in Search.GoogleMapMarkers) {
	                    Search.GoogleMapMarkers[v].infowindow.close();
	                }
	                infowindow.open(map,marker);
	            }
	        });
	        // Push the marker to the 'markers' array
	        Search.GoogleMapMarkers[place.place_id] = marker;
	        Search.GoogleMapMarkers[place.place_id].infowindow = infowindow;
	        
	        //add listener to google maps (fix size issue)
	        google.maps.event.addListenerOnce(map, 'idle', function () {
	           google.maps.event.trigger(map, 'resize');
	        });
	    });
	}
}
Search.ComputeZoomLevel = function(distance){
	var zoom = 15;
	if(distance>1){
		if(distance<4){
			zoom -=2;
		}
		else if(distance>4 && distance<10){
			zoom -=4;
		}
		else if(distance>10 && distance<30){
			zoom -=6;
		}
	}
	return zoom;
}
$(document).ready(function (e) {
    $('#search-places-btn').click(function (e) {
        e.preventDefault();
        e.stopImmediatePropagation();
        Search.FindLocationsAction();
    });
    Search.SearchFromValidationRules = $("#search-from-rules").ValidationGroup();
    Search.PlaceItemTemplate = $('#place-template').html();
});
