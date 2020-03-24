<%@ Page Title="" Language="C#" MasterPageFile="~/paginas/operador/operador.Master" AutoEventWireup="true" CodeBehind="lista-incidentes.aspx.cs" Inherits="PlataformaWeb.paginas.operador.salon.lista_incidentes" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Lista de Incidentes en Salones</title>
    <script>
        function seleccionar() {            
            var opcion = document.getElementsByClassName("opcion")[3];
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
        <a href="/paginas/operador/salon/lista-salones.aspx"><span class="icon-list"></span> Lista Salones</a>
        <a href="/paginas/operador/salon/crear-incidente.aspx"><span class="icon-plus"></span> Registrar Incidente</a>
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
            DataKeyNames="idIncidenteSalon" 
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
                <asp:BoundField DataField="idIncidenteSalon" HeaderText="Id" InsertVisible="False" ReadOnly="True" SortExpression="idIncidenteSalon" />
                <asp:BoundField DataField="FechaIncidente" HeaderText="Fecha" SortExpression="FechaIncidente" DataFormatString="{0:d}"/>
                <asp:BoundField DataField="Nombre" HeaderText="Responsable" SortExpression="Nombre" />
                <asp:BoundField DataField="Edificio" HeaderText="Edificio" SortExpression="Edificio" />
                <asp:BoundField DataField="Salon" HeaderText="Salon" SortExpression="Salon"/>
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

        <asp:SqlDataSource ID="DatosIncidentes" runat="server" ConnectionString="<%$ ConnectionStrings:PlataformaWebConnectionString %>" DeleteCommand="IncidenteSalon_Eliminar" DeleteCommandType="StoredProcedure" SelectCommand="SELECT [idIncidenteSalon], [FechaIncidente], U.[Nombre],  E.[Nombre] AS [Edificio], S.[Salon], EI.[Estado], [FechaCreacion] 
FROM [IncidenteSalon] I, [Usuario] U, [Salon] S, [Edificio] E, [EstadoIncidente] EI
WHERE I.idUsuario = U.idUsuario AND I.idSalon = S.idSalon AND S.idEdificio = E.idEdificio AND  I.idEstadoIncidente = EI.idEstadoIncidente
ORDER BY I.FechaCreacion desc
">
            <DeleteParameters>
                <asp:Parameter Name="idIncidenteSalon" Type="Int32" />
            </DeleteParameters>
        </asp:SqlDataSource>
        <asp:SqlDataSource ID="ListadodeIncidentes" runat="server"></asp:SqlDataSource>

        </form>
</asp:Content>
