<%@ Page Title="" Language="C#" MasterPageFile="~/paginas/operador/operador.Master" AutoEventWireup="true" CodeBehind="crear.aspx.cs" Inherits="PlataformaWeb.paginas.operador.usuario.crear" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Crear Usuario</title>
    <script>
        function seleccionar() {            
            var opcion = document.getElementsByClassName("opcion")[1];
            opcion.style.background = '#101010';
        }
        window.addEventListener("load", seleccionar, false);
    </script>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--operador-->
    <div class="subNavegacion">
        <a href="/paginas/operador/usuario/lista.aspx"><span class="icon-list "></span> Listado</a>        
    </div>

    <form method="post" runat="server" style="width:800px;">
        <h2><span class="icon-add-user"></span>  Nuevo Usuario</h2>
        <div class="caja">
            <a>Rol de Usuario*: </a>
            <asp:DropDownList ID="rol" runat="server" CssClass="cajaLista" >
                <asp:ListItem Value="4">Estudiante</asp:ListItem>
                <asp:ListItem Value="3">Instructor</asp:ListItem>
            </asp:DropDownList>

            <a>Nombre Completo*: </a>
            <asp:TextBox ID="nombre" placeholder="Nombre" runat="server" class="cajaTexto"></asp:TextBox>

            <a>Carnet*: </a>
            <asp:TextBox ID="carnet" placeholder="Carnet" runat="server" class="cajaTexto" TextMode="Number"></asp:TextBox>

            <a>Fecha de Nacimiento*: </a>
            <asp:TextBox ID="nacimiento" runat="server" class="cajaTexto" TextMode="Date"></asp:TextBox>
                        
             <a>Palabra Clave*: </a>
            <asp:TextBox ID="palabraClave" placeholder="Palabra Clave" runat="server" class="cajaTexto" TextMode="SingleLine"></asp:TextBox>

            <asp:Button ID="boton" runat="server" Text="Registrar" class="boton" OnClick="boton_Click" />
            <a id="forgot">(*) Campos Obligatorios.</a>

        </div>

        <div class="caja">
            <a>Correo*: </a>
            <asp:TextBox ID="correo" placeholder="Correo" runat="server" class="cajaTexto" TextMode="Email"></asp:TextBox>

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

</asp:Content>
