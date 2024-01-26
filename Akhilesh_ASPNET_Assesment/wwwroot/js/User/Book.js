var today = new Date().toISOString().split('T')[0];

document.getElementById("inputDate").value = today;

document.getElementById("inputDate").setAttribute("min", today);

$("#form").on("submit", (e) => {
    e.preventDefault();

    var doc_phone = $("#phNo").text()
    var date = $("#inputDate").val()

    console.log({ doc_phone: doc_phone, date: date })

    $.ajax({
        url: '/Service/bookSlot',
        type: 'POST',
        data: { doc_phone: doc_phone, date: date },
        success: (res) => {
            switch (res) {
                case 1:
                    alert("Slot Booked Successfully");
                    window.location = "/User/Index";
                    break;

                case 2:
                    alert("Sorry You don't have sufficient balance");
                    window.location = "/User/UpdateWallet";
                    break;

                case 3:
                    alert("Sorry no slots available or doctor doesn't have time");
                    window.location = "/User/Index";
                    break;

                case 4:
                    alert("You have already booked one slot for that day");
                    window.location = "/User/Index";
                    break;

                default:
                    alert("Something Went Wrong");
                    break;
            }
        },
        error: () => alert("Error")
    })
})