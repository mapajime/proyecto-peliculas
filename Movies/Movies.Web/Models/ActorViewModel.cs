using Microsoft.AspNetCore.Mvc.Rendering;
using Movies.Models;
using System.Collections.Generic;

namespace Movies.Web.Models
{
    public class ActorViewModel : ActorModel
    {
        public IEnumerable<SelectListItem> Genders { get; set; }
        public IEnumerable<SelectListItem> Nacionalities { get; set; }
    }
}
