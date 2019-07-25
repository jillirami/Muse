document.getElementById('edit').onclick = function changeContent() {
    document.getElementById('userinfo').innerHTML = '<p><b>@Html.DisplayNameFor(model => model.Name)</b>: @Html.DisplayFor(model => model.Name)</p>';
}