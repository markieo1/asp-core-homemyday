using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Text;
using System.Xml;

namespace HomeMyDay.Web.Site.Home.Controllers
{
	public class PressController : Controller
    {
        private string xml;

        public ViewResult Index()
        {
            xml = "https://www.nu.nl/rss/Algemeen";

            XmlReader reader = XmlReader.Create(new StringReader(xml));

            
            int startIndex = xml.IndexOf('<');
            if (startIndex > 0)
            {
                xml = xml.Remove(0, startIndex);
            

                while (reader.Read())
                {
                    if ((reader.NodeType == XmlNodeType.Element) && (reader.Name == "item"))
                    {
                        if (reader.HasAttributes)
                        {
                            reader.GetAttribute("title");
                            reader.GetAttribute("description");
                        }                        
                    }
                }
            }
            ViewData["reader"] = reader;
            return View();
        }
    }
}
