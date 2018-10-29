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
        static List<string> fileSize = new List<string>();
        static List<string> date = new List<string>();


        static public void AddLinkTolist(string fileName, string size, string link, string dateLink)
        {
            Delete20();
            name.Add(fileName);
            generatedLink.Add(link);
            fileSize.Add(size);
            date.Add(dateLink);
        }

        static void Delete20()
        {
            name.RemoveAt(19);
            generatedLink.RemoveAt(19);
            fileSize.RemoveAt(19);
            date.RemoveAt(19);
        }
    }
}