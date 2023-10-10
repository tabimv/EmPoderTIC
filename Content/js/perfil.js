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

/*=================================================
JavaScript para mostrar/ocultar el menú compartido
===================================================*/
// Primer botón de compartir
const compartirBtn1 = document.getElementById('compartir-btn-1');
const menuCompartir1 = document.querySelector('.menu-compartir-1');

compartirBtn1.addEventListener('click', () => {
    // Alternamos la clase "visible" en el menú compartido para mostrarlo u ocultarlo
    menuCompartir1.classList.toggle('visible');
});


// Segundo botón de compartir
const compartirBtn2 = document.getElementById('compartir-btn-2');
const menuCompartir2 = document.querySelector('.menu-compartir-2');

compartirBtn2.addEventListener('click', () => {
    // Alternamos la clase "visible" en el segundo menú compartido para mostrarlo u ocultarlo
    menuCompartir2.classList.toggle('visible');
});


// Tercer botón de compartir
const compartirBtn3 = document.getElementById('compartir-btn-3');
const menuCompartir3 = document.querySelector('.menu-compartir-3');

compartirBtn3.addEventListener('click', () => {
    // Alternamos la clase "visible" en el segundo menú compartido para mostrarlo u ocultarlo
    menuCompartir3.classList.toggle('visible');
});