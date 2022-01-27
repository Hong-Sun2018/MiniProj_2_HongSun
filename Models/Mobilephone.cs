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
        [Table("Mobilephones")]
        public class Mobilephone : Asset
        {
            [Key]
            public int MobilePhoneId { get; set; }

            private string _telephoneNumber;

            [MaxLength(20)]
            public string Model { get; set; }
            public MobileBrand PhoneBrand { get; set; }

            [MaxLength(20)]
            public string PhoneNumber {
                get { return _telephoneNumber; }
                set
                {
                    foreach (char chr in value.ToCharArray())
                    {
                        if (!char.IsDigit(chr))
                            throw new FormatException("Telephone number can only contains number.");
                        this._telephoneNumber = value;
                    }
                }
            }

            public Mobilephone(DateTime purchaseDate, double price, Location location, MobileBrand phoneBrand, string model, string phoneNumber)
                : base(purchaseDate, price, location)
            {
                this.Model = model;
                this.PhoneBrand = phoneBrand;
                this.PhoneNumber = phoneNumber;
            }
        }
}
