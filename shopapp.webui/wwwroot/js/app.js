
// DELETE MODAL KODLARI !

// Product Delete
$(".productDeleteModalBtn").click(function() {
    var url = $(this).attr("data-target");
    console.log("Url: "+url);

    $(".productDeleteButon").attr("href",url.toString());
});

// Category Delete
$(".categoryDeleteModalBtn").click(function() {
    var url = $(this).attr("data-target");
    console.log("Url: "+url);

    $(".categoryDeleteButon").attr("href",url.toString());
});

// Slider Delete
$(".sliderDeleteModal").click(function() {
    var url = $(this).attr("data-target");
    console.log("Url: "+url);

    $(".sliderDeleteButon").attr("href",url.toString());
});

// User Delete
$(".userDeleteModalBtn").click(function() {
    var url = $(this).attr("data-target");
    console.log("Url: "+url);

    $(".userDeleteButon").attr("href",url.toString());
});

// Role Delete
$(".roleDeleteModalBtn").click(function() {
    var url = $(this).attr("data-target");
    console.log("Url: "+url);

    $(".roleDeleteButon").attr("href",url.toString());
});


// DESİGN KODLARI !

$(".designOne").click(function() {
    $("html").css("scroll-behavior", "auto");
    $("body,html").animate({ scrollTop: 0 }, 400);

    $(".designOne").removeClass("btn-primary");
    $(".designOne").addClass("btn-secondary");

    $(".designTwo").removeClass("btn-secondary");
    $(".designTwo").addClass("btn-danger");    

    $(".navbar").removeClass("bg-primary");
    $(".navbar").addClass("bg-danger");

    $(".btnAddCart").removeClass("btn-primary");
    $(".btnAddCart").addClass("btn-danger");

    $("#adminDropdownButton").removeClass("btn-primary");
    $("#adminDropdownButton").addClass("btn-danger");  
    
    $("#adminDropdownButton2").removeClass("btn-primary");
    $("#adminDropdownButton2").addClass("btn-danger");  

    $(".categoryBadge").removeClass("bg-primary");
    $(".categoryBadge").addClass("bg-danger");
    
    $(".backToTop").removeClass("text-primary");
    $(".backToTop").addClass("text-danger");

    $(".searchButton").removeClass("btn-primary");
    $(".searchButton").addClass("btn-danger");

    $(".accountButton").removeClass("btn-primary");
    $(".accountButton").addClass("btn-danger");

    $(".cartCountBadge").removeClass("bg-danger");
    $(".cartCountBadge").addClass("bg-primary");

    $(".adminPanelButton").removeClass("bg-primary");
    $(".adminPanelButton").addClass("bg-danger");
});


$(".designTwo").click(function() {
    $("html").css("scroll-behavior", "auto");
    $("body,html").animate({ scrollTop: 0 }, 400);
    
    $(".designTwo").removeClass("btn-danger");
    $(".designTwo").addClass("btn-secondary");

    $(".designOne").removeClass("btn-secondary");
    $(".designOne").addClass("btn-primary");

    $(".navbar").removeClass("bg-danger");
    $(".navbar").addClass("bg-primary");

    $(".btnAddCart").removeClass("btn-danger");
    $(".btnAddCart").addClass("btn-primary");

    $("#adminDropdownButton").removeClass("btn-danger");
    $("#adminDropdownButton").addClass("btn-primary");

    $("#adminDropdownButton2").removeClass("btn-danger");
    $("#adminDropdownButton2").addClass("btn-primary");

    $(".categoryBadge").removeClass("bg-danger");
    $(".categoryBadge").addClass("bg-primary");

    $(".backToTop").removeClass("text-danger");
    $(".backToTop").addClass("text-primary");

    $(".searchButton").removeClass("btn-danger");
    $(".searchButton").addClass("btn-primary");

    $(".accountButton").removeClass("btn-danger");
    $(".accountButton").addClass("btn-primary");

    $(".cartCountBadge").removeClass("bg-primary");
    $(".cartCountBadge").addClass("bg-danger");

    $(".adminPanelButton").removeClass("bg-danger");
    $(".adminPanelButton").addClass("bg-primary");
});

var myAlert = document.querySelector(".designAlert");
var bsAlert = new bootstrap.Alert(myAlert);

setTimeout(function() {
    bsAlert.close();
},10000);

// BACK TO TOP KODLARI !
$(window).scroll(function() {
    if(scrollY >= 150)
    {
        $(".backToTop").css("bottom","50px");
    } else {
        $(".backToTop").css("bottom","-50px");
    }
})

$(".backToTop").click(function() {
    $("html").css("scroll-behavior", "auto");
    $("body,html").animate({ scrollTop: 0 }, 400);
    // $(window).scrollTop(0);
});


/* RESPONSİVE JS LAYOUT ! */


function responsiveOne() {
    if($(window).width() <= 992) {
        // $(".searchForm input").addClass("form-control-sm");
        // $(".searchForm button").addClass("btn-sm");

        $(".searchButton").removeClass("btn-lg");
    
        $(".detailsProductName").addClass("text-center");
    }
}
function responsiveTwo()
{
    if($(window).width() <= 330) {
        $(".homeProductList").removeClass("col-6");
        $(".homeProductList").addClass("col-12");
    } else {
        $(".homeProductList").removeClass("col-12");
        $(".homeProductList").addClass("col-6");
    }
}

responsiveOne();
responsiveTwo();

$(window).resize(function() {
    responsiveOne();
    responsiveTwo();
});

function ResponsiveImages(imageName, imageUrl)
{
    if(window.innerWidth <= 767)
    {
        $("."+imageName.toString()).attr("src","/img/slider/m_"+imageUrl.toString()); // Mobile
    } else {
        $("."+imageName.toString()).attr("src","/img/slider/"+imageUrl.toString()); // Desktop
    }
}