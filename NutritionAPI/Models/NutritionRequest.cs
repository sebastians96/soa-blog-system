using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NutritionAPI.Models
{
    public class NutritionRequest
    {
        public string Query { get; set; }
        public string TimeZone { get; set; }
    }
}