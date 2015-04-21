$(function () {

    $('#submitMessageBtn').on('click', function (e) {
        e.preventDefault();

        var subject = $('#').val();
        var message = $('#').val();
        var PostReceiverId = '';
    });

    $("#reviewTable").hide();

    $("#reviewTableHeader").on('click', function () {
        var that = $(this);

        $("#reviewTable").toggle('fast', function () {
            if ($(this).is(':visible')) {
                that.html('<span class="glyphicon glyphicon-circle-arrow-up"></span> Reviews');
            }
            else {
                that.html('<span class="glyphicon glyphicon-circle-arrow-down"></span> Reviews');
            }
        });
    });
})