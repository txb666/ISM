function validateCreateOrEditRegisterAccomodation() {
    var register_accomodation_id = document.getElementById("edit_register_accomodation_id").value;
    var student_id = document.getElementById("edit_student_id").value;
    var exchange_campus = document.getElementById("edit_exchange_campus").value;
    var accomodation_option = document.getElementById("edit_accomodation_option").value;
    var cost_per_month = document.getElementById("edit_cost_per_month").value;
    var room_size = document.getElementById("edit_room_size").value;
    var room_type = document.getElementById("edit_room_type").value;
    var distance = document.getElementById("edit_distance").value;
    var other_request = document.getElementById("edit_other_request").value;
    if (exchange_campus.trim().length == 0) {
        alert("Exchange Campus must not be empty");
        return;
    }
    $.ajax({
        type: "POST",
        url: "/RegisterAccomodation/CreateOrEdit",
        data: { register_accomodation_id: register_accomodation_id, student_id: student_id, exchange_campus: exchange_campus, accomodation_option: accomodation_option, cost_per_month: cost_per_month, room_size: room_size, room_type: room_type, distance: distance, other_request: other_request },
        dataType: "text",
        success: function (msg) {
            if (msg == "true") {
                alert("Edit successfull");
                window.location.href = "/RegisterAccomodation";
            }
            else {
                alert("Edit failed");
            }
        },
        error: function (req, status, error) {
            alert(error);
        }
    });
}