using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Web.Configuration;
using System.Drawing;
using System.Net;
using System.Collections.Specialized;
using System.Web.Http.Results;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using airbornefrs.Data.EcommerceEF;

namespace airbornefrs.Models
{
    public class GCServiceModels
    {
        //db_GreatCircleEntities OME = new db_GreatCircleEntities();
        db_AirborneEntities OME = new db_AirborneEntities();
        //public class TestModel
        //{
        //    public long page { get; set; }
        //    public long total { get; set; }
        //    public long records { get; set; }
        //    public IList<AirportModel> airportModelList { get; set; }
        //}

        public class AirportModel
        {

            public long id { get; set; }
            public string ident { get; set; }
            public string type { get; set; }
            public string name { get; set; }
            public string latitude_deg { get; set; }
            public string longitude_deg { get; set; }
            public string elevation_ft { get; set; }
            public string continent { get; set; }
            public string iso_country { get; set; }
            public string iso_region { get; set; }
            public string municipality { get; set; }
            public string scheduled_service { get; set; }
            public string gps_code { get; set; }
            public string iata_code { get; set; }
            public string local_code { get; set; }
            public string home_link { get; set; }
            public string wikipedia_link { get; set; }
            public string keywords { get; set; }
            public string country { get; set; }
            public string AirportRegion { get; set; }
            public IList<runwayModel> runwayModelList { get; set; }
            public IList<airportfrequenciesModel> frequenciesModelList { get; set; }
            public IList<navaidModel> navaidModelList { get; set; }
        }

        public class runwayModel
        {
            public long id { get; set; }
            public Nullable<long> airport_ref { get; set; }
            public string airport_ident { get; set; }
            public string length_ft { get; set; }
            public string width_ft { get; set; }
            public string surface { get; set; }
            public string lighted { get; set; }
            public string closed { get; set; }
            public string le_ident { get; set; }
            public string le_latitude_deg { get; set; }
            public string le_longitude_deg { get; set; }
            public string le_elevation_ft { get; set; }
            public string le_heading_degT { get; set; }
            public string le_displaced_threshold_ft { get; set; }
            public string he_ident { get; set; }
            public string he_latitude_deg { get; set; }
            public string he_longitude_deg { get; set; }
            public string he_elevation_ft { get; set; }
            public string he_heading_degT { get; set; }
            public string he_displaced_threshold_ft { get; set; }
        }

        public class airportfrequenciesModel
        {
            public long id { get; set; }
            public Nullable<long> airport_ref { get; set; }
            public string airport_ident { get; set; }
            public string type { get; set; }
            public string description { get; set; }
            public string frequency_mhz { get; set; }

        }

        public class navaidModel
        {
            public long id { get; set; }
            public string filename { get; set; }
            public string ident { get; set; }
            public string name { get; set; }
            public string type { get; set; }
            public string frequency_khz { get; set; }
            public string latitude_deg { get; set; }
            public string longitude_deg { get; set; }
            public string elevation_ft { get; set; }
            public string iso_country { get; set; }
            public string dme_frequency_khz { get; set; }
            public string dme_channel { get; set; }
            public string dme_latitude_deg { get; set; }
            public string dme_longitude_deg { get; set; }
            public string dme_elevation_ft { get; set; }
            public string slaved_variation_deg { get; set; }
            public string magnetic_variation_deg { get; set; }
            public string usageType { get; set; }
            public string power { get; set; }
            public string associated_airport { get; set; }
        }

        public List<AirportModel> GetAirportList(string _aiportname)
        {
            var query = OME.airports.Where(x => x.name.ToLower().StartsWith(_aiportname.ToLower())
            || x.iata_code.StartsWith(_aiportname.ToUpper())
             || x.ident.StartsWith(_aiportname.ToUpper())
            ).OrderBy(x => x.name);

            List<AirportModel> AirportList = new List<AirportModel>();
            query.ToList().ForEach(x =>
            {
                AirportList.Add(new AirportModel
                {
                    id = x.id,
                    ident = x.ident,
                    type = x.type,
                    name = x.name + " (" + x.ident + " | " + x.iata_code + ")",
                    latitude_deg = x.latitude_deg,
                    longitude_deg = x.longitude_deg,
                    elevation_ft = x.elevation_ft,
                    continent = x.continent,
                    iso_country = x.iso_country,
                    iso_region = x.iso_region,
                    municipality = x.municipality,
                    scheduled_service = x.scheduled_service,
                    gps_code = x.gps_code,
                    iata_code = x.iata_code,
                    local_code = x.local_code,
                    home_link = x.home_link,
                    wikipedia_link = x.wikipedia_link,
                    keywords = x.keywords,
                });
            });
            return AirportList;
        }


        //public string GetAirportList2(string _aiportname)
        //{
        //    var query = OME.airports.Where(x => x.name.ToLower().StartsWith(_aiportname.ToLower())
        //    || x.iata_code.StartsWith(_aiportname.ToUpper())
        //     || x.ident.StartsWith(_aiportname.ToUpper())
        //    ).OrderBy(x => x.name);

        //    List<AirportModel> AirportList = new List<AirportModel>();
        //    query.ToList().ForEach(x =>
        //    {
        //        AirportList.Add(new AirportModel
        //        {
        //            id = x.id,
        //            ident = x.ident,
        //            type = x.type,
        //            name = x.name + " (" + x.ident + " | " + x.iata_code + ")",
        //            latitude_deg = x.latitude_deg,
        //            longitude_deg = x.longitude_deg,
        //            elevation_ft = x.elevation_ft,
        //            continent = x.continent,
        //            iso_country = x.iso_country,
        //            iso_region = x.iso_region,
        //            municipality = x.municipality,
        //            scheduled_service = x.scheduled_service,
        //            gps_code = x.gps_code,
        //            iata_code = x.iata_code,
        //            local_code = x.local_code,
        //            home_link = x.home_link,
        //            wikipedia_link = x.wikipedia_link,
        //            keywords = x.keywords,
        //        });
        //    });

        //    TestModel[] Test = new TestModel[] {
        //    new TestModel()
        //    {
        //        page=query.Count(),
        //        total=query.Count(),
        //        records=query.Count(),
        //        airportModelList=AirportList
        //    }
        //};

        //    return new JavaScriptSerializer().Serialize(Test);
        //}


        public AirportModel GetAirportData(string _aiportname)
        {
            //var query = (from a in OME.airports select a).Distinct();
            //linq join in multiple parameters
            //join af in OME.airport_frequencies on new { oId = a.ident, sId = a.iata_code } equals new { oId = af.airport_ident, sId = af.type} into frequencyList
            AirportModel AirportData = (from a in OME.airports
                                        join af in OME.airport_frequencies on a.ident equals af.airport_ident into frequencyList
                                        join runw in OME.runways on a.ident equals runw.airport_ident into runwayList
                                        join nvaids in OME.navaids on a.ident equals nvaids.associated_airport into navaidsList
                                        where a.iata_code.Trim().ToLower() == _aiportname.ToLower() || a.ident.Trim().ToLower() == _aiportname.ToLower()
                                        select new AirportModel
                                        {
                                            id = a.id,
                                            ident = a.ident,
                                            type = a.type,
                                            name = a.name + " (" + a.ident + " | " + a.iata_code + ")",
                                            latitude_deg = a.latitude_deg,
                                            longitude_deg = a.longitude_deg,
                                            elevation_ft = a.elevation_ft,
                                            continent = a.continent,
                                            iso_country = a.iso_country,
                                            iso_region = a.iso_region,
                                            municipality = a.municipality,
                                            scheduled_service = a.scheduled_service,
                                            gps_code = a.gps_code,
                                            iata_code = a.iata_code,
                                            local_code = a.local_code,
                                            home_link = a.home_link,
                                            wikipedia_link = a.wikipedia_link,
                                            keywords = a.keywords,
                                            country = OME.countries.Where(c => c.code != null && c.code == a.iso_country).Select(x => x.name).FirstOrDefault(),
                                            AirportRegion = OME.regions.Where(r => r.code != null && r.code == a.iso_region).Select(x => x.name).FirstOrDefault(),
                                            frequenciesModelList = frequencyList.Select(x => new airportfrequenciesModel
                                            {
                                                id = x.id,
                                                airport_ref = x.airport_ref,
                                                airport_ident = x.airport_ident ?? "N/A",
                                                type = x.type ?? "N/A",
                                                description = x.description ?? "N/A",
                                                frequency_mhz = x.frequency_mhz ?? "N/A"
                                            }).ToList(),
                                            runwayModelList = runwayList.Select(y => new runwayModel
                                            {
                                                id = y.id,
                                                airport_ref = y.airport_ref,
                                                airport_ident = y.airport_ident ?? "N/A",
                                                length_ft = y.length_ft ?? "N/A",
                                                width_ft = y.width_ft ?? "N/A",
                                                surface = y.surface ?? "N/A",
                                                lighted = y.lighted ?? "N/A",
                                                closed = y.closed ?? "N/A",
                                                le_ident = y.le_ident ?? "N/A",
                                                le_latitude_deg = y.le_latitude_deg ?? "N/A",
                                                le_longitude_deg = y.le_longitude_deg ?? "N/A",
                                                le_elevation_ft = y.le_elevation_ft ?? "N/A",
                                                le_heading_degT = y.le_heading_degT ?? "N/A",
                                                le_displaced_threshold_ft = y.le_displaced_threshold_ft ?? "N/A",
                                                he_ident = y.he_ident ?? "N/A",
                                                he_latitude_deg = y.he_latitude_deg ?? "N/A",
                                                he_longitude_deg = y.he_longitude_deg ?? "N/A",
                                                he_elevation_ft = y.he_elevation_ft ?? "N/A",
                                                he_heading_degT = y.he_heading_degT ?? "N/A",
                                                he_displaced_threshold_ft = y.he_displaced_threshold_ft ?? "N/A"

                                            }).ToList(),
                                            navaidModelList = navaidsList.Select(z => new navaidModel
                                            {
                                                id = z.id,
                                                filename = z.filename ?? "N/A",
                                                ident = z.ident ?? "N/A",
                                                name = z.name ?? "N/A",
                                                type = z.type ?? "N/A",
                                                frequency_khz = z.frequency_khz ?? "N/A",
                                                latitude_deg = z.latitude_deg ?? "N/A",
                                                longitude_deg = z.longitude_deg ?? "N/A",
                                                elevation_ft = z.elevation_ft ?? "N/A",
                                                iso_country = z.iso_country ?? "N/A",
                                                dme_frequency_khz = z.dme_frequency_khz ?? "N/A",
                                                dme_channel = z.dme_channel ?? "N/A",
                                                dme_latitude_deg = z.dme_latitude_deg ?? "N/A",
                                                dme_longitude_deg = z.dme_longitude_deg ?? "N/A",
                                                dme_elevation_ft = z.dme_elevation_ft ?? "N/A",
                                                slaved_variation_deg = z.slaved_variation_deg ?? "N/A",
                                                magnetic_variation_deg = z.magnetic_variation_deg ?? "N/A",
                                                usageType = z.usageType ?? "N/A",
                                                power = z.power ?? "N/A",
                                                associated_airport = z.associated_airport ?? "N/A"

                                            }).ToList(),

                                            //runwayModelList = a.runways.Where(x => x.airport_ident == a.ident).ToList().Select(x => new runwayModel {
                                            //}).ToList(),

                                            //frequenciesModelList=a.airport_frequencies.Where(y=>y.airport_ident==a.ident).ToList().Select(y=>new airportfrequenciesModel {
                                            //}).ToList(),

                                        }).FirstOrDefault();

            return AirportData;
        }

        //public  string GetAirportIACOCode(string _aiportname)
        //{
        //    string result = "";
        //    using (WebClient client = new WebClient())
        //    {
        //        byte[] response =
        //        //client.UploadValues("http://iatageo.com/getCode/28.56649971/77.10310364", new NameValueCollection()
        //        client.UploadValues("http://iatageo.com/getCode/"+ _aiportname, new NameValueCollection()
        //        {

        //        });
        //        result = System.Text.Encoding.UTF8.GetString(response);
        //    }
        //    return result;
        //}

        public AirportModel GetAirportIACOCode(string _aiportlat, string _airportlong)
        {
            AirportModel data = OME.airports.Where(x => x.latitude_deg == _aiportlat && x.longitude_deg == _airportlong).Select(x => new AirportModel
            {
                id = x.id,
                ident = x.ident,
                type = x.type,
                name = x.name + " (" + x.ident + " | " + x.iata_code + ")",
                latitude_deg = x.latitude_deg,
                longitude_deg = x.longitude_deg,
                elevation_ft = x.elevation_ft,
                continent = x.continent,
                iso_country = x.iso_country,
                iso_region = x.iso_region,
                municipality = x.municipality,
                scheduled_service = x.scheduled_service,
                gps_code = x.gps_code,
                iata_code = x.iata_code,
                local_code = x.local_code,
                home_link = x.home_link,
                wikipedia_link = x.wikipedia_link,
                keywords = x.keywords,
            }).FirstOrDefault();
            return data;
        }


    }
}