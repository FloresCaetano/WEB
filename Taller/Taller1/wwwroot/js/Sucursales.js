window.onload = function () {
    listarTipoMedicamento();
}

async function listarTipoMedicamento() {

    pintar({
        url: "sucursal/listarSucursal",
        nombreCabeceras: ["Id Sucursal", "Nombre", "Direccion"],
        nombrePropiedades: ["idSucursal", "nombre", "direccion"]
    });

}

async function buscarSucursal() {
    
    let form = document.getElementById("frmSucursal");
    let frm = new FormData(form);
    fetchPost("Sucursal/filtrarSucursal", "json", frm, function (res) {
        let objConfiguracion = {
            url: "",
            nombreCabeceras: ["Id Sucursal", "Nombre", "Direccion"],
            nombrePropiedades: ["idSucursal", "nombre", "direccion"]
        }
        let contenido = generarTabla(objConfiguracion, res);
        document.getElementById("divTable").innerHTML = contenido;
    });
}

async function limpiarSucursal() {
    limpiarDatos("frmSucursal");
    buscarSucursal();
}