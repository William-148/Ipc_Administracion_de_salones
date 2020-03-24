<%@ Page Title="" Language="C#" MasterPageFile="~/paginas/operador/operador.Master" AutoEventWireup="true" CodeBehind="lista-incidentes.aspx.cs" Inherits="PlataformaWeb.paginas.operador.insumos.lista_incidentes" %>
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
            <a href="/paginas/operador/insumos/registrar-incidente.aspx"><span class="icon-circle-with-plus"></span> Registrar Incidente</a>
        </div>

        <form runat="server" class="cajaTabla">

        <p class="tituloTabla"> <span class="icon-list"></span> Lista de Incidentes</p>
        <asp:Button ID="boton" runat="server" Text="Aceptar" class="boton" OnClick="boton_Click"  />

         <asp:DropDownList ID="filtrar" CssClass="cajaLista" style="width:300px;" runat="server">
             <asp:ListItem Value="1">Todos Los Incidentes</asp:ListItem>
             <asp:ListItem Value="2">Incidentes No Resueltos</asp:ListItem>
             <asp:ListItem Value="3">Incidentes Resueltos</asp:ListItem>
        </asp:DropDownList>
        <a class="titulo">Filtrar por: </a>
            


        <asp:GridView 
            ID="GridView1" 
            runat="server" 
            AutoGenerateColumns="False" 
            DataKeyNames="idIncidenteInsumo" 
            DataSourceID="DatosIncidentes" 
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
                <asp:BoundField DataField="idIncidenteInsumo" HeaderText="Id" InsertVisible="False" ReadOnly="True" SortExpression="idIncidenteInsumo" />
                <asp:BoundField DataField="FechaIncidente" HeaderText="Fecha" SortExpression="FechaIncidente" DataFormatString="{0:d}"/>
                <asp:BoundField DataField="Nombre" HeaderText="Responsable" SortExpression="Nombre" />
                <asp:BoundField DataField="Insumo" HeaderText="Insumo" SortExpression="Insumo" />
                <asp:BoundField DataField="Estado" HeaderText="Estado" SortExpression="Estado" />
                <asp:BoundField DataField="FechaCreacion" HeaderText="Creación" SortExpression="FechaCreacion" DataFormatString="{0:dd/MM/yyyy HH:mm}" />
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

        <asp:SqlDataSource ID="DatosIncidentes" runat="server" ConnectionString="<%$ ConnectionStrings:PlataformaWebConnectionString %>" DeleteCommand="DELETE FROM IncidenteInsumo WHERE idIncidenteInsumo = @idIncidenteInsumo" SelectCommand="SELECT [idIncidenteInsumo], [FechaIncidente], U.[Nombre], N.[Nombre] As [Insumo], EI.[Estado], [FechaCreacion] 
FROM [IncidenteInsumo] I, [Usuario] U, [Insumo] N, [EstadoIncidente] EI
WHERE I.idUsuario = U.idUsuario AND I.idInsumo = N.idInsumo AND I.idEstadoIncidente = EI.idEstadoIncidente
ORDER BY I.FechaCreacion desc
">
            <DeleteParameters>
                <asp:Parameter Name="idIncidenteInsumo" Type ="Int32" />
            </DeleteParameters>
        </asp:SqlDataSource>
        <asp:SqlDataSource ID="ListadodeIncidentes" runat="server"></asp:SqlDataSource>

        </form>

</asp:Content>
