<%@ Page Title="" Language="C#" MasterPageFile="~/paginas/instructor/masterInstructor.Master" AutoEventWireup="true" CodeBehind="actividad.aspx.cs" Inherits="PlataformaWeb.paginas.instructor.reservas.actividad" %>
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
        <a href="/paginas/instructor/reservas/lista-reservas.aspx"><span class="icon-back"></span> Regresar</a>
    </div>


    <form method="post" runat="server" style="width:800px;">
        
        
        <div class="caja">
            <h2><span class="icon-circle-with-plus"></span> Editar Actividad</h2>

            <a>Id Actividad: </a>
            <asp:TextBox ID="idActividad" runat="server" class="cajaTexto" ReadOnly="True"></asp:TextBox>
            
            <a>Nombre*: </a>
            <asp:TextBox ID="nombre" placeholder="Definir nombre de actividad para completar" runat="server" class="cajaTexto" Wrap="True" ></asp:TextBox>                 

            <a>Descripción*: </a>
            <asp:TextBox ID="descripcion" placeholder="Definir una descripción de la actividad" runat="server" class="cajaTexto" Height="100px" TextMode="MultiLine" Wrap="True"></asp:TextBox>                 
               

            <a>Tipo de Actividad: </a>
            <asp:TextBox ID="tipoActividad" runat="server" class="cajaTexto" Wrap="True" ReadOnly="True"></asp:TextBox>                 

            <asp:TextBox ID="asistencia" Text="Se asistió a ésta actividad" Style="background:#08141d; color:#c1c1c1" runat="server" class="cajaTexto" Wrap="True" ReadOnly="True" Visible="false"></asp:TextBox>                 

                        
            <asp:Button ID="boton" runat="server" Text="Actualizar" class="boton" OnClick="boton_Click"  />
            <a class="forgot">(*) Campos Obligatorios. </a>

        </div>


        <div class="caja"> 
            <h2><span class="icon-upload"></span> Subir Presentación</h2>
            
            <a>Presentación: </a>
            <asp:TextBox ID="hayPresentacion" runat="server" class="cajaTexto" Wrap="True" ReadOnly="True"></asp:TextBox>                 

            <a>Elegir Presentación: </a>
            <asp:FileUpload ID="upload"  CssClass="fUpload" runat="server" />

            <asp:Button ID="subir" runat="server" Text="Subir" class="boton2" OnClick="subir_Click"   />
            
            <asp:Button ID="descargar" runat="server" Text="Descargar" class="boton2" OnClick="descargar_Click"   />            
            
            <h2><span class="icon-arrow-with-circle-up"></span> Cuestionario</h2>
            <asp:Button ID="cuestionario" runat="server" Text="Editar" class="boton2" OnClick="cuestionario_Click"   />

            <asp:TextBox ID="consola" placeholder="Mensajes del Sistema..." runat="server" class="cajaTexto" TextMode="MultiLine" ReadOnly="True" Height="100px"></asp:TextBox>          

        </div>        
      </form>


</asp:Content>
