function setTaskSettings() {
    var projectDueDate = $(".project-due-date").html();
    //set date picker for task settings
    var taskDatepicker = $('#taskDateChange');
    taskDatepicker.datetimepicker();
    taskDatepicker.data("DateTimePicker").minDate(new Date());
    taskDatepicker.data("DateTimePicker").date(null);
    //set max due date to be same as projects due date
    var date = new Date(projectDueDate);
    date.setDate(date.getDate() + 1);
    taskDatepicker.data("DateTimePicker").maxDate(new Date(date));
    taskDatepicker.data("DateTimePicker").date(null);

    //change title of current task
    $(".task-change-title-btn").on("click", function () {
        var inputVal = $(".task-change-title-input").val();
        //check if input is empty
        if (inputVal == "" || inputVal == null) {
            alertify.error("Input field is empty, Please enter new name and click Save");
            return;
        }
        //ask if user is sure about name change
        alertify.confirm("Are you sure you want to change task name", function (e) {
            if (e) {
                //get task id
                var taskId = $(".task-id").html();
                var currentTitle = $("#taskTitle").html();
                //get date for log
                var date = jQuery.format.date(new Date(), "dd/MM/yyyy HH:mm:ss");
                //make changes on server side using ajax post 
                $.post("/Task/ChangeTaskTitle", { Id: taskId, title: inputVal }, function (result) {                
                    //Create log for change made to the task
                    logTaskChange("Task's", currentTitle, date, "name changed");
                    //change titles in view
                    $(".taskItem.active .taskName").html(inputVal);
                    $("#taskTitle").html(inputVal);
                    //alert success
                    alertify.success("Name of task changed");
                    //clear input
                    $(".task-change-title-input").val("");
                });
            } else {
                alertify.error("Canceled");
                $(".task-change-title-input").val("");
                return;
            }
        });
    });
    //change current tasks status
    $(".task-change-status-btn").on("click", function () {
        var input = $(".task-change-status-input").val();
        var currentStatus = $(".task-current-status").html();
        //check if status is already set
        if (input == currentStatus) {
            alertify.error("Task status is already set to "+ currentStatus + "");
            return
        }
        alertify.confirm("Are you sure you want to change task status", function (e) {
            if (e) {
                //get task id
                var taskId = $(".task-id").html();
                var currentTitle = $("#taskTitle").html();
                //get date for log
                var date = jQuery.format.date(new Date(), "dd/MM/yyyy HH:mm:ss");
                //make changes on server side using ajax post 
                $.post("/Task/ChangeTaskStatus", { Id: taskId, status: input }, function (result) {
                    //change status in view
                    $(".task-current-status").html(input);
                    $(".taskItem.active span:last-child").remove();
                    switch (input) {
                        case "Pending":
                            $(".taskItem.active").append("<span class='label label-danger pull-right'>Pending</span>")
                            break;
                        case "InProgress":
                            $(".taskItem.active").append("<span class='label label-warning pull-right'>InProgress</span>")
                            break;
                        case "Done":
                            $(".taskItem.active").append("<span class='label label-success pull-right'>Done</span>")
                            break;
                        default:
                            alertify.error("Something went wrong, please try again");
                            return;
                            break;
                    }
                    //Create log for change made to the task
                    logTaskChange("Task's", currentTitle, date, "status changed");
                    //alert success
                    alertify.success("Status of task changed");
                });
            } else {
                alertify.error("Canceled");
                return;
            }
        });
    })

    //change current task due date
    $(".task-change-date-btn").on("click", function () {
        var input = $(".task-change-date-input").val();
        if (input == "" || input == null) {
            alertify.error("Input field is empty")
            return;
        }
        alertify.confirm("Are you sure you want to change task due date", function (e) {
            if (e) {
                //get task id
                var taskId = $(".task-id").html();
                var currentTitle = $("#taskTitle").html();
                //get date for log
                var currentDate = jQuery.format.date(new Date(), "dd/MM/yyyy HH:mm:ss");
                //make changes on server side using ajax post 
                $.post("/Task/ChangeTaskDate", { Id: taskId, date: input }, function (result) {
                    //change due date in view
                    input = jQuery.format.date(input, "DD/MMMM/yyyy");
                    $(".task-due-date").html("Due Date: " + input + "");
                    //Create log for change made to the task
                    logTaskChange("Task's", currentTitle, currentDate, "date changed");
                    //alert success
                    alertify.success("Date of task changed");
                });
            } else {
                alertify.error("Canceled");
                return;
            }
        });
    })

    $(".task-delete-btn").on("click", function () {
        alertify.confirm("Are you sure you want to delete this task", function (e) {
            if (e) {
                taskPanelOut();
                var taskId = $(".task-id").html();
                setTimeout(function () {
                    $.get("/Task/DeleteTask", { Id: taskId }, function (data) {
                        $(".task-overview").html(data);
                        $(".taskItem.active").remove();
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
            } else {
                alertify.error("Canceled");
                return;
            }
        });

    })

    $(".assign-to-task-modal").on("click", function () {
        var taskId = $(".task-id").html();
        var projectId = $(".project-id").html();
        var userToAssignList = $(".user-to-assign-list-task");
        userToAssignList.html("");
        $.get("/Task/GetUsersToAssign", { taskId: taskId, projectId: projectId }, function (data) {
            if (data.length == 0) {
                //if there are not people return 
                userToAssignList.append("There are no avaiable people to be assigned");
                return;
            }
            for (var i = 0; i < data.length; i++) {
                //get snippet for user list in modal 
                var snippet = UserToAssignTaskSnippet(data[i].FullName, data[i].Username, data[i].Avatar, data[i].Id);
                //adds the li to list
                userToAssignList.append(snippet);
                //adds click event to mark selcted user 
                $("#user-assign-task-" + data[i].Id + "").click(selectToAssignTask);
            }
        })
    })

    $(".assing-task-btn").on("click", function () {
        var idArray = [];
        var projectId = $(".project-id").html();
        var taskId = $(".task-id").html();
        // goes through all selected users that needs to be assigned to project and gets their Id's 
        $(".user-to-assign-list-task .user-assign-selected").each(function () {
            var selectedId = $(this).attr('id');
            //I need to split the Id since it is in format user-assign- "id"
            //And then get the last element of the array
            selectedId = selectedId.split("-");
            selectedId = selectedId[selectedId.length - 1];
            idArray.push(selectedId);
        })

        $.ajaxSettings.traditional = true;
        //Make the post method to save the assigned users to DB 
        $.get("/Task/AssignUserTask", { userIds: idArray, projectId: projectId, taskId: taskId }, function (data) {
            for (var i = 0; i < idArray.length ; i++) {
                $("#user-assign-task-" + idArray[i] + "").remove();
            }
            for (var i = 0; i < data.length; i++) {
                var snippet = AssignedUserTaskSnippet(data[i].FullName, data[i].Avatar);
                $(".assigned-users-task").append(snippet);
            }
            alertify.success("Assigned")
        })
    })
    function UserToAssignTaskSnippet(fullname, username, avatar, id) {
        var li = '<li class="list-group-item user-assign-task" id="user-assign-task-' + id + '">';
        li += '<div class="media">';
        li += '<div class="media-left">';
        li += '<a class="avatar" href="javascript:void(0)">';
        li += '<img class="img-responsive" src="/Avatars/' + avatar + '" alt="..." style="width:40px; height:40px">';
        li += '</a>';
        li += '</div>';
        li += '<div class="media-body">';
        li += '<h4 class="media-heading">' + fullname + '</h4>';
        li += '<small>' + username + '</small>';
        li += '</div>';
        li += '</div>';
        li += '</li>';
        return li;
    }

    //creates snippet for users assigned to project
    function AssignedUserTaskSnippet(fullname, avatar) {
        var li = '<div class="col-md-1">';
        li += '<span class="avatar" data-toggle="tooltip" data-placement="right" data-trigger="hover" data-original-title="' + fullname + '">';
        li += '<img class="my-avatar" src="/Avatars/' + avatar + '" style="width:40px; height:40px;">';
        li += '<i></i>';
        li += '</span>';
        li += '</div>';
        return li;
    }

    function selectToAssignTask() {
        $(this).toggleClass("user-assign-selected");
    }
}

