// Tăng giảm input
var value = parseInt(document.querySelector('.input-qty').value, 10);
var maxProduct = document.querySelector('.input-qty').getAttribute('max')

function minusProduct() {
    if (value > 0) {
        value = isNaN(value) ? 1 : value;
        value--;
    }

    document.querySelector('.input-qty').value = value;
}

function plusProduct() {
    value = isNaN(value) ? 0 : value;
    value++;
    if (value > maxProduct) {
        value = maxProduct;
    }
    document.querySelector('.input-qty').value = value;
}