$(document).ready(function () {
    //set datepicker for projcet date changing
    var projectChangeDatepicker = $(".project-date-change");
    projectChangeDatepicker.datetimepicker();
    projectChangeDatepicker.data("");
    projectChangeDatepicker.data("DateTimePicker").minDate(new Date());
    projectChangeDatepicker.data("DateTimePicker").date(null);

    //set date picker for adding new task
    var taskAddDatepicker = $('#taskAddDate');
    taskAddDatepicker.datetimepicker();
    var projectDueDate = $(".project-due-date").html();
    taskAddDatepicker.data("DateTimePicker").minDate(new Date());
    //set the maximum due date for the task to be as projects due date
    var date = new Date(projectDueDate);
    date.setDate(date.getDate() + 1);
    taskAddDatepicker.data("DateTimePicker").maxDate(new Date(date));
    taskAddDatepicker.data("DateTimePicker").date(null);

    //parameter object used to send new data to controller for changing the project settings
    var changeProject = {
        id: 0,
        title: "",
        dueDate: "",
        description: "",
    }

    //change project title
    $(".change-project-title").on("click", function () {
        changeProject.title = $(".project-new-title").val();
        //check if input empty
        if (changeProject.title == "" || changeProject.title == null) {
            alertify.error("Input field is empty, Please enter new name and click Save");
            return;
        }      
        alertify.confirm("Are you sure you want to change project name", function (e) {
            if (e) {
                //get project id
                changeProject.id = $(".project-id").html();
                //make changes on server side using ajax post 
                $.post("/Project/ChangeProjectTitle", changeProject, function () {
                    //change titles in view
                    $(".project-heading-title").html(changeProject.title);
                    $(".project-list-title-" + changeProject.id + "").html(changeProject.title);
                    //alert success
                    alertify.success("Name of project changed");
                    //clear input
                    $(".project-new-title").val("");
                });
            } else {
                alertify.error("Canceled");
                $(".project-new-title").val("");
                return;
            }
        });

    })

    //change project date
    $(".change-project-date-btn").on("click", function () {
        var newDate = $(".change-project-date-input").val();
        //check if input is empty and alert
        if (newDate == "" || newDate == null) {
            alertify.error("Input field is empty, Please enter new date and click Save");
            return;
        }
        alertify.confirm("Are you sure you want to change project due date", function (e) {
            if (e) {
                //get ready the parameter object by adding id of project that needs to be changed and new due date
                changeProject.id = $(".project-id").html();
                changeProject.dueDate = newDate;
                //make Ajax post to make the changes on server side
                $.post("/Project/ChangeProjectDate", changeProject, function () {
                    //change due date in view
                    $(".project-due-date").html(changeProject.dueDate);
                    //alert success
                    alertify.success("Due Date of project changed");
                    //clear input
                    $(".change-project-date-input").val("");
                });
            } else {
                alertify.error("Canceled");
                //clear input
                $(".change-project-date-input").val("");
                return;
            }
        });
    })

    //change project descrtipion
    $(".change-project-description-btn").on("click", function () {
        var newDescription = $(".change-project-description-input").val();
        if (newDescription == "" || newDescription == null) {
            alertify.error("Input field is empty, Please enter new description and click Save");
            return;
        }
        alertify.confirm("Are you sure you want to change project description", function (e) {
            if (e) {
                //get ready the parameter object
                changeProject.id = $(".project-id").html();
                changeProject.description = newDescription;
                $.post("/Project/ChangeProjectDescription", changeProject, function () {
                    $(".project-description").html(changeProject.description);
                    alertify.success("Description of project changed");
                    $(".change-project-description-input").val("");
                });
            } else {
                alertify.error("Canceled");
                //clear input
                $(".change-project-description-input").val("");
                return;
            }
        });

    })

    //fills the modal wiht list of all users available to assign to the project
    $(".assign-to-project-modal").on("click", function () {
        var projectId = $(".project-id").html();
        var userToAssignList = $(".user-to-assign-list");
        userToAssignList.html("");
        $.get("/Project/GetUsersToAssign", { id: projectId }, function (data) {
            if (data.length == 0) {
                //if there are not pepople return 
                userToAssignList.append("There are no avaiable people to be assigned");
                return;
            }
            for (var i = 0; i < data.length; i++) {
                //get snippet for user list in modal 
                var snippet = UserToAssignSnippet(data[i].FullName, data[i].Username, data[i].Avatar, data[i].Id);
                //adds the li to list
                userToAssignList.append(snippet);
                //adds click event to mark selcted user 
                $("#user-assign-" + data[i].Id + "").click(selectToAssign);
            }
        })
    })

    //assing pepople to project
    $(".assing-project-btn").on("click", function () {
        var idArray = [];
        var projectId = $(".project-id").html();
        // goes through all selected users that needs to be assigned to project and gets their Id's 
        $(".user-to-assign-list .user-assign-selected").each(function () {
            var selectedId = $(this).attr('id');
            //I need to split the Id since it is in format user-assign- "id"
            //And then get the last element of the array
            selectedId = selectedId.split("-");
            selectedId = selectedId[selectedId.length - 1];
            idArray.push(selectedId);
        })

        $.ajaxSettings.traditional = true;
        //Make the post method to save the assigned users to DB 
        $.get("/Project/AssignUserProject", { userIds: idArray, projectId: projectId }, function (data) {
            for (var i = 0; i < idArray.length ; i++) {
                $("#user-assign-" + idArray[i] + "").remove();
            }
            for (var i = 0; i < data.length; i++) {
                var snippet = AssignedUserSnippet(data[i].FullName, data[i].Avatar);
                $("#PeopleAssigned").append(snippet);
            }
            alertify.success("Assigned")
        })
    })

    //delete project 
    $(".delete-project-btn").on("click", function () {
        alertify.confirm("Are you sure you want to delete this project", function (e) {
            if (e) {
                var projectId = $(".project-id").html();
                $.post("/Project/DeleteProject", { id: projectId }, function (data) {
                    window.location.href = data;
                })
            } else {
                alertify.error("Canceled");
                return;
            }
        });
    })

    //add new task to project
    $(".add-new-task").on("click", function () {
        var $title = $(".new-task-title");
        var $description = $(".new-task-description");
        var $date = $(".new-task-date");
        var projectId = $(".project-id").html();
        //check if some input is empty
        if ($title.val() == "" || $description.val() == "" || $date.val() == "") {
            alertify.error("All fields are required")
            return;
        }
        $.post("/Task/CreateTask", { Title: $title.val(), Description: $description.val(), DueDate: $date.val(), ProjectId: projectId }, function (data) {
            var snippet = AddNewTaskSnippet($title.val(), data, "all");
            //add task to all tasks list
            $(".task-list-all").append(snippet);
            //add task to pending tasks list
            snippet = AddNewTaskSnippet($title.val(), data, "pending");
            $(".task-list-pending").append(snippet);
            //attach click event listener on added task
            //openTask function is implemented in panel.js file
            $(".task-list-" + data + "").click(openTask);
            alertify.success("Task " + $title.val() + " successfully added to project")
            //clear inputs
            $title.val("");
            $description.val("");
            $date.val("");
            $('#addNewTask').modal('hide');
        })
    })
})

//creates task snippet for the task list panel 
function AddNewTaskSnippet(title,id,list) {
    var li = '<li class="list-group-item taskItem task-list-' + id + '" id="task-'+list+'-'+id+'" style="border-radius:0px;">';
    li += '<span class="taskName">' + title + '</span>';
    li += '<span class="label label-danger pull-right">Pending</span>';
    li += '</li>';
    return li;
}

//creates the list snippet for users that can be assigned to project
function UserToAssignSnippet(fullname, username, avatar, id)
{
    var li = '<li class="list-group-item user-assign" id="user-assign-'+id+'">';
    li += '<div class="media">';
    li += '<div class="media-left">';
    li += '<a class="avatar" href="javascript:void(0)">';
    li += '<img class="img-responsive" src="/Avatars/'+avatar+'" alt="..." style="width:40px; height:40px">';
    li += '</a>';
    li += '</div>';
    li += '<div class="media-body">';
    li += '<h4 class="media-heading">'+fullname+'</h4>';
    li += '<small>'+username+'</small>';
    li += '</div>';
    li += '</div>';
    li += '</li>';
    return li;
}

//creates snippet for users assigned to project
function AssignedUserSnippet(fullname, avatar) {
    var li = '<div class="col-md-1">';
    li += '<span class="avatar" data-toggle="tooltip" data-placement="right" data-trigger="hover" data-original-title="'+fullname+'">';
    li += '<img class="my-avatar" src="/Avatars/'+avatar+'" style="width:40px; height:40px;">';
    li += '<i></i>';
    li += '</span>';
    li += '</div>';
    return li;
}

//toggles class to selected user for assignation
//this class is crucials for assigning users to project 
//all users with this class are assigned 
function selectToAssign() {
    $(this).toggleClass("user-assign-selected");
}