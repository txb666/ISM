
function deleteFAQ(faq_id) {
    if (confirm("Are you sure you want to delete this question")) {
        $.ajax({
            type: "POST",
            url: "/FAQ/Delete",
            data: { faq_id: faq_id },
            dataType: "text",
            success: function (msg) {
                if (msg == "true") {
                    alert("Successfull");
                    window.location.href = "/FAQ";
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

function editFAQ(faq_id, question, answer) {
    document.getElementById('edit_faq_id').value = faq_id;
    document.getElementById('edit_question').value = question;
    document.getElementById('edit_answer').value = answer;
}

function validateCreateFAQ() {
    var question = document.getElementById('create_question').value;
    var answer = document.getElementById('create_answer').value;
    if (/^[A-Za-z0-9\s]+$/.test(question) == false || /^\s*$/.test(question) == true) {
        alert("Question must not be empty or contain special character");
        return;
    }
    if (/^[A-Za-z0-9\s]+$/.test(answer) == false || /^\s*$/.test(answer) == true) {
        alert("Answer must not be empty or contain special character");
        return;
    }
    $.ajax({
        type: "POST",
        url: "/FAQ/Create",
        data: { question: question, answer: answer },
        dataType: "text",
        success: function (msg) {
            if (msg == "true") {
                alert("Successfull");
                window.location.href = "/FAQ";
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

function validateEditFAQ() {
    var faq_id = document.getElementById('edit_faq_id').value;
    var question = document.getElementById('edit_question').value;
    var answer = document.getElementById('edit_answer').value;
    if (/^[A-Za-z0-9\s]+$/.test(question) == false || /^\s*$/.test(question) == true) {
        alert("Question must not be empty or contain special character");
        return;
    }
    if (/^[A-Za-z0-9\s]+$/.test(answer) == false || /^\s*$/.test(answer) == true) {
        alert("Answer must not be empty or contain special character");
        return;
    }
    $.ajax({
        type: "POST",
        url: "/FAQ/Edit",
        data: { faq_id: faq_id, question: question, answer: answer },
        dataType: "text",
        success: function (msg) {
            if (msg == "true") {
                alert("Successfull");
                window.location.href = "/FAQ";
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