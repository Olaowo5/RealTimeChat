<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="ChatApplication.Login" %>

<!DOCTYPE html>

<html lang ="en">
<head>


    <meta charset="UTF-8">
    <meta name ="veiwport" content="width=device-width, intial-scale=1.0">
    <meta http-equiv="X-UA-Compatible" content="ie=edge">
    <title>RealTime Chat App | OlaOwo</title>
    <link rel ="stylesheet" href ="StyleSheet1.css">
    <link rel="stylesheet" href ="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.2/css/all.min.css"/>
</head>
<body>
    <div class="wrapper">
        <section class="form login">
            <header> Realtime Chat App</header>
            <form action="#" runat="server">
                <div class="error-text" runat="server" id ="errortxt">THis is an Error Message!</div>
               
                    <div class="field input">
                        <label>Email</label>
                        <input type="text" placeholder="Enter Your Email" id="Emal" runat="server"/>
                    </div>

                    <div class="field input">
                        <label>Password</label>
                        <input type="password" placeholder="Enter Password" id="PassW" runat="server"/>
                        <i class="fas fa-eye"></i>
                    </div>

                 

                    <div class="field button">
                      
                         <asp:Button ID="SMButton" runat="server" Text="Continue to Chat"  OnClick="SMButton_Click" UseSubmitBehavior="False"  />
                    </div>
                
            </form>
            <div class="link">Not yet signed up? <a href="WebForm1.aspx">SignUp Now</a></div>
        </section>
    </div>
    <script src="Javascript/pass-show-hide.js"></script>
</body>
</html>