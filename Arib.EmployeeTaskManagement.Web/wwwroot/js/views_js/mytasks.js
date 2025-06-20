$(document).ready(function () {
    loadTaskData();
});
function updateTaskStatus(taskId, newStatusId) {
    $.ajax({
        type: "POST",
        url: "/Task/UpdateStatus",
        data: { Id: taskId, StatusId: newStatusId },
        success: function (response) {
            if (response.isSuccess) {
                Swal.fire("Success", "Status updated successfully.", "success");
                loadTaskData();
            } else {
                Swal.fire("Error", response.message || "Failed to update status.", "error");
            }
        },
        error: function () {
            Swal.fire("Error", "An error occurred while updating status.", "error");
        }
    });
}
function loadTaskData() {
    const columns = [
        { data: "title" },
        { data: "description" },
        { data: "employeeName" },
        { data: "insertionDate" },
        {
            data: "statusId", render: function (data, type, row) {
                const statuses = [
                    { id: 1, name: "Pending", icon: "🕒", class: "text-warning" },
                    { id: 2, name: "In Progress", icon: "🔄", class: "text-primary" },
                    { id: 3, name: "Completed", icon: "✅", class: "text-success" }
                ];

                const options = statuses.map(s =>
                    `<option value="${s.id}" class="${s.class}" ${s.id === data ? 'selected' : ''}>
                ${s.icon} ${s.name}
            </option>`).join('');

                return `<select class="form-select form-select-sm" onchange="updateTaskStatus(${row.id}, this.value)">
                    ${options}
                </select>`;
            }
        }
    ];

    const url = "/Task/GetEmployeeTasks";
    const tableId = "task_data";
    generate_dataTable(url, tableId, columns);
}
function renderStatusDropdown(taskId, currentStatusId) {
    return $.get(`/api/DropDown/GetTaskStatuses`)
        .then(statuses => {
            const options = statuses.map(s =>
                `<option value="${s.value}" ${s.value == currentStatusId ? "selected" : ""}>
                    ${s.text}
                </option>`
            ).join("");

            return `<select class="form-select form-select-sm" onchange="updateTaskStatus(${taskId}, this.value)">
                        ${options}
                    </select>`;
        });
}
