using System.Collections.Generic;
using AgriBook.DB.Models;
using System.Data.Entity.Migrations;
using System.Linq;

namespace AgriBook.DB.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<AgriBook.DB.Models.AgriBookContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(AgriBook.DB.Models.AgriBookContext context)
        {
            var metricUnits = new List<MetricUnit>()
            {
                new MetricUnit()
                {
                    Name = "kg",
                    Type = 1
                },
                new MetricUnit()
                {
                    Name = "tona",
                    Type = 1
                },
                new MetricUnit()
                {
                    Name = "bala",
                    Type = 1
                },
                new MetricUnit()
                {
                    Name = "dunum",
                    Type = 2
                },
                new MetricUnit()
                {
                    Name = "hektar",
                    Type = 2
                },
                new MetricUnit()
                {
                    Name = "kvadratni metar",
                    Type = 2
                }
            };

            context.MetricUnits.AddRange(metricUnits);

            context.SaveChanges();

            var svgPoints = new List<string>()
            {
                "0.61 211.88 21.28 153.88 41.28 103.21 53.28 108.21 12.28 218.21 0.61 211.88",
                "23.28 231.88 12.28 218.21 53.28 108.21 62.28 82.21 67.61 60.88 69.61 27.54 71.61 12.88 84.61 5.88 79.61 76.21 66.95 120.88 23.28 231.88",
                "33.62 238.21 23.28 231.88 67.52 118.88 79.61 76.21 84.61 5.88 94.61 5.88 96.95 49.21 94.95 74.21 89.28 98.21 82.95 118.88 33.62 238.21",
                "42.62 244.54 33.62 238.21 82.95 118.88 89.28 98.21 94.95 74.21 96.95 49.21 94.61 5.88 104.28 3.54 107.95 28.88 107.95 72.88 104.61 90.21 100.61 105.54 93.61 126.21 84.61 147.88 42.62 244.54",
                "55.62 253.54 42.62 244.54 87.92 139.92 105.28 143.54 55.62 253.54",
                "128.62 1.54 104.28 3.54 107.95 28.88 107.95 72.88 104.61 90.21 100.61 105.54 93.61 126.21 87.92 139.92 105.28 143.54 117.28 106.88 130.62 50.54 128.62 1.54",
                "63.95 259.88 55.62 253.54 83.54 191.69 92.28 199.21 63.95 259.88",
                "130.62 110.88 117.28 106.88 105.28 143.54 83.54 191.69 92.28 199.21 118.95 143.54 130.62 110.88",
                "146.62 9.88 128.62 1.54 137.62 30.54 130.62 50.54 117.28 106.88 130.62 110.88 136.62 87.21 141.28 60.88 144.62 35.21 146.62 9.88",
                "78.28 266.21 63.95 259.88 118.95 143.54 130.62 110.88 136.62 87.21 141.28 60.88 144.62 35.21 145.77 20.58 166.28 22.21 158.62 65.21 144.62 115.54 128.62 158.88 78.28 266.21",
                "83.54 281.54 78.28 266.21 128.62 158.88 144.62 115.54 154.28 118.88 146.62 150.21 83.54 281.54",
                "94.95 287.21 83.54 281.54 146.62 150.21 153.88 118.53 163.62 122.04 155.62 152.54 137.62 200.21 94.95 287.21",
                "176.62 70.54 158.62 65.21 144.62 115.54 164.62 122.04 176.62 70.54",
                "188.95 5.88 169.62 8.88 166.28 22.21 158.62 65.21 176.62 70.54 188.95 5.88",
                "158.62 8.88 169.62 8.88 166.28 22.21 145.77 20.58 146.62 9.88 158.62 8.88",
                "129.62 26.21 128.7 3.54 137.62 30.54 130.95 49.73 129.62 26.21",
                "188.95 5.88 199.95 20.58 190.62 69.21 166.28 158.54 155.62 152.54 176.25 70.54 188.95 5.88",
                "105.28 293.88 94.95 287.21 138.37 198.21 155.62 152.54 166.28 158.54 144.62 215.21 105.28 293.88",
                "117.28 295.21 105.28 293.88 144.62 215.21 166.28 158.54 177.78 163.21 155.62 219.54 117.28 295.21",
                "211.28 18.88 207.28 62.88 177.78 163.21 166.28 158.54 190.62 69.21 199.95 20.58 211.28 18.88",
                "188.95 167.88 203.62 118.88 212.95 76.54 219.95 40.54 223.62 12.21 211.28 18.88 207.28 62.88 177.78 163.21 155.62 219.54 117.28 295.21 128.7 299.54 130.62 301.21 158.62 241.21 173.78 210.21 188.95 167.88",
                "233.95 5.88 231.95 40.54 221.54 28.24 223.62 10.54 233.95 5.88",
                "199.95 171.21 213.28 131.54 219.95 98.54 227.74 70.54 231.95 40.54 221.54 28.24 212.95 76.54 203.62 118.88 188.95 167.88 199.95 171.21",
                "137.62 316.21 130.62 301.21 173.78 210.21 188.95 167.88 199.95 171.21 197.28 188.21 137.62 316.21",
                "157.69 321.21 196.28 236.88 215.95 189.88 197.28 188.21 137.62 316.21 157.69 321.21",
                "250.62 5.88 233.95 5.88 232.08 38.21 227.74 70.54 217.88 108.8 212.11 135.04 199.95 171.21 197.28 188.21 215.95 189.88 227.74 145.88 240.28 97.88 247.28 56.21 250.62 32.21 250.62 5.88"
            };

            var areaMetricUnit = context.MetricUnits.FirstOrDefault(mu => mu.Type == 2);

            var parcels = svgPoints.Select((t, i) => new Parcel()
            {
                GruntId = "GruntId" + (i + 1),
                Name = "Parcela " + (i + 1),
                OwnerName = "Vlasnik" + (i + 1),
                ParcelAreas = new List<ParcelArea>()
                {
                    new ParcelArea()
                    {
                        MetricUnit = areaMetricUnit,
                        Quantity = i + 1
                    }
                },
                Points = t
            }).ToList();

            context.Parcels.AddRange(parcels);

            var crops = new List<Crop>()
            {
                new Crop() {Id = 1, Name = "Kukuruz", Color = "#fa2727", Description = "Opcionalni opis sjemena.", ImageUrl = "http://res.cloudinary.com/laktal-stipanjici/image/upload/v1458645958/kukuruz_qfzohd.jpg"},
                new Crop() {Id = 2, Name = "Jeèam ozimi", Color = "#dce00a", Description = "Opcionalni opis sjemena.",ImageUrl = "http://res.cloudinary.com/laktal-stipanjici/image/upload/v1458645967/jesenjiJecam_lpdle8.jpg"},
                new Crop() {Id = 3, Name = "Jeèam proljetni", Color = "#ffef62", Description = "Opcionalni opis sjemena.",ImageUrl = "http://res.cloudinary.com/laktal-stipanjici/image/upload/v1458645983/proljetniJecam_fudshc.png"},
                new Crop() {Id = 4, Name = "Pšenica ozima", Color = "#874c06", Description = "Opcionalni opis sjemena.",ImageUrl = "http://res.cloudinary.com/laktal-stipanjici/image/upload/v1458645982/psenica_zuzcrf.jpg"},
                new Crop() {Id = 5, Name = "Pšenica proljetna", Color = "#e39f4e", Description = "Opcionalni opis sjemena.",ImageUrl = "http://res.cloudinary.com/laktal-stipanjici/image/upload/v1458645985/psenicaProljetna_dhymrz.jpg"},
                new Crop() {Id = 6, Name = "Zob", Color = "#f703a4", Description = "Opcionalni opis sjemena.",ImageUrl = "http://res.cloudinary.com/laktal-stipanjici/image/upload/v1458645989/zob_znvwmm.jpg"},
                new Crop() {Id = 7, Name = "Grašak", Color = "#2133d1", Description = "Opcionalni opis sjemena.",ImageUrl = "http://res.cloudinary.com/laktal-stipanjici/image/upload/v1458645969/grasak_c9q23f.jpg"},
                new Crop() {Id = 8, Name = "Djetelina lucerka", Color = "#058019", Description = "Opcionalni opis sjemena.",ImageUrl = "http://res.cloudinary.com/laktal-stipanjici/image/upload/v1458645971/djetelinaLucerka_cdinig.jpg"},
                new Crop() {Id = 9, Name = "Livada", Color = "#c31ced", Description = "Opcionalni opis sjemena.",ImageUrl = "http://res.cloudinary.com/laktal-stipanjici/image/upload/v1458645966/livada_hjosal.jpg"}
            };

            context.Crops.AddRange(crops);

            var fertilizers = new List<Fertilizer>()
            {
                new Fertilizer()
                {
                    Name = "Urea",
                    Description = "UREA je kvalitetno dušièno gnojivo, visoke topivosti u vodi koja se može upotrijebiti za folijarnu gnojidbu skoro svih kultura, potrebno je samo paziti na koncentraciju otopine, jer pri visokim koncentracijama može izazvati ožegotine na listu." +
                                "\n\nOsim koncentracije, važno je poznavati i kolièinu biureta u UREA gnojivu. UREA koja sadrži više od 1% biureta (piše na deklaraciji/etiketi pakiranja) nisu prikladne za folijarnu gnojidbu jer izazivaju ožegotine na lišæu poljoprivrednih kultura. Isto tako, ne preporuèa se folijarna primjena UREE u vrijeme cvatnje kultura."
                },
                new Fertilizer()
                {
                    Name = "NPK",
                    Description = "Kompletna gnojiva (èesto se nazivaju potpuna, kompleksna ili NPK gnojiva) pružaju sve bitne " +
                                "sastojke za rast biljaka tako da dodatna gnojiva nisu potrebna."
                },
                new Fertilizer()
                {
                    Name = "Kombinovano",
                    Description = "Miješana gnojiva se dobiju miješanjem odgovarajuæe kolièine pojedinaènih gnojiva."
                }
            };

            context.Fertilizers.AddRange(fertilizers);

            context.SaveChanges();
        }
    }
}
