using System;
using System.ComponentModel.DataAnnotations;

namespace Movies.Entities
{
    public class EntityBase
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
        [Required]
        public DateTime ModifiedAt { get; set; }
    }
}