using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace GlassLewisTest.Core.Entities
{
    public class Company : IValidatableObject
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(256)]
        public string Ticker { get; set; }

        [Required]
        [StringLength(256)]
        public string ISIN { get; set; }

        [StringLength(256)]
        public string Website { get; set; }

        public Guid ExchangeId { get; set; }

        [Required]
        public string ExchangeName { get; set; }

        public virtual IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();

            if (!string.IsNullOrEmpty(Website) &&
                !Uri.IsWellFormedUriString(Website, UriKind.Absolute))
            {
                results.Add(new ValidationResult("Website is not well formed uri string", new List<string>() { nameof(Website) }));
            }

            if (!string.IsNullOrEmpty(ISIN) &&
                !Regex.IsMatch(ISIN, "^[a-zA-Z]{2}"))
            { 
                results.Add(new ValidationResult("ISIN is not correct", new List<string>() { nameof(ISIN) }));
            }

            return results;
        }
    }
}
