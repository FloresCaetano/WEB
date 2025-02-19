window.onload = function() {
    listarTipoMedicamento();
}

async function listarTipoMedicamento() {

    pintar({
        url: "tipoMedicamento/listarTipoMedicamento",
        nombreCabeceras: ["Id tipo medicamento", "Nombre", "Descripcion", "Stock"],
        nombrePropiedades: ["idTipoMedicamento", "nombre", "descripcion", "stock"]
    });

}

async function buscarTipoMedicamento() {
    let nombre = document.getElementById("txtNombre").value;
    pintar({
        url: "tipoMedicamento/filtrarTipoMedicamento/?nombre=" + nombre,
        nombreCabeceras: ["Id tipo medicamento", "Nombre", "Descripcion", "Stock"],
        nombrePropiedades: ["idTipoMedicamento", "nombre", "descripcion", "stock"]
    });
}

async function filtrarTipoMedicamento() {
    let nombre = document.getElementById("txtNombre").value;
    buscarTipoMedicamento();
}