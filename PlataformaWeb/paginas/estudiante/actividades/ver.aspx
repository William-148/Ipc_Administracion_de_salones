<%@ Page Title="" Language="C#" MasterPageFile="~/paginas/estudiante/masterEstudiante.Master" AutoEventWireup="true" CodeBehind="ver.aspx.cs" Inherits="PlataformaWeb.paginas.estudiante.actividades.ver" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        function seleccionar() {            
            var opcion = document.getElementsByClassName("opcion")[1];
            opcion.style.background = '#101010';
        }
        window.addEventListener("load", seleccionar, false);
    </script>
    <style>
        .boton2{
            width: 100px;
            height: 40px;
            border: none;
            display:inline-block;
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
        <a href="/paginas/estudiante/actividades/Lista.aspx"><span class="icon-back"></span> Regresar</a>
    </div>


    <form method="post" runat="server" style="width:800px;">
        
        
        <div class="caja">
            <h2><span class="icon-circle-with-plus"></span> Información de Actividad</h2>

            <asp:Label ID="idActividad1" runat="server" Text="" Visible="False"></asp:Label>

            <a>Instructor: </a>
            <asp:TextBox ID="instructor" ReadOnly="True" runat="server" class="cajaTexto" Wrap="True" ></asp:TextBox>                 

            <a>Nombre de la Actividad: </a>
            <asp:TextBox ID="nombre" ReadOnly="True" runat="server" class="cajaTexto" Wrap="True" ></asp:TextBox>                 

            <a>Descripción: </a>
            <asp:TextBox ID="descripcion" runat="server" class="cajaTexto" Height="100px" ReadOnly="True" TextMode="MultiLine" Wrap="True"></asp:TextBox>                 
               
            <a>Tipo de Actividad: </a>
            <asp:TextBox ID="tipoActividad" runat="server" class="cajaTexto" Wrap="True" ReadOnly="True"></asp:TextBox>                 
                        
            <asp:Button ID="boton" runat="server" Text="Matricularse" class="boton" OnClick="boton_Click"  />
            

        </div>


        <div class="caja"> 
            <h2><span class="icon-circle-with-plus"></span> Lugar y Fecha</h2>
            
            <a>Fecha: </a>
            <asp:TextBox ID="fecha" runat="server" class="cajaTexto" Wrap="True" ReadOnly="True"></asp:TextBox>                 

            <a>Hora: </a>
            <asp:TextBox ID="hora" runat="server" class="cajaTexto" Wrap="True" ReadOnly="True"></asp:TextBox>                 

            <a>Edificio: </a>
            <asp:TextBox ID="edificio" runat="server" class="cajaTexto" Wrap="True" ReadOnly="True"></asp:TextBox>                 
            
            <a>Salón: </a>
            <asp:TextBox ID="salon" runat="server" class="cajaTexto" Wrap="True" ReadOnly="True"></asp:TextBox>                 


            
        </div>        
      </form>


</asp:Content>

