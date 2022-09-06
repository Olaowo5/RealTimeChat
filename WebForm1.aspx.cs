using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.ComponentModel.DataAnnotations;
using System.IO;
using ChatApplication.OScript;

namespace ChatApplication
{
    /// <summary>
    /// the reigistration page of the web pp
    /// </summary>
    public partial class WebForm1 : System.Web.UI.Page
    {
      

        protected void Page_Load(object sender, EventArgs e)
        {
            // iclick.Attributes.Add("onclick", "IClick()");
         
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
            //System.Diagnostics.Debug.WriteLine(msg);
            IsBusy = false;
        }

  
        String ConString = "";

        bool IsBusy = false;
        protected void SMButton_Click(object sender, EventArgs e)
        {
            if (IsBusy) return;

            SignToDB();
        }
        /// <summary>
        /// will reach out to the database
        /// </summary>
            void SignToDB()
        {
            IsBusy = true;

            //need to make sure the fields arent emppty
            if (string.IsNullOrEmpty(Fname.Value) || string.IsNullOrEmpty(Lname.Value) ||
                string.IsNullOrEmpty(Emal.Value) || string.IsNullOrEmpty(PassInput.Value))
            {
                DebugLog("All Input required");
                
            }
            else
            {
                string FirstN = Fname.Value; //first Name
                string LastN = Lname.Value; //lastName
                string PassW = PassInput.Value; //password
                string TheEmail = Emal.Value; //the email
                bool EmailExist = false;

                if (IsValidEmailAddress(TheEmail))
                {

                    // Get Connection string
                    ConString = Properties.Settings.Default.SDB;
                    SqlConnection conet = new SqlConnection(ConString);

                    conet.Open();// Open connection

                

                    string QueryCommand = $"SELECT COUNT(*) from ChatTable Where Email = @Ema";

                    //now check if email exists
                    using (SqlCommand sqlCommand = new SqlCommand(QueryCommand, conet))
                    {
                        //Conet.Open();
                        sqlCommand.Parameters.AddWithValue("@Ema", TheEmail);
                        //sqlCommand.Parameters.AddWithValue("@password", passWord);

                        if (sqlCommand != null)
                        {

                            int userCount = (int)sqlCommand.ExecuteScalar();
                            if (userCount > 0)
                            {
                                EmailExist = true;



                            }
                        }

                    }


                    if (EmailExist)
                    {
                        conet.Close();
                        DebugLog($" {TheEmail} Email ALready Exists");
                    }
                    else
                    {
                        //Now go here
                        //to check the file uploaded needs to be an image
                        if (!Imginput.HasFile)
                        {
                            //no file
                            //display a message 
                            //to please upload an image file
                            DebugLog("Please Upload an Image file with -jpg, -jpeg, -png");
                        }
                        else
                        {

                            string FileName = Imginput.PostedFile.FileName; //fileName

                            string GetFormat = Path.GetExtension(Imginput.PostedFile.FileName);



                            if (CheckExtension(GetFormat))
                            {
                                //passes
                                string TimeNow = GetTimestamp(DateTime.Now);
                                //get cuuretn time to edit the name of the file
                                //so we have a unique name everytime
                                //cause were gonna upload this to a private folder
                                //on the web app and save the url on the database
                                //compared to the url I used to do
                                //now i think this is much better


                                //string FilePPath = Server.MapPath("~/Files/") + TimeNow + " " + Path.GetFileName(FileName);
                                string NewFile = TimeNow + " " + Path.GetFileName(FileName);
                                string FilePPath = Server.MapPath("image/") + NewFile ;
                                Imginput.SaveAs(FilePPath);
                               
                                //move the file to the image stuff
                              

                                string Status = "Active Now"; //once user signed up their now active

                                Random RandomNumber = new Random();
                                int randy = RandomNumber.Next(1,1000000); //create a random number for the user

                                //now insert the user data in the database
                                //will have to insert
                                string QueryAdd = "INSERT INTO ChatTable (UniqueID, FirstName, LastName, Email, Passw, Img, Status_Active) " +
                                    "VALUES ('" + randy+ "','" + FirstN+ "','" + LastN + "','" + TheEmail+ "','" + PassW + "','" + NewFile + "','" + Status + "')";

                                //  SqlCommand Cmd;
                                SqlDataAdapter Adapter = new SqlDataAdapter();

                                // Cmd = new SqlCommand(QueryAdd, con);
                                Adapter.InsertCommand = new SqlCommand(QueryAdd, conet);
                                Adapter.InsertCommand.ExecuteNonQuery();

                                int UserID = randy; //the Unique ID were gonna pass
                                string ImgFile = NewFile; //file name
                                string Fname = FirstN;
                                string LName = LastN;
                                string StatusA = Status;

                                conet.Close();

                                IsBusy = false;
                               
                                //passing on the values to the next page, when navigating
                                string LinkVal = $"uid={UserID}&fnid={Fname}&lnid={LName}&Said={StatusA}&Imgid={ImgFile}";
                                IsBusy = false;
                                Response.Redirect($"users.aspx?{LinkVal}");
                                //Response.Redirect("users.aspx");
                            }
                            else
                            {
                                //fails
                                //display message
                                //please selcte an image file of jpeg jpg or png

                                conet.Close();
                                DebugLog("Please Upload an Image file with -jpg, -jpeg, -png");
                            }
                        }

                    }
                   
                }
                else
                {
                    //invalid email
                    DebugLog("Invalid Email");
                    
                }

            }

      }


        /// <summary>
        /// check if we have the correct extensions
        /// for Image file
        /// </summary>
        /// <param name="sp"></param>
        /// <returns></returns>
        public bool CheckExtension(string sp)
        {
            string[] extensionstrings = { ".jpg", ".jpeg", ".png", ".tiff" };


            foreach (string pc in extensionstrings)
            {
                if (sp.Contains(pc) || pc == sp)
                    return true;
            }


            return false;
        }

        //to check if the email enter is valid
        public bool IsValidEmailAddress(string email)
        {
            if (!string.IsNullOrEmpty(email) && new EmailAddressAttribute().IsValid(email))
                return true;
            else
                return false;
        }

        /// <summary>
        /// get the correct time
        /// will be used in renamimg the image file uploaded
        /// so site wont crash if we upload duplicates
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public  String GetTimestamp(DateTime value)
        {
            return value.ToString("yyyyMMddHHmmssffff");
        }
    }
}