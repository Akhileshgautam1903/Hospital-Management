$("#form").on("submit", (e) => {
    e.preventDefault();

    var num = $("#inputNumber").val();
    var pass = $("#inputPassword").val();

    $.ajax({
        url: '/Service/loginUser',
        type: 'GET',
        data: { num: num, pass: pass },
        success: (res) => {
            if (res) {
                alert("Login successful!!");
                window.location = '/User/Index';
            } else {
                alert("Incorrect Credentials");
            }
        },
        error: () => alert("Error")
    })
})