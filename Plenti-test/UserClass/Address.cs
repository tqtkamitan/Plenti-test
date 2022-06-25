using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserClass
{
    public class Address
    {
        public string StreetAddress { get; set; }
        public string Suburb { get; set; }
        public string State { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }

        public Address(string streetAddress, string suburb, string state, decimal latitude, decimal longitude)
        {
            StreetAddress = streetAddress;
            Suburb = suburb;
            State = state;
            Latitude = latitude;
            Longitude = longitude;
        }

        public Address()
        {
            StreetAddress = String.Empty;
            Suburb = String.Empty; 
            State = String.Empty;
            Latitude = 0;
            Longitude = 0;
        }
    }
}
