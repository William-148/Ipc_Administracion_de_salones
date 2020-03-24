<%@ Page Title="" Language="C#" MasterPageFile="~/paginas/operador/operador.Master" AutoEventWireup="true" CodeBehind="lista-prestamo.aspx.cs" Inherits="PlataformaWeb.paginas.operador.insumos.lista_prestamo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        function seleccionar() {            
            var opcion = document.getElementsByClassName("opcion")[4];
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
            <a href="/paginas/operador/insumos/lista-insumos.aspx"><span class="icon-list"></span> Lista Insumos</a>
            <a href="/paginas/operador/insumos/prestamo.aspx"><span class="icon-circle-with-plus"></span> Registrar Prestamo</a>
        </div>

        <form runat="server" class="cajaTabla">

        <p class="tituloTabla"> <span class="icon-list"></span> Lista de Prestamos</p>
        <asp:Button ID="boton" runat="server" Text="Aceptar" class="boton" OnClick="boton_Click"  />

         <asp:DropDownList ID="filtrar" CssClass="cajaLista" style="width:300px;" runat="server">
             <asp:ListItem Value="1">Todos Los Prestamos</asp:ListItem>
             <asp:ListItem Value="2">Prestamos No Devueltos</asp:ListItem>
             <asp:ListItem Value="3">Prestamos Devueltos</asp:ListItem>
        </asp:DropDownList>
        <a class="titulo">Filtrar por: </a>
            


        <asp:GridView 
            ID="GridView1" 
            runat="server" 
            AutoGenerateColumns="False" 
            DataKeyNames="idInsumoPrestado" 
            DataSourceID="DatosPrestamos" 
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
                <asp:BoundField DataField="idInsumoPrestado" HeaderText="Id" InsertVisible="False" ReadOnly="True" SortExpression="idInsumoPrestado" />
                <asp:BoundField DataField="Fecha" HeaderText="Fecha" SortExpression="Fecha" DataFormatString="{0:d}"/>
                <asp:BoundField DataField="Descripcion" HeaderText="Descripción" SortExpression="Descripcion" />
                <asp:BoundField DataField="Nombre" HeaderText="Usuario" SortExpression="Nombre" />
                <asp:BoundField DataField="Insumo" HeaderText="Insumo" SortExpression="Insumo" />
                <asp:BoundField DataField="Estado" HeaderText="Estado" SortExpression="Estado" />
                <asp:BoundField DataField="FechaDevuelto" HeaderText="Fecha de Devolución" SortExpression="FechaDevuelto" DataFormatString="{0:dd/MM/yyyy HH:mm}" />
                <asp:CommandField HeaderText="Eliminar" ShowDeleteButton="True" DeleteText="">
                <ControlStyle CssClass="tablaBoton icon-circle-with-cross "  />
                <HeaderStyle CssClass="cabeceraBoton" />
                </asp:CommandField>
                <asp:ButtonField Text="" HeaderText="Editar" CommandName="editar">
                <ControlStyle CssClass="tablaBoton icon-new-message" />
                <HeaderStyle CssClass="cabeceraBoton" />
                </asp:ButtonField>
            </Columns>

<PagerStyle CssClass="pieTabla"></PagerStyle>
        </asp:GridView>

        <asp:SqlDataSource ID="DatosPrestamos" runat="server" ConnectionString="<%$ ConnectionStrings:PlataformaWebConnectionString %>" DeleteCommand="Delete from InsumoPrestado where idInsumoPrestado = @idInsumoPrestado" SelectCommand="SELECT I.idInsumoPrestado, I.Fecha, I.Descripcion, U.Nombre, N.Nombre AS Insumo, E.Estado, I.FechaDevuelto
FROM InsumoPrestado I, Usuario U, Insumo N, EstadoPrestamo E
WHERE I.idUsuario = U.idUsuario AND I.idInsumo = N.idInsumo AND I.idEstadoPrestamo = E.idEstadoPrestamo
ORDER BY I.Fecha desc
">
            <DeleteParameters>
                <asp:Parameter Name="idInsumoPrestado" Type ="Int32" />
            </DeleteParameters>
        </asp:SqlDataSource>
        <asp:SqlDataSource ID="ListadodeIncidentes" runat="server"></asp:SqlDataSource>

        </form>
</asp:Content>
