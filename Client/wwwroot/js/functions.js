function focusFirstInput(element) {
    var firstInput = element.querySelector('input');
    if (firstInput) {
        firstInput.focus();
    }
}
function focusElement(element) {
    console.log(element)
    if (element) {
        element.focus();
    }
}