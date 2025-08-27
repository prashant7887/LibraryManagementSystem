async function setDateInputValue(inputId) {
    const dateInput = document.getElementById(inputId);
    if (dateInput) {
        const today = new Date();
        const yyyy = today.getFullYear();
        const mm = String(today.getMonth() + 1).padStart(2, '0');
        const dd = String(today.getDate()).padStart(2, '0');
        dateInput.value = `${yyyy}-${mm}-${dd}`;
    } else {
        console.error(`Input element with ID "${inputId}" not found.`);
    }
}
async function callAction() {
    var status = "";
    if ($("#action").is(":checked")) {
        status = "Active";
    }
    else {
        status = "InActive";
    }
    return status;
}