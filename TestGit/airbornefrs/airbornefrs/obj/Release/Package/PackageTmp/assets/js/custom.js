$(function () {
    //caches a jQuery object containing the header element

    $(window).on("scroll load", function () {
        var header = $("#header");
        var scroll = $(window).scrollTop();

        if (scroll >= 70) {
            header.addClass("darkHeader");
        } else {
            header.removeClass("darkHeader");
        }
    });
});
jQuery(document).ready(function ($) {
    //open popup
    $('.cd-popup-trigger').on('click', function (event) {
        event.preventDefault();
        $('.cd-popup').addClass('is-visible');
    });

    //close popup
    $('.cd-popup').on('click', function (event) {
        if ($(event.target).is('.cd-popup-close') || $(event.target).is('.cd-popup')) {
            event.preventDefault();
            $(this).removeClass('is-visible');
        }
    });
    //close popup when clicking the esc keyboard button
    $(document).keyup(function (event) {
        if (event.which == '27') {
            $('.cd-popup').removeClass('is-visible');
        }
    });
});
//$(function() {
//	    //caches a jQuery object containing the header element
//	    var header = $("#header");
//	    $(window).scroll(function() {
//	        var scroll = $(window).scrollTop();

//	        if (scroll >= 70) {
//	            header.addClass("darkHeader");
//	        } else {
//	            header.removeClass("darkHeader");
//	        }
//	    });
//	});
//	jQuery(document).ready(function($){
//		//open popup
//		$('.cd-popup-trigger').on('click', function(event){
//			event.preventDefault();
//			$('.cd-popup').addClass('is-visible');
//		});
		
//		//close popup
//		$('.cd-popup').on('click', function(event){
//			if( $(event.target).is('.cd-popup-close') || $(event.target).is('.cd-popup') ) {
//				event.preventDefault();
//				$(this).removeClass('is-visible');
//			}
//		});
//		//close popup when clicking the esc keyboard button
//		$(document).keyup(function(event){
//	    	if(event.which=='27'){
//	    		$('.cd-popup').removeClass('is-visible');
//		    }
//	    });
//	});
// for popup
/*function windowSize() {
    windowHeight = window.innerHeight || window.innerHeight;
    $(window).height();
    if ($(window).height() > 780) {
	    $(".pop-up-content").attr("style","max-height:none");
	  }
}
windowSize();
$( window ).resize(function() {
    $(window).height();
    if ($(window).height() > 780) {
	    $(".pop-up-content").attr("style","max-height:none");
	}
});*/

jQuery(document).ready(function(e) {
	$('#agreement').click(function(){
    
	  if($(this).prop("checked")){
	    $('#submit_btn').removeAttr("disabled").removeClass("disable");
	  }  else{
		      $('#submit_btn').attr("disabled","disabled").addClass("disable");
		  }                        
	});

});
