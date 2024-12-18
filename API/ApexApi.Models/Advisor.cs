using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Metrics;
using System.Net;
using System.Text.Json.Serialization;


namespace ApexApi.Models
{
    public class RequestObject
    {
        public Advisor advisor {get;set;}
    }
    
    [Index(nameof(sin), IsUnique = true)]
    public class Advisor
    {
        [Key]
        public long Id { get; set; }
        [Required(ErrorMessage = "Full name is required."), MaxLength(255,ErrorMessage ="Maximum length of Full Name can be only 255 charachters")]
        public string fullName { get; set; }
        [Required(ErrorMessage ="Sin is required."), Length(maximumLength: 9, minimumLength: 9, ErrorMessage = "Length of SIN can be only 9 numbers")]
        public string sin { get; set; }
        [MaxLength(255,ErrorMessage ="Maximum length of Address can be only 255 charachters")]
        public string? address { get; set; }
        [Length(maximumLength:10,minimumLength:10, ErrorMessage = "Length of phone number can be only 10 numbers")]
        public string? phoneNumber { get; set; }
        [JsonIgnore]
        public int healthStatus { get; set; }
        [NotMapped]
        public string displayHealthStatus { get {
                switch (healthStatus)
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
    
    public class RuleViolation
    {

        public string ErrorMessage { get; private set; }
        public string PropertyName { get; private set; }

        public RuleViolation(string errorMessage, string propertyName)
        {
            ErrorMessage = errorMessage;
            PropertyName = propertyName;
        }
    }
}
