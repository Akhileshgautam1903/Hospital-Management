$("#registrationForm").on("submit", (e) => {
    e.preventDefault();

    var formData = {
        Name: $("#inputName").val(),
        PhoneNumber: $("#inputPhoneNumber").val(),
        Password: $("#inputPassword").val(),
        Field: $("#inputField").val(),
        Status: $("#status").val(),
        Start_time: $("#startTime").val(),
        End_time: $("#endTime").val(),
        Slots: $("#inputSlots").val(),
        Fees: $("#inputFeesAmount").val(),
    }

    console.log(formData)

    $.ajax({
        url: '/Service/addDoc',
        type: 'POST',
        data: JSON.stringify(formData),
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        success: (res) => {
            if (res) {
                alert("Doctor Added successfull!!");
                window.location = '/Admin/Index';
            } else {
                alert("Phone number already registered")
            }
        },
        error: () => alert("error")
    })
})

function updateEndTimeMin() {
    var startTime = document.getElementById('startTime').value;
    document.getElementById('endTime').min = startTime;
}

function updateStartTimeMax() {
    var endTime = document.getElementById('endTime').value;
    document.getElementById('startTime').max = endTime;
}