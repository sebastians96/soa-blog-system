using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdminPanel_Service.Models
{
    /// <summary>
    /// Model of Delete Request that is used by User Controller.
    /// </summary>
    public class DeleteRequest
    {
        public int id { get; set; }
        public int delete { get; set; }
    }
}