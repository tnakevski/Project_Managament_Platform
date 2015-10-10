function setTaskOverview () {
    var subtaskTitle;
    //add on click events on all of the buttons
    $(".check-subtask-done").click(checkSubtask);
    $(".add-comment").click(addComment);


    //set enter keypress for comment text area 
    $('.comment-text-area').keypress(function (e) {
        if (e.keyCode == 13 && !e.shiftKey) {
            e.preventDefault();
            addComment();
        }
    });


    $(".subtask-overview-btn").on("click", function () {
        $(".subtask-overview-description").text("");
        var clickedId = $(this).attr("id");
        clickedId = clickedId.split("-");
        clickedId = clickedId[clickedId.length - 1];
        $.get("/Subtask/SubtaskDescription",{Id:clickedId}, function (data) {
            $(".subtask-overview-description").text(data);
        });
    })

    //check or uncheck Subtask and make a log
    function checkSubtask() {
        //get all of needed data
        var clicked = $(this);
        var li = clicked.parent().parent();
        var clickedId = $(this).attr("id");
        clickedId = clickedId.split("-");
        clickedId = clickedId[clickedId.length - 1];
        subtaskTitle = clicked.siblings("label").children().html();
        var date = jQuery.format.date(new Date(), "dd/MM/yyyy HH:mm:ss");

        //check if subtask is already checked
        if (li.hasClass("task-done")) {
            $.post("/Subtask/StatusFalse", { Id: clickedId }, function () {
                logTaskChange("Subtask", subtaskTitle, date, "unchecked");
                alertify.success("Subtask " + subtaskTitle + " unchecked");
            })
        }
        else {
            $.post("/Subtask/StatusTrue", { Id: clickedId }, function () {
                logTaskChange("Subtask", subtaskTitle, date, "marked as completed");
                alertify.success("Subtask " + subtaskTitle + " marked as completed");
            })
        }
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
        var taskId = $(".task-id").html();
        $.post("/LogsAndComments/AddComment", { taskId: taskId, comment: comment }, function () {
            //create comment snippet
            var li = "<li class='list-group-item bg-blue-grey-100 my-comment'>";
            li += "<div class='comment media'>";
            li += "<div class='media-left'>";
            li += "<div class='avatar avatar-sm'><img src='" + avatar + "' /></div>";
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
        })     
    }
};

