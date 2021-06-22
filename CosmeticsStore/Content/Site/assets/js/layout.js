// Btn show navbar
var menuBtn = document.querySelector('.navbar-icon')
var myMenu = document.querySelector('.header__nav')
menuBtn.onclick = function () {
    myMenu.classList.toggle('show')
    menuBtn.classList.toggle('open')
}


// Go to Top
function goToTop() {
    var timer = setInterval(function () {
        document.documentElement.scrollTop -= 20
        if (document.documentElement.scrollTop <= 0) {
            clearInterval(timer)
        }
    }, 10)
}
