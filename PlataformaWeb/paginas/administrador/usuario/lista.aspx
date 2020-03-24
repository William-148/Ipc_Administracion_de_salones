<%@ Page Title="" Language="C#" MasterPageFile="~/paginas/masterAdmin.Master" AutoEventWireup="true" CodeBehind="lista.aspx.cs" Inherits="PlataformaWeb.paginas.usuario.lista" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Lista de Usuarios</title>
    <script>
        function seleccionar() {
            var opcion = document.getElementsByClassName("opcion")[1];
            opcion.style.background = '#101010';
        }
        window.addEventListener("load", seleccionar, false);
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">


    <div class="subNavegacion">
        <a href="/paginas/administrador/usuario/crear.aspx"><span class="icon-add-user"></span> Crear Usuario</a>
    </div>

    <form runat="server" class="cajaTabla">

        <p class="tituloTabla"> <span class="icon-list"></span> Lista de Usuarios</p>

        <asp:GridView 
            ID="GridView" 
            runat="server" 
            AutoGenerateColumns="False" 
            DataKeyNames="idUsuario" 
            DataSourceID="listasUsuarios"
            PageSize="7"
            GridLines="None"
            CssClass="tabla"
            AlternatingRowStyle-CssClass ="alt" 
            PagerStyle-CssClass="pieTabla"
            AllowPaging="True" 
            AllowSorting="True" 
            OnRowCommand="GridView1_RowCommand"
            >
             <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>

        <Columns>
            <asp:BoundField DataField="idUsuario" HeaderText="Id" InsertVisible="False" ReadOnly="True" SortExpression="idUsuario" ></asp:BoundField>
            <asp:BoundField DataField="Usuario" HeaderText="Usuario" SortExpression="Usuario" ></asp:BoundField>
            <asp:BoundField DataField="Nombre" HeaderText="Nombre" SortExpression="Nombre" />
            <asp:BoundField DataField="Carnet" HeaderText="Carnet" SortExpression="Carnet" />
            <asp:BoundField DataField="FechaNacimiento" HeaderText="Fecha de Nacimiento" SortExpression="FechaNacimiento" DataFormatString="{0:d}" />
            <asp:BoundField DataField="Telefono" HeaderText="Teléfono" SortExpression="Telefono" />
            <asp:BoundField DataField="Correo" HeaderText="Correo" SortExpression="Correo" />
            <asp:BoundField DataField="Rol" HeaderText="Tipo" SortExpression="Rol" />
            
            <asp:CommandField ShowDeleteButton="True"  ControlStyle-CssClass="tablaBoton" DeleteText="" HeaderText="Eliminar" >
            <ControlStyle CssClass="tablaBoton icon-remove-user"></ControlStyle>
            <HeaderStyle CssClass="cabeceraBoton"/>
            </asp:CommandField>
            
            <asp:ButtonField CommandName="editar"  Text="" ControlStyle-CssClass="tablaBoton" HeaderText="Editar" >
            <ControlStyle CssClass="tablaBoton icon-new-message "></ControlStyle>
            <HeaderStyle CssClass="cabeceraBoton"/>
            </asp:ButtonField>
        </Columns>
        </asp:GridView>


        <asp:SqlDataSource ID="listasUsuarios" runat="server" ConnectionString="<%$ ConnectionStrings:PlataformaWebConnectionString %>" DeleteCommand="eliminar" DeleteCommandType="StoredProcedure" SelectCommand="lista_usuarios" SelectCommandType="StoredProcedure">
            <DeleteParameters>
            <asp:Parameter Name="idUsuario" Type="String" />
            </DeleteParameters>
        </asp:SqlDataSource>

    </form>


    

</asp:Content>
