$('#register_btn').click(function () {
    login_btn

    var obj = new Object();
    obj.Email = $('#reg_email').val();
    obj.Mobile = $('#reg_mobile').val();
    obj.Password = $('#reg_password').val();

    $.ajax({
        url: "",
        type: "POST",
        data: obj,
        success: function (result) {

        }
    });

});

$('#login_btn').click(function () {
    var obj = new Object();
    obj.Email = $('#reg_email').val();
    obj.Password = $('#reg_password').val();

    $.ajax({
        url: "",
        type: "POST",
        data: obj,
        success: function (result) {

        }
    });

});

$('#forget_btn').click(function () {
    var obj = new Object();
    obj.Email = $('#reg_email').val();

    $.ajax({
        url: "",
        type: "POST",
        data: obj,
        success: function (result) {

        }
    });

});

$(function () {
    $('#reg_state').on('change', function () {
        $.ajax({
            url: getCity + "/" + $("#reg_state").val(),
            type: "GET",
            success: function (data) {
                if (data.length > 0) {
                    $("#reg_City").find('option').remove();
                    $("#reg_loca").find('option').remove();
                    $("#reg_loca").append('<option value="' + '' + '">' + 'Please select' + '</option>');
                    $("#reg_City").append('<option value="' + '' + '">' + 'Please select' + '</option>');
                    for (i = 0; i < data.length; i++) {
                        $("#reg_City").append('<option value="' + data[i].id + '">' + data[i].name + '</option>');
                    }
                }
                else {
                    $("#reg_City").find('option').remove();
                    $("#reg_City").append('<option value="' + -1 + '">' + 'Other' + '</option>');
                    $("#reg_loca").find('option').remove();
                    $("#reg_loca").append('<option value="' + -1 + '">' + 'Other' + '</option>');
                }
            }
        });
    });

    $('#reg_City').on('change', function () {
        $.ajax({
            url: getLocality + "/" + $("#reg_City").val(),
            type: "GET",
            success: function (data) {
                if (data.length > 0) {
                    $("#reg_loca").find('option').remove();
                    $("#reg_loca").append('<option value="' + '' + '">' + 'Please select' + '</option>');
                    for (i = 0; i < data.length; i++) {
                        $("#reg_loca").append('<option value="' + data[i].id + '">' + data[i].name + '</option>');
                    }
                }
                else {
                    $("#reg_loca").find('option').remove();
                    $("#reg_loca").append('<option value="' + -1 + '">' + 'Other' + '</option>');
                }
            }
        });
    })
});


$(function () {
    $("#slider-range").slider({
        range: true,
        min: 5000,
        max: 100000,
        values: [5000, 30000],
        slide: function (event, ui) {

            $('#minValue').val(ui.values[0]);
            $('#maxValue').val(ui.values[1]);
            $("#amount").text(ui.values[0] + " - " + ui.values[1]);
        }
    });
    $("#amount").text($("#slider-range").slider("values", 0) +
      "-" + $("#slider-range").slider("values", 1));
});


