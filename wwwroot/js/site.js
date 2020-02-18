// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


//For the main page change element functions
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
//End

//Add user to course
$(".adduserbutton").click(function (event) {
    event.preventDefault();
    var userId = $(this).data("userid");
    var courseId = $(this).data("courseid");
    var mainPage = true;
    addUser(userId, courseId, mainPage);
});

function addUser(userId, courseId, mainPage) {
    $("#usercourselist").load("Courses/AddUserToCourse/", { userId, courseId, mainPage });
    $("#systemUsersModalCenter").modal('hide');
    $("#systemUsersModalCenter").removeData();

    /*$.ajax({
        
        url: "Courses/AddUserToCourse/",
        data: { userId: userId, courseId: courseId },
        success: function (data) {
        },
        error: function (data) {

        }
    })*/
}
//End
