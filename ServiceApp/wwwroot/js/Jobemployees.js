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
                "data": "employeeID", "width": "10%"
            },
            { "data": "image", "width": "30%" },
            { "data": "name", "width": "20%" },
            { "data": "suburb", "width": "20%" },
            { "data": "language", "width": "20%" },
            { "data": "review", "width": "20%" },
           
            {
                "data": "employeeID",
                "render": function (data) {
                    return `<div class="text-center">
                                <a href="/Admin/Employees/Details/${data}" class="btn btn-success text-white" style="cursor:pointer; width:100px;">
                                <i class="far fa-edit"></i> Details 
                                </a>
                            </div>
                            `;
                }, "width": "40%"
            }

        ]



    });
}
 