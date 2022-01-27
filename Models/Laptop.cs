using MiniProj_HongSun.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniProj_HongSun.Models
{
    [Table("Laptops")]
    public class Laptop : Asset
    {
        [Key]
        public int LaptopId { get; set; }

        private int _screenSize;

        [MaxLength(20)]
        public string Model { get; set; }
        public LaptopBrand Brand { get; set; }

        public int ScreenSize {
            get { return this._screenSize; }
            set
            {
                if (value >= 8 && value <= 19)
                    this._screenSize = value;
                else
                    throw new ArgumentOutOfRangeException(nameof(ScreenSize), "Ivalid screen size.");
            }
        }

        public Laptop(DateTime purchaseDate, double price, Location location, LaptopBrand brand, string model, int screenSize)
            : base(purchaseDate, price, location)
        {
            this.Model = model;
            this.Brand = brand;
            this.ScreenSize = screenSize;
        }
    }
}

