$(".table tr th div.checkbox input").click(function () {
    $(".table tr td div.checkbox input").prop('checked', $(this).prop('checked'));
});

$(".table tr td div.checkbox input").click(function () {
    let allChecked = $('.table tr td div.checkbox input').toArray().every((item, index, arr) => $(item).prop('checked'));
    $('.table tr th div.checkbox input').prop('checked', allChecked);
    let sel = 'div.modal' + $(this).attr('data-target') + ' .modal-body input';
    $(sel).prop('disabled', !$(this).prop('checked'));
});

var duplicates =
    $('tr.duplicate')
    .toArray()
    .map(item => ({
        value: item,
        id: $(item).attr('data-id')
    }));

groupBy(duplicates, item => [
    item.id
])
.forEach(group => {
    var color = randomColor();
    group.forEach(item => $(item.value).css('background-color', color));
});

function groupBy(array, callback) {
    var groups = {};

    array.forEach(item => {
        var group = JSON.stringify(callback(item));
        groups[group] = groups[group] || [];
        groups[group].push(item);
    });

    return Object.keys(groups).map(group => groups[group]);
}