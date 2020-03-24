﻿<%@ Page Title="" Language="C#" MasterPageFile="~/paginas/instructor/masterInstructor.Master" AutoEventWireup="true" CodeBehind="insumos-prestados.aspx.cs" Inherits="PlataformaWeb.paginas.instructor.insumos.insumos_prestado" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
        <a href="/paginas/instructor/insumos/insumos-devueltos.aspx"><span class="icon-list"></span> Insumos Devueltos</a>
    </div>

    <form runat="server" class="cajaTabla">
        <p class="tituloTabla"> <span class="icon-list"></span> Insumos Prestados</p>

        <asp:GridView 
            ID="GridView1" 
            runat="server" 
            AutoGenerateColumns="False" 
            DataKeyNames="idInsumoPrestado" 
            DataSourceID="ListadoInsumos" 
            AllowPaging="True" 
            EmptyDataText ="No hay insumos prestados"
            AllowSorting="True"
            PageSize="8"
            GridLines="None"
            CssClass="tabla"
            AlternatingRowStyle-CssClass ="alt" 
            PagerStyle-CssClass="pieTabla" 
            ><AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>

            <Columns>
                <asp:BoundField DataField="idInsumoPrestado" HeaderText="Id" InsertVisible="False" ReadOnly="True" SortExpression="idInsumoPrestado" />
                <asp:BoundField DataField="Fecha" HeaderText="Fecha" SortExpression="Fecha" DataFormatString="{0:d}" />
                <asp:BoundField DataField="Nombre" HeaderText="Insumo" SortExpression="Nombre" />
                <asp:BoundField DataField="Tipo" HeaderText="Tipo" SortExpression="Tipo"/>
                <asp:BoundField DataField="Descripcion" HeaderText="Descripción" SortExpression="Descripcion"/>
            </Columns>

<PagerStyle CssClass="pieTabla"></PagerStyle>
        </asp:GridView>
        <asp:SqlDataSource ID="ListadoInsumos" runat="server" ConnectionString="<%$ ConnectionStrings:PlataformaWebConnectionString %>" DeleteCommand="DELETE FROM Insumo WHERE idInsumo = @idInsumo" SelectCommand="SELECT I.idInsumoPrestado, I.Fecha, N.Nombre, T.Nombre AS Tipo, I.Descripcion
FROM InsumoPrestado I, Insumo N, TipoInsumo T
WHERE I.idUsuario = @idUsuario AND idEstadoPrestamo = 1 AND I.idInsumo = N.idInsumo AND N.idTipoInsumo = T.idTipoInsumo
ORDER BY I.Fecha DESC">
            <SelectParameters>
                <asp:SessionParameter Name="idUsuario" SessionField="idUsuario" />
            </SelectParameters>
        </asp:SqlDataSource>       

    </form>
</asp:Content>
