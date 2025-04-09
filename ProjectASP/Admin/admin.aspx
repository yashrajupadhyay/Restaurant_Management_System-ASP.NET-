<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="admin.aspx.cs" Inherits="ProjectASP.Admin.admin" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Admin Panel</title>
    <style>
        body {
            font-family: 'Poppins', sans-serif;
            background-color: #f4f7fc;
            display: flex;
            justify-content: center;
            align-items: center;
            height: 100vh;
            margin: 0;
        }
        .admin-container {
            width: 400px;
            background: #ffffff;
            padding: 40px;
            border-radius: 15px;
            box-shadow: 0 10px 30px rgba(0, 0, 0, 0.1);
            text-align: center;
        }
        .admin-container h2 {
            margin-bottom: 20px;
            color: #333;
            font-size: 26px;
            font-weight: 600;
        }
        .admin-container a, .logout-button {
            display: block;
            padding: 14px;
            margin: 12px 0;
            background: linear-gradient(135deg, #007bff, #0056b3);
            color: white;
            text-decoration: none;
            border-radius: 8px;
            font-size: 16px;
            font-weight: 500;
            transition: all 0.3s ease;
            cursor: pointer;
            border: none;
            width: 100%;
            text-align: center;
        }
        .admin-container a:hover, .logout-button:hover {
            background: linear-gradient(135deg, #0056b3, #0041a3);
            transform: translateY(-3px);
        }
        .admin-container a i {
            margin-right: 10px;
        }
    </style>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0/css/all.min.css">
</head>
<body>
    <form id="form1" runat="server">
        <div class="admin-container">
            <h2>Admin Dashboard</h2>
            <a href="Users.aspx"><i class="fas fa-users"></i> Show Users</a>
            <a href="Add_Product.aspx"><i class="fas fa-box"></i> Add Product</a>
            <a href="Add_Category.aspx"><i class="fas fa-tags"></i> Add Category</a>
            <a href="BookingDetails.aspx"><i class="fas fa-calendar-check"></i> Show Booking Details</a>
            <a href="ContactDetails.aspx"><i class="fas fa-address-book"></i> Show Contact Details</a>

            <!-- 🧾 New Show Orders button -->
            <a href="ShowOrders.aspx"><i class="fas fa-receipt"></i> Show Orders</a>

            <!-- 📍 New Address Details button -->
            <a href="AddressDetails.aspx"><i class="fas fa-map-marker-alt"></i> Address Details</a>

            <!-- 🚪 Logout Button -->
            <asp:Button ID="btnLogout" runat="server" Text="🚪 Logout" CssClass="logout-button" OnClick="btnLogout_Click" />
        </div>
    </form>
</body>
</html>
