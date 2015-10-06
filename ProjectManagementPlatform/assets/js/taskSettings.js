$(function () {

    //set date picker for task settings
    var taskDatepicker = $('#taskDateChange');
    taskDatepicker.datetimepicker();
    taskDatepicker.data("DateTimePicker").minDate(new Date());
    taskDatepicker.data("DateTimePicker").date(null);

    //set enter keypress for text area 
    $('.comment-text-area').keypress(function (e) {
        if (e.keyCode == 13 && !e.shiftKey) {
            e.preventDefault();
            addComment();
        }
    });
});
