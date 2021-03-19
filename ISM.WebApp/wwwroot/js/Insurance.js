
function validateEditInsurance() {
    var id = document.getElementById("edit_id").value;
    var startDate = document.getElementById("edit_startDate").value;
    var expiryDate = document.getElementById("edit_expiryDate").value;
    if (!startDate || !expiryDate) {
        alert("start date and expiry date must not be empty");
        return;
    }
    $.ajax({
        type: "POST",
        url: "/Insurance/Edit",
        data: { id: id, startDate: startDate, expiryDate: expiryDate },
        dataType: "text",
        success: function (msg) {
            if (msg == "true") {
                alert("Edit Insurance successfull");
                window.location.href = "/Insurance";
            }
            else {
                alert("Edit Insurance failed");
            }
        },
        error: function (req, status, error) {
            alert(error);
        }
    });
}

function editInsurance(id, account, fullname, startDate, expiryDate) {
    document.getElementById("edit_id").value = id;
    document.getElementById("edit_account").value = account;
    document.getElementById("edit_fullname").value = fullname;
    document.getElementById("edit_startDate").value = startDate;
    document.getElementById("edit_expiryDate").value = expiryDate;
}