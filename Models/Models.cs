using System;

namespace Kursach.Models
{
    public class Publication
    {
        public byte[] Photo { get; set; }
        public string Login { get; set; } 
        public string name_car { get; set; }
        public string Type_Car { get; set; }
        public string car_mark { get; set; }
        public string Model_Car { get; set; }
        public string Tech_State { get; set; }
        public DateTime date_publ { get; set; }
        public double price { get; set; }
        public string Status { get; set; } 
        public int id_publication { get; set; }
    }

    public class Dealer
    {
        public int IdDealer { get; set; }
        public string NameDealer { get; set; }
        public string Email { get; set; }
    }

    public class Client
    {
        public int IdClient { get; set; }
        public string NameClient { get; set; }
        public string Email { get; set; }
        public string UserType { get; set; }
    }

    public class Deal
    {
        public int IdDeal { get; set; }
        public int IdPublication { get; set; }
        public DateTime DateBuy { get; set; }
        public decimal FinalPrice { get; set; }
        public string Login { get; set; }
    }

    public class DealersPhone
    {
        public int IdDealer { get; set; }
        public string Phone { get; set; }
    }

    public class Mark
    {
        public string CarMark { get; set; }
        public string Country { get; set; }
    }

    public class Model
    {
        public string ModelCar { get; set; }
        public string CarMark { get; set; }

        public override string ToString()
        {
            return ModelCar; 
        }
    }

    public class Photo
    {
        public string IdPhoto { get; set; }
        public string Link { get; set; }
        public int IdPublication { get; set; }
    }

    public class TechState
    {
        public string Probeg { get; set; }
        public string Color { get; set; }
        public string Year { get; set; }
        public int IdPublication { get; set; }
    }

    public class User
    {
        public string Login { get; set; }
        public string Role { get; set; }
        public string Password { get; set; }
    }

    public class Statistics
    {
        public int TotalCarTypes { get; set; }
        public int TotalCarMarks { get; set; }
        public int TotalCarModels { get; set; }
        public decimal AveragePrice { get; set; }
        public decimal ActiveAdsPercentage { get; set; }
    }

}
