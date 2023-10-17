var dataTable;

$(document).ready(function () {
    loadDataTable();
});


function loadDataTable(full) {
    dataTable = $("#tblOrchards").DataTable({
        "ajax": {
            "url": "/Client/Orchards/GetAll",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            
            { "data": "name", "width": "30%" },
            { "data": "suburb", "width": "20%" },
            { "data": "jobType", "width": "20%" },
            {
                "data": "sseEmployer", render: (data, type, row) =>
                    type === 'display' ? '<input type="checkbox" disabled class="editor-active">' : data,
                className: 'dt-body-center' },
            {
                "data": "contractorID",
                "render": function (data) {
                    return `<div class="text-center">
                                <a href="/Client/Orchards/Details/${data}" class="btn btn-success text-white" style="cursor:pointer; width:100px;">
                                <i class="far fa-edit"></i> Details 
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
                }
                else {
                    toastr.error(data.message);
                }
            }
        });
    });
}