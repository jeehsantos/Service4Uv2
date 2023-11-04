var dataTable;

$(document).ready(function () {
   
    loadDataTable();
});


function loadDataTable(full) {
    dataTable = $("#tblEmployees").DataTable({
        "ajax": {
            "url": "/Admin/Employees/GetAll",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            {
             "data": "employeeID", "width": "10%" },
            { "data": "name", "width": "20%" },
            { "data": "suburb", "width": "20%" },
            { "data": "language", "width": "20%" },
            { "data": "review", "width": "20%" },
            {
                "data": "active", render: (data, type, row) =>
                    type === 'display' ? '<input type="checkbox" disabled asp-for="Active" class="editor-active">' : data,
                className: 'dt-body-center'
            },

            {
                "data": "employeeID",
                "render": function (data) {
                    return `<div class="text-center">
                                <a href="/Admin/Employees/Edit/${data}" class="btn btn-success text-white" style="cursor:pointer; width:100px;">
                                <i class="far fa-edit"></i> Edit 
                                </a>
                                &nbsp;
                                <a onclick=Delete("/Admin/Employees/Delete/${data}") class="btn btn-danger text-white" style="cursor:pointer; width:100px;">
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
        title: "Delete Employee?",
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