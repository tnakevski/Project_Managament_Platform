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

    $(".task-change-title-btn").on("click", function () {
        var inputVal = $(".task-change-title-input").val();
        if (inputVal == "" || inputVal == null) {
            alertify.error("Input field is empty, Please enter new name and click Save");
            return;
        }

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


}