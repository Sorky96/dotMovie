﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {   
        public ActionResult ViewPage1(string title)
        {
            OMDB_api imdb = new OMDB_api();


            
            imdb.GetMovieInfoFromOmbd(title);
            
            ViewBag.MovieTitle = imdb.title;
            ViewBag.Plot = imdb.plot;
            ViewBag.Year = imdb.year;
            ViewBag.Rated = imdb.rated;
            ViewBag.Released = imdb.released;
            ViewBag.Director = imdb.director;
            ViewBag.Metascore = imdb.metascore;
            ViewBag.ImdbRating = imdb.imdbRating;
            ViewBag.Poster = imdb.poster;
            ViewBag.Duration = imdb.duration;
            ViewBag.Genre = imdb.genre;
            ViewBag.Actors = imdb.actors;
            
            

            return View();

        }

        public ActionResult Index()
        {
            Hostings rapidu = new Hostings();
            rapidu.GetTransferLeft();
            ViewBag.premiumEndDate = rapidu.premiumEnd;
            ViewBag.premiumLeft = rapidu.premiumLeft;

            return View();
        }

        public ActionResult Downloader(string getUrl)
        {
            
            Hostings rapidu = new Hostings();            
            rapidu.Loginner();
            rapidu.GetDownloadLinkv2(getUrl);

           
            

            ViewBag.GeneratedLink = rapidu.GeneratedLink;
            ViewBag.FileName = rapidu.fileName;
            ViewBag.FileSize = rapidu.fileSize;
            LinkList.AddLinkTolist(rapidu.fileName, rapidu.fileSize, rapidu.GeneratedLink, Convert.ToString(DateTime.Now));
            return View();
        }

        public ActionResult About()
        {
            

                ViewBag.test = LinkList.name.ToString();
           return View();
        }
        
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

    }
}