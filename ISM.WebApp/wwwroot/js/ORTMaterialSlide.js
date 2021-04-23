function validateCreateORTMaterialSlide() {
    var student_id = document.getElementById("create_student_id").value;
    var content = document.getElementById("create_content").value;
    var program = document.getElementById("create_program").value;
    var material = document.getElementById("create_material").value;
    if (/^[A-Za-z0-9\s]+$/.test(content) == false || /^\s*$/.test(content) == true) {
        enableButton('save_create');
        alert("Content must not be empty or contain special character");
        return;
    }
    if (/^[A-Za-z0-9\s]+$/.test(program) == false || /^\s*$/.test(program) == true) {
        enableButton('save_create');
        alert("Program must not be empty or contain special character");
        return;
    }
    if (/^[A-Za-z0-9\s]+$/.test(material) == false || /^\s*$/.test(material) == true) {
        enableButton('save_create');
        alert("Material must not be empty or contain special character");
        return;
    }
    $.ajax({
        type: "POST",
        url: "/ORTMaterialSlide/Create",
        data: { student_id: student_id, program: program, material: material, content: content },
        dataType: "text",
        success: function (msg) {
            if (msg == "true") {
                alert("Create successfull");
                document.getElementById("xmas-popup").style.display = "none";
                location.reload();
            }
            else {
                enableButton('save_create');
                alert("Create failed")
            }
        },
        error: function (req, status, error) {
            alert(error);
        }
    });
}

function EditORTMaterialSlide(ort_material_slide_id, program, content, material) {
    document.getElementById("edit_ort_material_slide_id").value = ort_material_slide_id;
    document.getElementById("edit_content").value = content;
    document.getElementById("edit_program").value = program;
    document.getElementById("edit_material").value = material;
}

function validateEditORTMaterialSlide() {
    var ort_material_slide_id = document.getElementById("edit_ort_material_slide_id").value;
    var content = document.getElementById("edit_content").value;
    var program = document.getElementById("edit_program").value;
    var material = document.getElementById("edit_material").value;
    if (/^[A-Za-z0-9\s]+$/.test(content) == false || /^\s*$/.test(content) == true) {
        enableButton('save_edit');
        alert("Content must not be empty or contain special character");
        return;
    }
    if (/^[A-Za-z0-9\s]+$/.test(program) == false || /^\s*$/.test(program) == true) {
        enableButton('save_edit');
        alert("Program must not be empty or contain special character");
        return;
    }
    if (/^[A-Za-z0-9\s]+$/.test(material) == false || /^\s*$/.test(material) == true) {
        enableButton('save_edit');
        alert("Material must not be empty or contain special character");
        return;
    }
    $.ajax({
        type: "POST",
        url: "/ORTMaterialSlide/Edit",
        data: { ort_material_slide_id: ort_material_slide_id, program: program, content: content, material: material },
        dataType: "text",
        success: function (msg) {
            if (msg == "true") {
                alert("Edit successfull");
                document.getElementById("xmas-popup-assign").style.display = "none";
                location.reload();
            }
            else {
                enableButton('save_edit');
                alert("Edit failed")
            }
        },
        error: function (req, status, error) {
            alert(error);
        }
    });
}

function deleteORTMaterialSlide(ort_material_slide_id) {
    if (confirm("Are you sure to delete?")) {
        $.ajax({
            type: "POST",
            url: "/ORTMaterialSlide/Delete",
            data: { ort_material_slide_id: ort_material_slide_id },
            dataType: "text",
            success: function (msg) {
                if (msg == "true") {
                    alert("Delete  successfull");
                    location.reload();
                }
                else {
                    alert("Delete failed")
                }
            },
            error: function (req, status, error) {
                alert(error);
            }
        });
    }
}