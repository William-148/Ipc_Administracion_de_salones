<%@ Page Title="" Language="C#" MasterPageFile="~/paginas/instructor/masterInstructor.Master" AutoEventWireup="true" CodeBehind="incidente-insumo.aspx.cs" Inherits="PlataformaWeb.paginas.instructor.incidentes.incidente_insumo1" %>
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
            margin-top:0px;
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
        .caja{
            margin-top: 20px;
            border-radius: 10px;
            background: #ffffff;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <form runat="server" class="cajaTabla">       
        
        <div class="caja">
            <p class="tituloTabla"> <span class="icon-list"></span> Incidentes Sin Resolver</p>
            <asp:GridView 
            ID="GridView2" 
            runat="server" 
            AutoGenerateColumns="False" 
            DataKeyNames="idIncidenteInsumo" 
            DataSourceID="SqlDataSource1" 
            EmptyDataText ="No hay incidentes"
            AllowPaging="True" 
            AllowSorting="True"
            PageSize="8"
            GridLines="None"
            CssClass="tabla"
            AlternatingRowStyle-CssClass ="alt" 
            PagerStyle-CssClass="pieTabla" 
            ><AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>

            <Columns>
                <asp:BoundField DataField="idIncidenteInsumo" HeaderText="Id" InsertVisible="False" ReadOnly="True" SortExpression="idIncidenteSalon" />
                <asp:BoundField DataField="FechaIncidente" HeaderText="Fecha" SortExpression="FechaIncidente" DataFormatString="{0:d}" />
                <asp:BoundField DataField="Nombre" HeaderText="Edificio" SortExpression="Nombre" />
                <asp:BoundField DataField="Descripcion" HeaderText="Descripción" SortExpression="Descripcion"/>
            </Columns>

<PagerStyle CssClass="pieTabla"></PagerStyle>
        </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:PlataformaWebConnectionString %>" DeleteCommand="DELETE FROM Insumo WHERE idInsumo = @idInsumo" SelectCommand="SELECT I.idIncidenteInsumo, I.FechaIncidente, E.Nombre , I.Descripcion
FROM IncidenteInsumo I, Insumo E
WHERE I.idUsuario = @idUsuario AND I.idInsumo = E.idInsumo AND I.idEstadoIncidente = 1
ORDER BY I.FechaIncidente DESC">
            <DeleteParameters>
                <asp:Parameter Name="idInsumo" />
            </DeleteParameters>
            <SelectParameters>
                <asp:SessionParameter Name="idUsuario" SessionField="idUsuario" />
            </SelectParameters>
        </asp:SqlDataSource>   

        </div>
        <div class="caja">
            <p class="tituloTabla"> <span class="icon-list"></span> Incidentes Resueltos</p>
            <asp:GridView 
            ID="GridView1" 
            runat="server" 
            AutoGenerateColumns="False" 
            DataKeyNames="idIncidenteInsumo" 
            DataSourceID="ListadoInsumos" 
            EmptyDataText ="No hay incidentes resueltos"
            AllowPaging="True" 
            AllowSorting="True"
            PageSize="8"
            GridLines="None"
            CssClass="tabla"
            AlternatingRowStyle-CssClass ="alt" 
            PagerStyle-CssClass="pieTabla" 
            ><AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>

            <Columns>
                <asp:BoundField DataField="idIncidenteInsumo" HeaderText="Id" InsertVisible="False" ReadOnly="True" SortExpression="idIncidenteSalon" />
                <asp:BoundField DataField="FechaIncidente" HeaderText="Fecha" SortExpression="FechaIncidente" DataFormatString="{0:d}" />
                <asp:BoundField DataField="Nombre" HeaderText="Edificio" SortExpression="Nombre" />
                <asp:BoundField DataField="Descripcion" HeaderText="Descripción" SortExpression="Descripcion"/>
            </Columns>

<PagerStyle CssClass="pieTabla"></PagerStyle>
        </asp:GridView>
        <asp:SqlDataSource ID="ListadoInsumos" runat="server" ConnectionString="<%$ ConnectionStrings:PlataformaWebConnectionString %>" DeleteCommand="DELETE FROM Insumo WHERE idInsumo = @idInsumo" SelectCommand="SELECT I.idIncidenteInsumo, I.FechaIncidente, E.Nombre , I.Descripcion
FROM IncidenteInsumo I, Insumo E
WHERE I.idUsuario = @idUsuario AND I.idInsumo = E.idInsumo AND I.idEstadoIncidente = 2
ORDER BY I.FechaIncidente DESC">
            <DeleteParameters>
                <asp:Parameter Name="idInsumo" />
            </DeleteParameters>
            <SelectParameters>
                <asp:SessionParameter Name="idUsuario" SessionField="idUsuario" />
            </SelectParameters>
        </asp:SqlDataSource>   

        </div>            

    </form>
</asp:Content>