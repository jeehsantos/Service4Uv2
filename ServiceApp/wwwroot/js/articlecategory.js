var dataTable;

$(document).ready(function () {
    loadDataTable();
});


function loadDataTable() {
    dataTable = $("#tblCategories").DataTable({
        "ajax": {
            "url": "/Admin/Categories/GetAll",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            {
                "data": 
                    "categoryID", "width": "5%"
            },
            { "data": "categoryName", "width": "50%" },
            { "data": "sort", "width": "20%" },
            {
                "data": "categoryID",
                "render": function (data) {
                    return `<div class="text-center">
                                <a href="/Admin/Categories/Edit/${data}" class="btn btn-success text-white" style="cursor:pointer; width:100px;">
                                <i class="far fa-edit"></i> Edit 
                                </a>
                                &nbsp;
                                <a onclick=Delete("/Admin/Categories/Delete/${data}") class="btn btn-danger text-white" style="cursor:pointer; width:100px;">
                                <i class="far fa-trash-alt"></i> Delete
                                </a>
                            </div>
                            `;
                }, "width": "30%"
            }
            ]
           
         

    });
}

function Delete(url) {
    swal({
        title: "Delete category?",
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