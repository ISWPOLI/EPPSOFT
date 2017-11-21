<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UCDetailAvances.ascx.cs" Inherits="UserControl_UCDetailAvances" %>
<style type="text/css">
    .auto-style1
    {
        height: 174px;
        text-align: center;
    }
    .auto-style2
    {
        height: 17px;
    }
    .auto-style3
    {
        width: 10%;
        height: 35px;
    }
    .auto-style4
    {
        width: 80%;
        height: 35px;
    }
    .auto-style5
    {
        width: 10%;
        height: 34px;
    }
    .auto-style6
    {
        width: 80%;
        height: 34px;
    }
</style>

    <table style="width: 100%">
        <tr>
            <td colspan="3" class="auto-style2">
            </td>
            
        </tr>
        <tr>
            <td class="auto-style3">
            </td>
            <td style="text-align:center; background-color:#00385D; " class="auto-style4">
                <asp:Label ID="lblNombre" CssClass="TituloDetalle" runat="server" Text=""></asp:Label>
            </td>
            <td class="auto-style3">
            </td>
        </tr>
        <tr>
            <td class="auto-style5">
            </td>
            <td style="text-align:left; vertical-align:bottom " class="auto-style6">
                <asp:Label ID="lblCasos" runat="server" Font-Bold="True" Font-Size="Large" ForeColor="Gray"></asp:Label>
            </td>
            <td class="auto-style5">
            </td>
        </tr>
        <tr>
            <td style="width:10%">
            </td>
            <td style="width:80%">
                <table style="width: 100%">

                    <tr>
                        <td class="auto-style1">
                            <asp:Chart ID="Chart1" runat="server" Height="370px" Width="544px">
                        <Series>
                            <asp:Series ChartType="Doughnut" Name="Series1" IsValueShownAsLabel="True" Legend="Legend1">
                                <Points>
                                    <asp:DataPoint BackImageWrapMode="Tile" Color="0, 192, 0" CustomProperties="Exploded=True" Font="Microsoft Sans Serif, 13pt, style=Bold" IsValueShownAsLabel="False" Label="" LabelBorderDashStyle="Solid" LegendText="Cumpliendo." ToolTip="" YValues="0" LabelForeColor="64, 64, 64" />
                                    <asp:DataPoint Color="Yellow" CustomProperties="Exploded=True" Font="Microsoft Sans Serif, 13pt, style=Bold" Label="" LegendText="Atrasado hasta 48 horas." YValues="0" IsValueShownAsLabel="False" LabelForeColor="64, 64, 64" />
                                    <asp:DataPoint Color="Red" CustomProperties="Exploded=True" Font="Microsoft Sans Serif, 13pt, style=Bold" Label="" LegendText="Atrasado mas de 48 horas." YValues="0" IsValueShownAsLabel="False" LabelForeColor="64, 64, 64" />
                                </Points>
                                <EmptyPointStyle CustomProperties="Exploded=True" />
                            </asp:Series>
                        </Series>
                        <ChartAreas>
                            <asp:ChartArea Name="ChartArea1" AlignmentOrientation="All" BackImageWrapMode="Scaled">
                                <Area3DStyle Inclination="10" Rotation="5" />
                            </asp:ChartArea>
                        </ChartAreas>
                        <Legends>
                            <asp:Legend Alignment="Center" Docking="Bottom" HeaderSeparator="ThickLine" Name="Legend1">
                            </asp:Legend>
                        </Legends>
                    </asp:Chart>

                        </td>
                    </tr>
                </table>
            </td>
            <td style="width:10%">
            </td>
        </tr>
    </table>






