using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursach.Models
{
    public class Publication
    {
        public int IdPublication { get; set; }
        public DateTime DatePubl { get; set; }
        public string NameCar { get; set; }
        public double Price { get; set; }
        public string ModelCar { get; set; }
        public string TechState { get; set; }
        public string TypeCar { get; set; }
        public byte[] Photo { get; set; }
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
        public DateTime DateBuy { get; set; }
    }

    public class ClientsPhone
    {
        public int IdClient { get; set; }
        public string Phone { get; set; }
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



}
