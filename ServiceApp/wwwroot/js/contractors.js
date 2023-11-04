var dataTable;

$(document).ready(function () {
    loadDataTable();
});


function loadDataTable(full) {
    dataTable = $("#tblContractors").DataTable({
        "ajax": {
            "url": "/Admin/Contractors/GetAll",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            {
             "data": "contractorID", "width": "10%" },
            { "data": "name", "width": "30%" },
            { "data": "suburb", "width": "20%" },
            { "data": "jobType", "width": "20%" },
            {
                "data": "sseEmployer", render: (data, type, row) =>
                    type === 'display' ? '<input type="checkbox" disabled class="editor-active">' : data,
                className: 'dt-body-center' },
            {
                "data": "active", render: (data, type, row) =>
                    type === 'display' ? '<input type="checkbox" disabled class="editor-active">' : data,
                className: 'dt-body-center' },
           
            {
                "data": "contractorID",
                "render": function (data) {
                    return `<div class="text-center">
                                <a href="/Admin/Contractors/Edit/${data}" class="btn btn-success text-white" style="cursor:pointer; width:100px;">
                                <i class="far fa-edit"></i> Edit 
                                </a>
                                &nbsp;
                                <a onclick=Delete("/Admin/Contractors/Delete/${data}") class="btn btn-danger text-white" style="cursor:pointer; width:100px;">
                                <i class="far fa-trash-alt"></i> Delete
                                </a>
                            </div>
                            `;
                }, "width": "40%"
            }
            ]
           
         

    });
}

function Delete(url) {
    swal({
        title: "Delete contractor?",
        text: "It will not be retrieved!",
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: "#DDB255",
        confirmButtonText: "Yes, delete!",
        closeOnConfirm: true
    }, function () {
        $.ajax({
            type: 'DELETE',
            url: url,
            success: function (data) {
                if (data.success) {
                    toastr.success(data.message);
                    dataTable.ajax.reload();
                    location.reload();
                }
                else {
                    toastr.error(data.message);
                }
            }
        });
    });
}