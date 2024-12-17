using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;


namespace ApexApi.Models
{
    
    [Index(nameof(SIN), IsUnique = true)]
    public class Advisor
    {
        [Key]
        public int Id { get; set; }
        [Required, MaxLength(255,ErrorMessage ="Maximum length of Full Name can be only 255 charachters")]
        public string FullName { get; set; }
        [Required, Length(maximumLength: 9, minimumLength: 9, ErrorMessage = "Length of SIN can be only 9 numbers")]
        public string SIN { get; set; }
        [MaxLength(255,ErrorMessage ="Maximum length of Address can be only 255 charachters")]
        public string? Address { get; set; }
        [Length(maximumLength:10,minimumLength:10, ErrorMessage = "Length of phone number can be only 10 numbers")]
        public string? PhoneNumber { get; set; }
        [JsonIgnore]
        public int HealthStatus { get; set; }
        [NotMapped]
        public string DisplayHealthStatus { get {
                switch (HealthStatus)
                {
                    case 1:
                        return "green";
                    case 2:
                        return "yellow";
                    case 3:
                        return "red";
                    default:
                        return "green";
                }
            } }
    }
}
