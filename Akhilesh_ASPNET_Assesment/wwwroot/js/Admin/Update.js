$("form").submit(function (event) {
    // Prevent the default form submission
    event.preventDefault();

    // Get form data and create an object
    var formData = {
        PhoneNumber: $("#phNo").text(),
        Fees: $("#inputFeesAmount").val(),
        Status: $("#status").val()
    };

    //ajax call
    $.ajax({
        url: '/Service/updateDoc',
        type: 'POST',
        data: JSON.stringify(formData),
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        success: (res) => {
            if (res) {
                alert("Updated successfully!");
                window.location = "/Admin/Index";
            } else {
                alert("Something Went wrong");
            }
        },
        error: () => alert("error")
    })
});