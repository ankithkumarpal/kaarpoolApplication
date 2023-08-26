using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Location
    {
        [Key]
        public int Id { get; set ; } 
        public string Name { get; set ; }
        public int Occupency { get; set; }
    }
}
   