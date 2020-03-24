<%@ Page Title="" Language="C#" MasterPageFile="~/paginas/operador/operador.Master" AutoEventWireup="true" CodeBehind="ver-reserva.aspx.cs" Inherits="PlataformaWeb.paginas.operador.reserva.ver_reserva" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Ver Reserva</title>
    <script>
        function seleccionar() {
            var opcion = document.getElementsByClassName("opcion")[2];
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
        <a href="/paginas/operador/reserva/lista-reservas.aspx"><span class="icon-list"></span> Lista de Reservaciones</a>
        <a href="/paginas/operador/reserva/crear.aspx"><span class="icon-circle-with-plus"></span> Crear Reservación</a>
    </div>

    <form method="post" runat="server"  class="formulario">
        <h2><span class="icon-circle-with-plus"></span>  Información de Reservación</h2>

        <div class="caja">
            
            <a>Fecha a reservar*: </a>
            <asp:TextBox ID="fecha" runat="server" class="cajaTexto" TextMode="Date"></asp:TextBox>

            <a>Hora de la reserva (horas:minutos)*: </a>
            <asp:TextBox ID="hora" runat="server" class="cajaTexto" TextMode="Time"></asp:TextBox>
                        
            <a>Duración de la reserva (horas:minutos)*: </a>
            <asp:TextBox ID="duracion" runat="server" class="cajaTexto" TextMode="Time"></asp:TextBox>

            <a>Tipo de Actividad*: </a>
            <asp:DropDownList ID="tipoActividad" CssClass="cajaLista" runat="server" DataSourceID="actividad" DataTextField="Nombre" DataValueField="idTipoActividad"></asp:DropDownList>
            <asp:SqlDataSource ID="actividad" runat="server" ConnectionString="<%$ ConnectionStrings:PlataformaWebConnectionString %>" SelectCommand="SELECT * FROM [TipoActividad]"></asp:SqlDataSource>

                        
            <a>Periodo de la reserva*: </a>
            <asp:DropDownList ID="periodo" runat="server" CssClass="cajaLista" DataSourceID="SqlDataSource1" DataTextField="Periodo" DataValueField="idPeriodoReserva" ></asp:DropDownList>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:PlataformaWebConnectionString %>" SelectCommand="SELECT * FROM [PeriodoReserva]"></asp:SqlDataSource>
           
            <a>Estado de la Reserva: </a>
            <asp:TextBox ID="estado" runat="server" class="cajaTexto" TextMode="SingleLine" ReadOnly="True"></asp:TextBox>

            <asp:Label ID="titulo" runat="server" Text="Vigencia de reserva*:" CssClass="letras"></asp:Label>
            <asp:DropDownList ID="vigencia" runat="server" CssClass="cajaLista" DataSourceID="SqlDataSource2" DataTextField="Vigencia" DataValueField="idVigenciaReserva" ></asp:DropDownList>
            <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:PlataformaWebConnectionString %>" SelectCommand="SELECT * FROM [VigenciaReserva]"></asp:SqlDataSource>

            
            <asp:Button ID="boton" runat="server" Text="Actualizar" class="boton" OnClick="boton_Click"  />
            <a id="forgot">(*) Campos Obligatorios.</a>

        </div>

        <div class="caja">

            <a>Id del Instructor*: </a>
            <asp:TextBox ID="idInstructor" placeholder="Identificación en el Sistema" runat="server" class="cajaTexto" TextMode="Number"></asp:TextBox>
            
            <a>Instructor Encargado: </a>
            <asp:TextBox ID="instructor" runat="server" class="cajaTexto" ReadOnly="True"></asp:TextBox>

            <a>Operador Responsable: </a>
            <asp:TextBox ID="operador" runat="server" class="cajaTexto" ReadOnly="True"></asp:TextBox>

            <a>Edificio del Salon*: </a>
            <asp:DropDownList ID="edificio" runat="server" CssClass="cajaLista" DataSourceID="edificios" DataTextField="Nombre" DataValueField="idEdificio" ></asp:DropDownList>
            <asp:SqlDataSource ID="edificios" runat="server" ConnectionString="<%$ ConnectionStrings:PlataformaWebConnectionString %>" SelectCommand="SELECT * FROM [Edificio]"></asp:SqlDataSource>

            <asp:Button ID="verSalones" runat="server" Text="Ver Salones" class="boton2" OnClick="verSalones_Click"  />

            <asp:Label ID="tituloSalon" runat="server" Text="prueba:" CssClass="letras"></asp:Label>
            <asp:DropDownList ID="salon" runat="server" CssClass="cajaLista" DataSourceID="nombresSalones" DataTextField="Salon" DataValueField="idSalon" ></asp:DropDownList>
            <asp:SqlDataSource ID="nombresSalones" runat="server" ConnectionString="<%$ ConnectionStrings:PlataformaWebConnectionString %>" SelectCommand="SELECT [idSalon], [Salon] FROM Salon WHERE idEdificio = 1 ORDER BY Salon"></asp:SqlDataSource>

           
            <asp:TextBox ID="consola" placeholder="Mensajes del Sistema" runat="server" class="cajaTexto" TextMode="MultiLine" ReadOnly="True" Height="100px"></asp:TextBox>

        </div>
        
        <h2><span class="icon-upload"></span>  Subir Carta de Solicitud</h2>

        <div class="caja">
            <a>Elegir Imagen: </a>
            <asp:FileUpload ID="uploadImagen"  accept=".jpg" CssClass="fUpload" runat="server" />

            <asp:Button ID="subir" runat="server" Text="Subir Carta" class="boton2" OnClick="subir_Click"   />
            
            <a>Carta: </a>
            <asp:Image ID="Image" CssClass="imageCarta" runat="server"   />
            
        </div>
        <div class="caja">
            
            <a>Codigo QR: </a>
            <asp:Image ID="ImageQR" CssClass="imageQr" runat="server"  />
            
        </div>
      </form>






</asp:Content>
