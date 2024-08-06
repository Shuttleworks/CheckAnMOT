const inputElement = document.querySelector("#Registration");
const buttonElement = document.querySelector("#btnSubmit");
const formElement = document.querySelector("#frmMot");

inputElement.addEventListener("input", (event) => {

    if (event.target.value != "" && event.target.value.length <= 8) {
        buttonElement.removeAttribute("disabled");
        buttonElement.classList.add("btn-success");
    } else {
        buttonElement.setAttribute("disabled", "");
        buttonElement.classList.remove("btn-success");
    }

});

inputElement.addEventListener("click", (event) => {
    if (event.target.value != "" && event.target.value.length <= 8) {
        event.target.value = "";
    }
});


