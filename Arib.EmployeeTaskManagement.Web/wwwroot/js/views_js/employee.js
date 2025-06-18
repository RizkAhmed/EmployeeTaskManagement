$(document).ready(function () {
    $("#Books").addClass("active-menu");
    loadData();

});
$("#add_employee").on('click', function (e) {
    $('#submit').prop('disabled', false);

    $('.clearr').val("");
    $(".modal-title").css("display", "none");
    $("#employee_model_lable_add").css("display", "block");
    $("#employee_model").modal("show");
});
$("#submit").on('click', function (e) {
    e.preventDefault();
    if ($('#employee_form').valid()) {
        $('#submit').prop('disabled', true);
        create_edit_employee();
    }
});
function create_edit_employee() {
    $('#submit').prop('disabled', true);

    const formData = new FormData();
    const fileInput = $("#ImageFile")[0];

    if (fileInput && fileInput.files.length > 0) {
        formData.append("ImageFile", fileInput.files[0]);
    }

    // Append all form fields
    ["Id", "FirstName", "LastName", "Salary", "ManagerId", "DepartmentId"].forEach(field => {
        formData.append(field, $(`#${field}`).val());
    });

    $.ajax({
        url: "/Employee/AddOrEditEmployee",
        type: "POST",
        data: formData,
        contentType: false,
        processData: false,
        success: function (result) {
            $('#submit').prop('disabled', false);

            if (result.isSuccess) {
                $("#employee_model").modal("hide");

                Swal.fire({
                    icon: 'success',
                    title: 'Success',
                    text: result.message,
                    confirmButtonText: 'OK'
                });

                loadData();
            } else {
                Swal.fire({
                    icon: 'error',
                    title: 'Error',
                    text: result.message || 'Something went wrong. Please try again.',
                    confirmButtonText: 'OK'
                });
            }
        },
        error: function (xhr) {
            $('#submit').prop('disabled', false);

            let message = "A server error occurred. Please try again later.";

            Swal.fire({
                icon: 'error',
                title: 'Server Error',
                text: message,
                confirmButtonText: 'OK'
            });
        }
    });
}
$(document).on('click', '.view-image', function () {
    const imagePath = $(this).data('image');
    $('#modalImage').attr('src', imagePath);
    $('#imageModal').modal('show');
});

async function build_edit_employee_form(index) {
    var row = $('#employee_data').DataTable().row(index);
    var data = row.data();
    $('#ImageFile')[0].files = await SetFile(data.imagePath);

    $('#Id').val(data.id);
    $('#FirstName').val(data.firstName);
    $('#LastName').val(data.lastName);
    $('#Salary').val(data.salary);
    if (data.managerId > 0)
        $('#ManagerId').val(`${data.managerId}`);
    $('#DepartmentId').val(`${data.departmentId}`);

    $(".modal-title").css("display", "none");
    $("#employee_model_lable_edit").css("display", "block");

    $('#submit').prop('disabled', false);

    $("#employee_model").modal("show");
}
function delete_employee(id) {
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
                url: "/Employee/Delete",
                data: { id: id },
                success: function (response) {
                    Swal.fire({
                        icon: "success",
                        title: "Deleted!",
                        text: response.message,
                        confirmButtonText: "OK"
                    });
                    loadData(); // Refresh the table or list
                },
                error: function (xhr) {
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

function loadData() {
    let coloumns = [
        {
            data: null, render: function (data, type, row, dataIndex) {
                return `${data.firstName} ${data.lastName}`
            }
        },
        { data: "departmentName" },
        { data: "salary" },
        { data: "managerName" },
        {
            responsivePriority: 1,
            data: null,
            orderable: false,
            render: function (data, type, row, dataIndex) {
                return `
            <div>
                <a href="javascript:void(0);" 
                   class="text-danger ms-2 view-image" 
                   data-image="/${data.imagePath}" 
                   title="View Image">
                    <i class="bi bi-image fs-4"></i>
                </a>
            </div>`;
            }
        },
        { data: "insertionDate" },
        {
            data: null, render: function (data, type, row, dataIndex) {
                return `<div class='d-flex justify-content-around'><a  href='#1' onclick='build_edit_employee_form(${dataIndex.row})' ><i class='bi bi-pencil-square' style='font-size: 1.5rem;'></i></a>
                <a href='#1' onclick='delete_employee(${data.id})' ><i class='bi bi-trash text-danger' style='font-size: 1.5rem;'></i></a></div>`;
            }
        },

    ]
    let url = `/Employee/GetAllEmployee`
    let tableId = `employee_data`
    generate_dataTable(url, tableId, coloumns);
}
