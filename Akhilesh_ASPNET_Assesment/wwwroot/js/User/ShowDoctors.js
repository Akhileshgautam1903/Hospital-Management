function filterByFields() {
    var selectedField = $("#inputField").val().toLowerCase();
    var doctors = document.getElementsByClassName("doctors");
    var displayedField = document.getElementsByClassName("DisplayedField");

    for (var i = 0; i < doctors.length; i++) {
        var showDoc = (selectedField == "all" || selectedField == displayedField[i].textContent.toLowerCase());

        doctors[i].style.display = showDoc ? "" : "none";
    }
}


$("#inputField").change(function () {
    filterByFields();
});