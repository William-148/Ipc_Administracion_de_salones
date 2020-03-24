<%@ Page Title="" Language="C#" MasterPageFile="~/paginas/estudiante/masterEstudiante.Master" AutoEventWireup="true" CodeBehind="actividades-asistidas.aspx.cs" Inherits="PlataformaWeb.paginas.estudiante.actividades.actividades_asistidas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        function seleccionar() {            
            var opcion = document.getElementsByClassName("opcion")[2];
            opcion.style.background = '#101010';
        }
        window.addEventListener("load", seleccionar, false);
    </script>
    <style>
        .tituloTabla{
            float:left;
            margin-top:30px;
        }
        .cajaLista{
            margin-top:30px;            
            float:right;
        }
        .titulo{
            margin-top:30px;
            float:right;
        }

        .boton{
            margin-top:30px;
            float:right;
            margin-right: 30px;
            border-radius: 7px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="subNavegacion">
        <a href="/paginas/estudiante/actividades/actividades-matriculadas.aspx"><span class="icon-list"></span> Actividades Matriculadas</a>
    </div>
    <form runat="server" class="cajaTabla">

        <p class="tituloTabla"> <span class="icon-list"></span> Actividades Asistidas</p>
        
        <asp:GridView 
            ID="GridView1" 
            runat="server" 
            AutoGenerateColumns="False" 
            DataKeyNames="idActividad" 
            DataSourceID="SqlDataSource1" 
            AllowPaging="True" 
            AllowSorting="True"
            EmptyDataText ="No se ha asistido a ninguna actividad"
            PageSize="8"
            GridLines="None"
            CssClass="tabla"
            AlternatingRowStyle-CssClass ="alt" 
            PagerStyle-CssClass="pieTabla" 
            OnRowCommand="GridView1_RowCommand"
            ><AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>

            <Columns>
                <asp:BoundField DataField="idActividad" HeaderText="Id" InsertVisible="False" ReadOnly="True" SortExpression="idActividad" />
                <asp:BoundField DataField="Nombre" HeaderText="Actividad" SortExpression="Nombre" />
                <asp:BoundField DataField="Fecha" HeaderText="Fecha" SortExpression="Fecha" DataFormatString="{0:d}"/>
                <asp:BoundField DataField="HoraInicio" HeaderText="Inicio" SortExpression="HoraInicio" />
                <asp:BoundField DataField="HoraFin" HeaderText="Fin" SortExpression="HoraFin" />
                <asp:BoundField DataField="Tipo" HeaderText="Tipo" SortExpression="Tipo" />
                <asp:BoundField DataField="Instructor" HeaderText="Instructor" SortExpression="Instructor"/>

                <asp:ButtonField CommandName="detalle" HeaderText="Detalles" Text="">
                <ControlStyle CssClass="tablaBoton icon-new-message" />
                <HeaderStyle CssClass="cabeceraBoton" />
                </asp:ButtonField>

            </Columns>

            <EmptyDataRowStyle CssClass="sinResultados" />

            <PagerStyle CssClass="pieTabla"></PagerStyle>
            </asp:GridView>

         <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:PlataformaWebConnectionString %>" SelectCommand="SELECT A.idActividad, A.Nombre, R.Fecha, R.HoraInicio, R.HoraFin, T.Nombre AS Tipo, U.Nombre AS Instructor
	FROM Actividad A, ReservaSalon R, Usuario U, TipoActividad T, Matriculacion M, Asistencia SA
	WHERE A.idActividad = R.idActividad AND R.idInstructor = U.idUsuario AND A.idTipoActividad = T.idTipoActividad
	AND A.Nombre IS NOT NULL AND A.ExtensionPresentacion IS NOT NULL AND A.idActividad = M.idActividad AND M.idUsuario = @idUsuario AND SA.idActividad = A.idActividad AND SA.idUsuario = U.idUsuario
	ORDER BY R.Fecha DESC">
             <SelectParameters>
                 <asp:SessionParameter DefaultValue="" Name="idUsuario" SessionField="idUsuario" />
             </SelectParameters>
        </asp:SqlDataSource>

         </form>

</asp:Content>