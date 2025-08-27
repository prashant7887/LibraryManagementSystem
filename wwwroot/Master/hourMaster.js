$(document).ready(async function () {
    await bindHours();
    $("#btnhoursave").click(async function () {
        if ($("#hour").val() == "") {
            showAlert("warning", "Enter the Hour. !!!");
            return;
        }
        showLoader();
        var formData = {
            id: parseInt($("#hid").val()),
            hournumber: $("#hour").val(),
            action: await callAction()
        };
        try {
            const response = await $.ajax({
                url: "/Master/addHour",
                method: "POST",
                data: JSON.stringify(formData),
                contentType: "application/json",
            });
            let res = response.message;
            if (res.split(':')[0] == "200") {
                showAlert("success", res.split(':')[1]);
                await clearControls();
                await bindHours();
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
async function bindHours()
{
    try {
        showLoader();
        const response = await $.ajax({
            url: "/Master/getHourDetails",
            method: "GET",
            data: "",
            contentType: "application/json",
        });
        $("#tblstudet tbody tr").remove();
        let res = response.message;
        if (res.length > 0) {
            res.forEach(function (item, index) {
                let button = `<i id="edit_${index}" class="btn btn-warning fa fa-edit" onclick="editRecord(${item.id},this.id)"></i>
                              <i id="delete_${index}" class="btn btn-danger fa fa-trash" onclick="deleteRecord(${item.id},this.id)"></i>
                             `
                $("#tblstudet tbody").append(`
                    <tr>
                    <td>${(index + 1)}</td>
                    <td id="hr${index}">${item.hournumber}</td>
                    <td id="curdt${index}">${item.curdate}</td>
                    <td id="status${index}">${item.action}</td>
                    <td class="d-flex inline-item-center gap-2">${button}</td>
                    </tr>
                 `);
            });
        }
        else {
            $("#tblstudet tbody").append(`
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
document.getElementById("hour").addEventListener("input", function () {
    this.value = this.value.replace(/[^0-9]/g, "");
});
document.getElementById("btnreset").addEventListener("click", async function () {
    await clearControls();
    await bindHours();
});


function showLoader() {
    document.getElementById('loaderOverlay').style.display = 'flex';
}
function hideLoader() {
    document.getElementById('loaderOverlay').style.display = 'none';
}
async function editRecord(id,buttonId)
{
    debugger;
    let index = buttonId.split('_')[1];
    $("#hour").val($("#hr" + index).text());
    if ($("#status" + index).text() == "Active")
        $("#action").attr("checked", true);
    else
        $("#action1").attr("checked", true);

    $("#hid").val(id);
    $("#btnhoursave").html(`<i class="fa fa-save"></i> Update`);
}
async function deleteRecord(id, button) {
    if (id == null || id == 0 || id=="0") {
        showAlert("warning", "Invalid Record. !!!");
    }
    showLoader();
    try {
        const response = await $.ajax({
            url: "/Master/deleteHours",
            method: "POST",
            data: { id: id }
        });
        let res = response.message;
        if (res.split(':')[0] == "200") {
            showAlert("success", res.split(':')[1]);
            await bindHours();
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
    $("#hour").val("");
    $("#action").attr("checked", true);
    $("#hid").val("0");
    $("#btnhoursave").html(`<i class="fa fa-save"></i> Save`);
}