using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;


namespace Database_Hospital_Application.Models.Tools
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Net.Http;
    using System.Threading.Tasks;

        public class CountryCodesLoader
        {
            private readonly HttpClient _httpClient;
            public ObservableCollection<CountryInfo> CountryCodes { get; private set; }

            public CountryCodesLoader()
            {
                _httpClient = new HttpClient();
                CountryCodes = new ObservableCollection<CountryInfo>();
            }

            public async Task LoadCountryCodesAsync()
            {
                try
                {
                    HttpResponseMessage response = await _httpClient.GetAsync("https://restcountries.com/v3.1/all");

                    if (response.IsSuccessStatusCode)
                    {
                        string jsonResponse = await response.Content.ReadAsStringAsync();
                        var countries = JsonConvert.DeserializeObject<List<CountryInfo>>(jsonResponse);

                        foreach (var country in countries)
                        {
                            if (!string.IsNullOrWhiteSpace(country.Cca3))
                            {
                                CountryCodes.Add(new CountryInfo
                                {
                                    Cca3 = country.Cca3,
                                    OfficialName = country.Translations?.ces?.official ?? country.Name?.Official
                            });
                            }
                        }
                        SortCountryCodesByName();
                    }
                    else
                    {
                        Console.WriteLine("Chyba: " + response.StatusCode);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Chyba: " + ex.Message);
                }
            }

        private void SortCountryCodesByName()
        {
            var sortedCountries = CountryCodes.OrderBy(c => c.OfficialName).ToList();
            CountryCodes.Clear();
            foreach (var country in sortedCountries)
            {
                CountryCodes.Add(country);
            }
        }

    }

        public class CountryInfo
        {
            public string Cca3 { get; set; }
            public string OfficialName { get; set; }
            public Translations Translations { get; set; } 
            public Name Name { get; set; }
        }

        public class Translations
        {
            public Ces ces { get; set; } 
        }

        public class Name
        {
            public string Official { get; set; } 
        }

        public class Ces
        {
            public string official { get; set; } 
        }

}


