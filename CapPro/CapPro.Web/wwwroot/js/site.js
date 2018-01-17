$(function () {

    loadData();
    $("#btnAdd").on('click', Add);
       
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
        $('.validation-summary-errors > ul > li').hide();
    }


});

function loadData() {
    $.ajax({
        url: "/Customer/GetCustomers",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            var html = '';
            $.each(result, function (key, item) {
                html += '<tr>';
                html += '<td>' + item.name + '</td>';
                html += '<td>' + item.surname + '</td>';
                html += '<td>' + item.address + '</td>';
                html += '<td>' + item.telephoneNumber + '</td>';
                html += '<td hidden>' + item.id + '</td>';
                html += '<td><a id="editLink" href="#" onclick="getById(' + item.id + '); return false;">Edit</a> | <a href="#" onclick="Delele(' + item.id + ')">Delete</a></td>';
                html += '</tr>';
            });
            $('.tbody').html(html);
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

function Add() {
    var form = $("#form");
    form.validate();
    if ($("#form").valid()) {

        var customer = {
            ID: "0",
            Name: $('#Name').val(),
            Surname: $('#Surname').val(),
            Address: $('#Address').val(),
            TelephoneNumber: $('#TelephoneNumber').val()
        };
        $.ajax({
            url: "/Customer/Add",
            data: JSON.stringify(customer),
            type: "POST",
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (result) {
                loadData();
                $('#customerModal').modal('hide');
            },
            error: function (errormess) {
                console.log(errormess);
                alert(errormess.responseText);
            }
        });
    }
};
function getById(customerID) {
    $('#Name').css('border-color', 'lightgrey');
    $('#Surname').css('border-color', 'lightgrey');
    $('#Address').css('border-color', 'lightgrey');
    $('#TelephoneNumber').css('border-color', 'lightgrey');
    $.ajax({
        url: "/Customer/GetCustomer/" + customerID,
        typr: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            $('#CustomerID').val(result.id);
            $('#Name').val(result.name);
            $('#Surname').val(result.surname);
            $('#Address').val(result.address);
            $('#TelephoneNumber').val(result.telephoneNumber);

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

function Delele(ID) {
    var ans = confirm("Are you sure you want to delete this Record?");
    if (ans) {
        $.ajax({
            url: "/Customer/Delete/" + ID,
            type: "POST",
            contentType: "application/json;charset=UTF-8",
            dataType: "json",
            success: function (result) {
                console.log(result)
                if (result === true) {
                    alert("Customer removed successfully!");
                    loadData();
                }
                else {
                    alert("Customer not found!");
                }
            },
            error: function (errormessSurname) {
                alert(errormessSurname.responseText);
            }
        });
    }
}

function Update() {
    var form = $("#form");
    form.validate();
    if ($("#form").valid()) {
        var customerObj = {
            ID: $('#CustomerID').val(),
            Name: $('#Name').val(),
            Surname: $('#Surname').val(),
            Address: $('#Address').val(),
            TelephoneNumber: $('#TelephoneNumber').val()
        };
        $.ajax({
            url: "/Customer/Update",
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
}