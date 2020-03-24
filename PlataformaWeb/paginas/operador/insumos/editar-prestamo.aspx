<%@ Page Title="" Language="C#" MasterPageFile="~/paginas/operador/operador.Master" AutoEventWireup="true" CodeBehind="editar-prestamo.aspx.cs" Inherits="PlataformaWeb.paginas.operador.insumos.editar_prestamo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        function seleccionar() {            
            var opcion = document.getElementsByClassName("opcion")[4];
            opcion.style.background = '#101010';
        }
        window.addEventListener("load", seleccionar, false);
    </script>
    <style>
        .boton2{
            width: 100px;
            height: 40px;
            border: none;
            display:block;
            border-radius: 5px;
            padding: 0;
            text-align: center;
            font-size: 13px;
            background: #0a9d90;
            color: #fff;
            margin-left:0px;
            margin-bottom: 10px;
            cursor:pointer;
        }

        .letras{
            display:inline-block;
            color: #141618;
            font: normal 14px arial,sans-serif;
            padding-bottom:5px;
            padding-right:10px;
        }
        .mensaje{
            display: block;
        }

    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="subNavegacion">
        <a href="/paginas/operador/insumos/lista-insumos.aspx"><span class="icon-list"></span> Lista Insumos</a>
        <a href="/paginas/operador/insumos/lista-prestamo.aspx"><span class="icon-list"></span> Lista Prestamo</a>
    </div>

    <form method="post" runat="server" style="width:800px;">
        <h2><span class="icon-circle-with-plus"></span> Editar Prestamo</h2>
        
        <div class="caja">
            <a>Insumo*: </a>
            <asp:DropDownList ID="insumoLista" CssClass="cajaLista" runat="server" DataSourceID="insumodatos" DataTextField="Nombre" DataValueField="idInsumo" Width="70%"></asp:DropDownList>
            <asp:SqlDataSource ID="insumodatos" runat="server" ConnectionString="<%$ ConnectionStrings:PlataformaWebConnectionString %>" SelectCommand="SELECT [idInsumo], [Nombre] FROM [Insumo]"></asp:SqlDataSource>

            <a>Id Usuario*: </a>
            <asp:TextBox ID="idUsuario" placeholder="Identificación en el sistema" runat="server" class="cajaTexto" TextMode="Number"></asp:TextBox>
            
            <a>Usuario*: </a>
            <asp:TextBox ID="nombre"  runat="server" class="cajaTexto" Wrap="True" ReadOnly="True"></asp:TextBox>                 
                
                                                   
            <asp:Button ID="boton" runat="server" Text="Actualizar" class="boton" OnClick="boton_Click"  />
            <a class="forgot">(*) Campos Obligatorios. </a>

        </div>


        <div class="caja">            
            <a>Estado*: </a>
            <asp:DropDownList ID="estado" CssClass="cajaLista" runat="server" DataSourceID="SqlDataSource1" DataTextField="Estado" DataValueField="idEstadoPrestamo" Width="70%"></asp:DropDownList>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:PlataformaWebConnectionString %>" SelectCommand="SELECT * FROM [EstadoPrestamo]"></asp:SqlDataSource>

            <a>Descripción*: </a>
            <asp:TextBox ID="descripcion" placeholder="Descripción" runat="server" class="cajaTexto" Height="100px" TextMode="MultiLine" Wrap="True"></asp:TextBox>                 
                

            <asp:TextBox ID="consola" placeholder="Mensajes del Sistema..." runat="server" class="cajaTexto" TextMode="MultiLine" ReadOnly="True" Height="100px"></asp:TextBox>          
        </div>
        
      </form>



</asp:Content>
