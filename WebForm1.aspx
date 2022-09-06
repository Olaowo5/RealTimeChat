<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="ChatApplication.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>


    <meta charset="UTF-8"/>
    <meta name ="veiwport" content="width=device-width, intial-scale=1.0"/>
    <meta http-equiv="X-UA-Compatible" content="ie=edge"/>
    <title>RealTime Chat App | OlaOwo</title>
    <link rel ="stylesheet" href ="StyleSheet1.css"/>
    <link rel="stylesheet" href ="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.2/css/all.min.css"/>
</head>
<body>
    <div class="wrapper">
        <section class="form signup">
            <header> Realtime Chat App</header>
            <form action="#" runat="server">
                <div class="error-text" runat ="server" id ="errortxt">THis is an Error Message!</div>
                <div class="name-details">
                    <div class="field input">
                        <label>First Name</label>
                        <input type="text" placeholder="First Name" id="Fname" runat="server"/>
                    </div>

                    <div class="field input">
                        <label>Last Name</label>
                        <input type="text" placeholder="Last Name" id="Lname" runat="server"/>
                    </div>
                </div>
                    <div class="field input">
                        <label>Email</label>
                        <input type="text" placeholder="Enter Your Email" id="Emal" runat="server"/>
                    </div>

                    <div class="field input">
                        <label>Password</label>
                        <input type="password" placeholder="Enter New Password" id="PassInput" runat="server"/>
                        <i class="fas fa-eye" runat="server" id="iclick"> </i>
                    </div>

                    <div class="field image">
                        <label>Select Profile Picture</label>
                        
                     
                          <asp:FileUpload placeholder="Upload Profile Picture" ID="Imginput" runat="server"/>
                    </div>

                    <div class="field button">
                      
                         <asp:Button ID="SMButton" runat="server" Text="Continue to Chat"  OnClick="SMButton_Click" UseSubmitBehavior="False"  />
                    </div>
                
            </form>
            <div class="link">Already signed up? <a href="Login.aspx">Login Now</a></div>
        </section>
    </div>
 <script src="Javascript/pass-show-hide.js"></script>
</body>
</html>
