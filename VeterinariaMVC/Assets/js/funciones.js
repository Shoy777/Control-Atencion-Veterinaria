$.validator.methods.number = function (value, element) {
    value = floatValue(value);
    return this.optional(element) || !isNaN(value);
}
$.validator.methods.range = function (value, element, param) {
    value = floatValue(value);
    return this.optional(element) || (value >= param[0] && value <= param[1]);
}
function floatValue(value) {
    return parseFloat(value.replace(",", "."));
}

function readURL(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();
        reader.onload = function (e) {
            $('#preViewImg').attr('src', e.target.result);
        }

        reader.readAsDataURL(input.files[0]);
    }
}

$(document).on('change', '#imagen', function () {
    readURL(this);
});