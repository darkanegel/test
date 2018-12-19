<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SEO.aspx.cs" Inherits="SimpleSEOAnalyser.SEO" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h2>SIMPLE SEO Analyser Test</h2>
            <asp:TextBox ID="TextBox1" runat="server" Width="626px"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfv1" runat="server"
                    ControlToValidate="TextBox1" Display="Dynamic"
                    ErrorMessage="Required" ForeColor="Red"></asp:RequiredFieldValidator>
            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Button" />
        </div>

        <div>
            <asp:CheckBox ID="CheckBox1" Checked="true" Text="page filters out stop-words" runat="server" /> <br />
            <asp:CheckBox ID="CheckBox2" Checked="true" Text="calculates number of occurrences on the page of each word" runat="server" /> <br />
            <asp:CheckBox ID="CheckBox3" Checked="true" Text="number of occurrences on the page of each word listed in meta tags" runat="server" /> <br />
            <asp:CheckBox ID="CheckBox4" Checked="true" Text="number of external links in the text" runat="server" /> <br />
        </div>
        <div>
            <h2> calculates number of occurrences on the page of each word </h2>

            <asp:GridView ID="GridView1" runat="server" AllowSorting="True"   
                        AutoGenerateColumns="False" BackColor="White" BorderColor="#999999"   
                        BorderStyle="Solid" BorderWidth="1px" CellPadding="3" DataKeyNames="Keys"   
                        ForeColor="Black" GridLines="Vertical" onsorting="GridView1_Sorting">  
                        <AlternatingRowStyle BackColor="#CCCCCC" />  
                        <Columns>  
                            <asp:TemplateField HeaderText="Keys" SortExpression="Keys">  
                                <ItemTemplate>  
                                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("Keys") %>'></asp:Label>  
                                </ItemTemplate>  
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Value" SortExpression="Value">  
                                <ItemTemplate>  
                                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("Value") %>'></asp:Label>  
                                </ItemTemplate>  
                            </asp:TemplateField>
                        </Columns>  
                        <FooterStyle BackColor="#CCCCCC" />  
                        <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />  
                        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />  
                        <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />  
                        <SortedAscendingCellStyle BackColor="#F1F1F1" />  
                        <SortedAscendingHeaderStyle BackColor="#808080" />  
                        <SortedDescendingCellStyle BackColor="#CAC9C9" />  
                        <SortedDescendingHeaderStyle BackColor="#383838" />  
                    </asp:GridView> 

        </div>
        <div>
            <h2> number of occurrences on the page of each word listed in meta tags </h2>
            <asp:GridView ID="GridView2" runat="server" AllowSorting="True"   
                        AutoGenerateColumns="False" BackColor="White" BorderColor="#999999"   
                        BorderStyle="Solid" BorderWidth="1px" CellPadding="3"  
                        ForeColor="Black" GridLines="Vertical" onsorting="GridView2_Sorting">  
                        <AlternatingRowStyle BackColor="#CCCCCC" />  
                        <Columns>  
                            <asp:TemplateField HeaderText="Name" SortExpression="Name">  
                                <ItemTemplate>  
                                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("Name") %>'></asp:Label>  
                                </ItemTemplate>  
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Content" SortExpression="Content">  
                                <ItemTemplate>  
                                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("Content") %>'></asp:Label>  
                                </ItemTemplate>  
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Words" SortExpression="Words">  
                                <ItemTemplate>  
                                    <asp:Label ID="Label3" runat="server" Text='<%# Bind("Words") %>'></asp:Label>  
                                </ItemTemplate>  
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="TotalWordCount" SortExpression="TotalWordCount">  
                                <ItemTemplate>  
                                    <asp:Label ID="Label4" runat="server" Text='<%# Bind("TotalWordCount") %>'></asp:Label>  
                                </ItemTemplate>  
                            </asp:TemplateField>
                        </Columns>  
                        <FooterStyle BackColor="#CCCCCC" />  
                        <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />  
                        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />  
                        <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />  
                        <SortedAscendingCellStyle BackColor="#F1F1F1" />  
                        <SortedAscendingHeaderStyle BackColor="#808080" />  
                        <SortedDescendingCellStyle BackColor="#CAC9C9" />  
                        <SortedDescendingHeaderStyle BackColor="#383838" />  
                    </asp:GridView>
        </div>
        <div>
            <h2> number of external links in the text </h2>
            <asp:GridView ID="GridView3" runat="server" AllowSorting="True"   
                        AutoGenerateColumns="False" BackColor="White" BorderColor="#999999"   
                        BorderStyle="Solid" BorderWidth="1px" CellPadding="3" DataKeyNames="Keys"   
                        ForeColor="Black" GridLines="Vertical" onsorting="GridView3_Sorting">  
                        <AlternatingRowStyle BackColor="#CCCCCC" />  
                        <Columns>  
                            <asp:TemplateField HeaderText="Keys" SortExpression="Keys">  
                                <ItemTemplate>  
                                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("Keys") %>'></asp:Label>  
                                </ItemTemplate>  
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Value" SortExpression="Value">  
                                <ItemTemplate>  
                                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("Value") %>'></asp:Label>  
                                </ItemTemplate>  
                            </asp:TemplateField>
                        </Columns>  
                        <FooterStyle BackColor="#CCCCCC" />  
                        <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />  
                        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />  
                        <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />  
                        <SortedAscendingCellStyle BackColor="#F1F1F1" />  
                        <SortedAscendingHeaderStyle BackColor="#808080" />  
                        <SortedDescendingCellStyle BackColor="#CAC9C9" />  
                        <SortedDescendingHeaderStyle BackColor="#383838" />  
                    </asp:GridView>
        </div>
    </form>
</body>
</html>
