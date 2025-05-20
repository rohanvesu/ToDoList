const apiToken = 'd14ecd26-a300-4c1a-af0f-b484c1ee7ab9';

$(document).ready(function () {
    $('#todoForm').on('submit', function (e) {
        e.preventDefault();

        const todo = {
            title: $('#title').val(),
            description: $('#description').val(),
            startDate: $('#startDate').val(),
            endDate: $('#endDate').val()
        };

        $.ajax({
            url: '/Todo/Add',
            method: 'POST',
            headers: { 'APIToken': apiToken },
            data: {
                todos: JSON.stringify(todo)
            },
            success: function () {
                alert('To-Do added!');
                location.reload();
            },
            error: function (xhr) {
                alert('Error: ' + xhr.responseText);
            }
        });
    });

    $('.mark-complete').on('click', function () {
        const id = $(this).data('id');

        $.ajax({
            url: `/Todo/Complete?id=${id}`,
            method: 'PUT',
            headers: { 'APIToken': apiToken },
            contentType: 'application/json',
            data: JSON.stringify({ isCompleted: true }),
            success: function () {
                alert('Marked as completed!');
                location.reload();
            },
            error: function (xhr) {
                alert('Error: ' + xhr.responseText);
            }
        });
    });
});
