using MiniProj_HongSun.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniProj_HongSun.Models
{
    public class Asset
    {
        public DateTime PurchaseDate { get; set; }
        public Location Location { get; set; }
        private double _price;

        public double Price
        {
            get { return this._price; }
            set
            {
                if (value > 0)
                {
                    // round the price to 2 decimal places
                    this._price = Math.Round(value, 2);
                }
                else
                {
                    // if price <= 0 throw Exception
                    throw new ArgumentOutOfRangeException(nameof(Price), "Price of assets must be greater than 0.");
                }
            }
        }

        public Asset(DateTime puchaseDate, double price, Location location)
        {
            this.PurchaseDate = puchaseDate;
            this.Location = location;
            this.Price = price;
        }
    }
}
