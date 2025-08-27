$(document).ready(async function () {
    await bindCourse();
    $("#btnsave").click(async function () {
        if ($("#course").val() == "") {
            showAlert("warning", "Enter the Course Name. !!!");
            return;
        }
        showLoader();
        var formData = {
            id: parseInt($("#hid").val()),
            course: $("#course").val(),
            action: await callAction()
        };
        try {
            const response = await $.ajax({
                url: "/Master/AddCourse",
                method: "POST",
                data: JSON.stringify(formData),
                contentType: "application/json",
            });
            let res = response.message;
            if (res.split(':')[0] == "200") {
                showAlert("success", res.split(':')[1]);
                await clearControls();
                await bindCourse();
            }
            else if (res.split(':')[0] == "402") {
                showAlert("warning", res.split(':')[1]);
            }
            else {
                showAlert("error", res.split(':')[1]);
            }
            hideLoader();
        } catch (e) {
            hideLoader();
            showAlert("error", e);
        }
    });
});
async function bindCourse() {
    try {
        showLoader();
        const response = await $.ajax({
            url: "/Master/getCourseDetails",
            method: "GET",
            data: "",
            contentType: "application/json",
        });
        $("#tblcourse tbody tr").remove();
        let res = response.message;
        if (res.length > 0) {
            res.forEach(function (item, index) {
                let button = `<i id="edit_${index}" class="btn btn-warning fa fa-edit" onclick="editRecord(${item.id},this.id)"></i>
                              <i id="delete_${index}" class="btn btn-danger fa fa-trash" onclick="deleteRecord(${item.id},this.id)"></i>
                             `
                $("#tblcourse tbody").append(`
                    <tr>
                    <td>${(index + 1)}</td>
                    <td id="course${index}">${item.course}</td>
                    <td id="curdt${index}">${item.curdate}</td>
                    <td id="status${index}">${item.action}</td>
                    <td class="d-flex inline-item-center gap-2">${button}</td>
                    </tr>
                 `);
            });
        }
        else {
            $("#tblcourse tbody").append(`
                    <tr>
                      <td class="text-center" colspan="5"> -- No Record Found -- </td>
                    </tr>
                 `);
        }
        hideLoader();
    } catch (e) {
        hideLoader();
        showAlert("error", e);
    }
}
document.getElementById("course").addEventListener("input", function () {
    this.value = this.value.replace(/[^A-Za-z]/g, "");
});
document.getElementById("btnreset").addEventListener("click", async function () {
    await clearControls();
    await bindCourse();
});


function showLoader() {
    document.getElementById('loaderOverlay').style.display = 'flex';
}
function hideLoader() {
    document.getElementById('loaderOverlay').style.display = 'none';
}
async function editRecord(id, buttonId) {
    debugger;
    let index = buttonId.split('_')[1];
    $("#course").val($("#course" + index).text());
    if ($("#status" + index).text() == "Active")
        $("#action").attr("checked", true);
    else
        $("#action1").attr("checked", true);

    $("#hid").val(id);
    $("#btnsave").html(`<i class="fa fa-save"></i> Update`);
}
async function deleteRecord(id, button) {
    if (id == null || id == 0 || id == "0") {
        showAlert("warning", "Invalid Record. !!!");
    }
    showLoader();
    try {
        const response = await $.ajax({
            url: "/Master/deleteCourse",
            method: "POST",
            data: { id: id }
        });
        let res = response.message;
        if (res.split(':')[0] == "200") {
            showAlert("success", res.split(':')[1]);
            await bindCourse();
        }
        else if (res.split(':')[0] == "402") {
            showAlert("warning", res.split(':')[1]);
        }
        else {
            showAlert("error", res.split(':')[1]);
        }
        hideLoader();
    } catch (e) {
        hideLoader();
        showAlert("error", e);
    }
}
async function clearControls() {
    $("#course").val("");
    $("#action").attr("checked", true);
    $("#hid").val("0");
    $("#btnsave").html(`<i class="fa fa-save"></i> Save`);
}