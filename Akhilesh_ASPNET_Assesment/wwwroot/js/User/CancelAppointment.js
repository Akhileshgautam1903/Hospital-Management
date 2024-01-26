function cancelSlot(id) {
    $.ajax({
        url: '/Service/cancelSlot',
        type: 'GET',
        data: { id: id },
        success: (res) => {
            if (res) {
                alert("Slot cancelled successfully");
                window.location = "/User/Index";
            } else {
                alert("something went wrong");
            }
        },
        error: () => alert("error")
    })
}