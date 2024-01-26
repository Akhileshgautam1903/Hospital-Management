$("#form").on("submit", (e) => {
    e.preventDefault();

    var wallet = $("#inputWalletAmount").val();

    $.ajax({
        url: '/Service/updateWallet',
        type: 'GET',
        data: { amt: wallet },
        success: (res) => {
            if (res) {
                alert("Wallet Updated Successfully!")
                window.location = '/User/UpdateWallet';
            } else {
                alert("Something went wrong")
            }
        },
        error: () => alert("error")
    })
})