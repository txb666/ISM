// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function search(page) {
    var form = document.getElementById('searchForm');
    var inputPage = document.getElementById('inputPage');
    inputPage.value = page;
    form.submit();
}

function searchDegreeOrMobility() {
    document.getElementById('search_degreeOrMobility').value = document.getElementById('outside_degreeOrMobility').value;
    search(1);
}