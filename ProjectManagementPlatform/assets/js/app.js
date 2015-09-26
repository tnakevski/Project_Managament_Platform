$(document).ready(function () {
    //scroll only the container you are currently hovering on
    $(".scroll-container").bind('mousewheel DOMMouseScroll', function (e) {
        var scrollTo = null;

        if (e.type == 'mousewheel') {
            scrollTo = (e.originalEvent.wheelDelta * -1);
        }
        else if (e.type == 'DOMMouseScroll') {
            scrollTo = 40 * e.originalEvent.detail;
        }

        if (scrollTo) {
            e.preventDefault();
            $(this).scrollTop(scrollTo + $(this).scrollTop());
        }
    });

    //set enter keypress for add subtask modal
    $(".new-project-task-input").keypress(function (e) {
        if (e.keyCode == 13) {
            e.preventDefault();
            newProjectAddTask();
        }
    });

    //add event listener to add task button when creating new project
    $(".new-project-add-task").click(newProjectAddTask);

    //focus on input field when opening add project modal
    $('#addProjectModal').on('shown.bs.modal', function () {
        $('.new-project-title-input').focus();
    })

    //set date picker for project create
    var createProjectDatepicker = $('#createProjectDatetimePicker')
    createProjectDatepicker.datetimepicker();
    createProjectDatepicker.data("DateTimePicker").minDate(new Date());
    createProjectDatepicker.data("DateTimePicker").date(null);
});

function newProjectAddTask() {
    //get value from input field
    var inputField = $(".new-project-task-input");
    var inputVal = inputField.val();

    //ensure input is not empty
    if (inputVal == "")
        return;

    var liCount = $(".new-project-task-list li").length + 1;
    if (liCount > 5)
        return;
    //create li snippet 
    var newTask = "<li class='list-group-item bg-blue-grey-200' style='color: #62A8EA;'>";
    newTask += "<span>" + inputVal + "</span>";
    newTask += "<span class='icon wb-trash pull-right text-middle' id='new-project-delete-task" + liCount + "'></span></li>";
    //clear the input
    inputField.val("");
    //add li to list
    $(".new-project-task-list").prepend(newTask);
    //add click event listener on the delete button
    $("#new-project-delete-task" + liCount + "").click(newProjectDeleteTask);
}

function newProjectDeleteTask() {
    var clicked = $(this);
    clicked.parent().remove();
}