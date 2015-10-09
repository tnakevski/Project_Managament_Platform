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

    //animate task panel out
    taskPanelOut()
    //get id of clicked task
    var taskId = $this.attr('id');
    taskId = taskId.split("-");
    taskId = taskId[taskId.length - 1];
    //add class active to clicked task in all panels (all tasks, pending, in progress and done)
    $(".taskItem[id$="+taskId+"]").addClass("active")
    //timeout is set because panel out animation lasts 1 sec
    //So I need to ensure the data will not be shown before the panel exit
    setTimeout(function () {
        $.get("/Task/TaskOverview", { Id: taskId }, function (data) {
            $(".task-overview").html(data);
            var role = $(".task-user-role").html();
            //setTaskOverView function is implemented in taskOverview.js
            //setTaskSettings function is implemented in taskSettings.js
            switch (role) {
                case "admin":
                    setTaskOverview();
                    setTaskSettings();
                    break;
                case "assigned":
                    setTaskOverview();
                    break;
            }
        }).done(function () {
            //when done return the panel
            taskPanelIn(this);
            setTimeout(function () {
                //I need to remove this class since it adds some style to the panel 
                //making it behave oddly 
                $(".task-overview").removeClass('animated bounceInRight');
            }, 1000);
        });
    }, 1000);

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

