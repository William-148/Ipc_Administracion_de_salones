<%@ Page Title="" Language="C#" MasterPageFile="~/paginas/operador/operador.Master" AutoEventWireup="true" CodeBehind="lista-insumos.aspx.cs" Inherits="PlataformaWeb.paginas.operador.insumos.lista_insumos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Lista de Insumos</title>
    <script>
        function seleccionar() {            
            var opcion = document.getElementsByClassName("opcion")[4];
            opcion.style.background = '#101010';
        }
        window.addEventListener("load", seleccionar, false);
    </script>
    <style>
        .formInsumos .cajaTexto{
            border: solid 1px black;
        }

        .idTitulo{
            display:block;
            font: normal 15px Arial;
        }

        .formInsumos .cajaLista{
            border: solid 1px black;
        }

        .tituloTabla{
            margin-top: 5px;
        }

        .mensaje{
            display:inline-block;
        }

        .formInsumos {
            margin-left:10px;
            margin-top: 10px;
            margin-bottom: 30px;
            width: 28%;
            padding: 15px;
            box-sizing: border-box;
            display: inline-block;
            background:#fff;
            vertical-align: top;
            border-radius: 8px;
        }

        .tablaInsumos{
            margin-left: 20px;
            margin-top: 10px;
            margin-bottom: 30px;
            width: 67%;
            padding: 15px 0;
            background:#fff;
            box-sizing: border-box;
            display: inline-block;
            vertical-align: top;
            border-radius: 8px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="subNavegacion">
        <a href="/paginas/operador/insumos/lista-incidentes.aspx"><span class="icon-list"></span> Lista Incidentes</a>
        <a href="/paginas/operador/insumos/lista-prestamo.aspx"><span class="icon-list"></span> Lista Prestamos</a>
        <a href="/paginas/operador/insumos/registrar-incidente.aspx"><span class="icon-circle-with-plus"></span> Registrar Incidente</a>
        <a href="/paginas/operador/insumos/prestamo.aspx"><span class="icon-circle-with-plus"></span> Registrar Prestamo</a>
    </div>

    <form runat="server" class="cajaTabla">

        <div class="tablaInsumos">
            
            <p class="tituloTabla"> <span class="icon-list"></span> Lista de Insumos </p>

        <asp:GridView 
            ID="GridView1" 
            runat="server" 
            AutoGenerateColumns="False" 
            DataKeyNames="idInsumo" 
            DataSourceID="ListadoInsumos" 
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
                <asp:BoundField DataField="idInsumo" HeaderText="Id" InsertVisible="False" ReadOnly="True" SortExpression="idInsumo" />
                <asp:BoundField DataField="Nombre" HeaderText="Nombre" SortExpression="Nombre" />
                <asp:BoundField DataField="Descripcion" HeaderText="Descripción" SortExpression="Descripcion" />
                <asp:BoundField DataField="Tipo" HeaderText="Tipo" SortExpression="Tipo"/>

                <asp:CommandField  ShowDeleteButton="True" ControlStyle-CssClass="tablaBoton" DeleteText="" HeaderText="Eliminar"  >
                    <ControlStyle CssClass="tablaBoton icon-remove-user"></ControlStyle>
                    <HeaderStyle CssClass="cabeceraBoton" />
                <ItemStyle Width="80px" />
                </asp:CommandField>


                <asp:ButtonField HeaderText="Editar" ControlStyle-CssClass="tablaBoton" CommandName="editar">
                    <ControlStyle CssClass="tablaBoton icon-new-message" />
                    <HeaderStyle CssClass="cabeceraBoton" />
                <ItemStyle Width="80px" />
                </asp:ButtonField>

            </Columns>

<PagerStyle CssClass="pieTabla"></PagerStyle>
        </asp:GridView>
        <asp:SqlDataSource ID="ListadoInsumos" runat="server" ConnectionString="<%$ ConnectionStrings:PlataformaWebConnectionString %>" DeleteCommand="DELETE FROM Insumo WHERE idInsumo = @idInsumo" SelectCommand="SELECT I.idInsumo, I.Nombre, I.Descripcion, T.Nombre AS Tipo FROM Insumo I, TipoInsumo T WHERE I.idTipoInsumo = T.idTipoInsumo">
            <DeleteParameters>
                <asp:Parameter Name="idInsumo" />
            </DeleteParameters>
        </asp:SqlDataSource>
            
        </div>

        <div class="formInsumos">
            <p class="tituloTabla"> <span class="icon-add-to-list"></span><asp:Label CssClass="tituloTabla" ID="tituloFormulario" runat="server" Text="Nuevo Insumo"></asp:Label></p>

            <asp:Label CssClass="idTitulo" ID="idtitulo" runat="server" Text=""></asp:Label>

            <a>Tipo de Insumo*: </a>
            <asp:DropDownList ID="tipoInsumo" runat="server" CssClass="cajaLista" DataSourceID="tipoInsumodatos" DataTextField="Nombre" DataValueField="idTipoInsumo" ></asp:DropDownList>
            <asp:SqlDataSource ID="tipoInsumodatos" runat="server" ConnectionString="<%$ ConnectionStrings:PlataformaWebConnectionString %>" SelectCommand="SELECT * FROM [TipoInsumo]"></asp:SqlDataSource>

            <a>Nombre*: </a>
            <asp:TextBox ID="nombre"  placeholder="Nombre del Insumo" runat="server" class="cajaTexto"></asp:TextBox>

            <a>Descripción*: </a>
            <asp:TextBox ID="descripcion" placeholder="Descripción del Insumo" runat="server" class="cajaTexto" TextMode="MultiLine" Height="130px"></asp:TextBox>
                        
            <asp:Button ID="boton" runat="server" Text="Registrar" class="boton" OnClick="boton_Click" />
            <asp:Button ID="cancelar" runat="server" Text="Cancelar" class="boton" OnClick="cancelar_Click" />

            <a id="forgot">(*) Campos Obligatorios.</a>

            <asp:Label ID="mensaje" runat="server" Text="" CssClass="mensaje"></asp:Label>


        </div>

        

    </form>
</asp:Content>
