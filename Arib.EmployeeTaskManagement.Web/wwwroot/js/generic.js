function Build_DropdownList(html_id, url, selected_val = "") {
    let parentElement = $("#" + html_id).empty().append(`<option value="">Loading data...</option>`);

    $.get(`/api/DropDown/${url}`)
        .done(response => {
            let options = response.map(item => `<option value="${item.value}">${item.text}</option>`).join('');
            parentElement.html(`<option value="">-- Select --</option>` + options);

            // Set the selected value if it exists in the response
            if (selected_val && response.some(item => item.value == selected_val)) {
                parentElement.val(selected_val).trigger("change");
            }
        })
        .fail((jqXHR, textStatus, errorThrown) => {
            console.error("AJAX Error:", textStatus, errorThrown);
            swal({
                title: 'Error!',
                text: 'Error while getting data',
                icon: 'error',
                confirmButtonText: 'OK'
            });
        });
}
function generate_dataTable(url, tableId, columns) {
    $.ajax({
        url: url,
        type: 'GET',
        dataType: 'json',
        success: function (response) {
            console.log(response);
            $(`#${tableId}`).DataTable({
                responsive: true,
                //select: true,
                order: [],
                destroy: true,
                data: response.data,
                columns: columns,
                

            });

        },
        error: function (response) {
            swal({
                position: 'top-end',
                icon: 'error',
                title: 'Server Error',
                showConfirmButton: false,
                timer: 1500
            });
        }
    });

}

async function SetFile(filePath) {
    try {
        const response = await fetch(`/${filePath}`);
        if (!response.ok) {
            throw new Error('Failed to fetch file');
        }

        // Get the byte array (ArrayBuffer) from the response
        const fileBytes = await response.arrayBuffer();

        // Determine the MIME type based on the file extension
        const fileExtension = filePath.split('.').pop().toLowerCase();
        const mimeType = {
            jpg: 'image/jpeg',
            jpeg: 'image/jpeg',
            png: 'image/png',
            gif: 'image/gif',
            svg: 'image/svg+xml',
            webp: 'image/webp',
        }[fileExtension] || 'application/octet-stream';

        // Create a Blob from the ArrayBuffer
        const blob = new Blob([fileBytes], { type: mimeType });

        const fileName = filePath.split('-').slice(1).join('-');

        // Create a File from the Blob
        const file = new File([blob], fileName, { type: mimeType });

        // Create a DataTransfer object to hold the file
        const dataTransfer = new DataTransfer();
        dataTransfer.items.add(file);

        // Set the file input's files to the DataTransfer object's files using jQuery
        return dataTransfer.files;
    } catch (error) {
        console.log('Error fetching file:', error);
        return null;
    }
}