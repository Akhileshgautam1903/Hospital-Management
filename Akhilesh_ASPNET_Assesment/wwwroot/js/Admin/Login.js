$("#form").on("submit", (e) => {
    e.preventDefault();

    var num = $("#inputNumber").val();
    var pass = $("#inputPassword").val();

    $.ajax({
        url: '/Service/loginAdmin',
        type: 'GET',
        data: { num: num, pass: pass },
        success: (res) => {
            if (res) {
                alert("Login successful!!");
                window.location = '/Admin/Index';
            } else {
                alert("Incorrect Credentials");
            }
        },
        error: () => alert("Error")
    })
})