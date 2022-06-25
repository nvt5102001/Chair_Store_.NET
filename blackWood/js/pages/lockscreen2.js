"use strict";
$(window).on("load",function() {
    $('.preloader img').fadeOut();
    $('.preloader').fadeOut(1000);
});
$(document).ready(function () {

    $(".unlock").on("click",function () {
        $(".lock_show").show();
        $(".unlock").hide();
    });
    $("#lockscreen_validator").bootstrapValidator({
        fields: {
            password: {
                validators: {
                    notEmpty: {
                        message: 'Please provide a password'
                    }
                }
            }
        }
    });
});