function InitializeAddUpdateRecordDialog() {
    $('#addUpdateRecordDialog').dialog({
        autoOpen: false,
        resizable: false,
        width: 900,
        height: 500,
        modal: true
    });
}

function closeDialog() {
    $('#addUpdateRecordDialog').dialog('close');
}

function deleteItem(itemID) {
    var dialogTitle = 'Permanently Delete Item ' + itemID + '?';

    $("#errorConfirmationDialog").html('<p><span class="ui-icon ui-icon-alert" style="float:left; margin:0 7px 20px 0;"></span>Please click delete to confirm deletion.</p>');

    $("#errorConfirmationDialog").dialog({
        title: dialogTitle,
        modal: true,
        buttons: {
            "Delete": function () {
                callWebMethod(urlAndMethod + itemID, '');
                $(this).dialog("close");
            },
            "Cancel": function () { $(this).dialog("close"); }
        }
    });

    $('#errorConfirmationDialog').dialog('open');
    return false;
}

function showHideItem(itemID) {
    if (itemID == null) {
        // add
        $('#addUpdateRecordDialog').dialog('open');
        $("#addUpdateRecordDialog").dialog("option", "title", "Add New " + addEditTitle);
        $('#inputSubmit').attr('value', 'Add');

        if ($("#trPrimaryKey") != null)
            $("#trPrimaryKey").hide();
    }
    else {
        // update
        $('#addUpdateRecordDialog').dialog('open');
        $("#addUpdateRecordDialog").dialog("option", "title", "Edit " + addEditTitle + " " + itemID);
        $('#inputSubmit').attr('value', 'Update');

        if ($("#trPrimaryKey") != null)
            $("#trPrimaryKey").show();
    }
}

function resetValidationErrors() {
    $('.field-validation-error').removeClass('field-validation-error').addClass('field-validation-valid');
    $('.input-validation-error').removeClass('input-validation-error').addClass('input-validation-valid');
}

function callWebMethod(urlAndMethod, parameter) {
    jQuery.ajax({
        type: "POST",
        url: urlAndMethod,
        data: "{" + parameter + "}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            $("#list-grid").trigger("reloadGrid");
        },
        error: function (xhr, textStatus, errorThrown) {
            ShowError(xhr.responseText, '');
        }
    });
}

function ShowError(errorMessage, errorTitle) {
    $(document).ready(function () {
        $("#errorDialog").html(errorMessage);
        $("#errorDialog").dialog({
            modal: true,
            title: errorTitle,
            buttons: {
                Ok: function () {
                    $(this).dialog("close");
                }
            }
        });
    });
}