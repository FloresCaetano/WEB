window.onload = function () {
    filtrarMedicamento();
}

async function filtrarMedicamento() {
    let form = document.getElementById("frmFiltrar");
    let frm = new FormData(form);
    fetchPost("Medicamento/filtrarMedicamento", "json", frm, function (res) {
        let objConfiguracion = {
            url: "",
            nombreCabeceras: ["Id medicamento", "Nombre", "Nombre Lab", "Nombre tipo medicamento"],
            nombrePropiedades: ["idMedicamento", "nombre", "nombreLaboratorio", "nombreTipoMedicamento"],
            editar: true,
            eliminar: true,
            propiedadID: "idMedicamento"
        };
        let contenido = generarTabla(objConfiguracion, res);
        document.getElementById("divTable").innerHTML = contenido;
        iniciarDataTable();
    })
}

async function guardarMedicamento() {
    let form = document.getElementById("frmOperaciones");
    let frm = new FormData(form);
    confirmacion(undefined, undefined, function (resp) {
        fetchPost("Medicamento/guardarMedicamento", "json", frm, function (res) {
            if (res == -1) {
                ErrorA();
                return;
            }
            if (res == 0) {
                ErrorA("Revisa id Lab e id Tipo med (deben coincidir con los valores existentes)");
                return;
            }
            limpiarMedicamento();
            Exito();
        });

    });
}

function Editar(id) {
    if (id != 0) {
        fetchGet("Medicamento/recuperarMedicamento?id=" + id, "json", function (data) {
            setN("idMedicamento", data.idMedicamento);
            setN("codigo", data.codigo);
            setN("nombre", data.nombre);
            setN("idLab", data.idLab);
            setN("idTipoMed", data.idTipoMed);
            setN("uso", data.uso);
            setN("contenido", data.contenido);
        });
    }
    else {
        limpiarMedicamento();
        setN("idMedicamento", id);
    }

}

function eliminar(id) {
    fetchGet("Medicamento/eliminarMedicamento/?id=" + id, "json", function (data) {
        confirmacion(undefined, "¿Seguro desea eliminar?", function (resp) {
            limpiarMedicamento();
            Exito();
        });
    });
}

function limpiarMedicamento() {
    limpiarDatos("frmOperaciones");
    filtrarMedicamento();
}
