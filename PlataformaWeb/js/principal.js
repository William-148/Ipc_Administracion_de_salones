var menu, contenido, botonMenu, barra;
function anchoContenido(){
  botonMenu = document.getElementsByClassName("menu-bar")[0];
  menu = document.getElementsByTagName("aside")[0];
  barra = document.getElementsByTagName("nav")[0];
  contenido = document.getElementsByTagName("section")[0];

  botonMenu.addEventListener("click", modificarAncho, false);
}

function establecerAncho(){
    var aux = parseInt(barra.offsetWidth - menu.offsetWidth);
    contenido.style.webkitTransition = 'all 0s';
    contenido.style.width = aux + "px";
    contenido.style.webkitTransition = 'all 0.5s';
}

function modificarAncho(){


  var margenIzquierdo = contenido.offsetLeft;
  if( margenIzquierdo == 0){
    var aux = parseInt(barra.offsetWidth - menu.offsetWidth);
    contenido.style.width = aux +"px";
  }else{
    contenido.style.width = "100%";
  }
}

window.addEventListener("load", anchoContenido, false);
window.addEventListener("load", establecerAncho, false);
