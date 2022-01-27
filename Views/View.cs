using MiniProj_HongSun.Controlers;
using MiniProj_HongSun.Models;
using MiniProj_HongSun.Models.Enums;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MiniProj_HongSun.Views
{
    public class View
    {
        public AssetController AssetController = new AssetController();
        public RateController RateController = new RateController();

        public void WriteLineRed(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        public void WriteLineYellow(string message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;  
            Console.WriteLine(message);
            Console.ResetColor ();
        }
    
        public void WriteLineGreen(string message)
        {
            Console.ForegroundColor= ConsoleColor.Green;
            Console.WriteLine(message);
            Console.ResetColor () ;
        }

        public void WriteLine(string message)
        {
            Console.WriteLine (message);
        }

        public void DisplayAddLaptop()
        {
            Console.Clear ();
            WriteLineGreen("Add Laptop: ");
            Console.WriteLine();
            while (true)
            {
                LaptopBrand? brand = GetLapTopBrandFromInput();
                if (brand == null) DisplayMainMenu();

                string model = GetModelFromInput();
                if (model == null) DisplayMainMenu();

                string date = GetDateFromInput();
                if (date == null) DisplayMainMenu();

                string price = GetPriceFromInput();
                if (price == null) DisplayMainMenu();

                Location? location = GetLocationFromInput();
                if (location == null) DisplayMainMenu();

                string size = GetScreenSizeFromInput();
                if (size == null) DisplayMainMenu();

                // Add product to the list.
                try
                {
                    DateTime purchDate = DateTime.ParseExact(date, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                    int screenSize = Int32.Parse(size);
                    double purchPrice = double.Parse(price);
                    Laptop laptop = new Laptop(purchDate, purchPrice, (Location)location, (LaptopBrand)brand, model.ToUpper(), screenSize);
                    this.AssetController.AddLaptop(laptop);
                    this.WriteLineGreen("Added an asset");
                    Thread.Sleep(1500);
                    return;
                }
                catch (Exception e)
                {
                    WriteLineRed(e.Message);
                    WriteLineRed("Invalid input, try again");
                }

                Thread.Sleep(1500);
            }   
        }

        public void DisplayAddMobilephone()
        {
            Console.Clear();
            WriteLineGreen("Add Mobilephone: ");
            while (true)
            {
                Console.Clear();
                WriteLineGreen("Add Laptop: ");
                Console.WriteLine();
                
                MobileBrand? brand = GetMobileBrandFromInput();
                if (brand == null) DisplayMainMenu();

                string model = GetModelFromInput();
                if (model == null) DisplayMainMenu();

                string date = GetDateFromInput();
                if (date == null) DisplayMainMenu();

                string price = GetPriceFromInput();
                if (price == null) DisplayMainMenu();

                Location? location = GetLocationFromInput();
                if (location == null) DisplayMainMenu();

                string number = GetPhoneNumberFromInput();
                if (number == null) DisplayMainMenu();

                // Add product to the list.
                try
                {
                    DateTime purchDate = DateTime.ParseExact(date, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                    double purchPrice = double.Parse(price);
                    Mobilephone phone = new Mobilephone(purchDate, purchPrice, location.Value, brand.Value, model.ToUpper(), number);
                    this.AssetController.AddMobile(phone);
                    this.WriteLineGreen("Added an asset");
                    Thread.Sleep(1500);
                    return;
                }
                catch (Exception e)
                {
                    WriteLineRed(e.Message);
                    WriteLineRed("Invalid input, try again");
                }

                Thread.Sleep(1500);
            }
        }

        public void DisplayUpdateAsset()
        {
            Console.Clear();
            this.PrintList(this.Sort(this.AssetController.GetAssetList()));
            Console.WriteLine();
            this.WriteLineGreen("Update Asset");
            Console.WriteLine();
            Console.WriteLine("Enter the id of an asset: ");
            string input = Console.ReadLine();
            int id;
            Int32.TryParse(input, out id);
            bool isMobile = id % 2 == 1;

            MobileBrand? mobileBrand=null;
            LaptopBrand? laptopBrand=null;
            if (isMobile)
            {
                mobileBrand = GetMobileBrandFromInput();
                if (mobileBrand == null) DisplayMainMenu();
            } 
            else
            {
                laptopBrand = GetLapTopBrandFromInput();
                if (laptopBrand == null) DisplayMainMenu();
            }

            string model = GetModelFromInput();
            if (model == null) DisplayMainMenu();

            string date = GetDateFromInput();
            if (date == null) DisplayMainMenu();

            string price = GetPriceFromInput();
            if (price == null) DisplayMainMenu();

            Location? location = GetLocationFromInput();
            if (location == null) DisplayMainMenu();

            string number;
            if (isMobile)
            {
                number = GetPhoneNumberFromInput();
                if (number == null) DisplayMainMenu();

                try
                {
                    DateTime purchDate = DateTime.ParseExact(date, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                    double purchPrice = double.Parse(price);
                    Mobilephone phone = new Mobilephone(purchDate, purchPrice, location.Value, mobileBrand.Value, model.ToUpper(), number);
                    phone.MobilePhoneId = id;
                    this.AssetController.UpdateAsset(phone);
                    this.WriteLineGreen("Updating success");
                }
                catch (Exception e)
                {
                    WriteLineRed(e.Message);
                    WriteLineRed("Invalid input, try again");
                }
            }
            else
            {
                number = GetScreenSizeFromInput();
                if (number == null) DisplayMainMenu();

                try
                {
                    DateTime purchDate = DateTime.ParseExact(date, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                    int screenSize = Int32.Parse(number);
                    double purchPrice = double.Parse(price);
                    Laptop laptop = new Laptop(purchDate, purchPrice, location.Value, laptopBrand.Value, model.ToUpper(), screenSize);
                    laptop.LaptopId = id;
                    this.AssetController.UpdateAsset(laptop);
                    this.WriteLineGreen("Updating success");
                }
                catch (Exception e)
                {
                    WriteLineRed(e.Message);
                    WriteLineRed("Invalid input, try again");
                }
            }
            Thread.Sleep(1500);
            this.DisplayMainMenu();
        }

        public void DisplayDeleteAsset()
        {
            Console.Clear();
            this.PrintList(this.Sort(this.AssetController.GetAssetList()));
            Console.WriteLine();
            this.WriteLineGreen("Update Asset");
            Console.WriteLine();
            Console.WriteLine("Enter the id of an asset: ");
            string input = Console.ReadLine();
            int id;
            Int32.TryParse(input, out id);
            this.AssetController.DeleteAssetById(id);
        }

        public void DisplayMainMenu()
        {
            Console.Clear();
            Console.WriteLine();
            this.PrintList(this.Sort(this.AssetController.GetAssetList()));
            Console.WriteLine();
            this.WriteLineGreen("Main Menu");
            Console.WriteLine();
            this.WriteLineYellow("Select one of the following options:");
            Console.WriteLine(" 1 - Add a new laptop.");
            Console.WriteLine(" 2 - Add a new mobilephone.");
            Console.WriteLine(" 3 - Update the information of an asset.");
            Console.WriteLine(" 4 - Delete an asset from the list. ");
            Console.WriteLine(" Other - Fetch online exchange rate data. ");
            Console.Write("Selection: " );
            string input = Console.ReadLine().Trim();
            switch (input)
            {
                case "1":
                    this.DisplayAddLaptop();
                    break;
                case "2":
                    this.DisplayAddMobilephone();
                    break;
                case "3":
                    this.DisplayUpdateAsset();
                    break;
                case "4":
                    this.DisplayDeleteAsset();
                    break;
                default:
                    this.WriteLineGreen("Fetching online rates...");
                    bool result = this.RateController.FetchOnlineRates().Result;
                    if (result) this.WriteLineGreen("succeed");
                    else this.WriteLineRed("failed");
                    break;
            }

            Thread.Sleep(1500);
            this.DisplayMainMenu();
        }

        private List<Asset> Sort(List<Asset> assetList)
        {
            List<Asset> sortedList = assetList.OrderBy(asset => asset.Location.ToString()).ThenBy(asset => asset.PurchaseDate).ToList();
            return sortedList;
        }
        private bool IsQ(string input)
        {
            if (input.ToLower().Trim() == "q")
            {
                WriteLineYellow("User quit input");
                return true;
            }
            else
                return false;
        }

        public void PrintList(List<Asset> assetList)
        {
            
            DateTime current = DateTime.Now;
            if (assetList.Count() > 0)
            {
                Console.WriteLine("--------------------------------------------------------------------------------------------------------");
                Console.WriteLine(
                    "|" + "ID".PadRight(5) +
                    "|" + "Type".PadRight(15) +
                    "|" + "Brand".PadRight(15) +
                    "|" + "Model".PadRight(15) +
                    "|" + "Price".PadRight(15) +
                    "|" + "Purch Date".PadRight(15) +
                    "|" + "Office Loca".PadRight(15) + " | "
                );
                Console.WriteLine("--------------------------------------------------------------------------------------------------------");
                List<Asset> sortedList = Sort(assetList);

                foreach (Asset device in sortedList)
                {
                    TimeSpan ts = current - device.PurchaseDate;
                    if ( device.GetType() == typeof(Mobilephone)) {
                        if (ts.Days > 365 * 3 - 90)
                            WriteLineRed((
                                "|" + ((Mobilephone)device).MobilePhoneId.ToString().PadRight(5) +
                                "|" + device.GetType().ToString().Split(".")[2].PadRight(15) +
                                "|" + ((Mobilephone)device).PhoneBrand.ToString().PadRight(15) +
                                "|" + ((Mobilephone)device).Model.PadRight(15) +
                                "|" + CalculateAmount(device).PadRight(15) +
                                "|" + device.PurchaseDate.ToString("yyyy-MM-dd").PadRight(15) +
                                "|" + device.Location.ToString().PadRight(15) + " |")
                            );
                        else if (ts.Days > 365 * 3 - 180)
                            WriteLineYellow((
                                "|" + ((Mobilephone)device).MobilePhoneId.ToString().PadRight(5) +
                                "|" + device.GetType().ToString().Split(".")[2].PadRight(15) +
                                "|" + ((Mobilephone)device).PhoneBrand.ToString().PadRight(15) +
                                "|" + ((Mobilephone)device).Model.PadRight(15) +
                                "|" + CalculateAmount(device).PadRight(15) +
                                "|" + device.PurchaseDate.ToString("yyyy-MM-dd").PadRight(15) +
                                "|" + device.Location.ToString().PadRight(15) + " |")
                            );
                        else
                            Console.WriteLine((
                                "|" + ((Mobilephone)device).MobilePhoneId.ToString().PadRight(5) +
                                "|" + device.GetType().ToString().Split(".")[2].PadRight(15) +
                                "|" + ((Mobilephone)device).PhoneBrand.ToString().PadRight(15) +
                                "|" + ((Mobilephone)device).Model.PadRight(15) +
                                "|" + CalculateAmount(device).PadRight(15) +
                                "|" + device.PurchaseDate.ToString("yyyy-MM-dd").PadRight(15) +
                                "|" + device.Location.ToString().PadRight(15) + " |")
                             );
                    }
                    else
                    {
                        if (ts.Days > 365 * 3 - 90)
                            WriteLineRed((
                                "|" + ((Laptop)device).LaptopId.ToString().PadRight(5) +
                                "|" + device.GetType().ToString().Split(".")[2].PadRight(15) +
                                "|" + ((Laptop)device).Brand.ToString().PadRight(15) +
                                "|" + ((Laptop)device).Model.PadRight(15) +
                                "|" + CalculateAmount(device).PadRight(15) +
                                "|" + device.PurchaseDate.ToString("yyyy-MM-dd").PadRight(15) +
                                "|" + device.Location.ToString().PadRight(15) + " |")
                            );
                        else if (ts.Days > 365 * 3 - 180)
                            WriteLineYellow((
                                "|" + ((Laptop)device).LaptopId.ToString().PadRight(5) +
                                "|" + device.GetType().ToString().Split(".")[2].PadRight(15) +
                                "|" + ((Laptop)device).Brand.ToString().PadRight(15) +
                                "|" + ((Laptop)device).Model.PadRight(15) +
                                "|" + CalculateAmount(device).PadRight(15) +
                                "|" + device.PurchaseDate.ToString("yyyy-MM-dd").PadRight(15) +
                                "|" + device.Location.ToString().PadRight(15) + " |")
                            );
                        else
                            Console.WriteLine((
                                "|" + ((Laptop)device).LaptopId.ToString().PadRight(5) +
                                "|" + device.GetType().ToString().Split(".")[2].PadRight(15) +
                                "|" + ((Laptop)device).Brand.ToString().PadRight(15) +
                                "|" + ((Laptop)device).Model.PadRight(15) +
                                "|" + CalculateAmount(device).PadRight(15) +
                                "|" + device.PurchaseDate.ToString("yyyy-MM-dd").PadRight(15) +
                                "|" + device.Location.ToString().PadRight(15) + " |")
                             );
                    }
                }
                Console.WriteLine("--------------------------------------------------------------------------------------------------------");
            }
            else
            {
                WriteLineYellow("No Product Added To The List.");
            }
            Console.WriteLine();
        }

        private string CalculateAmount(Asset asset)
        {

            string currencyName;
            switch (asset.Location)
            {
                case Location.Sweden:
                    currencyName = "SEK";
                    break;
                case Location.Norway:
                    currencyName = "NOK";
                    break;
                case Location.Denmark:
                    currencyName = "DKK";
                    break;
                case Location.Finland:
                    currencyName = "EUR";
                    break;
                default:
                    currencyName = "USD";
                    break; ;
            }
            if (currencyName == "USD") // rates is USD based, therefore "USD" key is not in the dictionary
                return asset.Price + " USD";
            else
            {
                double rate = this.RateController.GetRate(asset.Location);
                return Math.Round(rate * asset.Price, 2) + " " + currencyName;
            }
        }

        private LaptopBrand? GetLapTopBrandFromInput()
        {
            // QUIT enter product if 'q' is entered
            Console.WriteLine("Select the BRAND of a laptop, enter 'q' to quit:   ");
            Console.WriteLine(" M - MacBook");
            Console.WriteLine(" A - Asus");
            Console.WriteLine(" Other - Lenovo");
            Console.Write("Enter your selection: ");
            string input = Console.ReadLine().Trim();
            if (IsQ(input))
                return null;
            LaptopBrand brand = input.ToUpper() switch
            {
                "M" => LaptopBrand.MacBook,
                "A" => LaptopBrand.Asus,
                _ => LaptopBrand.Lenovo
            };
            return brand;
        }

        private MobileBrand? GetMobileBrandFromInput()
        {
            MobileBrand brand;
            Console.WriteLine("Select the BRAND of a mobilephone, enter 'q' to quit:   ");
            Console.WriteLine(" I - IPhone");
            Console.WriteLine(" S - Samsung");
            Console.WriteLine(" Other - Nokia");
            Console.Write("Enter your selection: ");
            string input = Console.ReadLine().Trim();
            if (IsQ(input))
                return null;
            brand = input.ToUpper() switch
            {
                "I" => MobileBrand.IPhone,
                "S" => MobileBrand.Samsung,
                _ => MobileBrand.Nokia
            };
            return brand;
        }

        private string GetPhoneNumberFromInput()
        {
            Console.Write("Input the Phone Number, enter 'q' to quit:   ");
            string number = Console.ReadLine().Trim();
            if (IsQ(number))
                return null;
            else
                return number;
        }

        private string GetModelFromInput()
        {
            Console.Write("Input the MODEL, enter 'q' to quit:   ");
            string model = Console.ReadLine().Trim();
            if (IsQ(model))
                return null;
            else 
                return model;
        }

        private string GetDateFromInput()
        {
            Console.Write("Input the PURCHASE DATE, format 'yyyy-MM-dd', enter 'q' to quit: ");
            string date = Console.ReadLine().Trim();
            if (IsQ(date))
                return null;
            else
                return date;
        }

        private string GetPriceFromInput()
        {
            Console.Write("Input the PRICE in USD, enter 'q' to quit:   ");
            string price = Console.ReadLine().Trim();
            if (IsQ(price))
                return null;
            else
                return price;
        }

        private Location? GetLocationFromInput()
        {
            Console.WriteLine("Select the OFFICE LOCATION, enter 'q' to quit:");
            Console.WriteLine(" 1 - Denmark ");
            Console.WriteLine(" 2 - Sweden ");
            Console.WriteLine(" 3 - Norway ");
            Console.WriteLine(" 4 - Finland ");
            Console.WriteLine(" Other input - Other Location");
            Console.Write("Enter your selection: ");
            string selection = Console.ReadLine().Trim();
            if (IsQ(selection))
                return null;
            Location location = selection switch
            {
                "1" => Location.Denmark,
                "2" => Location.Sweden,
                "3" => Location.Norway,
                "4" => Location.Finland,
                _ => Location.Others,
            };
            return location;
        }

        private string GetScreenSizeFromInput()
        {
            Console.Write("Input the SCREEN SIZE of the laptop, enter 'q' to quit:   ");
            string number = Console.ReadLine().Trim();
            if (IsQ(number))
                return null;
            else
                return number;
        }

    }
}

