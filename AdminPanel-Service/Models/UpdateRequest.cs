using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdminPanel_Service.Models
{
    public class UpdateRequest
    {
        public int id { get; set; }
        public int update { get; set; }
        public string status { get; set; }
    }
}