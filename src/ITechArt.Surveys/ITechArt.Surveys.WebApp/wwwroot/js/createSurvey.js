$('#questions-menu').children().each(function () {
    $(this).click(function () {
        $('#questions-counter').html(function (i, val) {
            return val * 1 + 1
        });
    })
})

// $('#create-text-question').click(function () {
//     const textQuestionHTMLCode = "<div>THIS IS A TEXT QUESTION</div>";
//     $('#survey-content').append(textQuestionHTMLCode);
// })

$('#create-text-question').click(function () {
    var i = $('.thingRow').length;
    $.ajax({
        url: 'AddQuestion?index=' + i,
        cache: false,
        success: function (data) {
            $('#survey-content').append(data);
        },
        error: function (a, b, c) {
            alert(a + " " + b + " " + c);
        }
    })
})
