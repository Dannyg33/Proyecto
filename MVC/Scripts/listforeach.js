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

function callWebMethod(urlAndMethod, parameter) {
    jQuery.ajax({
        type: "POST",
        url: urlAndMethod,
        data: "{" + parameter + "}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            location.reload(true);
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