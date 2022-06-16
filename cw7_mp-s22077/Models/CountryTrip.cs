using System;
using System.Collections.Generic;

#nullable disable

namespace cw7_mp_s22077.Models
{
    public partial class CountryTrip
    {
        public int IdCountry { get; set; }
        public int IdTrip { get; set; }

        public virtual Country IdCountryNavigation { get; set; }
        public virtual Trip IdTripNavigation { get; set; }
    }
}
