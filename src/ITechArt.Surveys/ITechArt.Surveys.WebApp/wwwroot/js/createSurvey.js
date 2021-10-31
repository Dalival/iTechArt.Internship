$('#questions-menu').children().each(function () {
    $(this).click(function () {
        $('#questions-counter').html(function (i, val) {
            return val * 1 + 1
        });
    })
})

$('#create-text-question').click(function () {
    const textQuestionHTMLCode = "<div>THIS IS A TEXT QUESTION</div>";
    $('#survey-content').append(textQuestionHTMLCode);
})
