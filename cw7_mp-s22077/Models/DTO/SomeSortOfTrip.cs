using System;
using System.Collections.Generic;

namespace cw7_mp_s22077.Models.DTO
{
    public class SomeSortOfTrip
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public int MyProperty { get; set; }
        public int MaxPeople { get; set; }
        public IEnumerable<SomeSortOfCountry> Countries { get; set; }
        public IEnumerable<SomeSortOfClient> Clients { get; set; }
    }
}
