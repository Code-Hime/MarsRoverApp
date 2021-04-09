using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarsRoverApp
{
    public class ApiSettings
    {
        public string ApiKey { get; set; }

        public string ApiBaseUrl { get; set; }

        public string ApodBaseUrl => $"{ ApiBaseUrl }?api_key={ ApiKey }";

        public string ApodDateUrl(string date) => $"{ ApodBaseUrl }&date={ date }";
    }
}
