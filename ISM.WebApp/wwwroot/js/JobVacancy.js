function validateCreateJobVacancy() {
    var job_name = document.getElementById('create_jv_name_id').value;
    var job_location = document.getElementById('create_jv_location_id').value;
    var job_emp_type = document.getElementById('create_et_id').value;
    var job_content = document.getElementById('create_content_id').value;
    var job_deadline = document.getElementById('create_deadline_id').value;
    if (!job_name) {
        alert("Job name must not be empty");
        return;
    }
    if (!job_location) {
        alert("Job location must not be empty");
        return;
    }
    if (!job_emp_type) {
        alert("Job employment type must not be empty");
        return;
    }
    if (!job_content) {
        alert("Job content must not be empty");
        return;
    }
    if (!job_deadline) {
        alert("Job deadline must not be empty");
        return;
    }
    $.ajax({
        type: "POST",
        url: "/JobVacancy/CreateJV",
        data: { job_name: job_name, job_location: job_location, employment_type: job_emp_type, content: job_content, deadline: job_deadline },
        dataType: "text",
        success: function (msg) {
            if (msg == "true") {
                alert("Create successfull");
                window.location.href = "/JobVacancy";
            }
            else {
                alert("Create failed")
            }
        },
        error: function (req, status, error) {
            alert(error);
        }
    });
}

function editJV(job_id, job_name, job_location, job_emp_type, job_content, job_deadline) {
    document.getElementById('edit_jv_id').value = job_id;
    document.getElementById('edit_jv_name_id').value = job_name;
    document.getElementById('edit_jv_location_id').value = job_location;
    document.getElementById('edit_et_id').value = job_emp_type;
    document.getElementById('edit_content_id').value = job_content;
    document.getElementById('edit_deadline_id').value = job_deadline;
}

function validateEditJobVacancy() {
    var job_id = document.getElementById('edit_jv_id').value;
    var job_name = document.getElementById('edit_jv_name_id').value;
    var job_location = document.getElementById('edit_jv_location_id').value;
    var job_emp_type = document.getElementById('edit_et_id').value;
    var job_content = document.getElementById('edit_content_id').value;
    var job_deadline = document.getElementById('edit_deadline_id').value;
    if (!job_name) {
        alert("Job name must not be empty");
        return;
    }
    if (!job_location) {
        alert("Job location must not be empty");
        return;
    }
    if (!job_emp_type) {
        alert("Job employment type must not be empty");
        return;
    }
    if (!job_content) {
        alert("Job content must not be empty");
        return;
    }
    if (!job_deadline) {
        alert("Job deadline must not be empty");
        return;
    }
    $.ajax({
        type: "POST",
        url: "/JobVacancy/EditJV",
        data: { job_id: job_id, job_name: job_name, job_location: job_location, employment_type: job_emp_type, content: job_content, deadline: job_deadline },
        dataType: "text",
        success: function (msg) {
            if (msg == "true") {
                alert("Edit successfull");
                window.location.href = "/JobVacancy";
            }
            else {
                alert("Edit failed")
            }
        },
        error: function (req, status, error) {
            alert(error);
        }
    });
}

function deleteJV() {
    var job_id = document.getElementById('delete_JobVacancy_id').value;
    var temp = confirm("Do you want to delete?");
    if (temp) {
        $.ajax({
            type: "POST",
            url: "/JobVacancy/DeleteJV",
            data: { job_id: job_id },
            dataType: "text",
            success: function (msg) {
                if (msg == "true") {
                    alert("Delete successfull");
                    window.location.href = "/JobVacancy";
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
    else {
        return;
    }
}