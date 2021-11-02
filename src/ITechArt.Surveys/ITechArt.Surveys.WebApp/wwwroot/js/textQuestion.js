$(document).click(function (e) {
    const target = e.target;
    if (!$(target).is('.question-edit-mode') &&
        !$(target).parents().is('.question-edit-mode') &&
        !$(target).is('span[id^=edit-question-]') &&
        !$(target).parents().is('span[id^=edit-question-]')) {
        alert('switch to view mode');
        $('.question-edit-mode').hide();
        $('.question-view-mode').show();
    }
});

$('span[id^=edit-question-]').click(function () {
    const id = $(this).attr('id');
    const index = id.substring(id.lastIndexOf('-') + 1);
    alert(`switch to edit mode at question ${index}`);
    $('#question-' + index + ' > .question-view-mode').hide();
    $('#question-' + index + ' > .question-edit-mode').show();
});

$('input[id^=Questions_][id$=__Title]').change(function () {
    let enteredText = $(this).val();
    const id = $(this).attr('id');
    const collection = id.split('_');
    const index = collection[1];
    if(enteredText === '') {
        enteredText = `Question ${index}`;
    }
    $('#question-' + index + '-title').html(enteredText);
})

$('textarea[id^=Questions_][id$=__Description]').change(function () {
    const enteredText = $(this).val();
    const id = $(this).attr('id');
    const collection = id.split('_');
    const index = collection[1];
    $('#question-' + index + '-description').html(enteredText);
})
