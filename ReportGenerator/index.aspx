<%@ Page Title="" Language="C#" MasterPageFile="~/ReportGenerator.Master" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="ReportGenerator.index" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .contents {
            background: #f8f8f8;
            border-radius: 5px;
            margin-bottom: 10px;
            padding: 0px 0 10px 10px;
            box-shadow: 1px 1px 5px 1px #888888;
        }
        h2 {
            padding-top: 10px;
            font-family:'Raleway',sans-serif;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <asp:UpdatePanel ID="OrdersPanel" runat="server">
    <ContentTemplate>
    <div id="container">
        <div class="row">
            <div class="col-md-6">
                <div class="contents">
                    <div id="report_menu" class="border border-white">
                        <h2>Reports</h2>
                        <asp:Label runat="server" Text="Division"></asp:Label>
                        <br />
                        <asp:DropDownList id="ActionList" AutoPostBack="True" runat="server" class="btn btn-default dropdown-toggle" style="width:200px">
                            <asp:ListItem Selected="True" Value="GR Plan"> Eros </asp:ListItem>
                            <asp:ListItem Value="ErosTest"> Eros-Test </asp:ListItem>
                        </asp:DropDownList>
                        <br>
                        <br/>
                        <asp:Label runat="server" Text="Module"></asp:Label>
                        <asp:Label runat="server" id="label11"></asp:Label>
                        <br>
                        <div id="Tabs" role="tabpanel">
                            <ul class="nav nav-tabs" id="myTab" role="tablist">
                                <li id="list1" class="active" runat="server">
                                    <asp:LinkButton ID="inbound_linkbutton" runat="server" OnClick="test1_Click">Inbound</asp:LinkButton>
                                </li>
                                <li id="list2" runat="server">
                                    <asp:LinkButton ID="midbound_linkbutton" runat="server" OnClick="test2_Click">Midbound</asp:LinkButton>
                                </li>
                                <li id="list3" runat="server">
                                    <asp:LinkButton ID="outbound_linkbutton" runat="server" OnClick="test3_Click">Outbound</asp:LinkButton>
                                </li>
                            </ul>
                            <div class="tab-content" id="myTabContent1">
                                <div class="tab-pane fade active in" id="inbound" role="tabpanel" aria-labelledby="home-tab" runat="server">
                                    <asp:DropDownList id="DropDownList1" runat="server" AutoPostBack="true"  class="btn btn-default dropdown-toggle" style="width:250px"  OnTextChanged="ActionList_TextChanged">
                                        <asp:ListItem Selected="True" Value="GR Plan"> GR Plan V2 </asp:ListItem>
                                        <asp:ListItem Value="Item List"> Item List </asp:ListItem>
                                        <asp:ListItem Value="Palletization Report"> Palletization Report </asp:ListItem>
                                        <asp:ListItem Value="GR Execution Report V2"> GR Execution Report V2 </asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="tab-pane fade" id="midbound" role="tabpanel" aria-labelledby="profile-tab" runat="server">
                                    <asp:DropDownList id="DropDownList2" runat="server" AutoPostBack="true" class="btn btn-default dropdown-toggle" style="width:250px" OnTextChanged="ActionList2_TextChanged">
                                        <asp:ListItem Selected="True" Value="Bin To Bin Transactions"> Bin To Bin Transactions </asp:ListItem>
                                        <asp:ListItem Value="Bin To Bin Plan"> Bin To Bin Plan </asp:ListItem>
                                        <asp:ListItem Value="Replenishment"> Replenishment </asp:ListItem>
                                        <asp:ListItem Value="Replenishment V2"> Replenishment V2 </asp:ListItem>
                                        <asp:ListItem Value="Cycle Count Sheet"> Cycle Count Sheet </asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="tab-pane fade" id="outbound" role="tabpanel" aria-labelledby="contact-tab" runat="server">
                                    <asp:DropDownList id="DropDownList3" runat="server" AutoPostBack="true" class="btn btn-default dropdown-toggle" style="width:250px"  OnTextChanged="ActionList3_TextChanged">
                                        <asp:ListItem Selected="True" Value="Bin To Bin Transactions"> ATP's for Outbound Order </asp:ListItem>
                                        <asp:ListItem Value="Draft Wave Plan"> Draft Wave Plan </asp:ListItem>
                                        <asp:ListItem Value="Pick Plan"> Pick Plan </asp:ListItem>
                                        <asp:ListItem Value="Load List"> Load List </asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <br/>
                        <br/>
                    </div>
                </div>
            </div>
            <div class="col-md-6">
                <div class="contents">
                    <div id="filter" class="border border-white">
                        <h2>Filters</h2>
                        <div ID="GRPlan" runat="server">
                            <h3>GR Plan</h3>
                            <asp:Label runat="server" Text="ASN No."></asp:Label>
                            <asp:TextBox ID="TextBox5" runat="server" CssClass="form-control input-lg" Width="250px" Height="32px"></asp:TextBox>
                            <br>
                            <asp:Button ID="Button5" runat="server"  Text="Export to Excel" OnClick="btnExportToExcel_Click" class="btn btn-success"/>
                            <asp:LinkButton ID="LinkButton1" runat="server" Visible="true" OnClick="btnPrint_Click" Text="PRINT" class="btn btn-link"></asp:LinkButton>
                        </div>
                        <div ID="ItemList" runat="server" style="display:none;">
                            <h3>Item List</h3>
                            <asp:Label runat="server" Text="Item Code or Item Description"></asp:Label>
                            <asp:TextBox ID="TextBox6" runat="server" CssClass="form-control input-lg" Width="250px" Height="32px"></asp:TextBox>
                            <br>
                            <asp:Button ID="Button6" runat="server"  Text="Export to Excel" OnClick="btnExportToExcel_Click" class="btn btn-success"/>
                            <asp:LinkButton ID="LinkButton2" runat="server" Visible="true" OnClick="btnPrint_Click" Text="PRINT" class="btn btn-link"></asp:LinkButton>
                        </div>
                        <div ID="BinToBin" runat="server" style="display:none;">
                            <h3>Bin to Bin Transactions</h3>
                            <asp:Label runat="server" Text="Bin Location:"></asp:Label>
                            <asp:TextBox ID="txtBinLoc" runat="server" CssClass="form-control input-lg" Width="250px" Height="32px"></asp:TextBox>
                            <br>
                            <asp:Label runat="server" Text="Item Code:"></asp:Label>
                            <asp:TextBox ID="txtCode" runat="server" CssClass="form-control input-lg" Width="250px" Height="32px" TabIndex="1"></asp:TextBox>
                            <br>
                            <asp:Button ID="btnExportToExcel" runat="server" Text="Export to Excel" Visible="true" OnClick="btnExportToExcel_Click" class="btn btn-success"/>
                            <asp:LinkButton ID="btnPrint" runat="server" Visible="true" OnClick="btnPrint_Click" Text="PRINT" class="btn btn-link"></asp:LinkButton>
                        </div>
                        <div ID="CycleCount" runat="server" style="display:none;">
                            <h3>CycleCount</h3>
                            <asp:Label runat="server" Text="Bin Code (eg. ERO-A, ERO-B, ERO-C):"></asp:Label>
                            <asp:TextBox ID="binCodeTextBox" runat="server" CssClass="form-control input-lg" Width="250px" Height="32px"></asp:TextBox>
                            <br>
                            <asp:Label runat="server" Text="Bin Level (eg. 01, 02, 03):"></asp:Label>
                            <asp:TextBox ID="binLevelTextBox" runat="server" CssClass="form-control input-lg" Width="250px" Height="32px" TabIndex="1"></asp:TextBox>
                            <br>
                            <asp:Label runat="server" Text="Item Code:"></asp:Label>
                            <asp:TextBox ID="itemCodeTextBox" runat="server" CssClass="form-control input-lg" Width="250px" Height="32px" TabIndex="1"></asp:TextBox>
                            <br />
                            <asp:Button ID="Button1" runat="server" Text="Export to Excel" Visible="true" OnClick="btnExportToExcel_Click" class="btn btn-success"/>
                            <asp:LinkButton ID="LinkButton3" runat="server" Visible="true" OnClick="btnPrintCycleCount" Text="PRINT" class="btn btn-link"></asp:LinkButton>
                        </div>
                        <asp:GridView ID="gridviewContainer" runat="server" Width="620px"></asp:GridView>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-md-12">
            <div class="contents" style="height: 50%;">
                <div id="conversion" class="form-horizontal form-inline">
                    <div class="box box-info" style="padding-right:30px;">
                        <div class="box-header with-border"><h2 class="box-title" style="font-family:'Raleway',sans-serif;">Conversions</h2></div>
                        <div class="row">
                            <div class="col-md-3">
                                <asp:Label runat="server" Text="Item Code"></asp:Label>
                                <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control input-lg" Width="200px" Height="32px"></asp:TextBox>
                            </div>
                            <div class="col-md-3">
                                <asp:Label runat="server" Text="Case"></asp:Label>
                                <asp:TextBox ID="TextBox2" runat="server" CssClass="form-control input-lg" Width="200px" Height="32px"></asp:TextBox>
                            </div>
                            <div class="col-md-3">
                                <asp:Label runat="server" Text="Pieces"></asp:Label>
                                <asp:TextBox ID="TextBox3" runat="server" CssClass="form-control input-lg" Width="200px" Height="32px"></asp:TextBox>
                            </div>
                            <div class="col-md-3">
                                <asp:Label runat="server" Text="Pallet"></asp:Label>
                                <asp:TextBox ID="TextBox4" runat="server" CssClass="form-control input-lg" Width="200px" Height="32px"></asp:TextBox>
                            </div>
                        <asp:Button ID="Button3" runat="server"  Text="Compute" OnClick="btnSearch_Click" class="btn btn-success" style="margin-left:15px;float:left;margin-top:10px;"/>
                        <asp:Button ID="Button4" runat="server"  Text="Reset" OnClick="btnSearch_Click" class="btn btn-danger" style="float:right;margin-top:10px;"/>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
