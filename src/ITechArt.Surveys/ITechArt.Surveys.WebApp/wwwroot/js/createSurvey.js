// Prevent submitting form on Enter
$('#survey-form').on('keyup keypress', function(e) {
    var keyCode = e.keyCode || e.which;
    if (keyCode === 13 && !$(e.target).is('textarea')) {
        e.preventDefault();
        return false;
    }
});

// Increment question counter on creating new question
$('#questions-menu').children().each(function () {
    $(this).click(function () {
        $('#questions-counter').html(function (i, val) {
            return val * 1 + 1
        });
    })
})

// Insert new question html code on click creating new question
$('#create-text-question').click(function () {
    var i = $('.question').length;
    $.ajax({
        url: 'AddQuestion?index=' + i,
        cache: false,
        success: function (data) {
            $('#survey-content').append(data);
        },
        error: function (a, b, c) {
            alert(a + ' ' + b + ' ' + c);
        }
    })
})
