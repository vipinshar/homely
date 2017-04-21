
$(function () {
    $('.pagination-container ul').addClass('pagination pagination_home');
})

function cycleImages() {
    var $active = $('#cycler .active');
    var $next = ($active.next().length > 0) ? $active.next() : $('#cycler img:first');
    $next.css('z-index', 2); //move the next image up the pile
    $active.fadeOut(1500, function () { //fade out the top image
        $active.css('z-index', 1).show().removeClass('active'); //reset the z-index and unhide the image
        $next.css('z-index', 3).addClass('active'); //make the next image the top one
    });
}

$(function () {
    setInterval('cycleImages()', 7000);
})

$(function () {
    var dd = $('.vticker').easyTicker({
        direction: 'up',
        // easing: 'easeOutCubic',
        easing: 'swing',
        speed: 'slow',
        interval: 5000,
        height: 'auto',
        visible: 2,
        mousePause: 0,
        controls: {
            up: '.up',
            down: '.down',
            toggle: '.toggle',
            stopText: 'Stop !!!'
        }
    }).data('easyTicker');

    cc = 1;
    $('.aa').click(function () {
        $('.vticker ul').append('<li>' + cc + ' Triangles can be made easily using CSS also without any images. This trick requires only div tags and some</li>');
        cc++;
    });

    $('.vis').click(function () {
        dd.options['visible'] = 3;

    });

    $('.visall').click(function () {
        dd.stop();
        dd.options['visible'] = 0;
        dd.start();
    });
})

$(function () {
    var offset = $("#social").offset();
    var topPadding = 350;
    $(window).scroll(function () {
        if ($(window).scrollTop() > offset.top) {
            $("#social").stop().animate({
                marginTop: $(window).scrollTop() - offset.top + topPadding
            });
        } else {
            $("#social").stop().animate({
                marginTop: 50
            });
        };
    });
});


$(function () {
    $('#imgQkList').on('click', function () {
        $('#tblQkList').toggle(20);
    });

    //hide it when clicking anywhere else except the popup and the trigger
    $(document).on('click', function (event) {
        if (!$(event.target).parents().addBack().is('#tblQkList')) {
            $('#tblQkList').hide();
        }
    });

    // Stop propagation to prevent hiding "#tooltip" when clicking on it
    $('#imgQkList').on('click', function (event) {
        event.stopPropagation();
    });
});

$(document).ready(function () {
    $('#imgQkContact').click(function () {
        $('#tblQkContact').toggle(20);
        $('#btnSubmit').click(function () {
            $('#tblQkContact').hide();
        });
    });
});

$(function () {
    $('#datetimepicker').datetimepicker(
        {
            minDate: new Date()
        }
        );
});



$(function () {

    $(document).ready(function () {
        speed = 250
        trans = 'ease'
        delayVal = 0
        $(".img01,#divDelhiImage").hover(function () {
            $(this).attr("src", imgUrl + "images/img01-hover.jpg");
            $("#DelhiPopup").show();

        }, function () {
            $(this).attr("src", imgUrl + "images/img01.jpg");
            $("#DelhiPopup").hide();
        });

        $(".img02").hover(function () {
            $(this).attr("src", imgUrl + "images/img02-hover.jpg");
            $("#GGNPopup").show();
        }, function () {
            $(this).attr("src", imgUrl + "images/img02.jpg");
            $("#GGNPopup").hide();

        });


        $(".img03").hover(function () {
            $(this).attr("src", imgUrl + "images/img03-hover.jpg");
            $("#GZBPopup").show();

        }, function () {
            $(this).attr("src", imgUrl + "images/img03.jpg");
            $("#GZBPopup").hide();

        });

        $(".img04").hover(function () {
            $(this).attr("src", imgUrl + "images/img04-hover.jpg");
            $("#NoidaPopup").show();


        }, function () {
            $(this).attr("src", imgUrl + "images/img04.jpg");
            $("#NoidaPopup").hide();

        });

        $(".img05").hover(function () {
            $(this).attr("src", imgUrl + "images/img05-hover.jpg");
            $("#MumbaiPopup").show();

        }, function () {
            $(this).attr("src", imgUrl + "images/img05.jpg");
            $("#MumbaiPopup").hide();

        });

        $(".img06").hover(function () {
            $(this).attr("src", imgUrl + "images/img06-hover.jpg");



        }, function () {
            $(this).attr("src", imgUrl + "images/img06.jpg");


        });

        $(".img07").hover(function () {
            $(this).attr("src", imgUrl + "images/img07-hover.jpg");



        }, function () {
            $(this).attr("src", imgUrl + "images/img07.jpg");


        });

        $(".img08").hover(function () {
            $(this).attr("src", imgUrl + "images/img04-hover.jpg");


        }, function () {
            $(this).attr("src", imgUrl + "images/img04.jpg");


        });

        $(".img09").hover(function () {
            $(this).attr("src", imgUrl + "images/img09-hover.jpg");


        }, function () {
            $(this).attr("src", imgUrl + "images/img09.jpg");


        });

        $(".img10").hover(function () {
            $(this).attr("src", imgUrl + "images/img10-hover.jpg");


        }, function () {
            $(this).attr("src", imgUrl + "images/img10.jpg");

        });

        $(".img11").hover(function () {
            $(this).attr("src", imgUrl + "images/img11-hover.jpg");


        }, function () {
            $(this).attr("src", imgUrl + "images/img11.jpg");


        });
        $(".img12").hover(function () {
            $(this).attr("src", imgUrl + "images/img12-hover.jpg");


        }, function () {
            $(this).attr("src", imgUrl + "images/img12.jpg");


        });
        $(".img13").hover(function () {
            $(this).attr("src", imgUrl + "images/img13-hover.jpg");


        }, function () {
            $(this).attr("src", imgUrl + "images/img13.jpg");


        });

        $(".img09").hover(function () {
            $(this).attr("src", imgUrl + "images/img09-hover.jpg");


        }, function () {
            $(this).attr("src", imgUrl + "images/img09.jpg");


        });

    });
});


$(function () {
    $('body').on('click', '#propertyListing .pagination a', function (evt) {
        debugger;
        var currentSelectedCity = $('#CurrentHdnCity').val();
        var s = this.href + "&city=" + currentSelectedCity + "";

        evt.preventDefault();
        asyncRequest(this, {
            url: s,
            type: "GET",
            data: []
        });
        $('.pagination-container ul').addClass('pagination pagination_home');
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
          $("#City").find('option').remove();
                    $("#Locality").find('option').remove();
                    $("#City").append('<option value="' + '' + '">' + 'Select city' + '</option>');
                    $("#Locality").append('<option value="' + '' + '">' + 'Select locality' + '</option>');
        $.ajax({
            url: getCity + "/" + $("#State").val(),
            type: "GET",
            success: function (data) {
                if (data.length > 0) {

                  
                    for (i = 0; i < data.length; i++) {
                        $("#City").append('<option value="' + data[i].id + '">' + data[i].name + '</option>');
                    }

                }
                else {
                    $("#City").find('option').remove();
                    $("#City").append('<option value="' + -1 + '">' + 'OTHER' + '</option>');
                    $("#Locality").find('option').remove();
                    $("#Locality").append('<option value="' + '' + '">' + 'Select locality' + '</option>');
                }
            }
        });
    });

    $('#City').on('change', function () {
     $("#Locality").find('option').remove();
                    $("#Locality").append('<option value="' + '' + '">' + 'Select locality' + '</option>');
        $.ajax({
            url: getLocality + "/" + $("#City").val(),
            type: "GET",
            success: function (data) {
                if (data.length > 0) {
                   
                    for (i = 0; i < data.length; i++) {
                        $("#Locality").append('<option value="' + data[i].id + '">' + data[i].name + '</option>');
                    }
                }
                else {
                    $("#Locality").find('option').remove();
                    $("#Locality").append('<option value="' + -1 + '">' + 'OTHER' + '</option>');
                }
            }
        });
    })
});

function validateEmail(sEmail) {
    var filter = /^[\w\-\.\+]+\@[a-zA-Z0-9\.\-]+\.[a-zA-z0-9]{2,4}$/;
    if (filter.test(sEmail)) {
        return true;
    }
    else {
        return false;
    }
}

$(function () {
    $('#quickSubmit').click(function () {

        if ($('#quickName').val() == "") {
            $('#quickName').focus();
            return false;
        }
        if ($('#quickEmail').val() == "") {
            $('#quickEmail').focus();
            return false;
        }
        if (!validateEmail($('#quickEmail').val())) {
            $('#quickEmail').focus();
            return false;
        }

        if ($('#quickMobile').val() == "") {
            $('#quickMobile').focus();
            return false;
        }


        var obj = new Object();
        obj.email = $('#quickEmail').val();
        obj.For = $("input[name='quickFor']:checked").val();
        obj.Name = $('#quickName').val();
        obj.Mobile = $('#quickMobile').val();

        $.ajax({
            url: quickUrl,
            type: "POST",
            data: obj,
            success: function (result) {
                $('#tblQkList').hide();
            }

        })

    });

    $('input[name="rent"]:radio').change(
               function () {
                   debugger;
                   if (this.checked && this.value == '1') {
                       $("#searchFast").prop('value', 'PROCEED');
                       $('#hdnForChk').val(1);
                       $('#formHome').attr("action", "/Property/Index");
                   }
                   else {
                       $("#searchFast").prop('value', 'SEARCH');
                       $('#hdnForChk').val(2);
                       $('#formHome').attr("action", "/Property/SearchPropFromHomePage");
                   }
               });
});

$(function () {

    $("#divDelhiImage").mouseover(function () {
        $("#DelhiPopup");
        $("#DelhiPopup").show();
    });
    $("#divDelhiImage").mouseout(function () {
        $("#DelhiPopup").hide();
    });

    $("#divGGNImage").mouseover(function () {
        $("#GGNPopup");
        $("#GGNPopup").show();
    });
    $("#divGGNImage").mouseout(function () {
        $("#GGNPopup").hide();
    });

    $("#divGreatreNoidaImage").mouseover(function () {
        $("#GreateNoidaPopup");
        $("#GreateNoidaPopup").show();
    });
    $("#divGreatreNoidaImage").mouseout(function () {
        $("#GreateNoidaPopup").hide();
    });

    $("#divSonipatImage").mouseover(function () {
        $("#KundloTDI");
        $("#KundloTDI").show();
    });
    $("#divSonipatImage").mouseout(function () {
        $("#KundloTDI").hide();
    });

    $("#divFaridabadImage").mouseover(function () {
        $("#FaridabadPopup");
        $("#FaridabadPopup").show();
    });
    $("#divFaridabadImage").mouseout(function () {
        $("#FaridabadPopup").hide();
    });

    $("#divGZBImage").mouseover(function () {
        $("#GhaziabadPopup");
        $("#GhaziabadPopup").show();
    });
    $("#divGZBImage").mouseout(function () {
        $("#NoidaPopup").hide();
        $("#GhaziabadPopup").hide();
    });

    $("#divNoidaImage").mouseover(function () {
        $("#NoidaPopup");
        $("#NoidaPopup").show();
    });
    $("#divNoidaImage").mouseout(function () {
        $("#NoidaPopup").hide();
    });

})

$(document).ready(function () {
    $('input[type=radio][name=ownerShipType]').change(function () {
        debugger;
        switch (this.value) {
            case "1":
                $('#div_reg_tenet_perm_address').hide();
                $('#div_reg_company_name').hide();
                $('#div_reg_company_address').hide();
                $('#div_reg_tenet_perm_address').hide();
                $('#div_reg_tenet_comp_add').hide();
                break;
            case "2":
                $('#div_reg_company_name').show();
                $('#div_reg_company_address').show();
                $('#div_reg_tenet_perm_address').hide();
                $('#div_reg_tenet_comp_add').hide();
                break;
            case "3":
                $('#div_reg_company_name').hide();
                $('#div_reg_company_address').hide();
                $('#div_reg_tenet_perm_address').show();
                $('#div_reg_tenet_comp_add').show();
                break;
            case "4":
                break;
        }
    });


    var options = {

        // other available options: 
        url: "/Account/Registers",
        type: "POST"

    };
    //$('#register_btn').click(function () {
    //    debugger;

    //    $('#signUp').ajaxForm(options);

    //});



    //$('#register_btn').click(function () {
    //    var obj = new Object();
    //    obj.userName = $('#reg_name').val();
    //    obj.emailId = $('#reg_email').val();
    //    obj.mobile = $('#reg_mobile').val();
    //    obj.password = $('#reg_password').val();
    //    obj.stateId = $('#reg_state').val();
    //    obj.cityId = $('#reg_City').val();
    //    obj.companyName = $('#div_reg_company_name').val();
    //    obj.companyAddress = $('#div_reg_company_address').val();
    //    obj.tenantAddress = $('#div_reg_tenet_perm_address').val();
    //    obj.tenantCompany = $('#div_reg_tenet_comp_add').val();
    //    debugger;
    //    var formData = new FormData();
    //    var file = document.getElementById('reg_file').files[0];
    //    formData.image = file;
    //    formData.data = obj;

    //    $.ajax({
    //        url: "/Account/Registers",
    //        type: "POST",
    //        data: formData,
    //        processData: false,
    //        contentType: false,
    //        success: function (res) {
    //            alert('h');
    //        }
    //    });
    //});
});



