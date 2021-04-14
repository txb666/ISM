

function validateEditArticle() {
    var article_id = document.getElementById("edit_article_id").value;
    var old_file_name = document.getElementById("edit_old_file_name").value;
    var type = document.getElementById("edit_type").value;
    var title = document.getElementById("edit_title").value;
    var file = document.getElementById("edit_file").files[0];
    var fileName = document.getElementById("edit_file").value;
    var idxDot = fileName.lastIndexOf(".") + 1;
    var extFile = fileName.substr(idxDot, fileName.length).toLowerCase();
    if (/^[A-Za-z0-9\s]+$/.test(title) == false || /^\s*$/.test(title) == true) {
        alert("Title must not be empty or contain special character");
        return;
    }
    if (!extFile == "pdf") {
        alert("Only pdf file are allowed!");
        return;
    }
    var fdata = new FormData();
    fdata.append("article_id", article_id);
    fdata.append("old_file_name", old_file_name);
    fdata.append("type", type);
    fdata.append("title", title);
    fdata.append("file", file);
    $.ajax({
        type: "post",
        url: "/Article/Edit",
        contentType: false,
        processData: false,
        data: fdata,
        success: function () {
            alert("Edit successful");
            document.getElementById("xmas-popup").style.display="none";
            location.reload();
        },
        error: function () {
            alert("Edit failed");
        }
    });
}

function validateCreateArticle() {
    var type = document.getElementById("create_type").value;
    var title = document.getElementById("create_title").value;
    var file = document.getElementById("create_file").files[0];
    var fileName = document.getElementById("create_file").value;
    var idxDot = fileName.lastIndexOf(".") + 1;
    var extFile = fileName.substr(idxDot, fileName.length).toLowerCase();
    if (/^[A-Za-z0-9\s]+$/.test(title) == false || /^\s*$/.test(title) == true) {
        alert("Title must not be empty or contain special character");
        return;
    }
    if (!extFile == "pdf") {
        alert("Only pdf file are allowed!");
        return;
    }
    var fdata = new FormData();
    fdata.append("type", type);
    fdata.append("title", title);
    fdata.append("file", file);
    $.ajax({
        type: "post",
        url: "/Article/Create",
        contentType: false,
        processData: false,
        data: fdata,
        success: function () {
            alert("Create successful");
            document.getElementById("xmas-popup").style.display = "none";
            location.reload();
        },
        error: function () {
            alert("Create failed");
        }
    });
}

function deleteArticle(article_id,type,file_name) {
    if (confirm("Are you sure to delete this article")) {
        $.ajax({
            type: "POST",
            url: "/Article/Delete",
            data: { article_id: article_id, file_name: file_name, type: type },
            dataType: "text",
            success: function (msg) {
                if (msg == "true") {
                    alert("Delete Successfull");
                    if (type == 'Insurance Instruction') {
                        window.location.href = "/Article/List?type=Insurance%20Instruction";
                    }
                    if (type == 'Tips and samples for CV') {
                        window.location.href = "/Article/List?type=Tips%20and%20samples%20for%20CV";
                    }
                    if (type == 'Tips to prepare for job interview') {
                        window.location.href = "/Article/List?type=Tips%20to%20prepare%20for%20job%20interview";
                    }
                    if (type == 'Jobs 101') {
                        window.location.href = "/Article/List?type=Jobs%20101";
                    }
                }
                else {
                    alert("Failed");
                }
            },
            error: function (req, status, error) {
                alert(error);
            }
        });
    }
    else {
        return;
    }
}