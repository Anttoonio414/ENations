using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Metrics;

namespace ENations.Models
{
    public class CountryFunds
    {
        [Key, ForeignKey("Country")]
        public int CountryId { get; set; }
        public string Currency { get; set; }
        public decimal Gold { get; set; }

        public virtual Country Country { get; set; }
    }
}
