// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function showActivity(id) {
    $("#maincourseelement").load("Home/ActivityDetails/" + id);
}
function showModule(id) {
    $("#maincourseelement").load("Home/ModuleDetails/" + id);
}

$(".showmodule").click(function (event) {
    event.preventDefault();
    var moduleId = $(this).data("moduleid");
    showModule(moduleId);
});

$(".showactivity").click(function (event) {
    event.preventDefault();
    var activityId = $(this).data("activityid");
    showActivity(activityId);
});

