var GlobalFunctions = {};
var Search = {};

Search.CurrentMainPlaceName = null;
Search.CurrentDistance = null;
Search.CurrentKeywords = null;

Search.CurrentLocations = null;
Search.CurrentPlaceSummary = null;
Search.SearchFromValidationRules = null;
Search.PlaceItemTemplate = null;
Search.GoogleMapMarkers = {};
Search.ApiKey = null;

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

Search.FindLocationsActionLock = false;
Search.FindLocationsAction = function () {

    if (!Search.FindLocationsActionLock) {

        Search.FindLocationsActionLock = true;
        var isValid = Search.SearchFromValidationRules.ValidateAll();
        if (!isValid) {
            Search.FindLocationsActionLock = false;
            return false;
        }

        var data = {};
        Search.CurrentMainPlaceName = data.place = $('#inp-place-name').val();
        Search.CurrentKeywords = data.keywords = $('#inp-keywords').val();
        Search.CurrentDistance = data.distance = Number($('#inp-distance').val()) * 1000;
        data.rankby = $('#inp-rank-by').val();

        $('#place-details-panel').hide();

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

                        // Add places to list container
                        var ul = $('<ul>');
                        for (var i in Search.CurrentLocations) {
                            var li = $('<li>' + Search.PlaceItemTemplate + '</li>');
                            $('.name', li).html(Search.CurrentLocations[i].name);
                            $('.logo', li).attr('src', Search.CurrentLocations[i].icon);
                            li.attr("rel", Search.CurrentLocations[i].place_id);
                            li.click(function (e, frommap) {
                                Search.PlaceSelected(this, frommap);
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
                Search.FindLocationsActionLock = false;
                return false;
            },
            errorfunc: function (msg) {
                $(window).trigger('Search.HideLoading');
                alert('error');
                $('#places-results').html('No places found.');
                $('#places-panel').slideDown();
                $('#map-panel').slideUp();
                Search.FindLocationsActionLock = false;
                return false;
            }
        });
    }
}
Search.PlaceSelected = function (liElement, frommap) {
    if (!$(liElement).hasClass('active')) {
        $(liElement).siblings().removeClass('active');
        $(liElement).addClass('active');
        $('#search-place-info').removeClass('disabled');
        if (frommap) {
            var offsetTop = $('#places-panel .panel-body').scrollTop() + $('#places-results>ul>li.active').offset().top - $('#places-panel .panel-body').offset().top;
            $('#places-panel .panel-body').scrollTop(offsetTop - ($('#places-panel .panel-body').height() / 2) - 30);
        }
        google.maps.event.trigger(Search.GoogleMapMarkers[$(liElement).attr("rel")], 'click');
    }
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
Search.GetPlaceDetailsLock = false;
Search.GetPlaceDetails = function (placeID) {

    if (!Search.GetPlaceDetailsLock) {

        Search.GetPlaceDetailsLock = true;

        var data = {};
        data.placeID = placeID;
        data.mainPlaceName = Search.CurrentMainPlaceName;

        $(window).trigger('Search.ShowLoading');
        GlobalFunctions.JqueryAjax({
            url: 'local-api/places/details',
            data: JSON.stringify(data),
            successfun: function (msg) {
                if (msg.IsValid) {
                    Search.CurrentPlaceSummary = msg.Obj;
                    Search.PlaceSummaryDetails();
                }
                else {
                    var message = '';
                    for (var key in msg.Errors) {
                        message += msg.Errors[key] + ', ';
                    }
                    alert(message);
                }
                $(window).trigger('Search.HideLoading');
                Search.GetPlaceDetailsLock = false;
                return false;
            },
            errorfunc: function (msg) {
                $(window).trigger('Search.HideLoading');
                alert('error');
                Search.GetPlaceDetailsLock = false;
                return false;
            }
        });

        Search.GetPlaceDetailsLock = false;
    }
}
Search.TestGetPlaceDetails = function () {
    Search.GetPlaceDetails('ChIJN1t_tDeuEmsRUsoyG83frY4');
}
Search.PlaceSummaryDetails = function () {
    if (Search.CurrentPlaceSummary != null && typeof Search.CurrentPlaceSummary.Place != 'undefined') {

        $('#place-details-panel').show();

        // General info
        $('#place-details-name').html(Search.CurrentPlaceSummary.Place.name);
        if (typeof Search.CurrentPlaceSummary.Place.rating != 'undefined' && !isNaN(Search.CurrentPlaceSummary.Place.rating)) {
            var width = $('#place-details-rating-stars .images').width();
            $('#place-details-rating-stars').width(parseInt((Search.CurrentPlaceSummary.Place.rating/5) * width));
            $('#place-details-rating').html(Search.CurrentPlaceSummary.Place.rating);
        }
        else {
            $('#place-details-rating-stars').width(0);
            $('#place-details-rating').html('Not rated');
        }
        $('#place-details-overview').html(Search.CurrentPlaceSummary.MainSummaryText);
        $('#place-details-address').html(Search.CurrentPlaceSummary.Place.formatted_address);
        $('#place-details-vicinity').html(Search.CurrentPlaceSummary.Place.vicinity);
        $('#place-details-phone').html(Search.CurrentPlaceSummary.Place.international_phone_number);
        $('#place-details-url').html(Search.CurrentPlaceSummary.Place.website);
        $('#place-details-geometry').html(Search.CurrentPlaceSummary.Place.geometry.location.lng + ', ' + Search.CurrentPlaceSummary.Place.geometry.location.lat);

        // Images
        $('#place-details-images').empty();
        if (typeof Search.CurrentPlaceSummary.Place.photos != 'undefined') {
            for (var i in Search.CurrentPlaceSummary.Place.photos) {
                if (!isNaN(i)) {
                    var image = $('<div/>', { 'class': 'image-item' });
                    var thumb_url = 'https://maps.googleapis.com/maps/api/place/photo?photoreference=' + Search.CurrentPlaceSummary.Place.photos[i].photo_reference +
                            '&sensor=false&maxheight=200&maxwidth=200&key=' + Search.ApiKey;
                    var url = 'https://maps.googleapis.com/maps/api/place/photo?photoreference=' + Search.CurrentPlaceSummary.Place.photos[i].photo_reference +
                            '&sensor=false&maxheight=1000&maxwidth=1000&key=' + Search.ApiKey;

                    image.append('<div class="img"><a href="' + url + '" rel="prettyPhoto[' + Search.CurrentPlaceSummary.PlaceIDToSummarize + ']" title="' + Search.CurrentPlaceSummary.Place.name + '">' +
                        '<span class="icon glyphicon glyphicon-zoom-in" aria-hidden="true"></span>' +
                        '<img src="' + thumb_url + '" alt="' + Search.CurrentPlaceSummary.Place.name + '" title="' + Search.CurrentPlaceSummary.Place.name + '" /></a></div>');
                    image.append('<label>' + Search.CurrentPlaceSummary.Place.name + '</label>');
                    $('#place-details-images').append(image);
                }
            }
        }
        if (typeof Search.CurrentPlaceSummary.Images != 'undefined') {
            for (var i in Search.CurrentPlaceSummary.Images) {
                if (!isNaN(i)) {
                    var image = $('<div/>', { 'class': 'image-item' });
                    var title = Search.CurrentPlaceSummary.Images[i].title;
                    image.append('<div class="img"><a href="' + Search.CurrentPlaceSummary.Images[i].url + '" rel="prettyPhoto[' + Search.CurrentPlaceSummary.PlaceIDToSummarize + ']" title="' + title + '">' +
                        '<span class="icon glyphicon glyphicon-zoom-in" aria-hidden="true"></span>' +
                        '<img src="' + Search.CurrentPlaceSummary.Images[i].thumb_url + '" alt="' + title + '" title="' + title + '" /></a></div>');
                    image.append('<label>' + title + '</label>');
                    $('#place-details-images').append(image);
                }
            }
        }
        if ($('#place-details-images').children().length == 0) {
            $('#place-details-images').append('<div>Not found</div>');
        }
        else {
            $("#place-details-images a[rel^='prettyPhoto']").prettyPhoto();
        }



        // Reviews
        $('#place-details-reviews').empty();
        if (typeof Search.CurrentPlaceSummary.Place.reviews != 'undefined') {
            for (var i in Search.CurrentPlaceSummary.Place.reviews) {
                if (!isNaN(i)) {
                    var review = $('<div/>', { 'class': 'review-item' });
                    review.append('<div class="author">' + Search.CurrentPlaceSummary.Place.reviews[i].author_name + '</div>');
                    review.append('<div class="date"> - ' + (new Date(Search.CurrentPlaceSummary.Place.reviews[i].time * 1000)).Format('dd/MM/yyyy') + '</div>');
                    review.append('<div class="rating-stars"><div class="images"></div></div>');
                    review.append('<div class="content">' + Search.CurrentPlaceSummary.Place.reviews[i].text + '</div>');

                    $('#place-details-reviews').append(review);
                }
            }
        }
        if ($('#place-details-reviews').children().length == 0) {
            $('#place-details-reviews').append('<div>Not found</div>');
        }

        // Videos
        $('#place-details-videos').empty();
        if (typeof Search.CurrentPlaceSummary.Videos != 'undefined') {
            for (var i in Search.CurrentPlaceSummary.Videos) {
                if (!isNaN(i)) {
                    var image = $('<div/>', { 'class': 'image-item' });
                    var title = Search.CurrentPlaceSummary.Videos[i].snippet.title;
                    var videosUrl = 'http://www.youtube.com/watch?v=' + Search.CurrentPlaceSummary.Videos[i].id.videoId;
                    var thumb_image_url = Search.CurrentPlaceSummary.Videos[i].snippet.thumbnails.Default.url;
                    image.append('<div class="img"><a href="' + videosUrl + '" rel="prettyPhoto[video-' + Search.CurrentPlaceSummary.PlaceIDToSummarize + ']" title="' + title + '">' +
                            '<span class="icon glyphicon glyphicon-play-circle" aria-hidden="true"></span>' +
                            '<img src="' + thumb_image_url + '" alt="' + title + '" title="' + title + '" /></a></div>');
                    image.append('<label>' + title + '</label>');
                    $('#place-details-videos').append(image);
                }
            }
        }
        if ($('#place-details-videos').children().length == 0) {
            $('#place-details-videos').append('<div>Not found</div>');
        }
        else {
            $("#place-details-videos a[rel^='prettyPhoto']").prettyPhoto();
        }
    }
}

$(document).ready(function (e) {
    $('#search-places-btn').click(function (e) {
        e.preventDefault();
        e.stopImmediatePropagation();
        Search.FindLocationsAction();
    });
    $('#search-place-info').click(function (e) {
        var selectedPlace = $('#places-results>ul>li.active');
        if (selectedPlace.length > 0) {
            var place_id = selectedPlace.attr('rel');
            Search.GetPlaceDetails(place_id);
        }
        else {
            alert('please select place');
        }
    });
    Search.SearchFromValidationRules = $("#search-from-rules").ValidationGroup();
    Search.PlaceItemTemplate = $('#place-template').html();
    Search.ApiKey = $('#api-key').val();
});
