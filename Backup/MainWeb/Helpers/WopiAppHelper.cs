using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Xml.Serialization;

namespace MainWeb.Helpers
{
    public class WopiAppHelper
    {
        string _discoveryFile;
        WopiHost.wopidiscovery _wopiDiscovery;

        public WopiAppHelper(string discoveryXml)
        {
            _discoveryFile = discoveryXml;

            using (StreamReader file = new StreamReader(discoveryXml))
            {
                XmlSerializer reader = new XmlSerializer(typeof(WopiHost.wopidiscovery));
                var wopiDiscovery = reader.Deserialize(file) as WopiHost.wopidiscovery;
                _wopiDiscovery = wopiDiscovery;
            }
        }


        public WopiHost.wopidiscoveryNetzoneApp GetZone(string AppName)
        {
            var rv = _wopiDiscovery.netzone.app.Where(c => c.name == AppName).FirstOrDefault();
            return rv;
        }

        public string  GetDocumentLink(string wopiHostandFile)
        {
            var fileName = wopiHostandFile.Substring(wopiHostandFile.LastIndexOf('/') + 1);
            var accessToken = GetToken(fileName);
            var fileExt = fileName.Substring(fileName.LastIndexOf('.') + 1);
            var tt = _wopiDiscovery.netzone.app.AsEnumerable().Where(c => c.action.Where(d => d.ext == fileExt).Count() > 0);

            var appName = tt.FirstOrDefault();

            if (null == appName) throw new ArgumentException("invalid file extension " + fileExt);

            var rv = GetDocumentLink(appName.name, fileExt, wopiHostandFile, accessToken);

            return rv;
        }

        string GetToken(string fileName)
        {
            KeyGen keyGen = new KeyGen();
            var rv = keyGen.GetHash(fileName);

            return HttpUtility.UrlEncode(rv);
        }

        const string s_WopiHostFormat = "{0}?WOPISrc={1}&access_token={2}";
        public string GetDocumentLink(string appName, string fileExtension, string wopiHostAndFile, string accessToken)
        {
            var wopiHostUrlsafe = HttpUtility.UrlEncode(wopiHostAndFile.Replace(" ", "%20"));
            var appStuff = _wopiDiscovery.netzone.app.Where(c => c.name == appName).FirstOrDefault();

            if (null == appStuff)
                throw new ApplicationException("Can't locate App: " + appName);


            var appAction = appStuff.action.Where(c => c.ext == fileExtension).FirstOrDefault();
                        if (null == appAction)
                throw new ApplicationException("Can't locate UrlSrc for : " + appName);

            var endPoint = appAction.urlsrc.IndexOf('?');
            var fullPath = string.Format(s_WopiHostFormat, appAction.urlsrc.Substring(0, endPoint), wopiHostUrlsafe, accessToken); 

            return fullPath;
        }
    }
}