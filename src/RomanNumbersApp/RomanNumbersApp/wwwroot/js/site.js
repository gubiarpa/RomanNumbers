// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function () {
    let txtRoman = $('#txtRoman'), txtInteger = $('#txtInteger');
    txtRoman.keyup(romanToInteger);
});

const romanToInteger = () => {

    let url = `${$(location).attr('href')}Home/RomanToInteger`;
    let txtRoman = $('#txtRoman'), txtInteger = $('#txtInteger');
    let data = { number: txtRoman.val() };

    $.ajax({
        type: "POST",
        url: url,
        data: JSON.stringify(data),
        contentType: "application/json",
        dataType: "json",
        success(result) {
            if (result != txtInteger.val()) txtInteger.val(result);
        },
        error(xhr, ajaxOptions, thrownError) {
            console.log(xhr.status);
            console.log(thrownError);
        },
    });
};