using System;
using System.Collections.Generic;
public class CustomItemDataGenerator {
    const long trillionMultiplier = 1000000000000;

    const double totalExportUSA = 2.377;
    const double totalExportGermany = 2.004;
    const double totalExportFrance = 0.969;
    const double totalExportItaly = 0.6873;
    const double totalExportUK = 0.9019;
    const double totalExportCanada = 0.6188;
    const double totalExportChina = 2.49;
    const double totalExportAustralia = 0.4046;
    const double totalExportMexico = 0.49159;
    const double totalExportJapan = 1.084;
    const double totalExportBrazil = 0.2914;

    public static List<Export> GetExportData() {
        return new List<Export>() {
                new Export("United States", "Canada", totalExportUSA, 0.17),
                new Export("United States", "Mexico", totalExportUSA, 0.16),
                new Export("United States", "China", totalExportUSA, 0.07),
                new Export("United States", "Japan", totalExportUSA, 0.05),

                new Export("Germany", "United States", totalExportGermany, 0.09),
                new Export("Germany", "France", totalExportGermany, 0.08),
                new Export("Germany", "China", totalExportGermany, 0.07),
                new Export("Germany", "Netherlands", totalExportGermany, 0.06),
                new Export("Germany", "United Kingdom", totalExportGermany, 0.06),
                new Export("Germany", "Italy", totalExportGermany, 0.05),
                new Export("Germany", "Poland", totalExportGermany, 0.05),
                new Export("Germany", "Austria", totalExportGermany, 0.05),

                new Export("France", "Germany", totalExportFrance, 0.14),
                new Export("France", "United States", totalExportFrance, 0.08),
                new Export("France", "Italy", totalExportFrance, 0.07),
                new Export("France", "Spain", totalExportFrance, 0.07),
                new Export("France", "Belgium", totalExportFrance, 0.07),
                new Export("France", "United Kingdom", totalExportFrance, 0.07),

                new Export("Italy", "Germany", totalExportItaly, 0.12),
                new Export("Italy", "France", totalExportItaly, 0.11),
                new Export("Italy", "United States", totalExportItaly, 0.10),
                new Export("Italy", "United Kingdom", totalExportItaly, 0.05),
                new Export("Italy", "Spain", totalExportItaly, 0.05),
                new Export("Italy", "Switzerland", totalExportItaly, 0.05),

                new Export("United Kingdom", "United States", totalExportUK, 0.15),
                new Export("United Kingdom", "Germany", totalExportUK, 0.10),
                new Export("United Kingdom", "China", totalExportUK, 0.07),
                new Export("United Kingdom", "Netherlands", totalExportUK, 0.07),
                new Export("United Kingdom", "France", totalExportUK, 0.07),
                new Export("United Kingdom", "Ireland", totalExportUK, 0.06),

                new Export("Canada", "United States", totalExportCanada, 0.73),

                new Export("China", "United States", totalExportChina, 0.17),
                new Export("China", "Hong Kong", totalExportChina, 0.10),
                new Export("China", "Japan", totalExportChina, 0.06),

                new Export("Australia", "China", totalExportAustralia, 0.39),
                new Export("Australia", "Japan", totalExportAustralia, 0.15),
                new Export("Australia", "South Korea", totalExportAustralia, 0.07),
                new Export("Australia", "India", totalExportAustralia, 0.05),

                new Export("Mexico", "United States", totalExportMexico, 0.75),

                new Export("Japan", "United States", totalExportJapan, 0.19),
                new Export("Japan", "China", totalExportJapan, 0.18),
                new Export("Japan", "South Korea", totalExportJapan, 0.06),
                new Export("Japan", "Taiwan", totalExportJapan, 0.06),

                new Export("Brazil", "China", totalExportBrazil, 0.28),
                new Export("Brazil", "United States", totalExportBrazil, 0.13),
            };
    }
    public static List<CountryInformation> GetContinentData() {
        return new List<CountryInformation>() {
            new CountryInformation("North America", "United States", "Washington", 38.8951, -77.0364),
            new CountryInformation("North America", "Canada", "Ottawa", 45.424721, -75.695000),
            new CountryInformation("North America", "Mexico", "Mexico City", 19.432608, -99.133209),

            new CountryInformation("Asia", "China","Beijing", 39.916668, 116.383331),
            new CountryInformation("Asia", "Hong Kong","Hong Kong", 22.302711,114.177216),
            new CountryInformation("Asia","Japan", "Tokyo", 35.652832,139.839478),
            new CountryInformation("Asia","South Korea", "Seoul", 37.532600,127.024612),
            new CountryInformation("Asia","Taiwan", "Taipei", 25.033964,121.564468),
            new CountryInformation("Asia","India", "New Delhi", 28.644800,77.216721),

            new CountryInformation("South America", "Brazil","Brasilia", -15.793889,-47.882778),

            new CountryInformation("Australia","Australia", "Canberra",-35.282001,149.128998),

            new CountryInformation("Europe","Netherlands","Amsterdam", 52.377956,4.897070),
            new CountryInformation("Europe","Germany","Berlin",  52.520008,13.381777),
            new CountryInformation("Europe","United Kingdom","London", 51.509865,-0.118092),
            new CountryInformation("Europe","Italy", "Rome",  41.902782,12.496366),
            new CountryInformation("Europe","France", "Paris", 48.864716,2.349014),
            new CountryInformation("Europe","Spain","Madrid", 40.416775,-3.703790),
            new CountryInformation("Europe","Poland","Warsaw ", 52.237049,21.017532),
            new CountryInformation("Europe","Austria","Vienna", 48.210033,16.363449),
            new CountryInformation("Europe","Belgium","Brussels", 50.8505,4.3488),
            new CountryInformation("Europe","Ireland","Dublin", 53.350140, -6.266155),
        };
    }
    public class Export {
        public string Exporter { get; set; }
        public string Importer { get; set; }
        public decimal Sum { get; set; }

        public Export(string exporter, string importer, double exporterTotalExportValue, double shareOfExports) {
            this.Exporter = exporter;
            this.Importer = importer;
            this.Sum = Convert.ToDecimal(exporterTotalExportValue * trillionMultiplier * shareOfExports);
        }
    }
    public class CountryInformation {
        public string Country { get; set; }
        public string Continent { get; set; }
        public string Capital { get; set; }
        public GeoLocation CapitalLocation { get; set; }

        public class GeoLocation {
            public double Latitude { get; set; }
            public double Longitude { get; set; }

            public GeoLocation(double latitude, double longitude) {
                this.Latitude = latitude;
                this.Longitude = longitude;
            }
        }
        public CountryInformation(string continent, string country, string capital, double capitalLatitude, double capitalLongitude) {
            this.Country = country;
            this.Continent = continent;
            this.Capital = capital;
            this.CapitalLocation = new GeoLocation(capitalLatitude, capitalLongitude);
        }
    }
}
