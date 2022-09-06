using ChatApplication.OScript;
using HtmlTags;
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
    /// the user page shows the user logged in
    /// available user they can chat with
    /// </summary>
    public partial class users : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SetStats();
            CheckJava();
            //SearchUp.Attributes.Add("onblur", "SearchOla");
            Searcho.Attributes.Add("placeholder", "Enter Name to Search");
        }


        /// <summary>
        /// used in searching between users for a target
        /// matches name with last name or firstname
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void SearchOla(object sender, EventArgs e)
        {

            //  UserClass.SearchName = Searcho.Text;
            CheckJava(Searcho.Text);
        }


        /// <summary>
        /// get the login user stats from previous
        /// page and assigneds to the current html element
        /// </summary>
        void SetStats()
        {
            var uid = Request.QueryString["uid"]; //get value that is passed down

            if (uid == null || string.IsNullOrEmpty(uid)) return;

            var sid = Request.QueryString["Said"]; //get value that is passed down
            var iid= Request.QueryString["Imgid"]; //get value that is passed down
            var lid = Request.QueryString["lnid"]; //get value that is passed down
            var fid = Request.QueryString["fnid"]; //get value that is passed down

            PStats.InnerText = sid; //set status
            SpnName.InnerText = $" {fid} {lid}";
            ProfilePic.Src = $"image/{iid}";

            UserId = int.Parse(uid);
            alogout.HRef = $"Login.aspx?uid={UserId }";
            
        }


        String ConString = "";

        int UserId = 0;
      

        /// <summary>
        /// load the div with all users in the database
        /// </summary>
        /// <param name="Sp"></param>
        void CheckJava(string Sp = "")
        {


            ConString = Properties.Settings.Default.SDB;
            SqlConnection conet = new SqlConnection(ConString);

            conet.Open();// Open connection

            string QueryCommand = "";
            SqlCommand GetTable = new SqlCommand();

            bool SearchPosiive = false;

            if (string.IsNullOrEmpty(Sp))
            {
                QueryCommand = $"SELECT * from ChatTable";
                GetTable = new SqlCommand(QueryCommand, conet);
                SearchPosiive = true;
            }
            else
            {



                string QueryCheck = $"SELECT COUNT(*) from ChatTable where FirstName like @Fn or LastName like @Fn";

                //now check if email exists
                using (SqlCommand sqlCommand = new SqlCommand(QueryCheck, conet))
                {
                    //Conet.Open();
                    sqlCommand.Parameters.AddWithValue("@Fn", Sp);
                    //sqlCommand.Parameters.AddWithValue("@password", passWord);

                    if (sqlCommand != null)
                    {

                        int userCount = (int)sqlCommand.ExecuteScalar();
                        if (userCount > 0)
                        {
                            SearchPosiive = true;
                            //search has found a match with the name


                        }
                    }

                }



                if (SearchPosiive)
                {

                    QueryCommand = $"SELECT * from ChatTable where FirstName like @Fn or LastName like @Fn";
                    GetTable = new SqlCommand(QueryCommand, conet);
                    GetTable.Parameters.AddWithValue("@Fn", Sp);
                }
            }


            Divlist.Controls.Clear(); //clear divlist

            if (SearchPosiive)
            {


                SqlDataAdapter adapter = new SqlDataAdapter(GetTable);

                DataSet dataSet = new DataSet();
                //-----------Create A column--------------//
                adapter.Fill(dataSet);

                DataRowCollection rows = dataSet.Tables[0].Rows;

                string FN = ""; // the first name of the other user
                string LN = ""; // the last name of the ohter user
                string IN = ""; // the image path of the other
                string SA = ""; // th online status of other user
                string LMsg = ""; // last message sent by other user
                int Uniq = -1;  //unique id of other


                for (int lp = 0; lp < rows.Count; lp++)
                {

                    FN = (string)rows[lp]["FirstName"];
                    LN = (string)rows[lp]["LastName"];
                    IN = (string)rows[lp]["Img"];

                    if(rows[lp]["Status_Active"] != null)
                    SA = (string)rows[lp]["Status_Active"];

                    Uniq = (int)rows[lp]["UniqueID"];

                 

                    if (UserId == Uniq)
                    {
                        continue;
                        //to prevent matching with the user
                    }
                    else
                    {
                        if(UserId > 0)
                        LMsg= GetLastMessage(Uniq,conet);

                        AssignNewA(FN, LN, IN, SA,LMsg,Uniq);
                    }

                }


               



            }
            else
            {
                HtmlGenericControl SpanO = new HtmlGenericControl("span");
                SpanO.InnerText = $"No User  was found in the search {Sp}";
                Divlist.Controls.Add(SpanO);

            }

            conet.Close();
            /*

            var Pl = new HtmlTag("p").Text("This Is a code test");
            var spanO = new HtmlTag("span").Text("Coding Nepal");
            var div2 = new HtmlTag("div").AddClass("details");
            var img1 = new HtmlTag("img").Attr("src","#");
            var div1 = new HtmlTag("div").AddClass("content");
            var Alist = new HtmlTag("a").Attr("href", "#");
            */
        }

        string GetLastMessage(int Uid, SqlConnection Conner)
        {
            string result = "";


            //need to get the last message sent by the users we have
            string QueryMess = $"SELECT TOP 2 * from Messagos where Incoming_Msg like @Inc and Outgoing_Msg like @Oug"
            + " or Outgoing_Msg like @Oug and Incoming_Msg like @Inc order by Msg_Id desc";

            SqlCommand GetTable = new SqlCommand(QueryMess, Conner);
             GetTable.Parameters.AddWithValue("@Inc", UserId); //get table
             GetTable.Parameters.AddWithValue("@Oug", Uid);

            SqlDataAdapter adapter = new SqlDataAdapter(GetTable);

            DataSet dataSet = new DataSet();


            //-----------Create A column--------------//
            adapter.Fill(dataSet);
            if(dataSet.Tables[0].Rows.Count<1)
            {
                return result;
            }
            var rowol = dataSet.Tables[0].Rows[0];

            if(rowol["Mesg"] != null)
            result = (string)rowol["Mesg"];

            if(!string.IsNullOrEmpty(result))
            {
                //too trim string
              result =  result.Length <= 16 ? result:result.Substring(0,16);
            }

         

            return result;
        }


        /// <summary>
        /// will assign the div showing users info
        /// and setting the href link
        /// </summary>
        /// <param name="Fn"></param>
        /// <param name="Ln"></param>
        /// <param name="In"></param>
        /// <param name="Sa"></param>
        /// <param name="LastMsg"></param>
        /// <param name="UniId"></param>
        void AssignNewA(string Fn, string Ln, string In, string Sa, string LastMsg, int UniId)
        {
            HtmlGenericControl ParaGraph = new HtmlGenericControl("p");
            HtmlGenericControl SpanO = new HtmlGenericControl("span");
            HtmlGenericControl divO2 = new HtmlGenericControl("div");
            HtmlGenericControl ImgO = new HtmlGenericControl("img");
            HtmlGenericControl divo0 = new HtmlGenericControl("div");
            HtmlGenericControl Ao = new HtmlGenericControl("a");
            HtmlGenericControl divo3 = new HtmlGenericControl("div");
            HtmlGenericControl Ihope = new HtmlGenericControl("i");

            ParaGraph.InnerText = $"{LastMsg}";
            SpanO.InnerText = $"{Fn} {Ln}"; // assign the name
            divO2.Attributes.Add("class", "details");
            divO2.Controls.AddAt(0, SpanO); divO2.Controls.AddAt(1, ParaGraph);

            
              

           
            divo3.Attributes.Add("class", "status-dot");

            if (Sa.Contains("Offline Now"))
            {
                //  divo3.Attributes.Add("class", "offline");

                divo3.Attributes.Add("class", "status-dot offline");
            }
            Ihope.Attributes.Add("class", "fas fa-circle");


            divo3.Controls.Add(Ihope);

            string ImgSrc = $"image/{In}";
            ImgO.Attributes.Add("src", ImgSrc);
            divo0.Attributes.Add("class", "content");
            string PassValues = $"uid={UniId}&myuid={UserId}";
            string LinkiVal = "chat.aspx?"+PassValues;
            Ao.Attributes.Add("href", LinkiVal);
            divo0.Controls.AddAt(0, ImgO); divo0.Controls.AddAt(1, divO2);



            Ao.Controls.AddAt(0,divo0);
            Ao.Controls.AddAt(1, divo3);

            Divlist.Controls.Add(Ao);
           
        }
    }
}