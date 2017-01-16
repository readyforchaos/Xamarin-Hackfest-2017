using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Swashbuckle.Swagger.Annotations;

namespace TidBankWebAppDemo.Controllers
{
    public class LocationsController : ApiController
    {

        [SwaggerOperation("GetLocations")]
        public IEnumerable<Location> Get()
        {
            return locations;
        }

        public class Location
        {
            public string ID { get; set; }
            public string Long { get; set; }
            public string Lat { get; set; }
            public string Name { get; set; }
        }

        static List<Location> locations = new List<Location>()
        {
            new Location() { ID = "1", Lat = "59.9238397", Long="10.7295549", Name = "Teknologihuset"},
            new Location() { ID = "2", Lat = "59.9250158", Long="10.7311643", Name = "Bislett stadion"},
            new Location() { ID = "3", Lat = "59.9245263", Long="10.7292303", Name = "Sonans"},
        };

    }
}
