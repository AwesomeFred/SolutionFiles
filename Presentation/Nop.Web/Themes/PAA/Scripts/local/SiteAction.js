
// image pre-load
if (FRONTPAGEARRAY !== null) {
    $(FRONTPAGEARRAY).each(function () { (new Image()).src = FRONTPAGEIMAGEPATH + this; });
}

// fired when page is completely loaded
$(function() {


    // start slide show
    slideShow();


});



// slide show driver
var curIndex = 0;
var imgDuration = 6000;


// home page
function slideShow() {

    var path = FRONTPAGEIMAGEPATH;
    var array = FRONTPAGEARRAY;


    $("#slider").addClass("fadeOut");

    setTimeout(function () {
        $("#slider").attr("src", path + array[curIndex]).removeClass("fadeOut");
    }, 1000);

    curIndex++;

    if (curIndex === array.length) { curIndex = 0; }
    setTimeout(slideShow, imgDuration);
}
