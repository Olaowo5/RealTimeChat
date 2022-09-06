const PasswordField = document.querySelector(".form  input[type='password']"),
    toggleBtn = document.querySelector(".form .field i");

toggleBtn.onclick = () => {
    //console.log("sup");
    if (PasswordField.type == "password") {
        PasswordField.type = "text";
        toggleBtn.classList.add("active");
    }
    else if (PasswordField.type == "text") {
        PasswordField.type = "password";
        toggleBtn.classList.remove("active");
    }
}