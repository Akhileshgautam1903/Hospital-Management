$("#form").on("submit", (e) => {
    e.preventDefault();

    var name = $("#inputName").val();

    var Data = new FormData();
    Data.append('Name', name);

    var photo = $("#inputPic")[0]
    if (photo.files.length > 0) {
        Data.append("Pic", photo.files[0]);
    }
    

    //var Data = {
    //    Name: name,
    //    Pic: Pic
    //}

    //var houseData = new FormData();
    //houseData.append('name')

    $.ajax({
        url: '/Service/uploadPic',
        type: 'POST',
        data: Data,
        contentType: false,
        processData: false,
        success: (res) => {
            if (res) {
                alert("Upload successful!!");
                window.location = '/Admin/Photos';
            } else {
                alert("Incorrect Credentials");
            }
        },
        error: () => alert("Error")
    })
})