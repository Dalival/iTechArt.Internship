var questionCreationButtons = Array.from(document.getElementById('questions-menu').children);
for (const button of questionCreationButtons) {
    $(button).click(function () {
        $('#questions-counter').html(function (i, val) {
            return val * 1 + 1
        });
    })
}
