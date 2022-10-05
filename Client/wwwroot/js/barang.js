

$(document).ready(() => {
    $("#myTable").DataTable({
        ajax: {
            url: "https://localhost:5001/api/Barang",
            dataSrc: "data",
            dataType: "JSON"
        },
        columns: [
            {
            data: "id"
            },
            {
            data: "nama"
            },
            {
            data: "satuan"
            },
            {
            data: "stock"
            },
        ]
    })
})