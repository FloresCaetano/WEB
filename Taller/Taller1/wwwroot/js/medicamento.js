window.onload = function () {
    listarTipoMedicamento();
}

async function listarTipoMedicamento() {
    fetchGet("Medicamento/saludar", "text", function (res) {
        alert(res);
    });

}