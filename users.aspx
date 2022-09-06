<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="users.aspx.cs" Inherits="ChatApplication.users" %>

<!DOCTYPE html>

<html lang ="en">
<head>


    <meta charset="UTF-8">
    <meta name ="veiwport" content="width=device-width, intial-scale=1.0">
    <meta http-equiv="X-UA-Compatible" content="ie=edge">
    <meta http-equiv="refresh" content="120">
    <title>RealTime Chat App | OlaOwo</title>
    <link rel ="stylesheet" href ="StyleSheet1.css">
    <link rel="stylesheet" href ="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.2/css/all.min.css"/>
</head>
<body>
    <div class="wrapper">
        <section class="users">
            <header>
                
                <div class="content">
                    <img src="image/flower3_0084.jpg" alt="" runat ="server" id ="ProfilePic">
                    <div class="details">
                        <span runat="server" id ="SpnName">OlaOwo</span>
                        <p runat="server" id="PStats">Active now</p>
                    </div>
                </div>
                <a href="Login.aspx" class="logout" runat="server" id ="alogout">Logout</a>
            </header>
        <form action="#" runat="server">
            <div class="search">
                  
                <span class="text">Select a user to start chat</span>
                
                <asp:TextBox ID="Searcho" runat="server" OnTextChanged ="SearchOla"></asp:TextBox>
                
                <button type="button"><i class="fas fa-search"></i></button>
                
          
            </div>
            
           
            <div class="users-list" runat="server" id="Divlist">
               
                
            </div>
            </form>
        </section>
        
    </div>
     <script src="Javascript/users.js"></script>
</body>
</html>
