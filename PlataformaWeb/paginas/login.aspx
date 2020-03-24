<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="PlataformaWeb.paginas.login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>LogIn</title>
    <link rel="stylesheet" href="/css/login.css"/>     
    <link rel="stylesheet" href="/fonts/style.css" />
</head>
<body>
    <!-- Barra de navegación-->
    <nav >
      <figure class="logo">
        <img id="logoUsac" src="/img/usac.png" alt=""/>
      </figure>
      <h2>USAC</h2>
      <a href="/paginas/registrar.aspx" id="Registro">Registrarse</a>
    </nav>

    <!-- Contenido-->
    <section>
      <form method="post" runat="server">
        <h2><span class="icon-login "></span>  - Iniciar Sesion</h2>
        <a>Nombre de Usuario: </a>
        <asp:TextBox ID="usuario" placeholder="Usuario" runat="server" class="cajaTexto"></asp:TextBox>

        <a>Contraseña: </a>
        <asp:TextBox ID="clave" placeholder="Contraseña" runat="server" class="cajaTexto" TextMode="Password"></asp:TextBox>
          <div>
              <asp:Label ID="mensaje" runat="server" Text="" CssClass="mensaje"></asp:Label>
          </div><br/>

         <asp:Button ID="boton" runat="server" Text="Login" class="boton" OnClick="boton_Click" />
        <a href="/paginas/recuperar-cuenta.aspx" class="forgot">¿Olvidó su Contraseña?</a>
          
      </form>
    </section>

    <!-- Pie de Pagina-->
    <footer class="piepagina">
      Facultad de Ingenieria USAC. Derechos Reservados, Teléfono: 43234533
    </footer>
</body>
</html>
