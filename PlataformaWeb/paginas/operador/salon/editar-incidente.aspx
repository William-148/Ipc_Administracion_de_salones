<%@ Page Title="" Language="C#" MasterPageFile="~/paginas/operador/operador.Master" AutoEventWireup="true" CodeBehind="editar-incidente.aspx.cs" Inherits="PlataformaWeb.paginas.operador.salon.editar_incidente" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Editar Incidente de Salón</title>
    <script>
        function seleccionar() {            
            var opcion = document.getElementsByClassName("opcion")[3];
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
        <a href="/paginas/operador/salon/lista-incidentes.aspx"><span class="icon-back"></span> Lista Incidentes</a>
    </div>

    <form method="post" runat="server" style="width:800px;">
        <h2><span class="icon-circle-with-plus"></span> Editar Incidente</h2>
        
        <div class="caja">
            <a>Fecha del Incidente*: </a>
            <asp:TextBox ID="fecha" runat="server" class="cajaTexto" TextMode="Date"></asp:TextBox>

            <a>Id Usuario Responsable*: </a>
            <asp:TextBox ID="idUsuario" placeholder="Identificación en el sistema" runat="server" class="cajaTexto" TextMode="Number"></asp:TextBox>

            <a>Nombre Responsable: </a>
            <asp:TextBox ID="nombre" runat="server" class="cajaTexto" ReadOnly="True"></asp:TextBox>

            <a>Descripción*: </a>
            <asp:TextBox ID="descripcion" placeholder="Descripción del suceso..." runat="server" class="cajaTexto" Height="100px" TextMode="MultiLine" Wrap="True"></asp:TextBox>
                                               
            <asp:Button ID="boton" runat="server" Text="Actualizar" class="boton" OnClick="boton_Click" />
            <a class="forgot">(*) Campos Obligatorios. </a>

        </div>


        <div class="caja">

            <a>Edificio: </a>
            <asp:DropDownList ID="edificio" CssClass="cajaLista" runat="server" DataSourceID="Edificios" DataTextField="Nombre" DataValueField="idEdificio" ></asp:DropDownList>
            <asp:SqlDataSource ID="Edificios" runat="server" ConnectionString="<%$ ConnectionStrings:PlataformaWebConnectionString %>" SelectCommand="SELECT * FROM [Edificio]"></asp:SqlDataSource>
            
            <asp:Button ID="verSalones" runat="server" Text="Ver Salones" class="boton2" OnClick="verSalones_Click"  />
            
            <asp:Label ID="tituloSalon" runat="server" Text="Salon*(T3):" CssClass="letras"></asp:Label>
            <asp:DropDownList ID="salon" runat="server" CssClass="cajaLista" DataSourceID="nombresSalones" DataTextField="Salon" DataValueField="idSalon" ></asp:DropDownList>
            <asp:SqlDataSource ID="nombresSalones" runat="server" ConnectionString="<%$ ConnectionStrings:PlataformaWebConnectionString %>" SelectCommand="SELECT [idSalon], [Salon] FROM Salon WHERE idEdificio = 1 ORDER BY Salon"></asp:SqlDataSource>

            <asp:Label ID="title" runat="server" Text="Estado del Incidente:" CssClass="letras"></asp:Label>
            <asp:DropDownList ID="estadoIncidente" runat="server" CssClass="cajaLista" DataSourceID="SqlDataSource1" DataTextField="Estado" DataValueField="idEstadoIncidente" ></asp:DropDownList>
            

            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:PlataformaWebConnectionString %>" SelectCommand="SELECT * FROM [EstadoIncidente]"></asp:SqlDataSource>
            

            <asp:TextBox ID="consola" placeholder="Mensajes del Sistema..." runat="server" class="cajaTexto" TextMode="MultiLine" ReadOnly="True" Height="100px"></asp:TextBox>          
        </div>
        
      </form>



</asp:Content>
