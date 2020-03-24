<%@ Page Title="" Language="C#" MasterPageFile="~/paginas/instructor/masterInstructor.Master" AutoEventWireup="true" CodeBehind="incidente-salon.aspx.cs" Inherits="PlataformaWeb.paginas.instructor.incidentes.incidente_salon1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
        
        .titulo{
            margin-top:0px;
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
    
    <form runat="server" class="cajaTabla">
        <p class="tituloTabla"> <span style="float:left;" class="icon-list"></span> <asp:Label ID="title" CssClass="tituloTabla" Style="margin-top:0;" runat="server" Text="   Incidentes Sin Resolver"></asp:Label></p>
        
        <asp:Button ID="boton" runat="server" Text="Incidentes Resueltos" class="boton" style="width: 200px;" OnClick="boton_Click"  />
        <asp:GridView 
            ID="GridView1" 
            runat="server" 
            AutoGenerateColumns="False" 
            DataKeyNames="idIncidenteSalon" 
            DataSourceID="ListadoInsumos" 
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
                <asp:BoundField DataField="idIncidenteSalon" HeaderText="Id" InsertVisible="False" ReadOnly="True" SortExpression="idIncidenteSalon" />
                <asp:BoundField DataField="FechaIncidente" HeaderText="Fecha" SortExpression="FechaIncidente" DataFormatString="{0:d}" />
                <asp:BoundField DataField="Nombre" HeaderText="Edificio" SortExpression="Nombre" />
                <asp:BoundField DataField="Salon" HeaderText="Salón" SortExpression="Salon"/>
                <asp:BoundField DataField="Descripcion" HeaderText="Descripción" SortExpression="Descripcion"/>
            </Columns>

<PagerStyle CssClass="pieTabla"></PagerStyle>
        </asp:GridView>
        <asp:SqlDataSource ID="ListadoInsumos" runat="server" ConnectionString="<%$ ConnectionStrings:PlataformaWebConnectionString %>" DeleteCommand="DELETE FROM Insumo WHERE idInsumo = @idInsumo" SelectCommand="SELECT I.idIncidenteSalon, I.FechaIncidente, E.Nombre ,S.Salon, I.Descripcion
FROM IncidenteSalon I, Salon S, Edificio E
WHERE I.idUsuario = @idUsuario AND I.idSalon = S.idSalon AND S.idEdificio = E.idEdificio AND I.idEstadoIncidente = 1
ORDER BY I.FechaIncidente DESC">
            <SelectParameters>
                <asp:SessionParameter Name="idUsuario" SessionField="idUsuario" />
            </SelectParameters>
        </asp:SqlDataSource>       

    </form>
</asp:Content>
