<%@ Page Title="" Language="C#" MasterPageFile="~/paginas/instructor/masterInstructor.Master" AutoEventWireup="true" CodeBehind="cuestionario.aspx.cs" Inherits="PlataformaWeb.paginas.instructor.reservas.cuestionario" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        function seleccionar() {            
            var opcion = document.getElementsByClassName("opcion")[1];
            opcion.style.background = '#101010';
        }
        window.addEventListener("load", seleccionar, false);
    </script>
    <style>

        .listaEventos tr{
            font: normal 1em arial;
            border-top: solid 3px #1f2c2e;
            border-bottom: solid 3px #1f2c2e;
            color:#282745;
        }

        .listaEventos tr:hover{
            color:#b200ff;
        }

        .btnlist{
            margin-right: 15px;
            border-radius:10px;
            padding: 10px 10px;
            color: #00ffff;
            background: #131026;
            text-decoration: none;
        }

        .textEditar{
            background:#090816;
            color: #989898;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="subNavegacion">
        <a href="/paginas/instructor/reservas/lista-reservas.aspx"><span class="icon-back"></span> Regresar</a>
    </div>


    <form method="post" runat="server" style="width:95%; height:83%;overflow-y:scroll;">
        
        <h2><span class="icon-arrow-with-circle-up"></span> Cuestionario</h2> 
        <div class="caja">
            <a>Pregunta: </a>
            <asp:TextBox ID="pregunta"  placeholder="Ingreser la pregunta" runat="server" class="cajaTexto" ValidationGroup="Validate" ></asp:TextBox>
                         
            <a>Respuesta: </a>
            <asp:TextBox ID="respuesta" placeholder="Ingreser la respuesta" runat="server" class="cajaTexto" Wrap="True" ValidationGroup="Validate"></asp:TextBox>                 
     
            <asp:Button ID="boton" runat="server" Text="Registrar" class="boton" OnClick="boton_Click" ValidationGroup="Validate"  />

            <asp:TextBox ID="consola" placeholder="Mensajes del Sistema..." runat="server" class="cajaTexto" TextMode="MultiLine" ReadOnly="True" Height="100px"></asp:TextBox>          
         </div>
        

        <div class="caja">
            <asp:DataList ID="DataList1" CssClass="listaEventos" runat="server" DataKeyField="idPregunta" DataSourceID="SqlDataSource1" Width="100%" OnItemCommand="DataList1_ItemCommand">
                <EditItemTemplate>
                    &nbsp;<br /> 
                    Pregunta:<br />
                    <asp:TextBox ID="TextBox1" runat="server" CssClass="cajaTexto textEditar" Text='<%# Eval("Pregunta") %>' TextMode="MultiLine"></asp:TextBox>
                    <br />
                    Respuesta:
                    <br />
                    <asp:TextBox ID="TextBox2" runat="server" CssClass="cajaTexto textEditar" Text='<%# Eval("Respuesta") %>' TextMode="MultiLine"></asp:TextBox>
                    <br />
                    <asp:LinkButton ID="Button1" Text="" CssClass="btnlist icon-check" runat="server" CommandName="actualizar" CommandArgument='<%# Eval("idPregunta") %>'  />
                    <asp:LinkButton ID="Button2" Text="" CssClass="btnlist icon-cross" runat="server" CommandName="cancelar"  />
                </EditItemTemplate>
                <ItemTemplate>
                    &nbsp;<asp:Label ID="idPreguntaLabel" runat="server" Text='<%# Eval("idPregunta") %>' Visible="False" />
                    <asp:Label ID="idActividadLabel" runat="server" Text='<%# Eval("idActividad") %>' Visible="False" />
                    <br />
                    Pregunta:<br />
                    <asp:TextBox ID="TextBox1" runat="server" CssClass="cajaTexto" ReadOnly="True" Text='<%# Eval("Pregunta") %>' TextMode="MultiLine"></asp:TextBox>
                    <br />
                    Respuesta:
                    <br />
                    <asp:TextBox ID="TextBox2" runat="server" CssClass="cajaTexto" ReadOnly="True" Text='<%# Eval("Respuesta") %>' TextMode="MultiLine"></asp:TextBox>
                    <br />
                    <asp:LinkButton ID="Button1" CssClass="btnlist icon-edit" runat="server" CommandName="editar" Text="" />
                    <asp:LinkButton ID="Button2" CssClass="btnlist icon-trash" runat="server" CommandArgument='<%# Eval("idPregunta") %>' CommandName="eliminar" Text="" />
                    <br />
                    <br />
                </ItemTemplate>
            </asp:DataList>

        </div>
        
            
 
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:PlataformaWebConnectionString %>" SelectCommand="SELECT * FROM [Pregunta] WHERE ([idActividad] = @idActividad)">
            <SelectParameters>
                <asp:SessionParameter Name="idActividad" SessionField="idActividad" Type="Int32" />
            </SelectParameters>
        </asp:SqlDataSource>
        
            
 
    </form>


</asp:Content>
