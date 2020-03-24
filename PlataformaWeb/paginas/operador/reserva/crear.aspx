<%@ Page Title="" Language="C#" MasterPageFile="~/paginas/operador/operador.Master" AutoEventWireup="true" CodeBehind="crear.aspx.cs" Inherits="PlataformaWeb.paginas.operador.reserva.crear" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Crear Reserva</title>
    <script>
        function seleccionar() {            
            var opcion = document.getElementsByClassName("opcion")[2];
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
        <a href="/paginas/operador/reserva/lista-reservas.aspx"><span class="icon-list"></span> Lista de Reservaciones</a>
    </div>

    <form method="post" runat="server" style="width:800px;">
        <h2><span class="icon-circle-with-plus"></span>  Crear Reservación</h2>

        <div class="caja">
            
            <a>Fecha a reservar*: </a>
            <asp:TextBox ID="fecha" runat="server" class="cajaTexto" TextMode="Date"></asp:TextBox>

            <a>Hora de la reserva (horas:minutos)*: </a>
            <asp:TextBox ID="hora" runat="server" class="cajaTexto" TextMode="Time"></asp:TextBox>
                        
            <a>Duración de la reserva (horas:minutos)*: </a>
            <asp:TextBox ID="duracion" runat="server" class="cajaTexto" TextMode="Time"></asp:TextBox>

            <a>Periodo de la reserva*: </a>
            <asp:DropDownList ID="periodo" runat="server" CssClass="cajaLista" DataSourceID="SqlDataSource1" DataTextField="Periodo" DataValueField="idPeriodoReserva" ></asp:DropDownList>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:PlataformaWebConnectionString %>" SelectCommand="SELECT * FROM [PeriodoReserva]"></asp:SqlDataSource>

            <a>Tipo de Actividad*: </a>
            <asp:DropDownList ID="tipoActividad" CssClass="cajaLista" runat="server" DataSourceID="actividad" DataTextField="Nombre" DataValueField="idTipoActividad"></asp:DropDownList>
            <asp:SqlDataSource ID="actividad" runat="server" ConnectionString="<%$ ConnectionStrings:PlataformaWebConnectionString %>" SelectCommand="SELECT * FROM [TipoActividad]"></asp:SqlDataSource>
                                                         
            <asp:Button ID="boton" runat="server" Text="Registrar" class="boton" OnClick="boton_Click"  />
            <a id="forgot">(*) Campos Obligatorios.</a>

        </div>

        <div class="caja">

            <a>Id del Instructor*: </a>
            <asp:TextBox ID="idInstructor" placeholder="Identificación en el Sistema" runat="server" class="cajaTexto" TextMode="Number"></asp:TextBox>
                        
            <a>Edificio del Salon*: </a>
            <asp:DropDownList ID="edificio" runat="server" CssClass="cajaLista" DataSourceID="edificios" DataTextField="Nombre" DataValueField="idEdificio" ></asp:DropDownList>
            <asp:SqlDataSource ID="edificios" runat="server" ConnectionString="<%$ ConnectionStrings:PlataformaWebConnectionString %>" SelectCommand="SELECT * FROM [Edificio]"></asp:SqlDataSource>

            <asp:Button ID="verSalones" runat="server" Text="Ver Salones" class="boton2" OnClick="verSalones_Click"  />

            <asp:Label ID="tituloSalon" runat="server" Text="Salon*(T3):" CssClass="letras"></asp:Label>
            <asp:DropDownList ID="salon" runat="server" CssClass="cajaLista" DataSourceID="nombresSalones" DataTextField="Salon" DataValueField="idSalon" ></asp:DropDownList>
            <asp:SqlDataSource ID="nombresSalones" runat="server" ConnectionString="<%$ ConnectionStrings:PlataformaWebConnectionString %>" SelectCommand="SELECT [idSalon], [Salon] FROM Salon WHERE idEdificio = 1 ORDER BY Salon"></asp:SqlDataSource>

            <asp:TextBox ID="consola" placeholder="Mensajes del Sistema" runat="server" class="cajaTexto" TextMode="MultiLine" ReadOnly="True" Height="100px"></asp:TextBox>

        </div>        
      </form>



</asp:Content>
