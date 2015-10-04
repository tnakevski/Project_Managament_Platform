$(function () {
    var subtaskTitle;
    //add on click events on all of the buttons
    $(".subtaskDeleteBtn").click(deleteSubtask);
    $(".check-subtask-done").click(checkSubtask);
    $(".add-comment").click(addComment);
    $("#saveSubtaskBtn").click(addSubtask);

    //set enter keypress for add subtask modal
    $('#addSubtaskModal').keypress(function (e) {
        if (e.keyCode == 13 && !e.shiftKey) {
            e.preventDefault();
            addSubtask();
        }
    });
    //focus on input field when opening add subtask modal
    $('#addSubtaskModal').on('shown.bs.modal', function () {
        $('#newSubtaskTitle').focus();
    })

    //get data for subrask overview
    $('#subtaskOverviewModal').on('shown.bs.modal', function () {
        $.get("/Home/TaskDescription", function (data) {
            $(".subtask-overview-description").text(data.Description);
        });     
    })
});


function addSubtask() {
    subtaskTitle = $("#newSubtaskTitle").val();
    var liCount = $(".subtasks li").length + 1;
    var date = jQuery.format.date(new Date(), "dd/MM/yyyy HH:mm:ss");

    //create the html snippet for subtask li
    var li = "<li class='list-group-item' data-role='task'>";
    li += "<div class='checkbox-custom checkbox-primary'>";
    li += "<input type='checkbox' id='checkBox" + liCount + "' name='inputCheckboxesRecieve' class='check-subtask-done'>";
    li += "<label for='checkBox" + liCount + "'>";
    li += "<span>" + subtaskTitle + "</span>";
    li += "</label>";
    li += "<i class='icon wb-trash subtaskDeleteBtn subtask-action-button' id='deleteSubtaskBtn" + liCount + "' style='float:right'></i>";
    li += "<span class='subtask-action-button' data-toggle='modal' data-target='#subtaskOverviewModal'>";
    li += "<span class='icon wb-list' data-toggle='tooltip' data-placement='left' data-trigger='hover' data-original-title='Subtask Overview'></span>";
    li += "</span>";
    li += "</div>";
    li += "</li>";

    //add snippet to list, clear the modal and add click event on delete subtask button
    $(".subtasks").prepend(li);

    //add log about subtask being added
    logTaskChange(subtaskTitle, date, "added")

    //clear the modal after adding subtask
    $("#newSubtaskTitle").val("");
    $("#addSubtaskModal").modal('hide');

    //add on click event to created subtask
    $("#deleteSubtaskBtn" + liCount + "").click(deleteSubtask);
    $("#checkBox" + liCount + "").click(checkSubtask);
    alertify.success("Subtask " + subtaskTitle + " added")
}

//delete subtask and make a log
function deleteSubtask() {
    //get all needed data
    var clicked = $(this);
    subtaskTitle = clicked.siblings("label").children().html();
    var date = jQuery.format.date(new Date(), "dd/MM/yyyy HH:mm:ss");
    alertify.confirm("Are you sure you want to delete this subtask", function (e) {
        if (e) {
            //remove the subtask
            clicked.parent().parent().remove();
            //add log about subtask being deleted
            logTaskChange(subtaskTitle, date, "deleted");
            alertify.success("Subtask " + subtaskTitle +" deleted");
        } else {
            alertify.error("Canceled");
            return;
        }
    });
}

//check or uncheck Subtask and make a log
function checkSubtask() {
    //get all of needed data
    var clicked = $(this);
    var li = clicked.parent().parent();
    subtaskTitle = clicked.siblings("label").children().html();
    var date = jQuery.format.date(new Date(), "dd/MM/yyyy HH:mm:ss");

    //check if subtask is already checked
    if (li.hasClass("task-done")) {
        logTaskChange(subtaskTitle, date, "unchecked");
        alertify.success("Subtask " + subtaskTitle + " unchecked");
    }
    else {
        logTaskChange(subtaskTitle, date, "marked as completed");
        alertify.success("Subtask " + subtaskTitle + " marked as completed");
    }
}

//adds log to task logs
function logTaskChange(subtaskTitle, date, action) {
    var log = "<li class='list-group-item bg-blue-grey-100 task-logs-item'>";
    log += "<p class='list-group-item-heading font-size-16'>Me</p>";
    log += "<p class='list-group-item-text'> Subtask " + subtaskTitle + " " + action + " </p>";
    log += "<p>" + date + "</p>";
    log += "</li>";
    $(".task-logs").prepend(log);
}
//create new comment
function addComment() {
    //get needed data
    var date = jQuery.format.date(new Date(), "dd/MM/yyyy HH:mm:ss");
    var textarea = $(".comment-text-area");
    var comment = textarea.val();
    var avatar = $(".my-avatar").attr("src");
    //check if textarea empty - if empty return
    if (comment == "")
        return;
    //create comment snippet
    var li = "<li class='list-group-item bg-blue-grey-100 my-comment'>";
    li += "<div class='comment media'>";
    li += "<div class='media-left'>";
    li += "<div class='avatar avatar-sm'><img src='"+ avatar +"' /></div>";
    li += "</div>";
    li += "<div class='comment-body media-body media-left'>";
    li += "<div class='comment-author'>Me</div>";
    li += "<div class='comment-meta'><span class='date'>" + date + "</span></div>";
    li += "<div class='comment-content'>";
    li += "<p>" + comment + "</p>";
    li += "</div></div></div></li>";

    //clear area and add comment to list
    textarea.val("");
    $(".comment-list").prepend(li);
}