using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Xml.Linq;

namespace GsaDummyService.Controllers
{
    public class SearchController : ApiController
    {
        public HttpResponseMessage Get()
        {
            string nullSearchFileName = @"emptygsaxml.xml";
            var filepath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, nullSearchFileName);
            var doc = XDocument.Load(filepath);
            return new HttpResponseMessage
            {
                Content = new StringContent(doc.ToString(), Encoding.UTF8, "application/xml")
            };
        }
    }
}