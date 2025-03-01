window.onload = function () {
    listarSucursal();
}

async function listarSucursal() {

    pintar({
        url: "sucursal/listarSucursal",
        nombreCabeceras: ["Id Sucursal", "Nombre", "Direccion"],
        nombrePropiedades: ["idSucursal", "nombre", "direccion"],
        editar: true,
        eliminar: true,
        propiedadID: "idSucursal"
    });

}

async function filtrarSucursal() {
    
    let form = document.getElementById("frmFiltrar");
    let frm = new FormData(form);
    fetchPost("Sucursal/filtrarSucursal", "json", frm, function (res) {
        let objConfiguracion = {
            url: "",
            nombreCabeceras: ["Id Sucursal", "Nombre", "Direccion"],
            nombrePropiedades: ["idSucursal", "nombre", "direccion"],
            editar: true,
            eliminar: true,
            propiedadID: "idSucursal"
        }
        let contenido = generarTabla(objConfiguracion, res);
        document.getElementById("divTable").innerHTML = contenido;
    });
}

function Editar(id) {
    if (id != 0) {
        fetchGet("Sucursal/recuperarSucursal/?id=" + id, "json", function (data) {
            setN("idSucursal", data.idSucursal);
            setN("nombre", data.nombre);
            setN("direccion", data.direccion);
        });
    }
    else {
        limpiarSucursal();
        setN("idSucursal", id);
    }

}

async function guardarSucursal() {
    let form = document.getElementById("frmOperaciones");
    let frm = new FormData(form);
    confirmacion(undefined, undefined, function (resp) {
        fetchPost("Sucursal/guardarSucursal", "json", frm, function (res) {
            if (res == 0) {
                ErrorA();
                return;
            }
            limpiarSucursal();
            Exito();
        });

    });
}

function eliminar(id) {
    fetchGet("Sucursal/eliminarSucursal/?id=" + id, "json", function (data) {
        confirmacion(undefined, "¿Seguro desea eliminar?", function (resp) {
            limpiarSucursal();
            Exito();
        });
    });
}

async function limpiarSucursal() {
    limpiarDatos("frmOperaciones");
    listarSucursal();
}