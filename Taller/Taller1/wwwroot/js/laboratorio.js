window.onload = function () {
    filtrarLaboratorio();
}


async function buscarLaboratorio(nombre, descripcion, contacto) {

    pintar({
        url: "Laboratorio/filtrarLaboratorio/?nombre=" + nombre + "&direccion=" + descripcion + "&contacto=" + contacto,
        nombreCabeceras: ["Id Laboratorio", "Nombre", "Direccion", "Persona Contacto"],
        nombrePropiedades: ["idLaboratorio", "nombre", "direccion", "personaContacto"]
    });
}
async function filtrarLaboratorio() {
    let form = document.getElementById("frmBusqueda");
    let frm = new FormData(form);
    fetchPost("Laboratorio/filtrarLaboratorio", "json", frm, function (res) {
        let objConfiguracion = {
            url: "",
            nombreCabeceras: ["Id Laboratorio", "Nombre", "Direccion", "Persona Contacto"],
            nombrePropiedades: ["idLaboratorio", "nombre", "direccion", "personaContacto"]
        };
        let contenido = generarTabla(objConfiguracion, res);
        document.getElementById("divTable").innerHTML = contenido;
    })
}

function limpiarLaboratorio() {
    limpiarDatos("frmBusqueda");
    filtrarLaboratorio();
}
