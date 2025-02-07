﻿var dtble;
$(document).ready(function () {
    loaddata();
});

function loaddata() {
    dtble = $("#mytable").DataTable({
        "ajax": {
            "url":"/Admin/Product/GetData"
        },
        "columns": [
            { "data": "productName" },
            { "data": "productDescription" },
            { "data": "productPrice" },
            { "data": "category.categotyName" },

            {
                "data": "productID",
                "render": function (data) {
                    return `
                            <a href = "/Admin/Product/Edit/${data}"  class= "btn btn-success">Edit</a>
                            <a onClick = DeleteItem("/Admin/Product/Delete/${data}")  class = "btn btn-danger">Delete</a>
                    
                             `
                }
            }
        ]
    });
}



function DeleteItem(url) {
    Swal.fire({
        title: "Are you sure?",
        text: "You won't be able to revert this!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Yes, delete it!"


    }).then((result) => {
        if (result.isConfirmed) {

            $.ajax({
                url: url,
                type: "Delete",
                success: function (data) {
                    if (data.success) {

                        dtble.ajax.reload();

                        toastr.success(data.message);
                     
                    } else {
                        toastr.error(data.message);
                    }
                }
            });

            Swal.fire({
                title: "Deleted!",
                text: "Your file has been deleted.",
                icon: "success"
            });
        }
    });
}