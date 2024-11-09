using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CSE445_Assignments_4_5_Customer_Service_Portal
{
    public partial class CaptchaImage : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CaptchaImg.ImageUrl = "~/ImageProcess.aspx";
            CaptchaService.ServiceClient fromService = new CaptchaService.ServiceClient();
            fromService.GetVerifierString("5");
        }

        //Component 1 Captcha Image
        protected void btnShowImage_Click(object sender, EventArgs e)
        {
            CaptchaService.ServiceClient fromService = new CaptchaService.ServiceClient();
            string userLen = "5";
            Session["userLength"] = userLen;
            string myStr = fromService.GetVerifierString(userLen);
            Session["generatedString"] = myStr;
            btnShowImage.Text = "New Captcha";
            CaptchaImg.Visible = true;
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (Session["generatedString"].Equals(CaptchaEnterTxtBx.Text))
            {
                VerificationLbl.Text = "You Entered the Captcha Correctly";
            }
            else
            {
                VerificationLbl.Text = "I am sorry, the string you entered does not match the image. Please try again!";
            }
        }
    }
}