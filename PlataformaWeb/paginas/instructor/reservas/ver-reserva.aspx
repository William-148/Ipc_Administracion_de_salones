<%@ Page Title="" Language="C#" MasterPageFile="~/paginas/instructor/masterInstructor.Master" AutoEventWireup="true" CodeBehind="ver-reserva.aspx.cs" Inherits="PlataformaWeb.paginas.instructor.reservas.ver_reserva" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Listado de Reservas</title>
    <script>
        function seleccionar() {
            var opcion = document.getElementsByClassName("opcion")[1];
            opcion.style.background = '#101010';
        }
        window.addEventListener("load", seleccionar, false);
    </script>
    <style>
        .formulario{
            margin-top: 0px;
            overflow-y: scroll;
            height: 85%;
            border:solid 1px #cfcfcf;
            width: 800px;
            margin: auto;
            margin-top: 10px;
            background: #eae9e9;
            padding: 10px 20px;
            box-sizing: border-box;
            border-radius: 8px;
        }
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
        .imageCarta{
            width:90%;
            margin-left:4%;
            border: solid 3px #6c6c6c;
        }
        .imageQr{
            width: 65%;
            margin-left:16%;
            border: solid 8px white;
        }

    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="subNavegacion">
        <a href="/paginas/instructor/reservas/lista-reservas.aspx"><span class="icon-list"></span> Lista de Reservaciones</a>
    </div>

    <form method="post" runat="server"  class="formulario" style="width: 800px;">
        <h2><span class="icon-circle-with-plus"></span>  Información de Reservación</h2>

        <div class="caja">
            
            <a>Fecha a reservar: </a>
            <asp:TextBox ID="fecha" runat="server" class="cajaTexto" ReadOnly="True"></asp:TextBox>

            <a>Hora de la reserva (horas:minutos): </a>
            <asp:TextBox ID="hora" runat="server" class="cajaTexto" ReadOnly="True"></asp:TextBox>
                        
            <a>Duración de la reserva (horas:minutos): </a>
            <asp:TextBox ID="duracion" runat="server" class="cajaTexto" ReadOnly="True"></asp:TextBox>
                        
            <a>Periodo de la reserva: </a>
            <asp:TextBox ID="periodoReserva" runat="server" class="cajaTexto" ReadOnly="True"></asp:TextBox>

            <a>Vigencia de la Reserva: </a>
            <asp:TextBox ID="vigencia1" runat="server" class="cajaTexto"  ReadOnly="True"></asp:TextBox>
            
            <a>Codigo QR: </a>
            <asp:Button ID="descargar" runat="server" Text="Descargar" class="boton2" OnClick="descargar_Click"   />
            <asp:Image ID="ImageQR" CssClass="imageQr" runat="server"  />

        </div>

        <div class="caja">

            <a>Id del Instructor: </a>
            <asp:TextBox ID="idInstructor"  runat="server" class="cajaTexto" ReadOnly="True"></asp:TextBox>
            
            <a>Instructor Encargado: </a>
            <asp:TextBox ID="instructor" runat="server" class="cajaTexto" ReadOnly="True"></asp:TextBox>

            <a>Operador Responsable: </a>
            <asp:TextBox ID="operador" runat="server" class="cajaTexto" ReadOnly="True"></asp:TextBox>

            <a>Salon: </a>
            <asp:TextBox ID="salon1" runat="server" class="cajaTexto" ReadOnly="True"></asp:TextBox>

            <a>Edificio: </a>
            <asp:TextBox ID="edificio1" runat="server" class="cajaTexto" ReadOnly="True"></asp:TextBox>

            <a>Carta: </a>
            <br />
            <asp:Image ID="imagenCarta" CssClass="imageCarta" runat="server"   />

        </div>
        

        </form>

</asp:Content>
