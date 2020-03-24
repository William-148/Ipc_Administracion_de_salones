<%@ Page Title="" Language="C#" MasterPageFile="~/paginas/operador/operador.Master" AutoEventWireup="true" CodeBehind="lista-salones.aspx.cs" Inherits="PlataformaWeb.paginas.operador.salon.lista_salones" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Lista de Salones</title>
    <script>
        function seleccionar() {            
            var opcion = document.getElementsByClassName("opcion")[3];
            opcion.style.background = '#101010';
        }
        window.addEventListener("load", seleccionar, false);
    </script>
    <style>
        .formSalon .cajaTexto{
            border: solid 1px black;
        }

        .idTitulo{
            display:block;
            font: normal 15px Arial;
        }

        .formSalon .cajaLista{
            border: solid 1px black;
        }

        .tituloTabla{
            margin-top: 5px;
        }

        .mensaje{
            display:inline-block;
        }

        .formSalon {
            margin-left:10px;
            margin-top: 10px;
            margin-bottom: 30px;
            width: 25%;
            padding: 15px;
            box-sizing: border-box;
            display: inline-block;
            background:#fff;
            vertical-align: top;
            border-radius: 8px;
        }

        .tablaSalon{
            margin-left: 20px;
            margin-top: 10px;
            margin-bottom: 30px;
            width: 69%;
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
        <a href="/paginas/operador/salon/lista-incidentes.aspx"><span class="icon-list"></span> Lista de Incidentes</a>
        <a href="/paginas/operador/salon/crear-incidente.aspx"><span class="icon-plus"></span> Registrar Incidente</a>
    </div>

    <form runat="server" class="cajaTabla" >     
          
        <div class="tablaSalon">
            <p class="tituloTabla"> <span class="icon-list"></span><asp:Label CssClass="tituloTabla" ID="tituloTabla" runat="server" Text="Lista de Salones"></asp:Label></p>

            <asp:GridView 
                ID="GridView1" 
                runat="server" 
                AllowPaging="True" 
                AllowSorting="True" 
                AutoGenerateColumns="False" 
                DataKeyNames="idSalon" 
                DataSourceID="listaSalon" 
                OnRowCommand="GridView1_RowCommand"
                PageSize="8"
                GridLines="None"
                CssClass="tabla"
                AlternatingRowStyle-CssClass ="alt" 
                PagerStyle-CssClass="pieTabla"                
                ><AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>

                <Columns>
                    <asp:BoundField DataField="idSalon" HeaderText="Id" InsertVisible="False" ReadOnly="True" SortExpression="idSalon" />
                    <asp:BoundField DataField="Edificio" HeaderText="Edificio" SortExpression="Edificio" />
                    <asp:BoundField DataField="Salon" HeaderText="Salón" SortExpression="Salon" />
                    <asp:BoundField DataField="Capacidad" HeaderText="Capacidad" SortExpression="Capacidad" />
                    <asp:BoundField DataField="Estado" HeaderText="Estado" SortExpression="Estado" />

                    <asp:CommandField HeaderText="Eliminar" ShowDeleteButton="True" ControlStyle-CssClass="tablaBoton" DeleteText="" >
                        <ControlStyle CssClass="tablaBoton icon-circle-with-cross "></ControlStyle>
                        <HeaderStyle CssClass="cabeceraBoton" />
                    </asp:CommandField>
                    
                    <asp:ButtonField CommandName="editar" HeaderText="Editar" Text="" >
                        <ControlStyle CssClass="tablaBoton icon-new-message" />
                        <HeaderStyle CssClass="cabeceraBoton" />
                    </asp:ButtonField>

                    <asp:ButtonField CommandName="verReservas" HeaderText="Reservas">
                    <ControlStyle CssClass="tablaBoton icon-creative-commons-share " />
                    <HeaderStyle CssClass="cabeceraBoton" />
                    </asp:ButtonField>
                </Columns>
                <PagerStyle CssClass="pieTabla"></PagerStyle>
            </asp:GridView>

            <asp:SqlDataSource ID="listaSalon" runat="server" ConnectionString="<%$ ConnectionStrings:PlataformaWebConnectionString %>" DeleteCommand="eliminar_salon" DeleteCommandType="StoredProcedure" SelectCommand="lista_salones" SelectCommandType="StoredProcedure">
                <DeleteParameters>
                    <asp:Parameter Name="idSalon" Type="Int32" />
                </DeleteParameters>
            </asp:SqlDataSource>

        </div>

        <div class="formSalon">
            <p class="tituloTabla"> <span class="icon-add-to-list"></span><asp:Label CssClass="tituloTabla" ID="tituloFormulario" runat="server" Text="Nuevo Salón"></asp:Label></p>

            <asp:Label CssClass="idTitulo" ID="idtitulo" runat="server" Text=""></asp:Label>

            <a>Edificio*: </a>
            <asp:DropDownList ID="edificio" runat="server" CssClass="cajaLista" DataSourceID="edificiosDatos" DataTextField="Nombre" DataValueField="idEdificio" ></asp:DropDownList>
            <asp:SqlDataSource ID="edificiosDatos" runat="server" ConnectionString="<%$ ConnectionStrings:PlataformaWebConnectionString %>" SelectCommand="SELECT * FROM [Edificio] ORDER BY [Nombre]"></asp:SqlDataSource>

            <a>Salon*: </a>
            <asp:TextBox ID="salon" placeholder="Nombre Salon" runat="server" class="cajaTexto"></asp:TextBox>

            <a>Capacidad*: </a>
            <asp:TextBox ID="capacidad" placeholder="Capacidad de alumnos" runat="server" class="cajaTexto" TextMode="Number"></asp:TextBox>

            <a>Estado*: </a>
            <asp:DropDownList ID="estado" runat="server" CssClass="cajaLista" DataSourceID="estadosEdificio" DataTextField="Nombre" DataValueField="idEstadoSalon" ></asp:DropDownList>            
            <asp:SqlDataSource ID="estadosEdificio" runat="server" ConnectionString="<%$ ConnectionStrings:PlataformaWebConnectionString %>" SelectCommand="SELECT * FROM [EstadoSalon]"></asp:SqlDataSource>
            
            <asp:Button ID="boton" runat="server" Text="Registrar" class="boton" OnClick="boton_Click" />
            <asp:Button ID="cancelar" runat="server" Text="Cancelar" class="boton" OnClick="cancelar_Click" />

            <a id="forgot">(*) Campos Obligatorios.</a>

            <asp:Label ID="mensaje" runat="server" Text="" CssClass="mensaje"></asp:Label>

        </div> 



    </form>

</asp:Content>
