<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="login2_master.aspx.cs" Inherits="ProjectASP.login2_master" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!DOCTYPE html>
<html>
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Login & Signup</title>
    <link rel="stylesheet" href="style.css">
</head>
<body>
    <div class="cont">
        <div class="form sign-in">
            <h2>Welcome</h2>
            <label>
                <span>Email</span>
                <asp:TextBox ID="txteml" runat="server"></asp:TextBox>
              <%--  <input type="email">--%>
            </label>
            <label>
                <span>Password</span>
              <%--  <input type="password">--%>
                <asp:TextBox ID="txtpass" runat="server" TextMode="Password"></asp:TextBox>
            </label>
            <!-- <p class="forgot-pass">Forgot password?</p> -->
           <%-- <button type="button" class="submit">Sign In</button>--%>
            <asp:Button ID="signin" class="submit" runat="server" Text="sIGN IN" OnClick="signin_Click" />
        </div>
        <div class="sub-cont">
            <div class="img">
                <div class="img__text m--up">
                    <h3>Don't have an account? Please Sign up!</h3>
                </div>
                <div class="img__text m--in">
                    <h3>If you already have an account, just sign in.</h3>
                </div>
                <div class="img__btn">
                    <span class="m--up">Sign Up</span>
                    <span class="m--in">Sign In</span>
                </div>
            </div>
            <div class="form sign-up">
                <h2>Create your Account</h2>
                <label>
                    <span>Name</span>
                    <%--<input type="text">--%>
                    <asp:TextBox ID="txtname" runat="server"></asp:TextBox>
                </label>
                <label>
                    <span>Email</span>
                   <%-- <input type="email">--%>
                    <asp:TextBox ID="txtemail_signup" runat="server"></asp:TextBox>
                </label>
                <label>
                    <span>Password</span>
                    <%--<input type="password">--%>
                    <asp:TextBox ID="txtpswd" runat="server"></asp:TextBox>
                </label>
                <label>
                    <span>Select Role</span>
                    <asp:DropDownList ID="ddlRole" runat="server">
                        <asp:ListItem Text="User" Value="User"></asp:ListItem>
                        <asp:ListItem Text="Admin" Value="Admin"></asp:ListItem>
                    </asp:DropDownList>
                </label>
               <%-- <button type="button" class="submit">Sign Up</button>--%>
                <asp:Button ID="btnsignup"  class="submit" runat="server" Text="SIGN UP" OnClick="btnsignup_Click"  />
            </div>
        </div>
    </div>
    <script>
        document.querySelector('.img__btn').addEventListener('click', function() {
            document.querySelector('.cont').classList.toggle('s--signup');
        });
    </script>
</body>
</html>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
</asp:Content>
