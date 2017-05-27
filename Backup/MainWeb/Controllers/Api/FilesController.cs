using MainWeb.Helpers;
using MainWeb.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Hosting;
using System.Web.Http;

namespace MainWeb.Controllers.Api
{
    /// <summary>
    /// Primary class for WOPI interface.  Supporting the 2 minimal API calls
    /// requred for base level View
    /// </summary>
    public class FilesController : ApiController
    {

        IFileHelper _fileHelper;
        IKeyGen _keyGen;
        /// <summary>
        /// Base constructor
        /// </summary>
        public FilesController() : this(new FileHelper(), new KeyGen())
        {
        }

        /// <summary>
        /// Constructor for DI
        /// </summary>
        /// <param name="fileHelper">File helper implementation</param>
        /// <param name="keyGen">KeyGen helper implementation</param>
        public FilesController(IFileHelper fileHelper, IKeyGen keyGen)
        {
            _fileHelper = fileHelper;
            _keyGen = keyGen;
            
        }


        /// <summary>
        /// Required for WOPI interface - on initial view
        /// </summary>
        /// <param name="name">file name</param>
        /// <param name="access_token">token that WOPI server will know</param>
        /// <returns></returns>
        public CheckFileInfo Get(string name, string access_token)
        {
            Validate(name, access_token);

            var fileInfo = _fileHelper.GetFileInfo(name);
            return fileInfo;
        }



        /// <summary>
        /// Required for View WOPI interface - returns stream of document.
        /// </summary>
        /// <param name="name">file name</param>
        /// <param name="access_token">token that WOPI server will know</param>
        /// <returns></returns>
        public HttpResponseMessage GetFile(string name, string access_token)
        {
            try
            {
                Validate(name, access_token);
                var file = HostingEnvironment.MapPath("~/App_Data/" + name);
                var rv = new HttpResponseMessage(HttpStatusCode.OK);
                var stream = new FileStream(file, FileMode.Open, FileAccess.Read);

                rv.Content = new StreamContent(stream);
                rv.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                return rv;
                //return null;

            }
            catch (Exception ex)
            {
                var rv = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                var stream = new MemoryStream(UTF8Encoding.Default.GetBytes(ex.Message ?? ""));
                rv.Content = new StreamContent(stream);
                return rv;
            }
        }



        void Validate(string name, string access_token)
        {
            //var decoded = access_token);
            var isValid = _keyGen.Validate(name, access_token);
            if (!isValid)
                throw new SecurityException("Access token doesn't match calculated token");
        }


    }
}
