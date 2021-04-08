using System;
using System.Text.Json.Serialization;

namespace MarsRoverApp.Models
{
    public class MarsRoverApod
    {
        //Date of image
        [JsonPropertyName("date")]
        public DateTime Date { get; set; }

        //The supplied text explanation of the image
        [JsonPropertyName("explanation")]
        public string Description { get; set; }

        //The title of the image
        [JsonPropertyName("title")]
        public string Title { get; set; }

        //The URL of the APOD image or video of the day
        [JsonPropertyName("url")]
        public string Url { get; set; }

        //The URL of the HD version of the APOD image or video of the day
        [JsonPropertyName("hdurl")]
        public string HDUrl { get; set; }

        //The type of media (data) returned. May either be 'image' or 'video' depending on content
        [JsonPropertyName("media_type")]
        public string MediaType { get; set; }
    }
}
