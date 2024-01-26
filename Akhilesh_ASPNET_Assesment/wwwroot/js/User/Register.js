$("#registrationForm").on("submit", (e) => {
    e.preventDefault();

    var formData = {
        Name: $("#inputName").val(),
        PhoneNumber: $("#inputPhoneNumber").val(),
        Password: $("#inputPassword").val(),
        Wallet: $("#inputWalletAmount").val()
    }

    console.log(formData)

    $.ajax({
        url: '/Service/registerUser',
        type: 'POST',
        data: JSON.stringify(formData),
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        success: (res) => {
            if (res) {
                alert("Registration successfull!!");
                window.location = '/User/Login';
            } else {
                alert("Phone number already registered")
            }
        },
        error: () => alert("error")
    })
})