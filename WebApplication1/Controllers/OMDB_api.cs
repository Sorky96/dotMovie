using System.Xml;
using System;
namespace WebApplication1.Controllers

{
    internal class OMDB_api
    {

        public string title;
        public string plot;
        public string year;
        public string rated;
        public string released;
        public string director;
        public string metascore;
        public string imdbRating;
        public string poster;
        public string duration;
        public string genre;
        public string actors;


        private string apikey = "&apikey=&r=xml";


        public void GetMovieInfoFromOmbd(string name)
        {

            XmlDocument doc = new XmlDocument();
            doc.Load("http://www.omdbapi.com/?t=" + name + apikey + "&plot=full");

            DateTime thisDay = DateTime.Today;
            foreach (XmlNode node in doc.DocumentElement.ChildNodes)
            {


                title = node.Attributes["title"]?.InnerText;
                plot = node.Attributes["plot"]?.InnerText;
                year = node.Attributes["year"]?.InnerText;
                rated = Convert.ToString(node.Attributes["rated"]?.InnerText);
                released = node.Attributes["released"]?.InnerText;
                director = node.Attributes["director"]?.InnerText;
                metascore = node.Attributes["metascore"]?.InnerText;
                imdbRating = node.Attributes["imdbRating"]?.InnerText;
                poster = node.Attributes["poster"]?.InnerText;
                duration = node.Attributes["runtime"]?.InnerText;
                genre = node.Attributes["genre"]?.InnerText;
                actors = node.Attributes["actors"]?.InnerText;
                if (poster == "N/A")
                {
                    poster = "http://www.filmfodder.com/reviews/images/poster-not-available.jpg";
                }

            }

        }
    }
}