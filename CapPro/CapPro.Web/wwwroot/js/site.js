$(function () {

    $("#btnAdd").on('click', Add);

    function Add() {
        var res = validate();
        if (res === false) {
            return false;
        }
        var customerObj = {
            sustomerID: $('#CustomerID').val(),
            name: $('#Name').val(),
            surname: $('#Surname').val(),
            sddress: $('#Address').val(),
            telephoneNumber: $('#TelephoneNumber').val()
        };
        $.ajax({
            url: "/Customer/Add",
            data: JSON.stringify(customerObj),
            type: "POST",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                loadData();
                $('#customerModal').modal('hide');
            },
            error: function (errormessSurname) {
                alert(errormessSurname.responseText);
            }
        });
    };

    //Function for getting the Data Based upon Customer ID  
    function getbyID(customerID) {
        $('#Name').css('border-color', 'lightgrey');
        $('#Surname').css('border-color', 'lightgrey');
        $('#Address').css('border-color', 'lightgrey');
        $('#TelephoneNumber').css('border-color', 'lightgrey');
        $.ajax({
            url: "/Home/getbyID/" + customerID,
            typr: "GET",
            contentType: "application/json;charset=UTF-8",
            dataType: "json",
            success: function (result) {
                $('#CustomerID').val(result.CustomerID);
                $('#Name').val(result.Name);
                $('#Surname').val(result.Surname);
                $('#Address').val(result.Address);
                $('#TelephoneNumber').val(result.TelephoneNumber);

                $('#customerModal').modal('show');
                $('#btnUpdate').show();
                $('#btnAdd').hide();
            },
            error: function (errormessSurname) {
                alert(errormessSurname.responseText);
            }
        });
        return false;
    }

    //function for updating Customer's record  
    function Update() {
        var res = validate();
        if (res === false) {
            return false;
        }
        var customerObj = {
            CustomerID: $('#CustomerID').val(),
            Name: $('#Name').val(),
            Surname: $('#Surname').val(),
            Address: $('#Address').val(),
            TelephoneNumber: $('#TelephoneNumber').val()
        };
        $.ajax({
            url: "/Home/Update",
            data: JSON.stringify(customerObj),
            type: "POST",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                loadData();
                $('#customerModal').modal('hide');
                $('#CustomerID').val("");
                $('#Name').val("");
                $('#Surname').val("");
                $('#Address').val("");
                $('#TelephoneNumber').val("");
            },
            error: function (errormessSurname) {
                alert(errormessSurname.responseText);
            }
        });
    }

    //function for deleting Customer's record  
    function Delele(ID) {
        var ans = confirm("Are you sure you want to delete this Record?");
        if (ans) {
            $.ajax({
                url: "/Home/Delete/" + ID,
                type: "POST",
                contentType: "application/json;charset=UTF-8",
                dataType: "json",
                success: function (result) {
                    loadData();
                },
                error: function (errormessSurname) {
                    alert(errormessSurname.responseText);
                }
            });
        }
    }

    //Function for clearing the textboxes  
    $("#addCustomerBtn").on('click', clearTextBox);

    function clearTextBox() {
        $('#CustomerID').val("");
        $('#Name').val("");
        $('#Surname').val("");
        $('#Address').val("");
        $('#TelephoneNumber').val("");
        $('#btnUpdate').hide();
        $('#btnAdd').show();
        $('#Name').css('border-color', 'lightgrey');
        $('#Surname').css('border-color', 'lightgrey');
        $('#Address').css('border-color', 'lightgrey');
        $('#TelephoneNumber').css('border-color', 'lightgrey');
    }
    //Valdidation using jquery  
    function validate() {
        var isValid = true;
        if ($('#Name').val().trim() === "") {
            $('#Name').css('border-color', 'Red');
            isValid = false;
        }
        else {
            $('#Name').css('border-color', 'lightgrey');
        }
        if ($('#Surname').val().trim() === "") {
            $('#Surname').css('border-color', 'Red');
            isValid = false;
        }
        else {
            $('#Surname').css('border-color', 'lightgrey');
        }
        if ($('#Address').val().trim() === "") {
            $('#Address').css('border-color', 'Red');
            isValid = false;
        }
        else {
            $('#Address').css('border-color', 'lightgrey');
        }
        if ($('#TelephoneNumber').val().trim() === "") {
            $('#TelephoneNumber').css('border-color', 'Red');
            isValid = false;
        }
        else {
            $('#TelephoneNumber').css('border-color', 'lightgrey');
        }
        return isValid;
    }

});