$(document).ready(function () {
    loadData();
});
$("#add_department").on('click', function (e) {
    $('#submit').prop('disabled', false);

    $('.clearr').val("");
    $(".modal-title").css("display", "none");
    $("#department_model_lable_add").css("display", "block");
    $("#department_model").modal("show");
});
$("#submit").on('click', function (e) {
    e.preventDefault();
    if ($('#department_form').valid()) {
        $('#submit').prop('disabled', true);
        create_edit_department();
    }
});



function build_edit_department_form(index) {
    $('.clearr').val("");
    var row = $('#department_data').DataTable().row(index);
    var data = row.data();


    $('#Id').val(`${data.id}`);
    $('#Name').val(`${data.name}`);

    $(".modal-title").css("display", "none");
    $("#department_model_lable_edit").css("display", "block");

    $('#submit').prop('disabled', false);

    $("#department_model").modal("show");
}
function create_edit_department() {
    $('#submit').prop('disabled', true);

    $.ajax({
        url: "/Department/AddOrEditDepartment",
        type: 'POST',
        data: $('#department_form').serialize(),
        content: 'application/json',
        success: function (result) {
            $('#submit').prop('disabled', false);

            if (result.isSuccess) {
                $("#department_model").modal("hide");

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

function delete_department(id) {
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
                url: "/Department/Delete",
                data: { id: id },
                success: function (response) {
                    if (response.isSuccess) {

                        Swal.fire({
                            icon: "success",
                            title: "Deleted!",
                            text: response.message,
                            confirmButtonText: "OK"
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
        { data: "name" },
        { data: "insertionDate" },
        { data: "countOfEmployees" },
        { data: "countOfEmpsSaleries" },
        
        {
            data: null, render: function (data, type, row, dataIndex) {
                return `<div class='d-flex justify-content-between p-1'><a  href='#1' onclick='build_edit_department_form(${dataIndex.row})' ><i class='bi bi-pencil-square' style='font-size: 1.5rem;'></i></a>` +
                    (data.countOfEmployees > 0 ? `` : `<a href='#1' onclick='delete_department(${data.id})' ><i class='bi bi-trash text-danger' style='font-size: 1.5rem;'></i></a></div>`);
            }
        },

    ]
    let url = `/Department/GetAllEmployee`
    let tableId = `department_data`
    generate_dataTable(url, tableId, coloumns);
}
