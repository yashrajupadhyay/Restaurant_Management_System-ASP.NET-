<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Add_Product.aspx.cs" Inherits="ProjectASP.Admin.Add_Product" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Add Product</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
    <style>
        body {
            background: linear-gradient(to right, #007bff, #6610f2);
            font-family: 'Arial', sans-serif;
            color: #333;
        }
        .container {
            max-width: 700px;
        }
        .card {
            background: white;
            padding: 30px;
            border-radius: 10px;
            box-shadow: 0px 10px 30px rgba(0, 0, 0, 0.2);
        }
        .btn-custom {
            width: 100%;
            padding: 12px;
            font-size: 16px;
            font-weight: bold;
            border-radius: 5px;
            transition: 0.3s;
        }
        .btn-primary:hover {
            background-color: #0056b3 !important;
        }
        .grid-container {
            margin-top: 20px;
            padding: 20px;
            background: white;
            border-radius: 10px;
            box-shadow: 0px 10px 30px rgba(0, 0, 0, 0.2);
        }
        .grid-container h3 {
            text-align: center;
            margin-bottom: 15px;
        }
    </style>
</head>
<body>

    <form id="form1" runat="server">
        <div class="container mt-5">
            <h2 class="text-center text-white">Add Product</h2>

            <!-- Product Form -->
            <div class="card mt-4">
                <div class="row mb-3">
                    <div class="col-md-6">
                        <label class="form-label">Select Category:</label>
                     <asp:DropDownList ID="ddlCategory" runat="server" CssClass="form-control">
    <asp:ListItem Text="-- Select Category --" Value="0"></asp:ListItem>
</asp:DropDownList>

                    </div>
                    <div class="col-md-6">
                        <label class="form-label">Product Name:</label>
                        <asp:TextBox ID="txtProductName" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                </div>

                <div class="row mb-3">
                    <div class="col-md-6">
                        <label class="form-label">Price:</label>
                        <asp:TextBox ID="txtPrice" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-md-6">
                        <label class="form-label">Upload Image:</label>
                        <asp:FileUpload ID="fldimg" runat="server" />
                            <br />
                            <asp:Image ID="imgPreview" runat="server" Width="100" Height="100" />

                    </div>
                </div>

                <div class="mb-3">
                    <label class="form-label">Description:</label>
                    <asp:TextBox ID="txtDescription" CssClass="form-control" TextMode="MultiLine" Rows="3" runat="server"></asp:TextBox>
                </div>

                <!-- Buttons -->
                <asp:Button ID="btnAddProduct" CssClass="btn btn-primary btn-custom" runat="server" Text="Add Product" OnClick="btnAddProduct_Click" />

            </div>

            <!-- Product List -->
            <div class="grid-container mt-4">
                <h3>Product List</h3>
                <asp:GridView ID="GridView1" runat="server" CssClass="table table-bordered table-striped" AutoGenerateColumns="False" OnRowCommand="GridView1_RowCommand">
                    <Columns>
                        <asp:TemplateField HeaderText="Id">
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("Id") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Name">
                            <ItemTemplate>
                                <asp:Label ID="Label2" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Description">
                            <ItemTemplate>
                                <asp:Label ID="Label3" runat="server" Text='<%# Eval("Description") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Price">
                            <ItemTemplate>
                                <asp:Label ID="Label4" runat="server" Text='<%# Eval("Price") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Image">
                            <ItemTemplate>
                              
                                <asp:Image ID="Image1" Height="100px" Width="100px" runat="server" 
                                    ImageUrl='<%# Eval("Image") %>' />

                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Update">
                            <ItemTemplate>
                                <asp:LinkButton ID="LinkButton1" runat="server" CommandArgument='<%# Eval("Id") %>' CommandName="cmd_edt">Update</asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Delete">
                            <ItemTemplate>
                                <asp:LinkButton ID="LinkButton2" runat="server" CommandArgument='<%# Eval("Id") %>' CommandName="md_dlt">Delete</asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>

        </div>
    </form>

</body>
</html>