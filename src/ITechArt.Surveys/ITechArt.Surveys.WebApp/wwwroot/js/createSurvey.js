// Prevent submitting form on Enter
$('#survey-form').on('keyup keypress', function(e) {
    const keyCode = e.keyCode || e.which;
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
    const index = $('.question').length;
    const questionHTMLcode = `<div class="question" id="question-@Model.Index">

        <input type="hidden"
               id="Questions_${index}__Index"
               name="Questions[${index}].Index"
               value="${index}"/>

        <div class="question-edit-mode" id="question-${index}-edit-mode">
            <input class="question-title"
                   id="Questions_${index}__Title"
                   name="Questions[${index}].Title"
                   type="text"
                   placeholder="Title"/>
        </div>

        <div class="question-view-mode" id="question-${index}-view-mode">
            <div class="question-title" id="question-${index}-title">
                Question @(Model.Index + 1)
            </div>
            <span class="material-icons-outlined" id="edit-question-${index}">edit</span>
            <span class="material-icons-outlined" id="delete-question-${index}">delete</span>
        </div>
    </div>`
    $('#survey-content').append(questionHTMLcode);
})


/*/////////////// FROM TEXT QUESTION ///////////////*/


// On click somewhere else view view-mode
$(document).click(function (e) {
    const target = e.target;
    const questionsCount = $(document.getElementsByClassName('question')).length; // THIS WORKS!!!! DO EVERYWHERE LIKE HERE!!!
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
