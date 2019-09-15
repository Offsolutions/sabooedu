using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Web;

namespace sabooedu.Models
{
    public class Helper
    {

        dbcontext db = new dbcontext();
        public string _FileName;
        const string passphrase = "password";
        public string uploadfile(HttpPostedFileBase file)
        {
            try
            {
                if (file.ContentLength > 0)
                {
                    _FileName = Path.GetFileName(file.FileName);

                    _FileName = Guid.NewGuid().ToString().Substring(0, 4) + _FileName;
                    string _path = (HttpContext.Current.Server.MapPath("/Uploadimage/" + _FileName));

                    file.SaveAs(_path);
                }

                return _FileName;
            }
            catch
            {
                return "False";
            }
        }
        public string EncryptData(string Message)
        {
            byte[] Results;
            System.Text.UTF8Encoding UTF8 = new System.Text.UTF8Encoding();
            MD5CryptoServiceProvider HashProvider = new MD5CryptoServiceProvider();
            byte[] TDESKey = HashProvider.ComputeHash(UTF8.GetBytes(passphrase));
            TripleDESCryptoServiceProvider TDESAlgorithm = new TripleDESCryptoServiceProvider();
            TDESAlgorithm.Key = TDESKey;
            TDESAlgorithm.Mode = CipherMode.ECB;
            TDESAlgorithm.Padding = PaddingMode.PKCS7;
            byte[] DataToEncrypt = UTF8.GetBytes(Message);
            try
            {
                ICryptoTransform Encryptor = TDESAlgorithm.CreateEncryptor();
                Results = Encryptor.TransformFinalBlock(DataToEncrypt, 0, DataToEncrypt.Length);
            }
            finally
            {
                TDESAlgorithm.Clear();

                HashProvider.Clear();
            }
            return Convert.ToBase64String(Results);
        }
        public string DecryptData(string Message)
        {
            byte[] Results;
            System.Text.UTF8Encoding UTF8 = new System.Text.UTF8Encoding();
            MD5CryptoServiceProvider HashProvider = new MD5CryptoServiceProvider();
            byte[] TDESKey = HashProvider.ComputeHash(UTF8.GetBytes(passphrase));
            TripleDESCryptoServiceProvider TDESAlgorithm = new TripleDESCryptoServiceProvider();
            TDESAlgorithm.Key = TDESKey;
            TDESAlgorithm.Mode = CipherMode.ECB;
            TDESAlgorithm.Padding = PaddingMode.PKCS7;
            byte[] DataToDecrypt = Convert.FromBase64String(Message);
            try
            {
                ICryptoTransform Decryptor = TDESAlgorithm.CreateDecryptor();
                Results = Decryptor.TransformFinalBlock(DataToDecrypt, 0, DataToDecrypt.Length);
            }
            finally
            {
                TDESAlgorithm.Clear();
                HashProvider.Clear();
            }
            return UTF8.GetString(Results);
        }
        public string GenerateRandomOTP(int iOTPLength, string[] saAllowedCharacters)
        {

            string sOTP = String.Empty;

            string sTempChars = String.Empty;

            Random rand = new Random();

            for (int i = 0; i < iOTPLength; i++)

            {

                int p = rand.Next(0, saAllowedCharacters.Length);

                sTempChars = saAllowedCharacters[rand.Next(0, saAllowedCharacters.Length)];

                sOTP += sTempChars;

            }

            return sOTP;

        }
        public void SendHtmlFormattedEmail(string recepientEmail, string subject, string body)
        {
            dbcontext db = new dbcontext();
            // var office = db.OfficeDetails.ToList();

            using (MailMessage mailMessage = new MailMessage("offsolut@gmail.com", recepientEmail))
            {

                mailMessage.Subject = subject;
                mailMessage.Body = body;
                mailMessage.IsBodyHtml = true;

                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = true;
                NetworkCredential NetworkCred = new NetworkCredential("offsolut@gmail.com", "Official@123");
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = NetworkCred;
                smtp.Port = 587;
                smtp.Send(mailMessage);
            }
        }
        public string SendSMS(string User, string sender, string to, string message, string type, string api)
        {
            string stringpost = "username=" + User + "&message=" + message + "&sendername=" + sender + "&smstype=" + type + "&numbers=" + to + "&apikey=" + api + "";
            //Response.Write(stringpost)
            //string functionReturnValue = null;
            //functionReturnValue = "";

            HttpWebRequest objWebRequest = null;
            HttpWebResponse objWebResponse = null;
            StreamWriter objStreamWriter = null;
            StreamReader objStreamReader = null;

            try
            {
                string stringResult = null;

                objWebRequest = (HttpWebRequest)WebRequest.Create("http://sms.officialsms.in/sendSMS");
                //domain name: Domain name Replace With Your Domain  
                objWebRequest.Method = "Post";

                // Response.Write(objWebRequest)

                // Use below code if you want to SETUP PROXY.
                //Parameters to pass: 1. ProxyAddress 2. Port
                //You can find both the parameters in Connection settings of your internet explorer.


                // If You are In the proxy Then You Uncomment the below lines and Enter IP And Port Number


                //System.Net.WebProxy myProxy = new System.Net.WebProxy("192.168.1.108", 6666);
                //myProxy.BypassProxyOnLocal = true;
                //objWebRequest.Proxy = myProxy;

                objWebRequest.ContentType = "application/x-www-form-urlencoded";

                objStreamWriter = new StreamWriter(objWebRequest.GetRequestStream());
                objStreamWriter.Write(stringpost);
                objStreamWriter.Flush();
                objStreamWriter.Close();

                objWebResponse = (HttpWebResponse)objWebRequest.GetResponse();


                objWebResponse = (HttpWebResponse)objWebRequest.GetResponse();

                objStreamReader = new StreamReader(objWebResponse.GetResponseStream());
                stringResult = objStreamReader.ReadToEnd();
                objStreamReader.Close();
                return (stringResult);
            }
            catch (Exception ex)
            {
                return (ex.ToString());

            }
            finally
            {
                if ((objStreamWriter != null))
                {
                    objStreamWriter.Close();
                }
                if ((objStreamReader != null))
                {
                    objStreamReader.Close();
                }
                objWebRequest = null;
                objWebResponse = null;

            }
        }

        //public string smssetting(string mobile, string Message)
        //{
        //    var setting = db.EmailSettings.ToList();
        //    if (setting != null)
        //    {

        //        SendSMS(setting[0].SmsUser, setting[0].SenderId, mobile, Message, "Trans", setting[0].Api);

        //        return ("Done");
        //    }
        //    else
        //    {
        //        return ("SMS Not Activated");
        //    }
        //}
        public string PopulateBody(string userName, string title, string url, string description)
        {
            string body = string.Empty;
            using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/App_Data/Template/ConfirmEmail.txt")))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{UserName}", userName);
            body = body.Replace("{Title}", title);
            body = body.Replace("{Url}", url);
            body = body.Replace("{Description}", description);
            return body;
        }
        //public void SendHtmlFormattedEmail(string recepientEmail, string subject, string body)
        //{
        //    dbcontext db = new dbcontext();
        //    var office = db.EmailSettings.ToList();

        //    using (MailMessage mailMessage = new MailMessage(recepientEmail, office[0].Email))
        //    {

        //        mailMessage.Subject = subject;
        //        mailMessage.Body = body;
        //        mailMessage.IsBodyHtml = true;

        //        SmtpClient smtp = new SmtpClient();
        //        smtp.Host = "smtp.gmail.com";
        //        smtp.EnableSsl = true;
        //        NetworkCredential NetworkCred = new NetworkCredential(office[0].Email, office[0].password);
        //        smtp.UseDefaultCredentials = true;
        //        smtp.Credentials = NetworkCred;
        //        smtp.Port = 587;
        //        smtp.Send(mailMessage);
        //    }
        //}
        //public void SendEmailAttachment(string recepientEmail, string subject, string body, HttpPostedFileBase file)
        //{
        //    dbcontext db = new dbcontext();
        //    var office = db.EmailSettings.ToList();

        //    using (MailMessage mailMessage = new MailMessage(recepientEmail, office[0].Email))
        //    {

        //        mailMessage.Subject = subject;
        //        mailMessage.Body = body;
        //        mailMessage.IsBodyHtml = true;
        //        if (file.ContentLength > 0)
        //        {
        //            string FileName = Path.GetFileName(file.FileName);
        //            mailMessage.Attachments.Add(new Attachment(file.InputStream, FileName));
        //        }

        //        SmtpClient smtp = new SmtpClient();
        //        smtp.Host = "smtp.gmail.com";
        //        smtp.EnableSsl = true;
        //        NetworkCredential NetworkCred = new NetworkCredential(office[0].Email, office[0].password);
        //        smtp.UseDefaultCredentials = true;
        //        smtp.Credentials = NetworkCred;
        //        smtp.Port = 587;
        //        smtp.Send(mailMessage);
        //    }


        //}
        public class simpletest
        {

        }
        //    public async Task sendemail2(string Toemail, string filePath)
        //    {
        //        try
        //        {
        //            await Task.Run(() =>
        //            {
        //                MailMessage mail = new MailMessage();
        //                SmtpClient SmtpServer = new SmtpClient("smtp.sendgrid.net");
        //                mail.From = new MailAddress("gagansngh966@gmail.com");
        //                mail.To.Add(Toemail);
        //                mail.Subject = "Test Mail - 1";
        //                mail.Body = "mail with attachment";
        //                System.Net.Mail.Attachment attachment;
        //                //attachment = new System.Net.Mail.Attachment(filePath);
        //                ///  mail.Attachments.Add(attachment);
        //                SmtpServer.Port = 25;
        //                SmtpServer.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["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"], ConfigurationManager.AppSettings["Official@123"]);
        //                SmtpServer.EnableSsl = true;
        //                SmtpServer.Send(mail);
        //            });
        //        }
        //        catch (Exception ex)
        //        {
        //            throw ex;
        //        }
        //    }
        //}
        #region Resize Image
        private static Bitmap ResizeImage(System.Drawing.Image imgPhoto, int Height, int Width)
        {
            int sourceWidth = imgPhoto.Width;
            int sourceHeight = imgPhoto.Height;
            int sourceX = 0;
            int sourceY = 0;
            int destX = 0;
            int destY = 0;

            float nPercent = 0;
            float nPercentW = 0;
            float nPercentH = 0;

            nPercentW = ((float)Width / (float)sourceWidth);
            nPercentH = ((float)Height / (float)sourceHeight);
            if (nPercentH < nPercentW)
            {
                nPercent = nPercentH;
                destX = System.Convert.ToInt16((Width - (sourceWidth * nPercent)) / 2);
            }
            else
            {
                nPercent = nPercentW;
                destY = System.Convert.ToInt16((Height - (sourceHeight * nPercent)) / 2);
            }

            int destWidth = (int)Math.Ceiling(sourceWidth * nPercent);
            int destHeight = (int)Math.Ceiling(sourceHeight * nPercent);

            Bitmap bmPhoto = new Bitmap(Width, Height, imgPhoto.PixelFormat);

            bmPhoto.SetResolution(imgPhoto.HorizontalResolution, imgPhoto.VerticalResolution);

            Graphics grPhoto = Graphics.FromImage(bmPhoto);
            grPhoto.Clear(Color.White);

            grPhoto.CompositingQuality = CompositingQuality.HighQuality;
            grPhoto.SmoothingMode = SmoothingMode.HighQuality;
            grPhoto.InterpolationMode = InterpolationMode.HighQualityBicubic;
            Rectangle rect = new Rectangle(0, 0, Width, Height);
            grPhoto.DrawImage(imgPhoto, rect, new Rectangle(sourceX, sourceY, sourceWidth, sourceHeight), GraphicsUnit.Pixel);
            grPhoto.Dispose();
            return bmPhoto;
        }
        #endregion
        public string Resize(HttpPostedFileBase file, int height, int width)
        {
            try
            {
                if (file != null)
                {
                    _FileName = Path.GetFileName(file.FileName);

                    _FileName = Guid.NewGuid().ToString().Substring(0, 4) + _FileName;
                    string _path = (HttpContext.Current.Server.MapPath("/Uploadimage/" + _FileName));

                    Stream Buffer2 = file.InputStream;
                    System.Drawing.Image Image2 = System.Drawing.Image.FromStream(Buffer2);
                    Bitmap bmp2 = ResizeImage(Image2, height, width);
                    bmp2.Save(_path, System.Drawing.Imaging.ImageFormat.Jpeg);

                    return _FileName;
                }
                else
                {
                    return "False";
                }


            }
            catch
            {
                return "False";
            }
        }
        public enum NotificationEnumeration
        {
            Success,
            Error,
            Warning
        }
    }
}