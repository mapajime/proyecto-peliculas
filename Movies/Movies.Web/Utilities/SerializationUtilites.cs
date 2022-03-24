using Movies.Entities;
using Movies.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace Movies.Web.Utilities
{
    public static class SerializationUtilites
    {
        public static StringContent CreateContentFromObject(this MovieGenderModel movieGenderModel)
        {
            var json = JsonConvert.SerializeObject(movieGenderModel);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            return data;
        }
    }
}
