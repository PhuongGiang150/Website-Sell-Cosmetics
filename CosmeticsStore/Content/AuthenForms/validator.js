function Validator(options) {

    function getParent(element, selector) {
        while (element.parentElement) {
            if (element.parentElement.matches(selector)) {
                return element.parentElement
            }
            element = element.parentElement
        }
    }
    var selectorRules = {};
    // Hàm thực hiện validate
    function validate(inputElement, rule) {
        var errorMessage
        var errorElemet = getParent(inputElement, options.formGroup).querySelector(options.errorSelector)
        var rules = selectorRules[rule.selector]
        lengthRule = rules.length
        for (var i = 0; i < lengthRule; i++) {
            switch (inputElement.type) {
                case 'radio':
                case 'checkbox':
                default:
                    errorMessage = rules[i](inputElement.value)
            }
            if (errorMessage) {
                break;
            }
        }
        if (errorMessage) {
            errorElemet.innerText = errorMessage
            inputElement.parentElement.classList.add('invalid')
        } else {
            errorElemet.innerText = ''
            inputElement.parentElement.classList.remove('invalid')

        }
        return !!errorMessage
    }
    var formElement = document.getElementById(options.form)
    if (formElement) {
        formElement.onsubmit = function(e) {
            // bỏ thao tác mặc định
            e.preventDefault()
            var isFormValid = true
                //lặp qua từng rule và validate
            options.rules.forEach((rule) => {
                var inputElement = formElement.querySelector(rule.selector)
                var isValid = validate(inputElement, rule)
                if (isValid) {
                    isFormValid = false
                }
            })
            if (isFormValid) {
                // Submit với javascript
                if (typeof options.onsubmit === 'function') {
                    var enabledInput = formElement.querySelectorAll('[name]:not([disabled])')
                    var formValues = Array.from(enabledInput).reduce((values, input) => {
                        values[input.name] = input.value
                        return values
                    }, {})
                    options.onsubmit(formValues)
                }
                // Submit với hành vi mặc định
                else {
                    formElement.submit()
                }
            }
        }
        options.rules.forEach((rule) => {
            if (Array.isArray(selectorRules[rule.selector])) {
                selectorRules[rule.selector].push(rule.test)
            } else {
                selectorRules[rule.selector] = [rule.test]
            }
            var inputElement = formElement.querySelector(rule.selector)
            if (inputElement) {
                inputElement.onblur = () => {
                    validate(inputElement, rule)
                }
                inputElement.oninput = () => {
                    var errorElemet = inputElement.parentElement.querySelector(options.errorSelector)
                    errorElemet.innerText = ''
                    inputElement.parentElement.classList.remove('invalid')
                }
            }
        })
    }
}
// Nguyên tắc của các rule
// 1. Khi có lỗi thì trả ra message lỗi
// 2. Khi ko lỗi thì trả về undefined
Validator.isRequired = function(selector) {
    return {
        selector,
        test: function(value) {
            return value.trim() ? undefined : 'Vui lòng nhập trường này'
        }
    }

}
Validator.isEmail = function(selector) {
    return {
        selector,
        test: function(value) {
            var regex = /^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$/
            return regex.test(value) ? undefined : 'Trường này phải là email'
        }
    }
}
Validator.minLength = function(selector, min) {
    return {
        selector,
        test: function(value) {
            return value.length >= min ? undefined : `Vui lòng nhập đủ ${min} kí tự`
        }
    }
}
Validator.isConfirm = function(selector, getConfirmValue, message) {
    return {
        selector,
        test: function(value) {
            return value === getConfirmValue() ? undefined : message || `Giá trị nhập lại không chính xác`
        }
    }
}