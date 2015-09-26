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

    //animate task panel out
    taskPanelOut();

    //make clicked task active
    $(".taskItem").removeClass("active");
    $(this).addClass("active");

    //Do Something
    //Make AJAX Get request to take the clicked task info


    //animate task panel in
    taskPanelIn(this);


}


//animate task panel out
function taskPanelOut() {
    $(".task-overview").removeClass('animated bounceInRight');
    $(".task-overview").addClass('animated bounceOutRight');
}

//animate task panel in
function taskPanelIn(clicked) {
    setTimeout(function () {
        var taskName = $(".taskName", clicked).html();
        $(".task-overview #taskTitle").html(taskName);
        //$(".task-settings").toggleClass("bounceOutRight bounceInRight")
        $(".task-overview").removeClass('animated bounceOutRight');
        $(".task-overview").addClass('animated bounceInRight');
    }, 1000);
}

