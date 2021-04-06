function validateEditLocalRecommendation() {
    var local_recommendation_id = document.getElementById("edit_local_recommendation_id").value;
    var title = document.getElementById("edit_title").value;
    var file = document.getElementById("edit_file").files[0];
    if (/^[A-Za-z0-9\s_-]+$/.test(title) == false || /^\s*$/.test(title) == true) {
        alert("Title must not be empty or contain special character");
        return;
    }
    var fdata = new FormData();
    fdata.append("local_recommendation_id", local_recommendation_id);
    fdata.append("title", title);
    fdata.append("file", file);
    $.ajax({
        type: "post",
        url: "/LocalRecommendation/Edit",
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