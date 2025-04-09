<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="AddToCart.aspx.cs" Inherits="ProjectASP.AddToCart" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      <!DOCTYPE html>
<html>

<head>
    <meta charset="utf-8">
    <title>Restoran - Bootstrap Restaurant Template</title>
    <meta content="width=device-width, initial-scale=1.0" name="viewport">
    <meta content="" name="keywords">
    <meta content="" name="description">
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <!-- Favicon -->
    <link href="img/favicon.ico" rel="icon">

    <!-- Google Web Fonts -->
    <link rel="preconnect" href="https://fonts.googleapis.com">
    <link rel="preconnect" href="https://fonts.gstatic.com" crossorigin>
    <link href="https://fonts.googleapis.com/css2?family=Heebo:wght@400;500;600&family=Nunito:wght@600;700;800&family=Pacifico&display=swap" rel="stylesheet">

    <!-- Icon Font Stylesheet -->
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.10.0/css/all.min.css" rel="stylesheet">
    <link href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.4.1/font/bootstrap-icons.css" rel="stylesheet">

    <!-- Libraries Stylesheet -->
    <link href="lib/animate/animate.min.css" rel="stylesheet">
    <link href="lib/owlcarousel/assets/owl.carousel.min.css" rel="stylesheet">
    <link href="lib/tempusdominus/css/tempusdominus-bootstrap-4.min.css" rel="stylesheet" />

    <!-- Customized Bootstrap Stylesheet -->
    <link href="css/bootstrap.min.css" rel="stylesheet">

    <!-- Template Stylesheet -->
    <link href="css/style.css" rel="stylesheet">
     <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
 <style>
    body {
        margin: 0;
        padding: 0;
        font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
        background: #f4f4f4;
    }

    .cart-wrapper {
        max-width: 960px;
        margin: 0 auto;
        padding: 20px;
        display: flex;
        flex-direction: column;
        min-height: calc(100vh - 80px); /* Adjust for header/footer if any */
    }

    h2 {
        text-align: center;
        margin-bottom: 30px;
        font-weight: 600;
    }

    .cart-item {
        border: 1px solid #ddd;
        background-color: #fff;
        padding: 20px;
        margin-bottom: 20px;
        display: flex;
        border-radius: 10px;
        box-shadow: 0 4px 10px rgba(0, 0, 0, 0.05);
        gap: 20px;
    }

    .cart-item img {
        width: 100px;
        height: 80px;
        object-fit: cover;
        border-radius: 10px;
    }

    .cart-details h3 {
        margin: 0 0 10px;
    }

    .cart-details p {
        margin: 4px 0;
        line-height: 1.4;
    }

    .order-button-container {
        text-align: right;
        margin-top: auto; /* Push button to bottom if needed */
        padding-top: 10px;
    }

    .order-button {
        background-color: #28a745;
        color: white;
        padding: 12px 30px;
        border: none;
        border-radius: 6px;
        font-size: 16px;
        cursor: pointer;
        transition: 0.3s ease;
    }

    .order-button:hover {
        background-color: #218838;
    }
</style>
</head>

<body>
    <div class="container-xxl bg-white p-0">
        <!-- Spinner Start -->
        <div id="spinner" class="show bg-white position-fixed translate-middle w-100 vh-100 top-50 start-50 d-flex align-items-center justify-content-center">
            <div class="spinner-border text-primary" style="width: 3rem; height: 3rem;" role="status">
                <span class="sr-only">Loading...</span>
            </div>
        </div>
        <!-- Spinner End -->


        <!-- Navbar & Hero Start -->
        <div class="container-xxl position-relative p-0">
            <nav class="navbar navbar-expand-lg navbar-dark bg-dark px-4 px-lg-5 py-3 py-lg-0">
                <a href="" class="navbar-brand p-0">
                    <h1 class="text-primary m-0"><i class="fa fa-utensils me-3"></i>Restoran</h1>
                    <!-- <img src="img/logo.png" alt="Logo"> -->
                </a>
                <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarCollapse">
                    <span class="fa fa-bars"></span>
                </button>
                <div class="collapse navbar-collapse" id="navbarCollapse">
                    <div class="navbar-nav ms-auto py-0 pe-4">
                        <a href="index.aspx" class="nav-item nav-link">Home</a>
                        <a href="about.aspx" class="nav-item nav-link">About</a>
                        <a href="service.aspx" class="nav-item nav-link">Service</a>
                        <a href="order.aspx" class="nav-item nav-link active">Menu</a>
                        <div class="nav-item dropdown">
                            <a href="#" class="nav-link dropdown-toggle" data-bs-toggle="dropdown">Pages</a>
                            <div class="dropdown-menu m-0">
                                <a href="booking.aspx" class="dropdown-item">Booking</a>
                                <a href="team.aspx" class="dropdown-item">Our Team</a>
                                <a href="testimonial.aspx" class="dropdown-item">Testimonial</a>
                            </div>
                        </div>
                        <a href="contact.aspx" class="nav-item nav-link">Contact</a>
                    </div>
                    <a href="booking.aspx" class="btn btn-primary py-2 px-4">Book A Table</a>
                    <%--<asp:Button ID="btnBookTable" runat="server" class="btn btn-primary " Text="BOOK A TABLE " OnClick="btnBookTable_Click1" />--%>
                </div>
            </nav>

            <div class="container-xxl py-5 bg-dark hero-header mb-5">
                <div class="container text-center my-5 pt-5 pb-4">
                    <h1 class="display-3 text-white mb-3 animated slideInDown">Add To Cart</h1>
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb justify-content-center text-uppercase">
                            <li class="breadcrumb-item"><a href="#">Home</a></li>
                            <li class="breadcrumb-item"><a href="#">Pages</a></li>
                            <li class="breadcrumb-item text-white active" aria-current="page">Product</li>
                        </ol>
                    </nav>
                </div>
            </div>
        </div>
        <!-- Navbar & Hero End -->
    </div>


</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
     

<div class="cart-wrapper">
    <h2>Your Cart Items</h2>

    <asp:DataList ID="DataListCart" runat="server" RepeatColumns="1" OnItemCommand="DataListCart_ItemCommand">
    <ItemTemplate>
        <div class="cart-item">
            <img src='<%# Eval("Image") %>' alt="Product" style="height: 100px;" />
            <div class="cart-details">
                <h3><%# Eval("Name") %></h3>
                <p><%# Eval("Description") %></p>
                <p><strong>Qty:</strong> <%# Eval("Quantity") %></p>
                <p><strong>Price:</strong> ₹<%# Eval("Price") %></p>
                <p><strong>Total:</strong> ₹<%# Eval("TotalPrice") %></p>

                <!-- ✅ Remove Button -->
                <asp:Button ID="btnRemove" runat="server" Text="Remove"
                    CommandName="RemoveItem"
                    CommandArgument='<%# Eval("ProductId") %>'
                    CssClass="order-button"
                    OnClientClick="return confirm('Are you sure you want to remove this item?');" />
            </div>
        </div>
    </ItemTemplate>
</asp:DataList>

    <center>
    <div class="order-button-container">
        <asp:Button ID="btnOrder" runat="server" Text="Place Order" CssClass="order-button" OnClick="btnOrder_Click"  />
    </div>
        </center>
</div>

  </asp:Content>