using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    static public class LinkList
    {
        static List<string> name = new List<string>();
        static List<string> generatedLink = new List<string>();
        static List<int> fileSize = new List<int>();
        static List<string> date = new List<string>();


        static void AddLinkTolist(string fileName, int size, string link, string dateLink)
        {
            Delete20();
            name.Add(fileName);
            generatedLink.Add(link);
            fileSize.Add(size);
            date.Add(dateLink);
        }

        static void Delete20()
        {

        }
    }
}