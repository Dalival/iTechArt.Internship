// Update information in view-mode on modifying question from edit-mode
// $('input[id^=Questions_][id$=__Title]').onchange(function () {
//     var enteredText = $(this).val();
//     var index = $(this).id().split('_').eq(1);
//     $('#question-' + index + '-title').html(enteredText);
// })

// Update information in view-mode on modifying question from edit-mode
// РАААААБОООЧИИИЙ ВАРИАААААНТ
$('input[id^=Questions_][id$=__Title]').change(function () {
    alert('onchange');
    var enteredText = $(this).val();
    var id = $(this).attr('id');
    var collection = id.split('_');
    var index = collection[1];
    $('#question-' + index + '-title').html(enteredText);
})

// $('input[id^=Questions_][id$=__Title]').onkeyup(function() {
//     var index = $(this).id().split('_').eq(1);
//     var value = this.val();
//     $('#question-' + index + '-title').text(value);
// })
