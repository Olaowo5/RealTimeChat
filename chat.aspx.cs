using ChatApplication.OScript;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ChatApplication
{
    /// <summary>
    /// the Chat page showing chats between users
    /// </summary>
    public partial class chat : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //get the value that was passed
            //the users UniqueID
            var ui = Request.QueryString["myuid"];

            if (ui != null && !string.IsNullOrEmpty(ui))
            {
                UserId = int.Parse(ui);


              
            }
            else
                aback.HRef = "users.aspx";
            LoadData();
        }

        
        String ConString = "";
        string Msgd = "";
        int GuestId = 0;
        int UserId = 0;
        string Messago = "";
        string GuestImg = "";

        int MeptyId = -1;


        protected void BtnMessage (Object sender,
                           EventArgs e)
        {
            UploadMessage();
        }

        /// <summary>
        /// to load the dat of the two users
        /// if values are passed correctly
        /// </summary>
        void LoadData()
        {
            if (UserId < 1) return;

            


            var id = Request.QueryString["uid"]; //get value that is passed down


            ConString = Properties.Settings.Default.SDB;
            SqlConnection conet = new SqlConnection(ConString);

            conet.Open();// Open connection
            AlertHref(conet);

            int Vid = int.Parse(id);

            string QueryCommand = $"SELECT * from ChatTable Where UniqueID = @Uniq";

            SqlCommand GetTable = new SqlCommand(QueryCommand, conet);
            GetTable.Parameters.AddWithValue("@Uniq", Vid);

            SqlDataAdapter adapter = new SqlDataAdapter(GetTable);

            DataSet dataSet = new DataSet();
            //-----------Create A column--------------//
            adapter.Fill(dataSet);

            DataRow rows = dataSet.Tables[0].Rows[0]; //get the only row

            conet.Close();
           // UserClass.UniqueId = (int)rows["UniqueId"]; //get UniquedId;
            string Fnam = (string)rows["FirstName"]; //get FirstName;
            string Lnam = (string)rows["LastName"]; //get LastName;
            string Actii = "";
            if(rows["Status_Active"] != null)
            Actii = (string)rows["Status_Active"];  //set wehere active

            string ImgN = (string)rows["Img"];// the img fileanme

            imgid.Src = "image/"+ImgN;
            GuestImg = imgid.Src;
            spaname.InnerText = $"{Fnam} {Lnam}";
            pactive.InnerText = $"{Actii}";

            GuestId = Vid;

            //currently not using
             Msgd = $"{UserId}0" + GuestId.ToString(); //add to string


           // string LinkVal = $"uid={Vid}&fnid={Fnam}&lnid={Lnam}&Said={Actii}&Imgid={ImgN}";
           

            LoadMessages();
        }


        /// <summary>
        /// will be used edit the back button
        /// need to return to the user page 
        /// and make sure the values pass are accurate
        /// so will get the users value from database
        /// </summary>
        /// <param name="conet"></param>
        void AlertHref(SqlConnection conet)
        {
            //now get table information
            string QueryName = $"Select * FROM ChatTable Where UniqueID = @Uni";

           

            SqlCommand GetTable = new SqlCommand(QueryName, conet);
            GetTable.Parameters.AddWithValue("@Uni", UserId);

            SqlDataAdapter adapter = new SqlDataAdapter(GetTable);

            DataSet dataSet = new DataSet();
            //-----------Create A column--------------//
            adapter.Fill(dataSet);

            DataRow rows = dataSet.Tables[0].Rows[0]; //get the only row

             int UserID = (int)rows["UniqueId"]; //get UniquedId;
            string Fname = (string)rows["FirstName"]; //get FirstName;
            string LName = (string)rows["LastName"]; //get LastName;
            string StatusA = "Active Now"; //set wehere active
            string ImgFile = (string)rows["Img"];// the img fileanme

            //Update database where active Nigga
           

          

            string LinkVal = $"uid={UserID}&fnid={Fname}&lnid={LName}&Said={StatusA}&Imgid={ImgFile}";

            //edit the Href link with possible values
            aback.HRef = $"users.aspx?{LinkVal}";
           
        }

        /// <summary>
        /// upload emssage to database
        /// so both users can view them
        /// </summary>
        void UploadMessage()
        {
            Messago = MesgId.Value; //send message

            if (string.IsNullOrEmpty(Messago)) return;
            

            ConString = Properties.Settings.Default.SDB;
            SqlConnection conet = new SqlConnection(ConString);

            conet.Open();// Open connection

            //will have to insert message
            string QueryAdd = "INSERT INTO Messagos (Incoming_Msg, Outgoing_Msg, Mesg) " +
                "VALUES ('" + GuestId + "','" + UserId + "','" + Messago + "')";

            //  SqlCommand Cmd;
            SqlDataAdapter Adapter = new SqlDataAdapter();

            // Cmd = new SqlCommand(QueryAdd, con);
            Adapter.InsertCommand = new SqlCommand(QueryAdd, conet);
            Adapter.InsertCommand.ExecuteNonQuery();

            
            conet.Close();

            MesgId.Value = ""; //empty the input field
            LoadMessages();
        }


        /// <summary>
        /// will load all messages between users
        /// recent messages will appear on top
        /// </summary>
        void LoadMessages()
        {
            ConString = Properties.Settings.Default.SDB;
            SqlConnection conet = new SqlConnection(ConString);

            conet.Open();// Open connection

            string QueryCommand = $"SELECT * from Messagos where Incoming_Msg like @Inc and Outgoing_Msg like @Oug"
                +" or Incoming_Msg like @Oug and Outgoing_Msg like @Inc order by Msg_Id desc" ;

            SqlCommand GetTable = new SqlCommand(QueryCommand, conet);
            GetTable.Parameters.AddWithValue("@Inc", GuestId); //get table
            GetTable.Parameters.AddWithValue("@Oug",UserId);

            SqlDataAdapter adapter = new SqlDataAdapter(GetTable);

            DataSet dataSet = new DataSet();
            //-----------Create A column--------------//
            adapter.Fill(dataSet);

            DataRowCollection rows = dataSet.Tables[0].Rows; //get the rows

            chatdiv.Controls.Clear(); //clear the div 

            for (int lp = 0; lp < rows.Count; lp++)
            {
                var rola = rows[lp];

                var Msg1 = rola["Incoming_Msg"]; // the id to make sure its guest
                var Msg2 = rola["Outgoing_Msg"]; //the id to amke its user

                bool IsInComing = false;
                bool IsOutgoing = false;

                if(Msg1 != null)
                {
                    //incoming message
                    int MsgInt = (int)Msg1;

                    if (MsgInt == GuestId)
                    {
                        IsOutgoing = true;
                        //the message is an outgoing
                        //message
                     
                    }
                    else if(MsgInt == UserId)
                    {
                        IsInComing = true;
                        //the message is an incoming message

                        //honestly not sure but its a 50/50 guess
                    }
                }
              /* if(Msg2 != null)
                {

                    int MsgInt = (int)Msg2;

                    if (MsgInt > 0)
                    {
                        IsOutgoing = true;
                      
                    }
                }
              */

                if(IsInComing)
                {
                    //if incoming assign the correct style format
                    //and get info from the other user
                    HtmlGenericControl divO1 = new HtmlGenericControl("div");
                    HtmlGenericControl divO2 = new HtmlGenericControl("div");
                    HtmlGenericControl para = new HtmlGenericControl("p");
                    HtmlGenericControl Imgo = new HtmlGenericControl("img");

                    para.InnerText = $"{rola["Mesg"]}"; //get the message
                    divO2.Attributes.Add("class", "details");
                    divO1.Attributes.Add("class", "chat incoming");
                    Imgo.Attributes.Add("src", GuestImg);

                    divO2.Controls.Add(para);

                    divO1.Controls.AddAt(0, Imgo);
                    divO1.Controls.AddAt(1, divO2);

                    chatdiv.Controls.Add(divO1);
                }

                else if(IsOutgoing)
                {
                    //outcomming Message
                    HtmlGenericControl divO1 = new HtmlGenericControl("div");
                    HtmlGenericControl divO2 = new HtmlGenericControl("div");
                    HtmlGenericControl para = new HtmlGenericControl("p");


                    para.InnerText = $"{rola["Mesg"]}"; //get the message
                    divO2.Attributes.Add("class", "details");
                    divO1.Attributes.Add("class", "chat outgoing");


                    divO2.Controls.Add(para);


                    divO1.Controls.AddAt(0, divO2);

                    chatdiv.Controls.Add(divO1);
                }

            }

            conet.Close(); //close connection
        }
    }
}