<%@ Page Title="" Language="C#" MasterPageFile="~/paginas/instructor/masterInstructor.Master" AutoEventWireup="true" CodeBehind="home.aspx.cs" Inherits="PlataformaWeb.paginas.instructor.home" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Inicio</title>
    <script>
        function seleccionar() {            
            var opcion = document.getElementsByClassName("opcion")[0];
            opcion.style.background = '#101010';
        }
        window.addEventListener("load", seleccionar, false);
    </script>
    <style>
        .contenedorCalendario .contenedorEventos{
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

        .contenedorEventos {
            margin-left:10px;
            margin-top: 10px;
            margin-bottom: 30px;
            width: 25%;
            height: 94%;
            padding: 15px;
            box-sizing: border-box;
            display: inline-block;
            background:#fff;
            vertical-align: top;
            border-radius: 8px;
            overflow-y: scroll;
        }

        .contenedorCalendario{
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

        .eventoFinalizado {
            color: #00ff21;
        }

        .eventoPrevio {
            color: #ff0000;
        }

        .listaEventos tr{
            font: normal 1em arial;
            border-top: solid 3px #ededed;
            border-bottom: solid 3px #ededed;
            color:#282745;
        }

        .listaEventos tr:hover{
            color: #bf400f
        }

        .listaBoton{
            width: 100%;
            padding: 5px 0;
            text-decoration:none;
            color: #808080;
            text-align: center;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <form runat="server" class="cajaTabla" >     
          
        <div class="contenedorCalendario">

            
            <asp:Calendar ID="Calendar1" runat="server" CssClass="calendario" BorderStyle="None" Height="500px" Width="100%" DayNameFormat="Full" NextMonthText="&gt;" PrevMonthText="&lt;" OnSelectionChanged="Calendar1_SelectionChanged" OnDayRender="Calendar1_DayRender">
                <DayHeaderStyle CssClass="diaHeader" />
                <DayStyle CssClass="dia" ForeColor="#666666" />
                <OtherMonthDayStyle CssClass="diaExterno" ForeColor="#CCCCCC" />
                <TitleStyle BackColor="White" CssClass="tituloCalendario" />
                <TodayDayStyle CssClass="diaHoy" />
            </asp:Calendar>         
            
            
        </div>

        <div class="contenedorEventos">
            <h2 class ="tituloTabla">Reservaciones</h2>
            
            <asp:DataList ID="DataList1" runat="server" CssClass="listaEventos" DataKeyField="idReservaSalon" DataSourceID="eventosReservas" OnItemCommand="DataList1_ItemCommand" Width="100%">
                <ItemTemplate>
                    <asp:Label ID="HoraInicioLabel" runat="server" Font-Size="Small" Text='<%# Eval("HoraInicio") %>' Font-Bold="True" />
                    -
                    <asp:Label ID="HoraFinLabel" runat="server" Font-Size="Small" Text='<%# Eval("HoraFin") %>' Font-Bold="True" />
                    <br />
                    <asp:Label ID="EdificioLabel" runat="server" Text='<%# Eval("Edificio") %>' />
                    -
                    <asp:Label ID="SalonLabel" runat="server" Text='<%# Eval("Salon") %>' />
                    <br />
                    Instructor -
                    <asp:Label ID="InstructorLabel" runat="server" Text='<%# Eval("Instructor") %>' />
                    <br />
                    Estado
                    <asp:Label ID="EstadoLabel" runat="server" Text='<%# Eval("Estado") %>' />
                    <br />
                    <asp:LinkButton ID="LinkButton1" runat="server" CssClass="listaBoton" CommandName="ver" CommandArgument='<%# Eval("idReservaSalon") %>'>Ver</asp:LinkButton>
<br />
                </ItemTemplate>
            </asp:DataList>

            <asp:SqlDataSource ID="eventosReservas" runat="server" ConnectionString="<%$ ConnectionStrings:PlataformaWebConnectionString %>" SelectCommand="SELECT R.idReservaSalon, Ed.Nombre as Edificio, S.Salon, R.HoraInicio, R.HoraFin, U.Nombre as Instructor, E.Estado
FROM ReservaSalon R,  EstadoReserva E, Usuario U, Salon S, Edificio Ed
WHERE R.idInstructor = @idInstructor AND R.idEstadoReserva = E.idEstadoReserva and R.idInstructor = u.idUsuario and R.idSalon = S.idSalon and S.idEdificio = Ed.idEdificio and R.Fecha = CONVERT (char(10), getdate(), 103)
ORDER BY HoraInicio ASC">
                <SelectParameters>
                    <asp:SessionParameter Name="idInstructor" SessionField="idUsuario" />
                </SelectParameters>
            </asp:SqlDataSource>

        </div> 
    </form>



</asp:Content>
