using ChatApplication.OScript;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ChatApplication
{
    /// <summary>
    /// the login page 
    /// </summary>
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            var id = Request.QueryString["uid"]; //get value that is passed down

            if (id != null && !string.IsNullOrEmpty(id))
            {
                UserID = int.Parse(id);
                LogOut();
            }
        }

        /// <summary>
        /// will be used in highlighting the error text
        /// it hidden by the stylesheet
        /// so will edit it attributes
        /// </summary>
        /// <param name="msg"></param>
        public void DebugLog(String msg)
        {

            errortxt.Visible = true;
            errortxt.InnerText = msg;
            //errortxt.Style.Value = "block";
            errortxt.Style.Add("display", "block");
           // System.Diagnostics.Debug.WriteLine(msg);
            IsBusy = false;
        }

        bool IsBusy = false;
        protected void SMButton_Click(object sender, EventArgs e)
        {

            if (IsBusy) return;

            LoginCall();
        }

        String ConString = "";

        int UserID = 0;

        /// <summary>
        /// to logut go to the registration page
        /// and shange the status active
        /// </summary>
        void LogOut()
        {
            if (UserID<= 0)
                return;

            IsBusy = true;

            int GetId = UserID;

            ConString = Properties.Settings.Default.SDB;
            SqlConnection conet = new SqlConnection(ConString);

            conet.Open();// Open connection

            //Update database where active Nigga
            string QueryNo = $"Update ChatTable set Status_Active = @Act Where UniqueID = @Uni";

            SqlCommand Comd = new SqlCommand(QueryNo, conet);
            Comd.Parameters.Add(new SqlParameter("@Uni", GetId));
            Comd.Parameters.Add(new SqlParameter("@Act", "Offline Now"));
            Comd.ExecuteNonQuery();

            conet.Close();

            IsBusy = false;
           

        }

        /// <summary>
        /// to login
        /// and naviagte to the user page
        /// </summary>
        void LoginCall()
        {

            IsBusy = true;
            if(string.IsNullOrEmpty(Emal.Value))
            {

                DebugLog("Please enter email");
                return;
            }


            if(string.IsNullOrEmpty(PassW.Value))
            {
                DebugLog("Please Enter Password");
                return;
            }


            string EmailN = Emal.Value; //get the email
            string PassN = PassW.Value; //get the password

            bool EmailExist = false;
            bool Credent = false;
            // Get Connection string
            ConString = Properties.Settings.Default.SDB;
            SqlConnection conet = new SqlConnection(ConString);

            conet.Open();// Open connection

            string QueryCommand = $"SELECT COUNT(*) from ChatTable Where Email = @Ema";

            //now check if email exists
            using (SqlCommand sqlCommand = new SqlCommand(QueryCommand, conet))
            {
                //Conet.Open();
                sqlCommand.Parameters.AddWithValue("@Ema", EmailN);
                //sqlCommand.Parameters.AddWithValue("@password", passWord);

                if (sqlCommand != null)
                {

                    int userCount = (int)sqlCommand.ExecuteScalar();
                    if (userCount > 0)
                    {
                        EmailExist = true;



                    }
                }

            }//

            if(!EmailExist)
            {

                DebugLog($"{EmailN} does not exist");
                conet.Close();
            }

            else
            {

                QueryCommand = $"SELECT COUNT(*) from ChatTable Where Email like @Ema AND  Passw like @Psw";

                

                //now check if email exists
                using (SqlCommand sqlCommand = new SqlCommand(QueryCommand, conet))
                {
                    //Conet.Open();
                    sqlCommand.Parameters.AddWithValue("@Ema", EmailN);
                    sqlCommand.Parameters.AddWithValue("@Psw",PassN);
                    //sqlCommand.Parameters.AddWithValue("@password", passWord);

                    if (sqlCommand != null)
                    {

                        int userCount = (int)sqlCommand.ExecuteScalar();
                        if (userCount > 0)
                        {
                            Credent = true;



                        }
                        
                        if(userCount >1)
                        {
                            Credent = false;
                            DebugLog($" {EmailN} Multiple Users with same credential Sorry");
                            conet.Close();
                            return;
                        }

                    }

                }//


                if(!Credent)
                {
                    //
                    DebugLog("The credentials entered are not correct");
                    conet.Close();
                }
                else
                {

                    //now get table information
                    string QueryName = $"Select UniqueID, FirstName, LastName, Img FROM ChatTable Where Email = @Ema";

                    SqlCommand GetTable = new SqlCommand(QueryName, conet);
                    GetTable.Parameters.AddWithValue("@Ema", EmailN);

                    SqlDataAdapter adapter = new SqlDataAdapter(GetTable);

                    DataSet dataSet = new DataSet();
                    //-----------Create A column--------------//
                    adapter.Fill(dataSet);

                    DataRow rows = dataSet.Tables[0].Rows[0]; //get the only row

                    UserID = (int)rows["UniqueId"]; //get UniquedId;
                  string Fname = (string)rows["FirstName"]; //get FirstName;
                    string LName = (string)rows["LastName"]; //get LastName;
                    string StatusA = "Active Now"; //set wehere active
                    string ImgFile = (string)rows["Img"];// the img fileanme

                    //Update database where active Nigga
                    string QueryNo = $"Update ChatTable set Status_Active = @Act Where Email = @Ema";

                    SqlCommand Comd = new SqlCommand(QueryNo, conet);
                    Comd.Parameters.Add(new SqlParameter("@Ema", EmailN));
                    Comd.Parameters.Add(new SqlParameter("@Act", "Active Now"));
                    Comd.ExecuteNonQuery();

                    conet.Close();

                    ///to naviaget to the user page and also pass the values
                    string LinkVal = $"uid={UserID}&fnid={Fname}&lnid={LName}&Said={StatusA}&Imgid={ImgFile}";
                    IsBusy = false;
                    Response.Redirect($"users.aspx?{LinkVal}");
                }

            }



        }
    }
}