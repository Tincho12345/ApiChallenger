var dataTable;

$(document).ready(function () {
    cargarDatatable();
});

function cargarDatatable() {
    dataTable = $("#tbl").DataTable({
        responsive: true,
        "ajax": {
            "url": "/contactos/GetTodosContactos",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "id", "width": "2%" },
            { "data": "nombre", "width": "%10" },
            { "data": "empresa", "width": "20%" },
            {
                "data": "urlImagen",
                "render": function (urlImagen) {
                    return `<img src="../${urlImagen}" h-100%; width="60"; text-center>`
                }, "width": "5%"
            },
            { "data": "email", "width": "15%" },
            { "data": "nacimiento", "width": "15%" },
            { "data": "address", "width": "15%" },

            {
                "data": "id",
                "render": function (data) {
                    return `<div class="text-center">
                                <a href="/contactos/Edit/${data}" class="btn btn-outline-primary" title="Editar" style="cursor:pointer; width:50px">
                                <i class="bi bi-pencil"></i>
                                </a>                                
                                &nbsp;
                                <a onclick=Delete("/contactos/Delete/${data}") class="btn btn-outline-danger" title="Eliminar" style="cursor:pointer; width:50px;">
                                    <i class="bi bi-trash"></i>
                                </a>`;
                }, "width": "20%"
            }
        ],
        "language": {
            "decimal": "",
            "emptyTable": "No hay registros",
            "info": "Mostrando _START_ de _TOTAL_ Entradas",
            "infoEmpty": "Mostrando 0 de 0 Entradas",
            "infoFiltered": "(Filtrado de _MAX_ total entradas)",
            "infoPostFix": "",
            "thousands": ",",
            "lengthMenu": "Mostrar _MENU_ Entradas",
            "loadingRecords": "Cargando...",
            "processing": "Procesando...",
            "search": "Buscar:",
            "zeroRecords": "Sin resultados encontrados",
            "paginate": {
                "first": "Primero",
                "last": "Ultimo",
                "next": "Siguiente",
                "previous": "Anterior"
            }
        },
        "width": "100%"
    });
}

function Delete(url) {
    swal({
        title: "Esta seguro de borrar?",
        text: "Este contenido no se puede recuperar!",
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: "#DD6B55",
        confirmButtonText: "Si, borrar!",
        closeOnconfirm: true
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
