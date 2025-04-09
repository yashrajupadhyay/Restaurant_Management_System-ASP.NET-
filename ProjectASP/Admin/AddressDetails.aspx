<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddressDetails.aspx.cs" Inherits="ProjectASP.Admin.AddressDetails" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Show Address Details</title>
    <link href="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" rel="stylesheet" />
    <style>
        .gridview-container {
            margin-top: 20px;
            margin-bottom: 40px;
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
            background-color: #007bff;
            color: white;
        }
        .gridview-table td {
            border: 1px solid #dee2e6;
        }
        .remove-button {
            color: red;
            font-weight: bold;
            text-decoration: none;
        }
        .remove-button:hover {
            color: darkred;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container gridview-container">
            <h2 class="text-center mb-4">Address Details</h2>
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" OnRowCommand="GridView1_RowCommand" CssClass="gridview-table">
                <Columns>
                    <asp:TemplateField HeaderText="ID">
                        <ItemTemplate>
                            <asp:Label ID="lblId" runat="server" Text='<%# Eval("Id") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Name">
                        <ItemTemplate>
                            <asp:Label ID="lblName" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Phone No">
                        <ItemTemplate>
                            <asp:Label ID="lblPhone" runat="server" Text='<%# Eval("PhoneNo") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Address">
                        <ItemTemplate>
                            <asp:Label ID="lblAddress" runat="server" Text='<%# Eval("Address") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Remove">
                        <ItemTemplate>
                            <asp:LinkButton ID="btnDelete" runat="server" CommandArgument='<%# Eval("Id") %>' CommandName="cmd_delete" CssClass="remove-button" OnClientClick="return confirm('Are you sure to delete this address?');">
                                Remove
                            </asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </form>
</body>
</html>
