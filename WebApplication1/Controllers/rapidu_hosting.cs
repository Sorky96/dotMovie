using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Services.Description;

namespace WebApplication1.Controllers
{

    public class Hostings
    {
        public Hostings()
            {
                LoginIntoSite();
                GetTransferLeft();
            }

        string formUrl = @"https://rapidu.net/ajax.php?a=getUserLogin"; // NOTE: This is the URL the form POSTs to, not the URL of the form (you can find this in the "action" attribute of the HTML's form tag
        static string login = "mojerapidu520";
        static string password = "md7ggll2";

        //Params that browser send to the server. Use ex. HTTP Header Live to get that Params
        string formParams = string.Format("login="+login+"&pass="+password+"&remember=1&_go=");

        private string cookieHeader;
        public string GeneratedLink;
        public string getUrl;
        public string fileName;
        public string fileSize;
        public string premiumEnd;
        public string premiumLeft;
        



        public void LoginIntoSite()
        {
            WebRequest req = WebRequest.Create(formUrl);
            req.ContentType = "application/x-www-form-urlencoded";
            req.Method = "POST";
            byte[] bytes = Encoding.ASCII.GetBytes(formParams);
            req.ContentLength = bytes.Length;

            using (Stream os = req.GetRequestStream())
            {
                os.Write(bytes, 0, bytes.Length);
            }
            WebResponse resp = req.GetResponse();
            cookieHeader = resp.Headers["set-cookie"];
            

            GetTransferLeft();


        }

      

        public void GetDownloadLink(string getUrl)
        {

            var request = (HttpWebRequest)WebRequest.Create(@"http://rapidu.net/api/getFileDownload/");
            var url = string.Join("", getUrl.ToCharArray().Where(Char.IsDigit));

            var postData = "login=" + login + "&password=" + password+"&id="+url;
            var data = Encoding.ASCII.GetBytes(postData);

            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = data.Length;

            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }
            var response = (HttpWebResponse)request.GetResponse();

            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

            dynamic array = JsonConvert.DeserializeObject(responseString);
            GeneratedLink = Convert.ToString(array.fileLocation);

            GetFileInfo(url);

        }

        private void GetFileInfo(string url = "id=9125399406")
        {
            var request = (HttpWebRequest)WebRequest.Create(@"http://rapidu.net/api/getFileDetails/");

            var postData = "id=" + url;
            var data = Encoding.ASCII.GetBytes(postData);

            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = data.Length;

            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }
            var response = (HttpWebResponse)request.GetResponse();

            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
            dynamic array = JsonConvert.DeserializeObject(responseString);
            foreach (var item in array)
            {

                foreach (var key in item)
                {
                    fileName = key.fileName;
                    fileSize = FormatBytes(Convert.ToInt64(key.fileSize));
                }
            }
               
        }



        private static string FormatBytes(long bytes)
        {
            string[] Suffix = { "B", "KB", "MB", "GB", "TB" };
            int i;
            double dblSByte = bytes;
            for (i = 0; i < Suffix.Length && bytes >= 1024; i++, bytes /= 1024)
            {
                dblSByte = bytes / 1024.0;
            }

            return String.Format("{0:0.##} {1}", dblSByte, Suffix[i]);
        }


        public void GetTransferLeft()
        {
            var request = (HttpWebRequest)WebRequest.Create(@"http://rapidu.net/api/getAccountDetails/");

            var postData = "login="+login+"&password="+password;
            var data = Encoding.ASCII.GetBytes(postData);

            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = data.Length;

            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }
            var response = (HttpWebResponse)request.GetResponse();

            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

            dynamic array = JsonConvert.DeserializeObject(responseString);

            premiumEnd = Convert.ToString(array.userPremiumDateEnd);
            premiumLeft = FormatBytes(Convert.ToInt64(array.userTrafficDay));
            


        }

        
    }

 

}
