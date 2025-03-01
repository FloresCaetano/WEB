function get(id) { 
    return document.getElementById(id).value;
}

function set(id, valor) {
    document.getElementById(id).value = valor;
}

function setN(nombre, valor) {
    document.getElementsByName(nombre)[0].value = valor;
}

function limpiarDatos(idFormulario) {
    let elemetoName = document.querySelectorAll("#" + idFormulario + "[name]");
    console.log(elemetoName);
    for (let i = 0; i < elemetoName.length; i++) {
        elemetoName[i].value = "";
    }
}

async function fetchGet(url, tipoRespuesta, callback) {
    try {
        let raiz = get("hdfOculto") //document.getElementById("hdfOculto").value;
        let urlCompleta = window.location.protocol + "//" + window.location.host + "/" +
            url;
        let res = await fetch(urlCompleta);
        if (tipoRespuesta == "json")
            res = await res.json();
        else if (tipoRespuesta == "text")
            res = await res.text();

        callback(res);
    } catch (e) {

    }
}

async function fetchPost(url, tipoRespuesta, obj, callback) {
    try {
        let raiz = get("hdfOculto")
        let urlCompleta = window.location.protocol + "//" + window.location.host + "/" +
            url;
        let res = await fetch(urlCompleta, {
            method: "POST",
            body: obj
        });

        if (tipoRespuesta == "json")
            res = await res.json();
        else if (tipoRespuesta == "text")
            res = await res.text();

        callback(res);

    } catch (e) {

    }
}

//{url: "":, nombreCabeceras:[], nombrePropiedades:[]}
function pintar(objConfiguracion) {
    //if (objConfiguracion.editar == undefined) {
    //    objConfiguracion.editar = false;
    //}
    //if (objConfiguracion.eliminar == undefined) {
    //    objConfiguracion.eliminar = false;
    //}
    fetchGet(objConfiguracion.url, "json", function (res) {
        let contenido = generarTabla(objConfiguracion, res);
        document.getElementById("divTable").innerHTML = contenido;
        iniciarDataTable();
    });

}

let objConfiguracionGlobal;
function generarTabla(objConfiguracion, res) {

    let contenido = "";
    contenido += "<table class = 'display' id='tablaCustom'>";
    contenido += "<thead>";
    contenido += "<tr>";

    for (let i = 0; i < objConfiguracion.nombreCabeceras.length; i++) {
        contenido += "<td>" + objConfiguracion.nombreCabeceras[i] + "</td>";
    }

    if (objConfiguracion.editar || objConfiguracion.eliminar) {
        contenido += "<td>Operaciones"
        contenido += `<i onclick="Editar(${0})" data-bs-toggle="modal" data-bs-target="#operacionesModal" class="btn btn-primary"><svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-floppy" viewBox="0 0 16 16">
                      <path d="M11 2H9v3h2z"/>
                      <path d="M1.5 0h11.586a1.5 1.5 0 0 1 1.06.44l1.415 1.414A1.5 1.5 0 0 1 16 2.914V14.5a1.5 1.5 0 0 1-1.5 1.5h-13A1.5 1.5 0 0 1 0 14.5v-13A1.5 1.5 0 0 1 1.5 0M1 1.5v13a.5.5 0 0 0 .5.5H2v-4.5A1.5 1.5 0 0 1 3.5 9h9a1.5 1.5 0 0 1 1.5 1.5V15h.5a.5.5 0 0 0 .5-.5V2.914a.5.5 0 0 0-.146-.353l-1.415-1.415A.5.5 0 0 0 13.086 1H13v4.5A1.5 1.5 0 0 1 11.5 7h-7A1.5 1.5 0 0 1 3 5.5V1H1.5a.5.5 0 0 0-.5.5m3 4a.5.5 0 0 0 .5.5h7a.5.5 0 0 0 .5-.5V1H4zM3 15h10v-4.5a.5.5 0 0 0-.5-.5h-9a.5.5 0 0 0-.5.5z"/>
                      </svg> </i></td>`
    }

    contenido += "</tr>";
    contenido += "</thead>";
    let nroRegistros = res.length;

    for (let i = 0; i < nroRegistros; i++) {
        obj = res[i];
        contenido += "<tr>";
        for (let j = 0; j < objConfiguracion.nombrePropiedades.length; j++) {
            contenido += "<td>" + obj[objConfiguracion.nombrePropiedades[j]] + "</td>";
        }

        contenido += "<td>";
        if (objConfiguracion.editar) {
            let propiedadid = objConfiguracion.propiedadID;

            contenido += `<i onclick="Editar(${obj[propiedadid]})" data-bs-toggle="modal" data-bs-target="#operacionesModal" class="btn btn-primary"><svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-pencil-square" viewBox="0 0 16 16">
                <path d="M15.502 1.94a.5.5 0 0 1 0 .706L14.459 3.69l-2-2L13.502.646a.5.5 0 0 1 .707 0l1.293 1.293zm-1.75 2.456-2-2L4.939 9.21a.5.5 0 0 0-.121.196l-.805 2.414a.25.25 0 0 0 .316.316l2.414-.805a.5.5 0 0 0 .196-.12l6.813-6.814z" />
                <path fill-rule="evenodd" d="M1 13.5A1.5 1.5 0 0 0 2.5 15h11a1.5 1.5 0 0 0 1.5-1.5v-6a.5.5 0 0 0-1 0v6a.5.5 0 0 1-.5.5h-11a.5.5 0 0 1-.5-.5v-11a.5.5 0 0 1 .5-.5H9a.5.5 0 0 0 0-1H2.5A1.5 1.5 0 0 0 1 2.5z" />
                 </svg> </i>`
        }
        if (objConfiguracion.eliminar) {
            let propiedadid = objConfiguracion.propiedadID;
            contenido += `<i onclick="eliminar(${obj[propiedadid]})" class="btn btn-danger"><svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-trash" viewBox="0 0 16 16">
                            <path d="M5.5 5.5A.5.5 0 0 1 6 6v6a.5.5 0 0 1-1 0V6a.5.5 0 0 1 .5-.5m2.5 0a.5.5 0 0 1 .5.5v6a.5.5 0 0 1-1 0V6a.5.5 0 0 1 .5-.5m3 .5a.5.5 0 0 0-1 0v6a.5.5 0 0 0 1 0z"/>
                            <path d="M14.5 3a1 1 0 0 1-1 1H13v9a2 2 0 0 1-2 2H5a2 2 0 0 1-2-2V4h-.5a1 1 0 0 1-1-1V2a1 1 0 0 1 1-1H6a1 1 0 0 1 1-1h2a1 1 0 0 1 1 1h3.5a1 1 0 0 1 1 1zM4.118 4 4 4.059V13a1 1 0 0 0 1 1h6a1 1 0 0 0 1-1V4.059L11.882 4zM2.5 3h11V2h-11z"/>
                            </svg> </i>`
        }
        contenido += "</td>";


        contenido += "</tr>";
    }
    contenido += "</table>";
    return contenido;

}

function iniciarDataTable() {
    new DataTable("#tablaCustom", {
        language: {
            "decimal": "",
            "emptyTable": "No hay datos disponibles en la tabla",
            "info": "Mostrando _START_ a _END_ de _TOTAL_ entradas",
            "infoEmpty": "Mostrando 0 a 0 de 0 entradas",
            "infoFiltered": "(filtrado de _MAX_ entradas en total)",
            "infoPostFix": "",
            "thousands": ",",
            "lengthMenu": "Mostrar _MENU_ entradas",
            "loadingRecords": "Cargando...",
            "processing": "",
            "search": "Buscar:",
            "zeroRecords": "No se encontraron registros coincidentes",
            "paginate": {
                "first": "Primero",
                "last": "Último",
                "next": "Siguiente",
                "previous": "Anterior"
            }
        }
    });

}


function Exito() {
    Swal.fire({
        position: "top-end",
        icon: "success",
        title: "Los cambios se han realizado exitosamente",
        showConfirmButton: false,
        timer: 1500
    });
}

function ErrorA(error = "No se han rellenado todos los campos") {
    Swal.fire({
        icon: "error",
        title: "Oops...",
        text: error
    });
}

function confirmacion(titulo = "Confirmacion", texto = "¿Desea guardar los cambios?", callback) {
    return Swal.fire({
        title: titulo,
        text: texto,
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Si"
    }).then((result) => {
        if (result.isConfirmed) {
            callback();
        }
    });
}