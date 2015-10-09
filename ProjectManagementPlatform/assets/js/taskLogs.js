
//adds log to task logs
function logTaskChange(kind ,Title, date, action) {
    var log = "<li class='list-group-item bg-blue-grey-100 task-logs-item'>";
    log += "<p class='list-group-item-heading font-size-16'>Me</p>";
    log += "<p class='list-group-item-text'> "+ kind + " " + Title + " " + action + " </p>";
    log += "<p>" + date + "</p>";
    log += "</li>";
    $(".task-logs").prepend(log);
}