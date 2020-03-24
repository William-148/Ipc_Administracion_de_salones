<%@ Page Title="" Language="C#" MasterPageFile="~/paginas/operador/operador.Master" AutoEventWireup="true" CodeBehind="lista-reservas.aspx.cs" Inherits="PlataformaWeb.paginas.operador.reserva.lista_reservas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Lista de Reservaciones</title>
    <script>
        function seleccionar() {            
            var opcion = document.getElementsByClassName("opcion")[2];
            opcion.style.background = '#101010';
        }
        window.addEventListener("load", seleccionar, false);
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="subNavegacion">
        <a href="/paginas/operador/reserva/crear.aspx"><span class="icon-circle-with-plus"></span> Crear Reservación</a>
    </div>

    <form runat="server" class="cajaTabla">

        <p class="tituloTabla"> <span class="icon-list"></span> Lista de Reservaciones</p>

        <asp:GridView 
            ID="GridView1" 
            runat="server" 
            AutoGenerateColumns="False" 
            DataKeyNames="idReservaSalon" 
            DataSourceID="listaReservas" 
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
                <asp:BoundField DataField="HoraInicio" HeaderText="Inicio" SortExpression="HoraInicio"/>
                <asp:BoundField DataField="HoraFin" HeaderText="Fin" SortExpression="HoraFin" />
                <asp:BoundField DataField="FechaCreacion" HeaderText="Creación" SortExpression="FechaCreacion" DataFormatString="{0:dd/MM/yyyy HH:mm}" />
                <asp:BoundField DataField="Periodo" HeaderText="Período" SortExpression="Periodo" />

                <asp:BoundField DataField="Estado" HeaderText="Estado" SortExpression="Estado" />
                <asp:BoundField DataField="Vigencia" HeaderText="Vigencia" SortExpression="Vigencia" />

                <asp:CommandField ShowDeleteButton="True" HeaderText="Eliminar"  ControlStyle-CssClass="tablaBoton" DeleteText="" >
                <ControlStyle CssClass="tablaBoton icon-circle-with-cross" />
                <HeaderStyle CssClass="cabeceraBoton" />
                </asp:CommandField>

                <asp:ButtonField CommandName="editar" HeaderText="Editar" Text="">
                <ControlStyle CssClass="tablaBoton icon-new-message" />
                <HeaderStyle CssClass="cabeceraBoton" />
                </asp:ButtonField>

            </Columns>

            <PagerStyle CssClass="pieTabla"></PagerStyle>
            </asp:GridView>

         <asp:SqlDataSource ID="listaReservas" runat="server" ConnectionString="<%$ ConnectionStrings:PlataformaWebConnectionString %>" DeleteCommand="eliminar_reserva" DeleteCommandType="StoredProcedure" SelectCommand="lista_reservas" SelectCommandType="StoredProcedure">
             <DeleteParameters>
                 <asp:Parameter Name="idReservaSalon" Type="Int32" />
             </DeleteParameters>
        </asp:SqlDataSource>

         </form>

</asp:Content>
