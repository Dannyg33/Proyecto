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