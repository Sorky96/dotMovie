using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Services.Description;
using WebApplication1.Controllers;

namespace WebApplication1.Controllers
{

    public class Hostings
    {
        public Hostings()
        {}
       
        public string GeneratedLink;
        public string getUrl;
        public string fileName;
        public string fileSize;
        public string premiumEnd;
        public string premiumLeft;




        public void AddAllAcounts()
        {
            //Accounts.AddAccountToList("mojerapidu520", "md7ggll2");
            //Accounts.AddAccountToList("businessrapidu80", "dt49nykr");
            //Accounts.AddAccountToList("orzech74", "orzech2749");            
        }



        public void GetDownloadLink(string getUrl)
        {
            int i = GetAccNumber();

            var request = (HttpWebRequest)WebRequest.Create(@"http://rapidu.net/api/getFileDownload/");
            var url = string.Join("", getUrl.ToCharArray().Where(Char.IsDigit));
            do
            {
                i = GetAccNumber();
            } while (!CheckIfUserHaveTransfer(i, url));

            var postData = "login=" + Accounts.logins.ElementAt(i) + "&password=" + Accounts.passwords.ElementAt(i) + "&id=" + url;
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

        private void GetFileInfo(string url = "id=0718263871")
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
                    fileSize = FormatBytes(Convert.ToUInt64(key.fileSize));
                }
            }

        }

        public void GetTransferLeft()
        {
            ulong valueOfTransfer = 0;
            int i = 0;
            premiumLeft = "";
            foreach (var item in Accounts.logins)
            {
                var request = (HttpWebRequest)WebRequest.Create(@"http://rapidu.net/api/getAccountDetails/");

                var postData = "login=" + item + "&password=" + Accounts.passwords.ElementAt(i);
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

                valueOfTransfer += Convert.ToUInt64(array.userTrafficDay);

                premiumEnd = Convert.ToString(array.userPremiumDateEnd);
               
                if (i++ == item.Count())
                {
                    break;
                }
                
                    
            }

           premiumLeft = FormatBytes(valueOfTransfer);            
        }

        private ulong GetFileWeight(string fileId)
        {
            var request = (HttpWebRequest)WebRequest.Create(@"http://rapidu.net/api/getFileDetails/");

            var postData = "id=" + fileId;
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
                    
                    return Convert.ToUInt64(key.fileSize);
                }
            }
            return 0;
        }
        private Boolean CheckIfUserHaveTransfer(int i, string fileId)
        {
            ulong valueOfTransfer = 0;
            var request = (HttpWebRequest)WebRequest.Create(@"http://rapidu.net/api/getAccountDetails/");

            var postData = "login=" + Accounts.logins.ElementAt(i) + "&password=" + Accounts.passwords.ElementAt(i);
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

            valueOfTransfer = Convert.ToUInt64(array.userTrafficDay);
            if (valueOfTransfer > GetFileWeight(fileId))
            {
                return true;
            }
            else
                return false;
        }

        private int GetAccNumber()
        {
            int number = 0;
            Random rnd = new Random();

            number = rnd.Next(Accounts.logins.Count());

            return number;
        }
        private static string FormatBytes(ulong bytes)
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



    }



}
