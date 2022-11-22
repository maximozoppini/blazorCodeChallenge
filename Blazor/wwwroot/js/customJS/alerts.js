function showError(mensajeError) {
    return $.growl.error({
        title: "Error",
        message: mensajeError
    });
}

function showSuccess(mensajeExito) {
    return $.growl.notice({
        title: "Exito",
        message: mensajeExito
    });
}

function showWarning(mensajeAdvertencia) {
    return $.growl.warning({
        title:"Advertencia",
        message: mensajeAdvertencia
    });
}


