<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="chat.aspx.cs" Inherits="ChatApplication.chat" %>

<!DOCTYPE html>

<html lang ="en">
<head>


    <meta charset="UTF-8">
    <meta name ="veiwport" content="width=device-width, intial-scale=1.0">
    <meta http-equiv="X-UA-Compatible" content="ie=edge">
    <meta http-equiv="refresh" content="20">
    <title>RealTime Chat App | OlaOwo</title>
    <link rel ="stylesheet" href ="StyleSheet1.css">
    <link rel="stylesheet" href ="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.2/css/all.min.css"/>
</head>
<body height="200">
    <div class="wrapper">
        <section class="chat-area">
            <header>
                <a href="#" runat ="server" id ="aback" class="back-icon"><i class ="fas fa-arrow-left"></i></a>
               <img src="image/flower3_0084.jpg" alt="" id ="imgid" runat="server">
               <div class="details">
                <span id ="spaname" runat="server">OlaOwo</span>
                <p id="pactive" runat="server">Active Now</p>
               </div>
            </header>
           <div class="chat-box" runat="server" id ="chatdiv">
            <div class="chat outgoing">
                <div class="details">
                    <p>Look at that booty</p>
                </div>
            </div>
           
            <div class="chat outgoing">
                <div class="details">
                    <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. </p>
                </div>
            </div>
            <div class="chat incoming">
                <img src="" alt="">
                <div class="details">
                    <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. </p>
                </div>
            </div>
            <div class="chat outgoing">
                <div class="details">
                    <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. </p>
                </div>
            </div>
            <div class="chat incoming">
                <img src="" alt="">
                <div class="details">
                    <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. </p>
                </div>
            </div>
            <div class="chat outgoing">
                <div class="details">
                    <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. </p>
                </div>
            </div>
            <div class="chat incoming">
                <img src="" alt="">
                <div class="details">
                    <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. </p>
                </div>
            </div>
            <div class="chat outgoing">
                <div class="details">
                    <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. </p>
                </div>
            </div>
            <div class="chat incoming">
                <img src="" alt="">
                <div class="details">
                    <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. </p>
                </div>
            </div>

           </div>
           <form action="#" class="typing-area" runat ="server">
            <input type="text" placeholder="Type a message here..." id ="MesgId" runat="server">
            <asp:LinkButton ID="linkMsg" runat="server" OnClick="BtnMessage" Font-Size="Large"><i class="fab fa-telegram-plane" UseSubmitBehavior="False"></i></asp:LinkButton>
           </form>
        </section>
        
    </div>
</body>
</html>