using System;
using System.ComponentModel;

namespace Movies.Models
{
    public class ActorModel : ModelBase
    {
        [DisplayName("Nombre")]
        public string FirstName { get; set; }
        [DisplayName("Apellidos")]
        public string LastName { get; set; }
        [DisplayName("Fecha de nacimiento")]
        public DateTime? DateOfBirth { get; set; }
        [DisplayName("Nacionalidad")]
        public Guid NationalityId { get; set; }
        [DisplayName("Peso")]
        public float? Weight { get; set; }
        [DisplayName("Altura")]
        public int Height { get; set; }
        [DisplayName("Valor Neto")]
        public float? NetWorth { get; set; }
        [DisplayName("Genero")]
        public Guid GenderId { get; set; }
        [DisplayName("Numero de hijos")]
        public int? NumberOfChildren { get; set; }
    }
}