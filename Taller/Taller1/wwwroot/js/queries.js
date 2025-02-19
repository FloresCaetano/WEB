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

    fetchGet(objConfiguracion.url, "json", function (res) {
        let contenido = generarTabla(objConfiguracion, res);
        document.getElementById("divTable").innerHTML = contenido;
    });

}

let objConfiguracionGlobal;
function generarTabla(objConfiguracion, res) {

    let contenido = "";
    contenido += "<table class = 'table'>";
    contenido += "<thead>";
    contenido += "<tr>";

    for (let i = 0; i < objConfiguracion.nombreCabeceras.length; i++) {
        contenido += "<td>" + objConfiguracion.nombreCabeceras[i] + "</td>";
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
        contenido += "</tr>";
    }
    contenido += "</table>";
    return contenido;

}
