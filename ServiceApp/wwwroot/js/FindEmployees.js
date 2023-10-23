var dataTable;

$(document).ready(function () {
    loadDataTable();
});


function loadDataTable(full) {
    dataTable = $("#tblEmployees").DataTable({
        "ajax": {
            "url": "/Client/Orchards/GetAll",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
           
            { "data": "name", "width": "30%" },
            { "data": "suburb", "width": "20%" },
            { "data": "phone", "width": "20%" },
            { "data": "language", "width": "20%" },
           
            {
                "data": "employeeID",
                "render": function (data) {
                    return `<div class="text-center">
                                <a href="/Client/Orchards/EmployeeDetails/${data}" class="btn btn-success text-white" style="cursor:pointer; width:100px;">
                                <i class="far fa-edit"></i> Details 
                                </a>
                            </div>
                            `;
                }, "width": "40%"
            }

        ]



    });
}
 