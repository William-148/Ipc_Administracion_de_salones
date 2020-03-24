<%@ Page Title="" Language="C#" MasterPageFile="~/paginas/estudiante/masterEstudiante.Master" AutoEventWireup="true" CodeBehind="buscar.aspx.cs" Inherits="PlataformaWeb.paginas.estudiante.actividades.buscar" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        function seleccionar() {            
            var opcion = document.getElementsByClassName("opcion")[1];
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
        <a href="/paginas/estudiante/actividades/Lista.aspx"><span class="icon-list"></span> Todas las Actividades</a>
    </div>

    <form runat="server" class="cajaTabla">

        <p class="tituloTabla"> <span class="icon-list"></span> Resultados</p>
        <asp:Button ID="boton" runat="server" Text="Buscar" class="boton" OnClick="boton_Click"  />
        <asp:TextBox ID="instructor" placeholder="Nombre del instructor" runat="server" CssClass="cajaLista" style="width:250px;"></asp:TextBox>
        <asp:TextBox ID="actividad" placeholder="Nombre de la actividad" runat="server" CssClass="cajaLista" style="width:250px;"></asp:TextBox>
        <a class="titulo">Buscar: </a>

        <asp:GridView 
            ID="GridView1" 
            runat="server" 
            AutoGenerateColumns="False" 
            DataKeyNames="idActividad" 
            DataSourceID="SqlDataSource1" 
            AllowPaging="True" 
            AllowSorting="True"
            EmptyDataText ="No hay resultados"
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

         <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:PlataformaWebConnectionString %>" SelectCommand="actividad_buscar" SelectCommandType="StoredProcedure">
             <SelectParameters>
                 <asp:SessionParameter DefaultValue="''" Name="actividad" SessionField="actividadBuscar" Type="String" />
                 <asp:SessionParameter DefaultValue="''" Name="instructor" SessionField="instructorBuscar" Type="String" />
             </SelectParameters>
        </asp:SqlDataSource>

         </form>

</asp:Content>
