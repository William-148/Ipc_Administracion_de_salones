<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="recuperar-cuenta.aspx.cs" Inherits="PlataformaWeb.paginas.recuperar_cuenta" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Recuperar Cuenta</title>
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
      <a href="/paginas/login.aspx" id="Registro">Ingresar</a>
    </nav>

    <!-- Contenido-->
    <section>
      <form method="post" runat="server" style="width:450px;">
        <h2><span class="icon-circle-with-plus"></span> Recuperar Contraseña</h2>
        

            <a>Correo*: </a>
            <asp:TextBox ID="correo" placeholder="Correo" runat="server" class="cajaTexto" TextMode="Email"></asp:TextBox>

             <a>Palabra Clave*: </a>
            <asp:TextBox ID="palabraClave" placeholder="Palabra Clave" runat="server" class="cajaTexto" TextMode="SingleLine"></asp:TextBox>

          <div>
              <asp:Label ID="mensaje" runat="server" Text="" CssClass="mensaje"></asp:Label>
          </div><br/>

            <asp:Button ID="boton" runat="server" Text="Enviar" class="boton" OnClick="boton_Click" />
            <a class="forgot">(*) Campos Obligatorios. </a>

        
      </form>
    </section>

    <!-- Pie de Pagina-->
    <footer class="piepagina">
      Facultad de Ingenieria USAC. Derechos Reservados, Teléfono: 43234533
    </footer>
</body>
</html>
