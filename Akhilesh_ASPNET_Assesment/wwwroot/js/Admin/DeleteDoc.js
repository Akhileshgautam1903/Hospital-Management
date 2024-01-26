function deleteDoc(id) {

    $.ajax({
        url: '/Service/deleteDoc',
        type: 'GET',
        data: { id: id },
        success: (res) => {
            if (res) {
                alert("Doctor deleted successfully");
                window.location = "/Admin/Index";
            } else {
                alert("something went wrong");
            }
        },
        error: () => alert("error")
    })
}