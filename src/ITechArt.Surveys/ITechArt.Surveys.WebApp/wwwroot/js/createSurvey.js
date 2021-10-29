for (const button of $('#questions-menu').children) {
    $(button).click(function () {
        $('#questions-counter').html(function (i, val) {
            return val * 1 + 1
        });
    })
}
