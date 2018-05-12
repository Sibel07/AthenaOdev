using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sibelApi.Entity
{
    public class Rezervasyon
    {
        public int hotelId;
        public int UnitId;
        public string date;
        public string time;
        public int reservationAdult;
        public int reservationChild;
        public string reservationNote;
        public RezervasyonGuest guest;
    }

    public class RezervasyonGuest
    {
        public string hotelCode;
        public string roomNumber;
        public int folio;
        public string firstName;
        public string lastName;
        public string title;
        public string gender;
        public string country;
        public int adult;
        public int child;
        public string arrivalDate;
        public string departureDate;
        public string panTip;
        public int nights;
    }
}
