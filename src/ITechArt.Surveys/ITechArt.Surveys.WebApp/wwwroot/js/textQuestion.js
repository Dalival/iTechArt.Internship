// On click somewhere else view view-mode
$(document).click(function (e) {
    const target = e.target;
    const questionsCount = $('.question').length;
    for (let i = 0; i < questionsCount; i++) {
        if (!$(target).is(`#question-${i}`) &&
            !$(target).parents().is(`#question-${i}`)) {
            $(`#question-${i}-edit-mode`).hide();
            $(`#question-${i}-view-mode`).show();
        }
    }
});

// View edit-mode on click edit button
$('span[id^=edit-question-]').click(function () {
    const id = $(this).attr('id');
    const index = id.substring(id.lastIndexOf('-') + 1);
    $('#question-' + index + ' > .question-view-mode').hide();
    $('#question-' + index + ' > .question-edit-mode').show();
});

// Fill view-mode on changing edit-mode
$('input[id^=Questions_][id$=__Title]').change(function () {
    let enteredText = $(this).val();
    const id = $(this).attr('id');
    const collection = id.split('_');
    const index = collection[1];
    if (enteredText.trim() === '') {
        const normalizedIndex = Number(index) + 1;
        enteredText = 'Question ' + normalizedIndex;
    }
    $('#question-' + index + '-title').html(enteredText);
})

// Delete question on click delete button
$('span[id^=delete-question-]').click(function () {
    const id = $(this).attr('id');
    const index = id.substring(id.lastIndexOf('-') + 1);
    $('#question-' + index).remove();
    // decrement questions counter
    $('#questions-counter').html($('.question').length);
});
