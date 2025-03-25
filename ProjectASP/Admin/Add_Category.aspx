<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Add_Category.aspx.cs" Inherits="ProjectASP.Admin.Add_Category" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    
    <center>
   <form id="form1" runat="server" style="display: flex; justify-content: center; align-items: center; height: 100vh; background: linear-gradient(to right, #007bff, #6610f2); font-family: Arial, sans-serif;">
    <div style="width: 500px; background: white; padding: 30px; border-radius: 10px; box-shadow: 0px 10px 30px rgba(0, 0, 0, 0.2); text-align: center;">

        <h2 style="color: #333; margin-bottom: 20px; font-size: 26px; font-weight: bold;">Add Category</h2>

        <asp:Label ID="lblMessage" runat="server" ForeColor="Red" 
            Style="font-weight: bold; display: block; margin-bottom: 15px; font-size: 14px;"></asp:Label>

        <div style="text-align: left; margin-bottom: 10px;">
            <asp:Label ID="lblCategoryName" runat="server" Text="Category Name:" 
                Style="font-size: 16px; font-weight: 600; color: #555;"></asp:Label>
        </div>

        <asp:TextBox ID="txtCategoryName" runat="server"
            Style="width: 100%; padding: 12px; border: 1px solid #ccc; border-radius: 5px; font-size: 16px; margin-bottom: 20px; outline: none; transition: border-color 0.3s;" 
            onfocus="this.style.borderColor='#007bff'" onblur="this.style.borderColor='#ccc'"></asp:TextBox>

        <asp:Button ID="btnAddCategory" runat="server" Text="Add Category"
            Style="width: 100%; background-color: #007bff; color: white; border: none; padding: 14px; border-radius: 5px; font-size: 18px; font-weight: bold; cursor: pointer; transition: 0.3s;" 
            OnMouseOver="this.style.backgroundColor='#0056b3'" OnMouseOut="this.style.backgroundColor='#007bff'" OnClick="btnAddCategory_Click1" />

    </div>
</form>
        </center>
</body>
</html>
