<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="show_detailview.aspx.cs" Inherits="ProjectASP.show_detailview" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Product Details</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:DataList ID="DataList1" runat="server">
                <ItemTemplate>
                    <asp:Image ID="Image1" runat="server" Height="100" Width="100" ImageUrl='<%# Eval("Image") %>' />
                    <br /><br />
                    <b>Description:</b>
                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("Description") %>'></asp:Label>
                    <br /><br />
                    <%--<b>Price:</b>
                    <asp:Label ID="Label2" runat="server" Text='<%# Eval("Price") %>'></asp:Label>--%>
                </ItemTemplate>
            </asp:DataList>
        </div>
    </form>
</body>
</html>
