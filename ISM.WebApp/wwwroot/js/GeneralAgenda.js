function validateEditGeneralAgenda() {
    var general_agenda_id = document.getElementById("edit_general_agenda_id").value;
    var note = document.getElementById("edit_note").value;
    var file = document.getElementById("edit_file").files[0];
    var fileName = document.getElementById("edit_file").value;
    var allowedExtensions = /(\.xlsx|\.xls|\.csv|\.pdf)$/i;
    if (!allowedExtensions.exec(fileName)) {
        alert('Only xlsx/xls/csv and pdf file are allowed!');
        file.value = '';
        return;
    }
    var fdata = new FormData();
    fdata.append("general_agenda_id", general_agenda_id);
    fdata.append("note", note);  
    fdata.append("file", file);
    $.ajax({
        type: "post",
        url: "/GeneralAgenda/Edit",
        contentType: false,
        processData: false,
        data: fdata,
        success: function () {
            alert("Edit successful");
            document.getElementById("xmas-popup").style.display = "none";
            location.reload();
        },
        error: function () {
            alert("Edit failed");
        }
    });
}