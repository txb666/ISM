
var coordinatorList = [];
var tableList = [];

function initializeCreate(coordinatorListNew) {
    coordinatorList = coordinatorListNew;
}

function initializeEdit(coordinatorListNew, tableListNew) {
    coordinatorList = coordinatorListNew;
    tableList = tableListNew;
}

function createStudentGroup() {
    var program_text = $("#create_program option:selected").text();
    var program_id = document.getElementById('create_program').value;
    var duration_start = document.getElementById('create_duration_start').value;
    var duration_end = document.getElementById('create_duration_end').value;
    var home_univercity = document.getElementById('create_home_univercity').value;
    var campus_id = document.getElementById('create_campus').value;
    var note = document.getElementById('create_note').value;
    var coordinators = [];
    for (var i = 0; i < tableList.length; i++) {
        coordinators.push(tableList[i].user_id);
    }
    if (!duration_start || !duration_end) {
        enableButton('save_create');
        alert("Duration must not be empty");
        return;
    }
    if (duration_start >= duration_end) {
        enableButton('save_create');
        alert("End date must be greater than start date.");
        return;
    }
    if (program_text != 'Degree') {
        if (home_univercity.trim().length == 0) {
            enableButton('save_create');
            alert("Student Group for mobility must have Home University");
            return;
        }
    }
    $.ajax({
        type: "POST",
        url: "/StudentGroup/isStudentGroupExist",
        data: { program_id: program_id, campus_id: campus_id, duration_start: duration_start, duration_end: duration_end, home_univercity: home_univercity },
        dataType: "text",
        success: function (msg) {
            if (msg == "true") {
                enableButton('save_create');
                alert("Student Group already exist");
                return;
            }
            else {
                $.ajax({
                    type: "POST",
                    url: "/StudentGroup/Create",
                    data: { program_id: program_id, campus_id: campus_id, duration_start: duration_start, duration_end: duration_end, home_univercity: home_univercity, note: note, coordinators: JSON.stringify(coordinators) },
                    dataType: "text",
                    success: function (msg) {
                        if (msg == "true") {
                            alert("Create Student Group successfull");
                            window.location.href = "/StudentGroup";
                        }
                        else {
                            enableButton('save_create');
                            alert("Create Student Group failed");
                        }
                    },
                    error: function (req, status, error) {
                        alert(error);
                    }
                });
            }
        },
        error: function (req, status, error) {
            alert(error);
        }
    }); 
}

function editStudentGroup() {  
    var id = document.getElementById('edit_id').value;
    var program_id = document.getElementById('edit_program').value;
    var program_text = $("#edit_program option:selected").text();
    var note = document.getElementById('edit_note').value;
    var coordinators = [];
    var duration_start = document.getElementById('edit_duration_start').value;
    var duration_end = document.getElementById('edit_duration_end').value;
    var home_univercity = document.getElementById('edit_home_univercity').value;
    var campus_id = document.getElementById('edit_campus').value;
    var original_duration_start = document.getElementById('original_duration_start').value;
    var original_duration_end = document.getElementById('original_duration_end').value;
    var original_home_univercity = document.getElementById('original_home_univercity').value;
    var original_campus_id = document.getElementById('original_campus').value;
    for (var i = 0; i < tableList.length; i++) {
        coordinators.push(tableList[i].user_id);
    }
    if (!duration_start || !duration_end) {
        enableButton('save_edit');
        alert("Duration must not be empty");
        return;
    }
    if (duration_start >= duration_end) {
        enableButton('save_edit');
        alert("End date must be greater than start date.");
        return;
    }
    if (program_text != 'Degree') {
        if (home_univercity.trim().length == 0) {
            enableButton('save_edit');
            alert("Student Group for mobility must have Home University");
            return;
        }
    }
    $.ajax({
        type: "POST",
        url: "/StudentGroup/isStudentGroupExist",
        data: { program_id: program_id, campus_id: campus_id, duration_start: duration_start, duration_end: duration_end, home_univercity: home_univercity },
        dataType: "text",
        success: function (msg) {
            if (msg == "true" && (duration_start != original_duration_start || duration_end != original_duration_end || home_univercity != original_home_univercity || campus_id != original_campus_id)) {
                enableButton('save_edit');
                alert("Student Group already exist");
                return;
            }
            else {
                $.ajax({
                    type: "POST",
                    url: "/StudentGroup/Edit",
                    data: { id: id, campus_id: campus_id, duration_start: duration_start, duration_end: duration_end, home_univercity: home_univercity, note: note, coordinators: JSON.stringify(coordinators), original_campus_id: original_campus_id, original_duration_start: original_duration_start, original_duration_end: original_duration_end, original_home_univercity: original_home_univercity, program_id: program_id },
                    dataType: "text",
                    success: function (msg) {
                        if (msg == "true") {
                            alert("Edit Student Group successfull");
                            window.location.href = "/StudentGroup";
                        }
                        else {
                            enableButton('save_edit');
                            alert("Edit Student Group failed");
                        }
                    },
                    error: function (req, status, error) {
                        alert(error);
                    }
                });
            }
        },
        error: function (req, status, error) {
            alert(error);
        }
    }); 
}

function addCoordinator() {
    var selectedCoordinator = document.getElementById('add_coordinator').value;
    for (var i = 0; i < coordinatorList.length; i++) {
        if (coordinatorList[i].user_id == selectedCoordinator) {
            tableList.push(coordinatorList[i]);
            coordinatorList.splice(i, 1);          
        }
    }
    rebuildSelectCoordinator();
    rebuildCoordinatorTable();
}

function rebuildSelectCoordinator() {
    var ele = document.getElementById('add_coordinator');
    var options = "";
    for (var i = 0; i < coordinatorList.length; i++) {
        options += "<option value='" + coordinatorList[i].user_id + "'>" + coordinatorList[i].account + "</option>"
    }
    ele.innerHTML = options;
}

function rebuildCoordinatorTable() {
    var ele = document.getElementById('create_coordinatorTable');
    var temp = 'class="btn btn-danger"';
    var content = "<tr>"
        + "<th scope='col'>Coordinator</th>"
        + "<th scope='col'> </th>"
        + "</tr>";
    for (var i = 0; i < tableList.length; i++) {
        var row = "<tr>"
            + "<td>" + tableList[i].account + "</td>"
            + "<td> <button onclick='deleteCoordinator(" + tableList[i].user_id + ")'" + temp + ">Remove</button></td>"
            + "</tr>";
        content += row;
    }
    ele.innerHTML = content;
}

function deleteCoordinator(id) {
    for (var i = 0; i < tableList.length; i++) {
        if (tableList[i].user_id == id) {
            coordinatorList.push(tableList[i]);
            tableList.splice(i, 1);
        }
    }
    rebuildSelectCoordinator();
    rebuildCoordinatorTable();
}

function myFunction1() {
    document.getElementById("myDropdown_search").classList.toggle("show_search_studentgroup");
}
function filterFunction() {
    var input, filter, ul, li, a, i;
    input = document.getElementById("myInput");
    filter = input.value.toUpperCase();
    div = document.getElementById("myDropdown_search");
    a = div.getElementsByTagName("a");
    for (i = 0; i < a.length; i++) {
        txtValue = a[i].textContent || a[i].innerText;
        if (txtValue.toUpperCase().indexOf(filter) > -1) {
            a[i].style.display = "";
        } else {
            a[i].style.display = "none";
        }
    }
}