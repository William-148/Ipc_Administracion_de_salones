<%@ Page Title="" Language="C#" MasterPageFile="~/paginas/estudiante/masterEstudiante.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="PlataformaWeb.paginas.estudiante._default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
    <form method="post" runat="server" style="width:70%; height:83%;overflow-y:scroll;">
        
        <h2><span class="icon-arrow-with-circle-up"></span> <asp:Label ID="titulo" CssClass="tituloTabla" runat="server" Text="Cuestionario"></asp:Label></h2> 
        
        
            <asp:DataList ID="DataList1" CssClass="listaEventos" runat="server" DataKeyField="idPregunta" DataSourceID="SqlDataSource1" Width="100%" >
                
                <ItemTemplate>
                    &nbsp;<asp:Label ID="idPregunta" runat="server" Text='<%# Eval("idPregunta") %>' Visible="False" />                    
                    <br />
                    Pregunta:<br />
                    <asp:TextBox ID="TextBox1" runat="server" CssClass="cajaTexto" ReadOnly="True" Text='<%# Eval("Pregunta") %>' ></asp:TextBox>
                    <br />
                    Respuesta:
                    <br />
                    <asp:TextBox ID="respuesta" runat="server" Placeholder="Ingrese su respuesta..." CssClass="cajaTexto"  TextMode="MultiLine"></asp:TextBox>
                    <br />
                    <br />
                    <br />
                </ItemTemplate>
            </asp:DataList>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:PlataformaWebConnectionString %>" ></asp:SqlDataSource>

        <asp:Button ID="boton" runat="server" Text="Guardar" class="boton" OnClick="boton_Click" ValidationGroup="Validate"  />
        
    </form>

</asp:Content>
