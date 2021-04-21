function validateCreateORTMaterials() {
    var student_group_id = document.getElementById("create_student_group_id").value;
    var content = document.getElementById("create_content").value;
    var note = document.getElementById("create_note").value;
    if (/^[A-Za-z0-9\s]+$/.test(content) == false || /^\s*$/.test(content) == true) {
        alert("Content must not be empty or contain special character");
        return;
    }  
    $.ajax({
        type: "POST",
        url: "/ORTMaterials/Create",
        data: { student_group_id: student_group_id, content: content, note: note },
        dataType: "text",
        success: function (msg) {
            if (msg == "true") {
                alert("Create ORT Materials successfull");
                document.getElementById("xmas-popup").style.display = "none";
                location.reload();
            }
            else {
                alert("Create ORT Materials failed")
            }
        },
        error: function (req, status, error) {
            alert(error);
        }
    });
}

function EditORTMaterials(ort_materials_id, content, note) {
    document.getElementById("edit_ort_materials_id").value = ort_materials_id;
    document.getElementById("edit_content").value = content;
    document.getElementById("edit_note").value = note;  
}

function validateEditORTMaterials() {
    var ort_materials_id = document.getElementById("edit_ort_materials_id").value;
    var content = document.getElementById("edit_content").value;
    var note = document.getElementById("edit_note").value;
    if (/^[A-Za-z0-9\s]+$/.test(content) == false || /^\s*$/.test(content) == true) {
        alert("Content must not be empty or contain special character");
        return;
    }    
    $.ajax({
        type: "POST",
        url: "/ORTMaterials/Edit",
        data: { ort_materials_id: ort_materials_id, content: content, note: note },
        dataType: "text",
        success: function (msg) {
            if (msg == "true") {
                alert("Edit ORT Materials successfull");
                document.getElementById("xmas-popup-assign").style.display = "none";
                location.reload();
            }
            else {
                alert("Edit ORT Materials failed")
            }
        },
        error: function (req, status, error) {
            alert(error);
        }
    });
}

function deleteORTMaterials(ort_materials_id) {
    if (confirm("Are you sure to delete this material?")) {
        $.ajax({
            type: "POST",
            url: "/ORTMaterials/Delete",
            data: { ort_materials_id: ort_materials_id },
            dataType: "text",
            success: function (msg) {
                if (msg == "true") {
                    alert("Delete ORT Materials successfull");
                    location.reload();
                }
                else {
                    alert("Delete ORT Materials failed")
                }
            },
            error: function (req, status, error) {
                alert(error);
            }
        });
    }
}