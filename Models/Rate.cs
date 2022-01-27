using MiniProj_HongSun.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniProj_HongSun.Models
{
    public class Rate
    {
        [Key]
        public Location RateLocation { get; set; }
        public double RateValue { get; set; }

        public Rate(Location rateLocation, double rateValue)
        {
            this.RateLocation = rateLocation;
            this.RateValue = rateValue;
        }
    }
}
