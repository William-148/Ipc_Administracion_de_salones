<%@ Page Title="" Language="C#" MasterPageFile="~/paginas/operador/operador.Master" AutoEventWireup="true" CodeBehind="lista.aspx.cs" Inherits="PlataformaWeb.paginas.operador.usuario.lista" %>
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
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="subNavegacion">
        <a href="/paginas/operador/usuario/crear.aspx"><span class="icon-add-user"></span> Crear Usuario</a>
    </div>

    <form runat="server" class="cajaTabla">

        <p class="tituloTabla"> <span class="icon-list"></span> Lista de Usuarios </p>

        <asp:GridView 
            ID="GridView1" 
            runat="server" 
            AutoGenerateColumns="False" 
            DataKeyNames="idUsuario" 
            DataSourceID="InstructoresEstudiantes" 
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
                <asp:BoundField DataField="idUsuario" HeaderText="Id" InsertVisible="False" ReadOnly="True" SortExpression="idUsuario" />
                <asp:BoundField DataField="Usuario" HeaderText="Usuario" SortExpression="Usuario" />
                <asp:BoundField DataField="Nombre" HeaderText="Nombre" SortExpression="Nombre" />
                <asp:BoundField DataField="Carnet" HeaderText="Carnet" SortExpression="Carnet" />
                <asp:BoundField DataField="FechaNacimiento" HeaderText="Fecha de Nacimiento" SortExpression="FechaNacimiento" DataFormatString="{0:d}"/>
                <asp:BoundField DataField="Telefono" HeaderText="Teléfono" SortExpression="Telefono" />
                <asp:BoundField DataField="Correo" HeaderText="Correo" SortExpression="Correo" />
                <asp:BoundField DataField="Rol" HeaderText="Rol" SortExpression="Rol" />

                <asp:CommandField  ShowDeleteButton="True" ControlStyle-CssClass="tablaBoton" DeleteText="" HeaderText="Eliminar"  >
                    <ControlStyle CssClass="tablaBoton icon-remove-user"></ControlStyle>
                    <HeaderStyle CssClass="cabeceraBoton" />
                </asp:CommandField>


                <asp:ButtonField HeaderText="Editar" ControlStyle-CssClass="tablaBoton" CommandName="editar">
                    <ControlStyle CssClass="tablaBoton icon-new-message" />
                    <HeaderStyle CssClass="cabeceraBoton" />
                </asp:ButtonField>

            </Columns>

<PagerStyle CssClass="pieTabla"></PagerStyle>
        </asp:GridView>



        <asp:SqlDataSource ID="InstructoresEstudiantes" runat="server" ConnectionString="<%$ ConnectionStrings:PlataformaWebConnectionString %>" DeleteCommand="eliminar" DeleteCommandType="StoredProcedure" SelectCommand="lista_instructor_estudiante" SelectCommandType="StoredProcedure">
            <DeleteParameters>
                <asp:Parameter Name="idUsuario" Type="String" />
            </DeleteParameters>
        </asp:SqlDataSource>



    </form>


</asp:Content>
