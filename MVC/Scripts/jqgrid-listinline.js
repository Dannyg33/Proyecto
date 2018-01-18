var isAddingRow = true;
var tempId = '-100abc';
var newRowSuffix = 'jqg';

function addRow() {
    if (isAddingRow) {
        $('#list-grid').addRow();
        $('#addLink1').addClass('disablelink');
        $('#addLink2').addClass('disablelink');
        toggleLinks(tempId);
        isAddingRow = false;
    }
}

function editRow(currentId) {
    $('#list-grid').editRow(currentId);
    toggleLinks(currentId);
}

function cancelRow(currentId) {
    $('#list-grid').restoreRow(currentId);
    toggleLinks(currentId);

    // cancel saving record
    if (currentId.indexOf(newRowSuffix) != -1) {
        $('#addLink1').removeClass('disablelink');
        $('#addLink2').removeClass('disablelink');
        isAddingRow = true;
    }
}

function saveRow(currentId, controller) {
    var isValid = isDataValid(currentId);

    if (!isValid) {
        ShowError(errorMessage, 'An error occured when saving!');
        errorMessage = '';
        return false;
    }

    var serializedData = getSerializedData(currentId);

    // save record
    if (currentId.indexOf(newRowSuffix) != -1) {
        callWebMethod('/' + controller + '/ListInlineAdd/', serializedData);

        $('#addLink1').removeClass('disablelink');
        $('#addLink2').removeClass('disablelink');
        isAddingRow = true;
    }
    else {
        // update               
        callWebMethod('/' + controller + '/ListInlineUpdate/', serializedData);
    }

    $('#list-grid').saveRow(currentId);
    toggleLinks(currentId);
}

function toggleLinks(currentId) {
    var ids = jQuery('#list-grid').jqGrid('getDataIDs');

    if (currentId == tempId)
        currentId = ids[0];

    $('#editLink' + currentId).toggle();
    $('#cancelLink' + currentId).toggle();
    $('#saveLink' + currentId).toggle();

    for (var i = 0; i < ids.length; i++) {
        if (ids[i] != currentId)
            $('#editLink' + ids[i]).toggle();

        $('#deleteLink' + ids[i]).toggle();
    }
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

function isNumeric(value) {
    var numericPattern = /^\d+$/;
    var matchArray = value.toString().match(numericPattern);

    if (matchArray != null)
        return true;

    return false;
}

function isDecimal(value) {
    var numericPattern = /^[0-9]+(\.[0-9]+)?$/;
    var matchArray = value.toString().match(numericPattern);

    if (matchArray != null)
        return true;

    return false;
}

function isDate(txtDate) {
    var objDate;
    var mSeconds;
    var day;
    var month;
    var year;

    if (txtDate.length !== 10 && txtDate.length !== 9 && txtDate.length !== 8)
        return false;

    // there should be 2 '/' slashes (mainStr.split(",").length - 1)
    if ((txtDate.split('/').length - 1) != 2)
        return false;

    // extract month, day and year from the txtDate (expected format is MM/dd/yyyy), e.g. 7/4/2013
    // subtraction will cast variables to integer implicitly (needed
    // for !== comparing)
    var dateArray = txtDate.split('/');
    month = dateArray[0];
    day = dateArray[1];
    year = dateArray[2];

    // test year range
    if (year < 1000 || year > 3000)
        return false;

    var objDate = new Date(year, month - 1, day);

    if (objDate.getFullYear() != year || objDate.getMonth() + 1 != month && objDate.getDate() != day)
        return false;

    // valid
    return true;
}

function getSelectedDate() {
    var currentTime = new Date();

    var month = parseInt(currentTime.getMonth() + 1);
    month = month <= 9 ? "0" + month : month;

    var day = currentTime.getDate();
    day = day <= 9 ? "0" + day : day;

    var year = currentTime.getFullYear();

    return day + '/' + month + "/" + year;
}