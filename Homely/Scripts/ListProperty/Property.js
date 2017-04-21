$(function () {



    function getlocalityByCity(id) {
        if (id != "") {
            $.ajax({

                url: GetLocalityByCity + "/" + id,
                type: "",
                success: function (data) {

                }
            });
        }
    }
});

$('#PropertyState').on('change', function () {

    $.ajax({
        url: getCity + "/" + $("#PropertyState").val(),
        type: "GET",
        success: function (data) {
            if (data.length > 0) {

                $("#PropertyCity").find('option').remove();
                $("#PropertyCity").append('<option value="' + '' + '">' + 'Please select' + '</option>');
                $("#Locality").find('option').remove();
                $("#Locality").append('<option value="' + '' + '">' + 'Please select' + '</option>');
                for (i = 0; i < data.length; i++) {
                    $("#PropertyCity").append('<option value="' + data[i].id + '">' + data[i].name + '</option>');
                }
                //$("#PropertyCity").append('<option value="' + -1 + '">' + 'OTHER' + '</option>');
            }
            else {
                $("#PropertyCity").find('option').remove();
                $("#PropertyCity").append('<option value="' + -1 + '">' + 'OTHER' + '</option>');
            }
        }
    });
});

$('#PropertyCity').on('change', function () {
    $.ajax({
        url: getLocality + "/" + $("#PropertyCity").val(),
        type: "GET",
        success: function (data) {
            if (data.length > 0) {
                $("#Locality").find('option').remove();
                $("#Locality").append('<option value="' + '' + '">' + 'Please select' + '</option>');
                for (i = 0; i < data.length; i++) {
                    $("#Locality").append('<option value="' + data[i].id + '">' + data[i].name + '</option>');
                }
                //$("#Locality").append('<option value="' + -1 + '">' + 'OTHER' + '</option>');
            }
            else {
                $("#Locality").find('option').remove();
                $("#Locality").append('<option value="' + -1 + '">' + 'Other' + '</option>');
            }
        }
    });
})

$('#OwnerState').on('change', function () {
    $.ajax({
        url: getCity + "/" + $("#OwnerState").val(),
        type: "GET",
        success: function (data) {
            if (data.length > 0) {
                $("#OwnerCity").find('option').remove();
                $("#OwnerCity").append('<option value="' + '' + '">' + 'Please select' + '</option>');
                for (i = 0; i < data.length; i++) {
                    $("#OwnerCity").append('<option value="' + data[i].id + '">' + data[i].name + '</option>');
                }
            }
            else {
                $("#OwnerCity").find('option').remove();
                $("#OwnerCity").append('<option value="' + -1 + '">' + 'Other' + '</option>');
            }
        }
    });
});

$(function () {
    //get file size
    $('#fileUpl').on('change', function () {
        debugger;
        if (document.getElementById('fileUpl').value != "") {
            var AllFiles = document.getElementById('fileUpl');
            var TotalFileSize = parseInt("0");

            for (var i = 0; i < AllFiles.files.length; i++) {

                var FileExt = AllFiles.files[i].name.substr(AllFiles.files[i].name.lastIndexOf('.') + 1).toUpperCase();

                if (FileExt != "JPG" &
                    FileExt != "PNG" &
                    FileExt != "BMP" &
                    FileExt != "JPEG" &
                    FileExt != "GIF") {
                    alert("Please upload only jpg or png or bmp or jpeg or gif image(s).");
                    document.getElementById('fileUpl').focus();
                    $('#fileUpl').val('');
                    return false;
                    break;
                }

                var FileSize = AllFiles.files[i].size;

                //1MB for each file
                if (FileSize > 1048576) {
                    alert("Uploaded image(s) is too long. The max size allowed for each file is 1 MB.");
                    document.getElementById('fileUpl').focus();
                    $('#fileUpl').val('');
                    return false;
                    break;
                }

                TotalFileSize = parseInt(TotalFileSize) + AllFiles.files[i].size;
                if (parseInt(TotalFileSize) > 6291456) {
                    alert("Uploaded image(s) are too long. The total size allowed is 6 MB.");
                    document.getElementById('fileUpl').focus();
                    $('#fileUpl').val('');
                    return false;
                }
            }
        }
    });

    $('#ownerFile').on('change', function () {
        debugger;
        if (document.getElementById('ownerFile').value != "") {
            var MemberFile = document.getElementById('ownerFile');
            var MemberFileExt = MemberFile.files[0].name.substr(MemberFile.files[0].name.lastIndexOf('.') + 1).toUpperCase();

            if (MemberFileExt != "JPG" &
                MemberFileExt != "PNG" &
                MemberFileExt != "BMP" &
                MemberFileExt != "JPEG" &
                MemberFileExt != "GIF") {
                alert("Please upload only jpg or png or bmp or jpeg or gif image.");
                document.getElementById('ownerFile').focus();
                $('#ownerFile').val('');
                return false;
            }

            //1MB for file
            if (MemberFile.files[0].size > 1048576) {
                alert("Uploaded image is too long. The max size allowed 1 MB.");
                document.getElementById('ownerFile').focus();
                $('#ownerFile').val('');
                return false;
            }
        }
    });
});

$(function () {
    $('.within input[type="checkbox"]').click(function () {
        var s = $(this);
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
