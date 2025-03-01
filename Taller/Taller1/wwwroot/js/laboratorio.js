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
    let form = document.getElementById("frmFiltrar");
    let frm = new FormData(form);
    fetchPost("Laboratorio/filtrarLaboratorio", "json", frm, function (res) {
        let objConfiguracion = {
            url: "",
            nombreCabeceras: ["Id Laboratorio", "Nombre", "Direccion", "Persona Contacto"],
            nombrePropiedades: ["idLaboratorio", "nombre", "direccion", "personaContacto"],
            editar: true,
            eliminar: true,
            propiedadID: "idLaboratorio"
        };
        let contenido = generarTabla(objConfiguracion, res);
        document.getElementById("divTable").innerHTML = contenido;
        iniciarDataTable();
    })
}

async function guardarLaboratorio() {
    let form = document.getElementById("frmOperaciones");
    let frm = new FormData(form);
    confirmacion(undefined, undefined, function (resp) {
        fetchPost("laboratorio/guardarLaboratorio", "json", frm, function (res) {
            if (res == 0) {
                ErrorA();
                return;
            }
            limpiarLaboratorio();
            Exito();
        });

    });
}

function Editar(id) {
    if (id != 0){
        fetchGet("laboratorio/recuperarLaboratorio/?id=" + id, "json", function (data) {
            setN("idLaboratorio", data.idLaboratorio);
            setN("nombre", data.nombre);
            setN("direccion", data.direccion);
            setN("personaContacto", data.personaContacto);
            setN("numeroContacto", data.numeroContacto);
        });
    }
    else {
        limpiarLaboratorio();
        setN("idLaboratorio", id);
    }
    
}

function eliminar(id) {
    fetchGet("laboratorio/eliminarLaboratorio/?id=" + id, "json", function (data) {
        confirmacion(undefined, "¿Seguro desea eliminar?", function (resp) {
            limpiarLaboratorio();
            Exito();
        });
    });
}

function limpiarLaboratorio() {
    limpiarDatos("frmOperaciones");
    filtrarLaboratorio();
}
