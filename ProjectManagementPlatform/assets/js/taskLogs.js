
//adds log to task logs
function logTaskChange(kind, Title, date, action) {
    var taskId = $(".task-id").html();
    var log = "<li class='list-group-item bg-blue-grey-100 task-logs-item'>";
    log += "<p class='list-group-item-heading font-size-16'>Me</p>";
    log += "<p class='list-group-item-text'> "+ kind + " " + Title + " " + action + " </p>";
    log += "<p>" + date + "</p>";
    log += "</li>";
    var description =""+ kind + " " + Title + " " + action+"";
    $.post("/LogsAndComments/AddLog", { taskId: taskId, description: description }, function () {
        $(".task-logs").prepend(log);
    })
}