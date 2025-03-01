window.onload = function() {
    listarSucursal();
}

async function listarSucursal() {

    pintar({
        url: "tipoMedicamento/listarTipoMedicamento",
        nombreCabeceras: ["Id tipo medicamento", "Nombre", "Descripcion"],
        nombrePropiedades: ["idTipoMedicamento", "nombre", "descripcion"],
        editar: true,
        eliminar: true,
        propiedadID: "idTipoMedicamento"
    });

}

async function buscarTipoMedicamento(nombre) {
    pintar({
        url: "tipoMedicamento/filtrarTipoMedicamento/?nombre=" + nombre,
        nombreCabeceras: ["Id tipo medicamento", "Nombre", "Descripcion"],
        nombrePropiedades: ["idTipoMedicamento", "nombre", "descripcion"],
        editar: true,
        eliminar: true,
        propiedadID: "idTipoMedicamento"
    });
}

async function filtrarTipoMedicamento() {
    let nombre = document.getElementsByName("txtNombre")[0].value;
    buscarTipoMedicamento(nombre);
}

async function guardarTipoMedicamento() {
    let form = document.getElementById("frmOperaciones");
    let frm = new FormData(form);

    confirmacion(undefined, undefined, function (resp) {
        fetchPost("TipoMedicamento/guardarTipoMedicamento", "json", frm, function (res) {
            if (res > 0) {
                limpiarTipoMedicamento();
                Exito();
            }
            else {
                ErrorA();
            }
        });

    });
}

function limpiarTipoMedicamento() {
    limpiarDatos("frmOperaciones");
    listarSucursal();
}

function eliminar(id) {
    fetchGet("tipoMedicamento/eliminarTipoMedicamento/?id=" + id, "json", function (data) {
        confirmacion(undefined, "¿Seguro desea eliminar?", function (resp) { 
            limpiarTipoMedicamento();
            Exito();
        });
    });
}

function Editar(id) {
    //tipoMedicamento/recuperarTipoMedicamento/?idTipoMedicamento=3
    if (id != 0) { 
        fetchGet("tipoMedicamento/recuperarTipoMedicamento/?idTipoMedicamento=" + id, "json", function (data) {
            setN("idTipoMedicamento", data.idTipoMedicamento);
            setN("nombre", data.nombre);
            setN("descripcion", data.descripcion);
        });
    }  
    else {
        limpiarTipoMedicamento();
        setN("idTipoMedicamento", id);
    }
}

