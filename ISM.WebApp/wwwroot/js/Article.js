
function validateEditArticle() {
    var article_id = document.getElementById("edit_article_id").value;
    var old_file_name = document.getElementById("edit_old_file_name").value;
    var type = document.getElementById("edit_type").value;
    var title = document.getElementById("edit_title").value;
    var file = document.getElementById("edit_file").files[0];
    if (/^[A-Za-z0-9\s]+$/.test(title) == false || /^\s*$/.test(title) == true) {
        alert("Title must not be empty or contain special character");
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
            location.reload();
        },
        error: function () {
            alert("Edit failed");
        }
    });
}