using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fare;

namespace PostOffice.WebApp.Services
{
    public static class SerialCodeService
    {
        public static string GetParcialSerial()
        {
            var xeger = new Xeger("^[a-zA-Z]{2}[0-9]{6}[a-zA-Z]{2}$");
            var generatedString = xeger.Generate();

            return generatedString;
        }
        public static string GetLetterBagSerial()
        {
            var xeger = new Xeger(@"^[a-zA-Z0-9]{13}$");
            var generatedString = xeger.Generate();

            return "b1" + generatedString;
        }
        public static string GetParcialBagSerial()
        {
            var xeger = new Xeger(@"^[a-zA-Z0-9]{13}$");
            var generatedString = xeger.Generate();

            return "b2" + generatedString;
        }
        public static string GetShipmentSerial()
        {
            var xeger = new Xeger(@"^[a-zA-Z0-9]{3}\-[a-zA-Z0-9]{6}$");
            var generatedString = xeger.Generate();

            return generatedString;
        }
        public static string GetFlightNumberSerial()
        {
            var xeger = new Xeger(@"^[a-zA-Z]{2}[0-9]{4}$");
            var generatedString = xeger.Generate();

            return generatedString;
        }
    }
}
