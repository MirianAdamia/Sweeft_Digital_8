using System.Collections.Generic;

namespace Sweeft_Digital_8.Models
{
    public class Country
    {
        public string Region { get; set; }
        public string Subregion { get; set; }
        public List<decimal> Latlng { get; set; }
        public decimal Area { get; set; }
        public int Population { get; set; }
    }
}
