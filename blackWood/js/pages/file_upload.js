"use strict";
$(document).ready(function () {
    $("#input-21").fileinput({
        theme: "fa",
        previewFileType: "image",
        browseClass: "btn btn-success",
        browseLabel: "Pick Image",
        removeClass: "btn btn-danger",
        removeLabel: "Delete"


    });
    $("#input-4").fileinput({showCaption: false,
        theme: "fa"});
    $("#input-22").fileinput({
        theme: "fa",
        previewFileType: "text",
        allowedFileExtensions: ["txt", "md", "ini", "text"],
        previewClass: "bg-warning"
    });
    $("#input-43").fileinput({
        theme: "fa",
        showPreview: false,
        allowedFileExtensions: ["zip", "rar", "gz", "tgz"],
        elErrorContainer: "#errorBlock"
    });
    $("#input-fa").fileinput({
        theme: "fa",
        uploadUrl: "/file-upload-batch/2"
    });
    //       ============================ jquery file upload========================================
    $('#fileupload').fileupload({
        dataType: 'json',
        maxNumberOfFiles: 4
    });
//    ====================================    End of jquery file upload==========================

//    ===============================    dropify===================================================
    $('.dropify').dropify();
    $("[data-max-file-size]").dropify({
        error: {
            'fileSize': 'The file size is too big ({{ value }}B max).'
        }
    });
//    ============================    End of dropify===============================================

});