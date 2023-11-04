/*=================================================
Detecta la posición automatica del mini container
===================================================*/
// JavaScript para ajustar automáticamente la altura del #header y el .mini-container
const header = document.getElementById("header");
const miniContainer = document.querySelector(".mini-container");

function ajustarAlturaHeader() {
    const miniContainerRect = miniContainer.getBoundingClientRect();
    const miniContainerHeight = miniContainerRect.height;

    // Ajustar la altura del #header
    header.style.minHeight = `${miniContainerHeight + 20}px`; // Agrega 20px de margen
}

// Llamar a la función inicialmente y cuando se redimensione la ventana
window.addEventListener("load", ajustarAlturaHeader);
window.addEventListener("resize", ajustarAlturaHeader);


