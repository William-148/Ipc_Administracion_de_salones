<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="registrar.aspx.cs" Inherits="PlataformaWeb.paginas.registrar" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Registrar</title>
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
      <form method="post" runat="server" style="width:800px;">
        <h2><span class="icon-circle-with-plus"></span> Registrarse</h2>
        <div class="caja">
            <a>Nombre Completo*: </a>
            <asp:TextBox ID="nombre" placeholder="Nombre" runat="server" class="cajaTexto"></asp:TextBox>

            <a>Carnet*: </a>
            <asp:TextBox ID="carnet" placeholder="Carnet" runat="server" class="cajaTexto" TextMode="Number"></asp:TextBox>

            <a>Fecha de Nacimiento*: </a>
            <asp:TextBox ID="nacimiento" runat="server" class="cajaTexto" TextMode="Date"></asp:TextBox>

             <a>Correo*: </a>
            <asp:TextBox ID="correo" placeholder="Correo" runat="server" class="cajaTexto" TextMode="Email"></asp:TextBox>

             <a>Palabra Clave*: </a>
            <asp:TextBox ID="palabraClave" placeholder="Palabra Clave" runat="server" class="cajaTexto" TextMode="SingleLine"></asp:TextBox>

            <asp:Button ID="boton" runat="server" Text="Registrar" class="boton" OnClick="boton_Click" />
            <a class="forgot">(*) Campos Obligatorios. </a>

        </div>

        <div class="caja">

            <a>Teléfono: </a>
            <asp:TextBox ID="telefono" placeholder="Telefono" runat="server" class="cajaTexto" TextMode="Phone"></asp:TextBox>

            <a>Usuario*: </a>
            <asp:TextBox ID="usuario" placeholder="Usuario" runat="server" class="cajaTexto"></asp:TextBox>

            <a>Contraseña*: </a>
            <asp:TextBox ID="clave" placeholder="Contraseña" runat="server" class="cajaTexto" TextMode="Password"></asp:TextBox>

            <a>Ingrese Nuevamente su Contraseña*: </a>
            <asp:TextBox ID="clave2" placeholder="Contraseña" runat="server" class="cajaTexto" TextMode="Password"></asp:TextBox>

            <asp:Label ID="mensaje" runat="server" Text="" CssClass="mensaje"></asp:Label>
        </div>
        
        
      </form>
    </section>

    <!-- Pie de Pagina-->
    <footer class="piepagina">
      Facultad de Ingenieria USAC. Derechos Reservados, Teléfono: 43234533
    </footer>
</body>
</html>
