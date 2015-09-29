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
