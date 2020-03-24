<%@ Page Title="" Language="C#" MasterPageFile="~/paginas/masterAdmin.Master" AutoEventWireup="true" CodeBehind="cargar-datos.aspx.cs" Inherits="PlataformaWeb.paginas.administrador.cargar_datos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Cargar Datos</title>
    <script>
        function seleccionar() {            
            var opcion = document.getElementsByClassName("opcion")[4];
            opcion.style.background = '#101010';
        }
        window.addEventListener("load", seleccionar, false);
    </script>
    <style>
        .cargarDatos{
            width: 100%;
            height: 40px;
            margin-bottom: 20px;

        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">


    <form method="post" runat="server" style="width:800px; margin-top: 150px">
        <h2><span class="icon-add-user"></span>  -Importar Usuarios</h2>

        <a>Seleccionar Archivo: </a>
        <asp:FileUpload ID="fileUpload" runat="server" CssClass="cargarDatos"  />

        <asp:TextBox ID="consola" runat="server" CssClass="cajaTexto" TextMode="MultiLine" Height="100px" ReadOnly="True"></asp:TextBox>

        <asp:Button ID="boton" runat="server" Text="Importar" class="boton" OnClick="boton_Click" />
           
      </form>



</asp:Content>
