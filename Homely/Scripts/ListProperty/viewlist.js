$(function () {
    $('#formSlider').hide();
    $('.pagination-container ul').addClass('pagination pagination_home');
})
$(function () {
    $('body').on('click', '#viewListPaging .pagination a', function (evt) {
        debugger;
        var s;
        var currentSelectedCity = $('#hdnTabCity').val();
        if (currentSelectedCity == 0) {
            s = this.href + "&For=RENT" + "&city=" + currentSelectedCity + "";
        }
        else {
            s = this.href + "&For=CITY" + "&city=" + currentSelectedCity + "";
        }


        evt.preventDefault();
        asyncRequest(this, {
            url: s,
            type: "GET",
            data: []
        });
    });

    function asyncRequest(element, options) {
        var confirm, loading, method, duration;

        confirm = element.getAttribute("data-ajax-confirm");
        if (confirm && !window.confirm(confirm)) {
            return;
        }

        loading = $(element.getAttribute("data-ajax-loading"));
        duration = element.getAttribute("data-ajax-loading-duration") || 0;

        $.extend(options, {
            type: element.getAttribute("data-ajax-method") || undefined,
            url: element.getAttribute("data-ajax-url") || undefined,
            beforeSend: function (xhr) {
                var result;
                asyncOnBeforeSend(xhr, method);
                result = getFunction(element.getAttribute("data-ajax-begin"), ["xhr"]).apply(this, arguments);
                if (result !== false) {
                    loading.show(duration);
                }
                return result;
            },
            complete: function () {
                loading.hide(duration);
                getFunction(element.getAttribute("data-ajax-complete"), ["xhr", "status"]).apply(this, arguments);
            },
            success: function (data, status, xhr) {
                asyncOnSuccess(element, data, xhr.getResponseHeader("Content-Type") || "text/html");
                getFunction(element.getAttribute("data-ajax-success"), ["data", "status", "xhr"]).apply(this, arguments);
                $(window).scrollTop(506);
                //$("html, body").animate({
                //    scrollTop: 0
                //}, "slow");
            },
            error: getFunction(element.getAttribute("data-ajax-failure"), ["xhr", "status", "error"])
        });

        options.data.push({ name: "X-Requested-With", value: "XMLHttpRequest" });

        method = options.type.toUpperCase();
        if (!isMethodProxySafe(method)) {
            options.type = "POST";
            options.data.push({ name: "X-HTTP-Method-Override", value: method });
        }

        $.ajax(options);
    }

    function getFunction(code, argNames) {
        var fn = window, parts = (code || "").split(".");
        while (fn && parts.length) {
            fn = fn[parts.shift()];
        }
        if (typeof (fn) === "function") {
            return fn;
        }
        argNames.push(code);
        return Function.constructor.apply(null, argNames);
    }

    function isMethodProxySafe(method) {
        return method === "GET" || method === "POST";
    }

    function asyncOnBeforeSend(xhr, method) {
        if (!isMethodProxySafe(method)) {
            xhr.setRequestHeader("X-HTTP-Method-Override", method);
        }
    }
    function asyncOnSuccess(element, data, contentType) {
        var mode;

        if (contentType.indexOf("application/x-javascript") !== -1) {  // jQuery already executes JavaScript for us
            return;
        }

        mode = (element.getAttribute("data-ajax-mode") || "").toUpperCase();
        $(element.getAttribute("data-ajax-update")).each(function (i, update) {
            var top;

            switch (mode) {
                case "BEFORE":
                    top = update.firstChild;
                    $("<div />").html(data).contents().each(function () {
                        update.insertBefore(this, top);
                    });
                    break;
                case "AFTER":
                    $("<div />").html(data).contents().each(function () {
                        update.appendChild(this);
                    });
                    break;
                default:
                    $(update).html(data);
                    break;
            }
        });
    }



    $('#homePageCities li').click(function (e) {
        debugger;
        var s = $(this).attr('id');
        $('#CurrentHdnCity').val(s);
        debugger;
        e.preventDefault();
        $.ajax({
            url: featuredUrl + '/' + s,
            type: "GET",
            data: [],
            success: function (data) {
                $('#featuredListingPartial').html('');
                $('#featuredListingPartial').html(data);
            }

        });

    });
})


$(function () {
    $('#State').on('change', function () {

        $.ajax({
            url: getCity + "/" + $("#State").val(),
            type: "GET",
            success: function (data) {
                if (data.length > 0) {

                    $("#City").find('option').remove();
                    $("#Locality").find('option').remove();
                    $("#City").append('<option value="' + '' + '">' + 'Please select' + '</option>');
                    $("#Locality").append('<option value="' + '' + '">' + 'Please select' + '</option>');
                    for (i = 0; i < data.length; i++) {
                        $("#City").append('<option value="' + data[i].id + '">' + data[i].name + '</option>');
                    }

                }
                else {
                    $("#City").find('option').remove();
                    $("#City").append('<option value="' + -1 + '">' + 'Other' + '</option>');
                    $("#Locality").find('option').remove();
                    $("#Locality").append('<option value="' + '' + '">' + 'Please select' + '</option>');
                }
            }
        });
    });

    $('#City').on('change', function () {
        $.ajax({
            url: getLocality + "/" + $("#City").val(),
            type: "GET",
            success: function (data) {
                if (data.length > 0) {
                    $("#Locality").find('option').remove();
                    $("#Locality").append('<option value="' + '' + '">' + 'Please select' + '</option>');
                    for (i = 0; i < data.length; i++) {
                        $("#Locality").append('<option value="' + data[i].id + '">' + data[i].name + '</option>');
                    }
                }
                else {
                    $("#Locality").find('option').remove();
                    $("#Locality").append('<option value="' + -1 + '">' + 'Other' + '</option>');
                }
            }
        });
    })
});


$(function () {
    $('.amenities-detail input[type="checkbox"]').click(function () {
        var s = $(this);
        debugger;
        var current = parseInt(s.attr('id').split('_')[1]);
        if ($(this).prop('checked') == true) {
            $('#small_' + current).hide();
            $('#big_' + current).show();
        }
        else {
            $('#small_' + current).show();
            $('#big_' + current).hide();
        }
    });

});

$(function () {
    $('#searchFast').click(function () {
        debugger;
        var sendObj = new Object();
        sendObj.City = 0;
        sendObj.State = 0;
        sendObj.Locality = 0;
        sendObj.bedroom = 0;
        sendObj.budget = "";
        sendObj.propertytype = 0;
        sendObj.PropertyCode = $('#PropertyCode').val();
        sendObj.minValue = 0;
        sendObj.maxValue = 0;
        sendObj.avail = 0;
        sendObj.furnish = 0;
        sendObj.minimumContract = "";

        var final = "";
        var count = 0;
        $('.chk:checked').each(function () {
            debugger;
            var values = $(this).val();
            if (count > 0) {
                final += "," + values;
            }
            else {
                final += values;
            }
            count = count + 1;
        });

        if (sendObj.PropertyCode == "") {
            sendObj.City = $('#City').val();
            sendObj.State = $('#State').val();
            sendObj.Locality = $('#Locality').val();
            sendObj.bedroom = $('#bedroom').val();
            sendObj.budget = $('#Budget').val();
            sendObj.propertytype = $('#propertytype').val();
            sendObj.PropertyCode = $('#PropertyCode').val();
            sendObj.minValue = $('#minValue').val();
            sendObj.maxValue = $('#maxValue').val();
            sendObj.avail = $('#avail').val();
            sendObj.Furnished = $('#Furnished').val();
            sendObj.minimumContract = $('#minimumContract').val();
        }
        else {
            sendObj.PropertyCode = $('#PropertyCode').val();
        }

        if ($('#City').val() != "") {
            $.ajax({
                url: searchProp,
                type: "POST",
                data: sendObj,
                success: function (result) {
                    debugger;
                    $('#viewListPaging').html('');
                    $('#viewListPaging').html(result);
                }
            });
        }
    });
})

$(function () {

    $(document).on('click', '#comapre', function () {
        debugger;
        var arr = [];
        var str = "";
        $("input[name='chk']:checked").each(function () {
            arr.push($(this).val());
            str += $(this).val() + ",";

        });
        if (arr.length > 1 && arr.length < 4) {
            window.location = compareProp + "/Index/" + str.trimRight(',');
        }
        else {
            alert('Please select minimum 2 and maximum 3 property.');
            return false;
        }
    });

    function validatefunc() {
        var arr = [];
        var str="";
        $("input[name='chk']:checked").each(function () {
            arr.push($(this).val());
            str+=$(this).val()+",";
            
        });
        if (arr.length > 1 && arr.length < 4) {
            window.location = compareProp + "/" + str.trimRight(',');
        }
        else {
            alert('Please select minimum 1 and maximum 3 property.');
            return false;
        }
    }});