<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShowOrders.aspx.cs" Inherits="ProjectASP.Admin.ShowOrders" %>




<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Manage Orders</title>
    <link href="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" rel="stylesheet" />
    <style>
        .gridview-container {
            margin-top: 20px;
            margin-bottom: 40px;
        }
        .remove-button {
            color: #dc3545;
            font-weight: bold;
            text-decoration: none;
        }
        .remove-button:hover {
            color: #ff4c4c;
        }
        .gridview-table {
            width: 100%;
            border-collapse: collapse;
        }
        .gridview-table th, .gridview-table td {
            padding: 10px;
            text-align: center;
        }
        .gridview-table th {
            background-color: #f8f9fa;
            border: 1px solid #dee2e6;
        }
        .gridview-table td {
            border: 1px solid #dee2e6;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container gridview-container">
            <h2 class="text-center mb-4">Manage Orders</h2>
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" OnRowCommand="GridView1_RowCommand" CssClass="gridview-table">
                <Columns>
                    <asp:TemplateField HeaderText="Order ID">
                        <ItemTemplate>
                            <asp:Label ID="lblOrderId" runat="server" Text='<%# Eval("Order_Id") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Cart ID">
                        <ItemTemplate>
                            <asp:Label ID="lblCartId" runat="server" Text='<%# Eval("Cart_Id") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="User ID">
                        <ItemTemplate>
                            <asp:Label ID="lblUserId" runat="server" Text='<%# Eval("User_Id") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Product ID">
                        <ItemTemplate>
                            <asp:Label ID="lblProductId" runat="server" Text='<%# Eval("Product_Id") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Order Date">
                        <ItemTemplate>
                            <asp:Label ID="lblOrderDate" runat="server" Text='<%# Eval("OrderDate", "{0:dd-MM-yyyy}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Status">
                        <ItemTemplate>
                            <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("Status") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Remove">
                        <ItemTemplate>
                            <asp:LinkButton ID="btnDelete" runat="server" CommandArgument='<%# Eval("Order_Id") %>' CommandName="cmd_delete" CssClass="remove-button" OnClientClick="return confirm('Are you sure you want to delete this order?');">
                                Remove
                            </asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>

        <!-- Bootstrap JS -->
        <script src="https://code.jquery.com/jquery-3.5.1.slim.min.js"></script>
        <script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.5.2/dist/umd/popper.min.js"></script>
        <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>
    </form>
</body>
</html>
