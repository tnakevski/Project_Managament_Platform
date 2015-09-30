$(document).ready(function () {
    var changeTitle = {
        id: 0,
        title : "",
    }
    $(".change-project-title").on("click", function () {
        changeTitle.title = $(".project-new-title").val();
        //check if input empty
        if (changeTitle.title == "" || changeTitle.title == null) {
            alertify.error("Input field is empty, Please enter new name and click Save");
            return;
        }
        changeTitle.id = $(".project-id").html();
        $.post("/Dashboard/ChangeProjectTitle", changeTitle, function () {
            $(".project-heading-title").html(changeTitle.title);
            $(".project-list-title-"+changeTitle.id+"").html(changeTitle.title);            
            alertify.success("Name of project changed");
            $(".project-new-title").val("");
        });
    })
})