﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MainWeb.Models
{
    public class CheckFileInfo
    {
        public string BaseFileName { get; set; }
        public string OwnerId { get; set; }
        public long Size { get; set; } //in bytes
        public string SHA256 { get; set; } //SHA256: A 256 bit SHA-2-encoded [FIPS180-2] hash of the file contents
        //http://msdn.microsoft.com/en-us/library/system.security.cryptography.sha256managed.aspx
        public string Version { get; set; }  //changes when file changes.

     public bool   UserCanWrite { get; set; }
        public bool   WebEditingDisabled { get; set; }
        public bool   RestrictedWebViewOnly { get; set; }
        public bool   ReadOnly { get; set; }
        public bool   SupportsUpdate { get; set; }
        public bool   SupportsLocks { get; set; }
        public bool   SupportsCoauth { get; set; }
        public bool   SupportsCobalt { get; set; }
        public bool   SupportsFolders { get; set; }
        public bool   SupportsScenarioLinks { get; set; }
        public bool   SupportsSecureStore { get; set; }
    }
}