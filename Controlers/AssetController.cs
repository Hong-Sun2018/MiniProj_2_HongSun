using MiniProj_HongSun.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniProj_HongSun.Controlers
{
    public class AssetController
    {
        private AppDbContext _context = new AppDbContext();
        
        public Asset GetAssetById(int id)
        {
            bool isMobile = id % 2 == 1;
            if (isMobile)
                return this._context.Mobilephones.Where(phone => phone.MobilePhoneId == id).First();
            else
                return this._context.Laptops.Where(laptop => laptop.LaptopId == id).First();
        }
        public void AddMobile(Mobilephone phone)
        {
            this._context.Mobilephones.Add(phone);
            this._context.SaveChanges();
        }

        public void AddLaptop(Laptop laptop)
        {
            this._context.Laptops.Add(laptop);
            this._context.SaveChanges();
        }

        public List<Asset> GetAssetList()
        {
            List<Asset> assets = new List<Asset>();

            List<Mobilephone> mobilephones = this._context.Mobilephones.ToList();
            List<Laptop> laptops = this._context.Laptops.ToList();

            assets.AddRange(laptops);
            assets.AddRange(mobilephones);  

            return assets;
        }

        public void DeleteAssetById(int id)
        {
            if (id % 2 == 1)
            {
                Mobilephone phone = this._context.Mobilephones.Where(phone => phone.MobilePhoneId == id).FirstOrDefault();
                this._context.Mobilephones.Remove(phone);
            }
            else
            {
                Laptop laptop = this._context.Laptops.Where(laptop => laptop.LaptopId == id).FirstOrDefault();
                this._context.Laptops.Remove(laptop);
            }
            this._context.SaveChanges();
        }

        public void UpdateAsset(Asset asset)
        {
            if (asset.GetType() == typeof(Mobilephone))
            {
                Mobilephone result = this._context.Mobilephones.Where(a => a.MobilePhoneId == ((Mobilephone)asset).MobilePhoneId).First();
                result.Price = asset.Price;
                result.Location = asset.Location;
                result.PhoneNumber = ((Mobilephone)asset).PhoneNumber;
                result.PurchaseDate = asset.PurchaseDate;
                result.PhoneBrand = ((Mobilephone)asset).PhoneBrand;
                result.Model = ((Mobilephone)asset).Model;
            }
            else if (asset.GetType() == typeof(Laptop))
            {
                Laptop result = this._context.Laptops.Where(a => a.LaptopId == ((Laptop)asset).LaptopId).First();
                result.Price = asset.Price;
                result.Location = asset.Location;
                result.ScreenSize = ((Laptop)asset).ScreenSize;
                result.PurchaseDate = asset.PurchaseDate;
                result.Brand = ((Laptop)asset).Brand;
                result.Model = ((Laptop)asset).Model;
            }
        }
    }
}
