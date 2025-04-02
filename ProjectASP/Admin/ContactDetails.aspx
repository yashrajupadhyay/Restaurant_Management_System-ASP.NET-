<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ContactDetails.aspx.cs" Inherits="ProjectASP.Admin.ContactDetails" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Manage Contact Details</title>
    <link href="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" rel="stylesheet" />
    <style>
        .gridview-container {
            margin-top: 20px;
            margin-bottom: 40px;
        }
        .gridview-header {
            background-color: #007bff;
            color: white;
            text-align: center;
        }
        .gridview-row:hover {
            background-color: #f1f1f1;
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
            <h2 class="text-center mb-4">Manage Contact Details</h2>
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" OnRowCommand="GridView1_RowCommand" CssClass="gridview-table">
                <Columns>
                    <asp:TemplateField HeaderText="ID">
                        <ItemTemplate>
                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("Id") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Name">
                        <ItemTemplate>
                            <asp:Label ID="Label2" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Email">
                        <ItemTemplate>
                            <asp:Label ID="Label3" runat="server" Text='<%# Eval("Email") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Subject">
                        <ItemTemplate>
                            <asp:Label ID="Label4" runat="server" Text='<%# Eval("Subject") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Message">
                        <ItemTemplate>
                            <asp:Label ID="Label5" runat="server" Text='<%# Eval("Message") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Remove">
                        <ItemTemplate>
                            <asp:LinkButton ID="LinkButton2" runat="server" CommandArgument='<%# Eval("Id") %>' CommandName="cmd_delete" OnClientClick="return confirm('Are you sure you want to delete this contact entry?');" CssClass="remove-button">
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
