function AddTempFeaturesTc() {
    var feature = {};
    feature.Title = $('#Tc_Title').val();
    feature.Description = $('#Tc_Description').val();
    var errors = "";
    if (feature.Title.trim().length === 0) {
        errors += "عنوان را وارد نمایید.<br>";
        $('#Tc_Title').addClass("border-danger");
    } else {
        $('#Tc_Title').removeClass("border-danger");
    }

    if (feature.Description.trim().length == 0) {
        errors += "لطفا توضیحات را وارد نمایید.<br>";
        $('#Tc_Description').addClass("border-danger");
    } else {
        $('#Tc_Description').removeClass("border-danger");
    }
    if (errors.length > 0) { //if errors detected then notify user and cancel transaction
        ShowMsnTc(errors);
        return false; //exit function
    }
    var existTitle = false; // < -- Main indicator
    $('#table-information_Tc > tbody  > tr').each(function () {
        var title = $(this).find('.TitleColTc').text(); // get text of current row by class selector
        if (feature.Title.toLowerCase() === title.toLowerCase()) { //Compare provided and existing title
            existTitle = true;
            return false;
        }
    });


    if (existTitle === false) {
        ClearMsn();
        //Create Row element with provided data
        var row = $('<tr>');
        $('<td>').html(feature.Title).addClass("TitleColTc").appendTo(row);
        $('<td>').html(feature.Description).appendTo(row);
        $('<td>').html("<div class='text-center'><button class='btn btn-danger btn-sm' onclick='DeleteTC($(this))'>حذف</button></div>").appendTo(row);

        //Append row to table's body
        $('#table-body_tc').append(row);
        CheckSubmitBtn(); // Enable submit button
    } else {
        ShowMsnTc("عنوان باید یکتا باشد.");
    }
    ClearFormTC();
}

function ClearFormTC() {
    $('#PrFeaturesTc input[type="text"]').val('');
    $('#Tc_Description').val('');
}

//Msn label for notifications
function ShowMsnTc(message) {
    $('#MsnTc').html(message);
}

//Clear text of Msn label
function ClearMsn() {
    $('#MsnTc').html("");
}

//Delete selected row
function DeleteTC(row) { // remove row from table
    row.closest('tr').remove();
    CheckSubmitBtnTC();
}

function CheckSubmitBtnTC() {
    if ($('#table-information_Tc > tbody  > tr').length > 0) { // count items in table if at least 1 item is found then enable button  
        $('#btnSubmit').removeAttr("disabled");
    } else {
        $('#btnSubmit').attr("disabled", "disabled");
    }
}