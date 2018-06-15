using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Swashbuckle.Swagger.Annotations;

namespace NutritionAPI.Controllers
{
    [RoutePrefix("api/Nutrition")]
    public class NutritionController : ApiController
    {
        private readonly string _uri = "https://trackapi.nutritionix.com/v2/natural/nutrients";
        private readonly HttpWebRequest _request;

        public NutritionController()
        {
            _request = (HttpWebRequest)WebRequest.Create(_uri);
            _request.Method = "POST";
            _request.Headers["postman-token"] = "db5aa6a4-c20f-b70d-dc42-6c938bf6d012";
            _request.Headers["cache-control"] = "no-cache";
            _request.Headers["x-remote-user-id"] = "michsien";
            _request.Headers["x-app-key"] = "1db624ad88c15d57e78216ba9fdd59fd";
            _request.Headers["x-app-id"] = "03cbd0aa";
            _request.Accept = "application/json";
            _request.ContentType = "application/json";
        }

        [HttpPost]
        [Route("GetCalories")]
        [SwaggerOperation("Create")]
        [SwaggerResponse(HttpStatusCode.Created)]
        public double GetCalories([FromBody] JObject data)
        {
            StreamWriter requestWriter = new StreamWriter(_request.GetRequestStream(), System.Text.Encoding.ASCII);
            requestWriter.Write(data);
            requestWriter.Close();

            WebResponse webResponse = _request.GetResponse();
            Stream webStream = webResponse.GetResponseStream();
            StreamReader responseReader = new StreamReader(webStream);
            string response = responseReader.ReadToEnd();
            responseReader.Close();

            var json = JObject.Parse(response);

            var calories = 0.0;
            foreach ( var food in json["foods"])
            {
                calories += (double) food["nf_calories"];
            }
            return calories;
        }

    }
}
