<%@ Page Title="" Language="C#" MasterPageFile="~/paginas/instructor/masterInstructor.Master" AutoEventWireup="true" CodeBehind="lista-reservas.aspx.cs" Inherits="PlataformaWeb.paginas.instructor.reservas.lista_reservas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Listado de Reservas</title>
    <script>
        function seleccionar() {
            var opcion = document.getElementsByClassName("opcion")[1];
            opcion.style.background = '#101010';
        }
        window.addEventListener("load", seleccionar, false);
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <form runat="server" class="cajaTabla">

        <p class="tituloTabla"> <span class="icon-list"></span> Lista de Reservaciones</p>

        <asp:GridView 
            ID="GridView1" 
            runat="server" 
            AutoGenerateColumns="False" 
            DataKeyNames="idReservaSalon" 
            DataSourceID="SqlDataSource1" 
            AllowPaging="True" 
            AllowSorting="True"
            PageSize="8"
            GridLines="None"
            CssClass="tabla"
            AlternatingRowStyle-CssClass ="alt" 
            PagerStyle-CssClass="pieTabla" 
            OnRowCommand="GridView1_RowCommand"
            ><AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>

            <Columns>
                <asp:BoundField DataField="idReservaSalon" HeaderText="Id" InsertVisible="False" ReadOnly="True" SortExpression="idReservaSalon" />
                <asp:BoundField DataField="Salon" HeaderText="Salón" SortExpression="Salon" />
                <asp:BoundField DataField="Nombre" HeaderText="Edificio" SortExpression="Nombre" />
                <asp:BoundField DataField="Fecha" HeaderText="Fecha" SortExpression="Fecha" DataFormatString="{0:d}"/>
                <asp:BoundField DataField="HoraInicio" HeaderText="Inicio" SortExpression="HoraInicio" />
                <asp:BoundField DataField="HoraFin" HeaderText="Fin" SortExpression="HoraFin" />
                <asp:BoundField DataField="FechaCreacion" HeaderText="Creación" SortExpression="FechaCreacion" DataFormatString="{0:dd/MM/yyyy HH:mm}" />
                <asp:BoundField DataField="Periodo" HeaderText="Período" SortExpression="Periodo" />

                <asp:BoundField DataField="Vigencia" HeaderText="Vigencia" SortExpression="Vigencia" />

                <asp:ButtonField CommandName="editar" HeaderText="Detalles" Text="">
                <ControlStyle CssClass="tablaBoton icon-new-message" />
                <HeaderStyle CssClass="cabeceraBoton" />
                </asp:ButtonField>

                <asp:ButtonField CommandName="actividad" HeaderText="Actividad" Text="">
                <ControlStyle CssClass="tablaBoton icon-news" />
                <HeaderStyle CssClass="cabeceraBoton" />
                </asp:ButtonField>

            </Columns>

            <PagerStyle CssClass="pieTabla"></PagerStyle>
            </asp:GridView>

         <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:PlataformaWebConnectionString %>" SelectCommand="lista_reservas_Instructor" SelectCommandType="StoredProcedure">
             <SelectParameters>
                 <asp:SessionParameter Name="idInstructor" SessionField="idUsuario" Type="Int32" />
             </SelectParameters>
        </asp:SqlDataSource>

         </form>
</asp:Content>
