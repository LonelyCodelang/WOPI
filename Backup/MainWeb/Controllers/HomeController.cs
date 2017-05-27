using MainWeb.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace MainWeb.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            var testLinks = GetTestLinks();
            var model = new FileRequest
            {
                name= WebConfigurationManager.AppSettings["appSampleLink"],
                Items = testLinks
            };
            return View(model);
        }

        public IEnumerable<SelectListItem> GetTestLinks()
        {
            var section = WebConfigurationManager.GetSection("wopiHostSettings/sampleUrls") as Hashtable;
            var rv = new List<SelectListItem>();
            foreach (DictionaryEntry item in section)
            {
                rv.Add(new SelectListItem
                {
                    Selected=false,
                    Text = item.Value.ToString(),
                    Value = item.Value.ToString()
                });
            }
            return rv;
        }

        public ActionResult Upload()
        {
            return View();
        }

        

    }
}
