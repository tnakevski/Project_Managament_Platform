$(document).ready(function () {
    $(".taskItem").click(openTask);

    $(".toggle-project-settings").on("click", function () {
        $(".project-settings").slideToggle(500);
        $(".toggle-project-settings-icon").toggleClass("wb-chevron-down wb-chevron-up");
    })

});

function openTask() {
    //check if clicked task is already opened
    if ($(this).hasClass("active"))
        return;
    //check if there is already data loading
    if ($(".task-overview").hasClass("bounceOutRight"))
        return;

    //make clicked task active  
    var $this = $(this);
    $(".taskItem").removeClass("active");
    $this.addClass("active");
    //animate task panel out
    taskPanelOut()
    //get id of clicked task
    var taskId = $this.attr('id');
    taskId = taskId.split("-");
    taskId = taskId[taskId.length - 1];
    //timeout is set because panel out animation lasts 1 sec
    //So I need to ensure the data will not be shown before the panel ex
    setTimeout(function () {
        $.get("/Task/TaskOverview", { Id: taskId }, function (data) {
            $(".task-overview").html(data);
            setTaskSettings();
            //setTaskOverView function is implemented in taskOverview.js
            setTaskOverview();
        }).done(function () {
            //when done return the panel
            taskPanelIn(this);
        });
    }, 1000);

}

function setTaskSettings() {
    //set date picker for task settings
    var taskDatepicker = $('#taskDateChange');
    taskDatepicker.datetimepicker();
    taskDatepicker.data("DateTimePicker").minDate(new Date());
    taskDatepicker.data("DateTimePicker").date(null);

    //set enter keypress for text area 
    $('.comment-text-area').keypress(function (e) {
        if (e.keyCode == 13 && !e.shiftKey) {
            e.preventDefault();
            addComment();
        }
    });
}

//animate task panel out
function taskPanelOut(callback) {
    $(".task-overview").removeClass('animated bounceInRight');
    $(".task-overview").addClass('animated bounceOutRight');
}

//animate task panel in
function taskPanelIn(clicked) {
    var taskName = $(".taskName", clicked).html();
    $(".task-overview #taskTitle").html(taskName);
    //$(".task-settings").toggleClass("bounceOutRight bounceInRight")
    $(".task-overview").removeClass('animated bounceOutRight');
    $(".task-overview").addClass('animated bounceInRight');
}

