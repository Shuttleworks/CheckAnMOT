const inputElement = document.querySelector("#Registration");
const buttonElement = document.querySelector("#btnSubmit");
const formElement = document.querySelector("#frmMot");
const reg = new RegExp(/[^a-zA-Z0-9\s]/);

inputElement.addEventListener("input", (event) => {

    let invalid = reg.test(event.target.value);
    
    if (event.target.value != "" && event.target.value.length <= 8 && !invalid) {
        buttonElement.removeAttribute("disabled");
        buttonElement.classList.add("btn-success");
    } else {
        buttonElement.setAttribute("disabled", "");
        buttonElement.classList.remove("btn-success");
    }

});

inputElement.addEventListener("click", (event) => {
    let invalid = reg.test(event.target.value);

    if (event.target.value != "" && event.target.value.length <= 8 && !invalid) {
        event.target.value = "";
    }
});


