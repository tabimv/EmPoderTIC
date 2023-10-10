/*========================================
Slider
==========================================*/
if (document.querySelector('.contenedor-slider')) {

    let index = 1;
    let selectedIndex = 1;

    const slides = document.querySelector('.slider');
    const slide = slides.children;
    const slidesTotal = slides.childElementCount;

    const dots = document.querySelector('.dots');
    const prev = document.querySelector('.prev');
    const next = document.querySelector('.next');


    //agregamos los punto de acuerdo a la cantidad de slides
    for (let i = 0; i < slidesTotal; i++) {
        dots.innerHTML += '<span class="dot"></span>';
    }

    //ejecutamos la funcion
    mostrarSlider(index);

    //hacemos que nuestro slide sea automatico
    setInterval(() => {
        mostrarSlider(index = selectedIndex);
    }, 5000); //rempresentados en milesegundos

    //funcion para mostrar el slider
    function mostrarSlider(num) {
        if (selectedIndex > slidesTotal) {
            selectedIndex = 1;
        } else {
            selectedIndex++;
        }

        if (num > slidesTotal) {
            index = 1;
        }

        if (num < 1) {
            index = slidesTotal;
        }

        //removemos la clase active de todos los slide
        for (let i = 0; i < slidesTotal; i++) {
            slide[i].classList.remove('active');
        }

        //removemos la clase active de los puntos

        for (let x = 0; x < dots.children.length; x++) {
            dots.children[x].classList.remove('active');
        }

        //mostramos el slide
        slide[index - 1].classList.add('active');

        //agregamos la clase active al punto donde se encuntra el slide
        dots.children[index - 1].classList.add('active');


    }

    //evento para nuestro botones prev y next
    next.addEventListener('click', (e) => {
        mostrarSlider(index += 1);
        selectedIndex = index;
    });

    prev.addEventListener('click', (e) => {
        mostrarSlider(index += -1);
        selectedIndex = index;
    });

    //puntos
    for (let y = 0; y < dots.children.length; y++) {

        //por cada dot que ecuentre le agregamos el evento click y ejecutamo la funcion de slide
        dots.children[y].addEventListener('click', () => {
            mostrarSlider(index = y + 1);
            selectedIndex = y + 1;
        });
    }

}




/*========================================
Mostrar contrase�a
==========================================*/
const mostrarClave = document.querySelectorAll('.mostrarClave');
const inputPass = document.querySelectorAll('.clave');

for (let i = 0; i < mostrarClave.length; i++) {

    mostrarClave[i].addEventListener('click', () => {

        if (inputPass[i].type === "password") {

            //cambiamos el tipo password a text
            inputPass[i].setAttribute('type', 'text');

            //removemos la clase del icono
            mostrarClave[i].classList.remove('fa-eye');

            //agregamos el nuevo icono
            mostrarClave[i].classList.add('fa-eye-slash');

            //le agregamos la clase active
            mostrarClave[i].classList.add('active');

        } else {
            //si el tipo de input es text

            //cambiamos el tipo text a password
            inputPass[i].setAttribute('type', 'password');

            //removemos la clase del icono
            mostrarClave[i].classList.remove('fa-eye-slash');

            //agregamos el nuevo icono
            mostrarClave[i].classList.add('fa-eye');

            //le agregamos la clase active
            mostrarClave[i].classList.remove('active');

        }

    });
}

/*========================================
    V�lidamos formulario de login
==========================================*/
/*========================================
    V�lidamos formulario de login
==========================================*/
if (document.getElementById('btnLogin')) {

    const btnLogin = document.getElementById('btnLogin');

    btnLogin.addEventListener('click', (e) => {

        e.preventDefault();

        const msError = document.querySelector('#formLogin .error-text');
        msError.innerHTML = ""; // Limpiamos cualquier mensaje de error existente
        msError.classList.remove('active');

        correo = formLogin.correo.value.trim();
        password = formLogin.password.value.trim();

        if (correo == "" && password == "") {
            mostrarError('Por favor ingrese su usuario/contrase�a', msError);
            inputError([formLogin.correo, formLogin.password]);
            return false;
        } else {
            inputErrorRemove([formLogin.correo, formLogin.password]);
        }

        // Validar que el correo contenga el s�mbolo "@"
        if (correo == "" || correo == null || correo.indexOf('@') === -1) {
            mostrarError('Por favor ingrese una direcci�n de correo v�lida con "@"', msError);
            inputError([formLogin.correo]);
            formLogin.correo.focus();
            return false;
        }

        if (password == "" || password == null) {
            mostrarError('Por favor ingrese su contrase�a', msError);
            inputError([formLogin.password]);
            formLogin.password.focus();
            return false;
        }

        //enviamos el formulario
        formLogin.submit();
        return true;

    });
}



/*
    CREAMOS FUNCIONES PARA MOSTRAR ERROR EN PANTALLA Y ADEMAS VALIDAR SI LOS CAMPOS SON INGRESADOS CORECTAMENTE
*/

/*========================================
Mostrar Error
==========================================*/
function mostrarError(mensaje, msError) {

    //agregamos la clase active
    msError.classList.add('active');

    msError.innerHTML = '<p>' + mensaje + '</p>';
}

/*========================================
Agregar class error input
==========================================*/
//a esta funcion le pasamos un array

function inputError(datos) {
    for (let i = 0; i < datos.length; i++) {

        //a cada input le agregamos una clase error
        datos[i].classList.add('input-error');

    }

}

//a esta funcion le pasamos un array
function inputErrorRemove(datos) {
    for (let i = 0; i < datos.length; i++) {
        //removemos la clase
        datos[i].classList.remove('input-error');

    }

}

/*===============================================
    V�lidamos solo letras y numeros
================================================*/
function validarLetrasNumeros(valor) {
    if (!/^[a-zA-Z0-9]+$/.test(valor)) {
        return false;
    }
    return true;
}


/*===============================================
    V�lidamos solo letras
================================================*/
function validarSoloLetras(valor) {
    if (!/^[a-zA-Z������������ ]*$/.test(valor)) {
        return false;
    }
    return true;
}

/*===============================================
    V�lidamos un corrreo valido
================================================*/

function validarCorreo(valor) {
    if (!/^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,4})+$/.test(valor)) {
        return false;
    }

    return true;
}

/*===============================================
    V�lidamos solo n�mero
================================================*/

function validarSoloNumeros(valor) {
    if (!/^([0-9]{8})*$/.test(valor)) {
        return false;
    }
    return true;
}

