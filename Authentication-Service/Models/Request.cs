using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Authentication_Service.Models
{
    /// <summary>
    /// Request that is used in parsing json in the User Controller.
    /// </summary>
    public class Request
    {
        public string username { get; set; }
        public string password { get; set; }
    }
}