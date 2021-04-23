function validateEditStudentHandbook() {
    var student_handbook_id = document.getElementById("edit_student_handbook_id").value;
    var title = document.getElementById("edit_title").value;
    var file = document.getElementById("edit_file").files[0];
    var fileName = document.getElementById("edit_file").value;
    var allowedExtensions = /(\.pdf)$/i;
    if (/^[A-Za-z0-9\s]+$/.test(title) == false || /^\s*$/.test(title) == true) {
        enableButton('save');
        alert("Title must not be empty or contain special character");
        return;
    }
    if (!fileName) {
        var fdata = new FormData();
        fdata.append("student_handbook_id", student_handbook_id);
        fdata.append("title", title);
        fdata.append("file", file);
        $.ajax({
            type: "post",
            url: "/StudentHandbook/Edit",
            contentType: false,
            processData: false,
            data: fdata,
            success: function () {
                alert("Edit successful");
                document.getElementById("xmas-popup").style.display = "none";
                location.reload();
            },
            error: function () {
                enableButton('save');
                alert("Edit failed");
            }
        });
    }
    else {
        if (!allowedExtensions.exec(fileName)) {
            enableButton('save');
            alert('Only pdf file are allowed!');
            file.value = '';
            return;
        }
        var fdata = new FormData();
        fdata.append("student_handbook_id", student_handbook_id);
        fdata.append("title", title);
        fdata.append("file", file);
        $.ajax({
            type: "post",
            url: "/StudentHandbook/Edit",
            contentType: false,
            processData: false,
            data: fdata,
            success: function () {
                alert("Edit successful");
                document.getElementById("xmas-popup").style.display = "none";
                location.reload();
            },
            error: function () {
                enableButton('save');
                alert("Edit failed");
            }
        });
    }
}