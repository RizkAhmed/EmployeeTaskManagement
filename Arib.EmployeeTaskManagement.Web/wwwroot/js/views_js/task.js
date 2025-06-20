$(document).ready(function () {
    loadTaskData();
});

// Open Add Task Modal
$("#add_task").on('click', function () {
    $('#submit_task').prop('disabled', false);
    $('.clearr').val("");
    $(".modal-title").hide();
    $("#task_modal_label_add").show();
    $("#task_modal").modal("show");
});

// Submit Task Form
$("#submit_task").on('click', function (e) {
    e.preventDefault();
    if ($('#task_form').valid()) {
        $('#submit_task').prop('disabled', true);
        createOrEditTask();
    }
});

// Build Edit Form
function build_edit_task_form(index) {
    $('.clearr').val("");
    var row = $('#task_data').DataTable().row(index);
    var data = row.data();

    $('#Id').val(data.id);
    $('#Title').val(data.title);
    $('#Description').val(data.description);
    $('#EmployeeId').val(data.employeeId);

    $(".modal-title").hide();
    $("#task_modal_label_edit").show();
    $('#submit_task').prop('disabled', false);

    $("#task_modal").modal("show");
}

// Create or Edit Task
function createOrEditTask() {
    $('#submit_task').prop('disabled', true);

    $.ajax({
        url: "/Task/AddOrEditTask",
        type: "POST",
        data: $('#task_form').serialize(),
        success: function (result) {
            $('#submit_task').prop('disabled', false);

            if (result.isSuccess) {
                $("#task_modal").modal("hide");
                Swal.fire({
                    icon: 'success',
                    title: 'Success',
                    text: result.message,
                    confirmButtonText: 'OK'
                });
                loadTaskData();
            } else {
                Swal.fire({
                    icon: 'error',
                    title: 'Error',
                    text: result.message || 'Something went wrong. Please try again.',
                    confirmButtonText: 'OK'
                });
            }
        },
        error: function () {
            $('#submit_task').prop('disabled', false);
            Swal.fire({
                icon: 'error',
                title: 'Server Error',
                text: 'A server error occurred. Please try again later.',
                confirmButtonText: 'OK'
            });
        }
    });
}

// Delete Task
function delete_task(id) {
    Swal.fire({
        title: "Are you sure?",
        text: "This action cannot be undone!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#d33",
        cancelButtonColor: "#3085d6",
        confirmButtonText: "Yes, delete it!",
        cancelButtonText: "Cancel"
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                type: "POST",
                url: "/Task/Delete",
                data: { id: id },
                success: function (response) {
                    if (response.isSuccess) {
                        Swal.fire({
                            icon: "success",
                            title: "Deleted!",
                            text: response.message,
                            confirmButtonText: "OK"
                        });
                        loadTaskData();
                    } else {
                        Swal.fire({
                            icon: 'error',
                            title: 'Error',
                            text: response.message || 'Something went wrong. Please try again.',
                            confirmButtonText: 'OK'
                        });
                    }
                },
                error: function () {
                    Swal.fire({
                        icon: "error",
                        title: "Error!",
                        text: "Something went wrong while deleting.",
                        confirmButtonText: "OK"
                    });
                }
            });
        }
    });
}

// Load Tasks into DataTable
function loadTaskData() {
    const columns = [
        { data: "title" },
        { data: "description" },
        { data: "employeeName" },
        { data: "statusName" },
        { data: "insertionDate" },
        {
            data: null, render: function (data, type, row, dataIndex) {
                return `<div class='d-flex justify-content-between p-1'>
                    <a href='#' onclick='build_edit_task_form(${dataIndex.row})'>
                        <i class='bi bi-pencil-square' style='font-size: 1.5rem;'></i>
                    </a>
                    <a href='#' onclick='delete_task(${data.id})'>
                        <i class='bi bi-trash text-danger' style='font-size: 1.5rem;'></i>
                    </a>
                </div>`;
            }
        }
    ];

    const url = "/Task/GetAllTasks";
    const tableId = "task_data";
    generate_dataTable(url, tableId, columns);
}
