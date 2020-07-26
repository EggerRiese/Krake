using System;
using System.Collections.Generic;
using System.Text;
using Krake.Object;
using System.Net.Http;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Newtonsoft.Json;
using System.Diagnostics;

namespace Krake.Network
{
    public class Connection
    {
        private HttpClient client;
        List<string> cityList = new List<string>();

        public Connection()
        {
            client = new HttpClient
            {
                //BaseAddress = new Uri("http://10.0.2.2:53426/")        // DEBUG
                BaseAddress = new Uri("https://www.krake-party.de/") // RELEASE
            };
            //TestConnectionAsync();
        }
        //http://s789101103.online.de/<<<yyyyyyy<y
        //http://www.krake-party.de/
        //HTTP Commands

        public async Task<bool> TestConnectionAsync(){
            
            var t = await client.GetStringAsync($"api/Event/test/connection");
            
            if (t.Equals("yes"))
            {
                return true;
            }else
            {
                return false;
            }
        }

        //gets the eventList at App start
        public async Task<List<Event>> GetEvent(string type, bool forceRefresh = false)
        {
            if (forceRefresh)
            {
                try
                {
                    var response = await client.GetStringAsync($"api/{type}/{Preferences.Get("City", "Oldenburg")}");
                    return await Task.Run(() => JsonConvert.DeserializeObject<List<Event>>(response));                    
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e);
                    return null;
                }
            }
            return null;
        }

        public async Task<List<Object.Location>> GetLocation(bool forceRefresh = false)
        {
            if (forceRefresh)
            {
                try
                {
                    var response = await client.GetStringAsync($"api/Location/{Preferences.Get("City", "Oldenburg")}");
                    return await Task.Run(() => JsonConvert.DeserializeObject<List<Object.Location>>(response));
                }
                catch (Exception) 
                {                    
                    return null;
                }
            }
            return null;
        }

        public async Task<List<Event>> GetEventsFromLocation(string locationName, bool forceRefresh = false)
        {
            if (forceRefresh)
            {
                try
                {
                    var response = await client.GetStringAsync($"api/Event/Location/{locationName}/{Preferences.Get("City", "Oldenburg")}");
                    return await Task.Run(() => JsonConvert.DeserializeObject<List<Event>>(response));
                }
                catch (Exception)
                {
                    return null;
                }
            }
            return null;
        }

        public async Task<List<string>> GetAllCitys(bool forceRefresh = false)
        {
            if (forceRefresh)
            {
                try
                {
                    var response = await client.GetStringAsync($"api/Location/All/Citys");
                    return await Task.Run(() => JsonConvert.DeserializeObject<List<string>>(response));
                }
                catch (Exception)
                {

                    return null;
                }                
            }
            return null;
        }

        //UWP Code
        public async Task<bool> AddEvent(Event e)
        {
            if (e == null)
                return false;

            var serializedItem = JsonConvert.SerializeObject(e);

            var response = await client.PostAsync($"api/Event", new StringContent(serializedItem, Encoding.UTF8, "application/json"));

            return response.IsSuccessStatusCode;
        }

        //UWP Code
        public async Task<bool> AddLocation(Event l)
        {
            if (l == null)
                return false;

            var serializedItem = JsonConvert.SerializeObject(l);

            var response = await client.PostAsync($"api/Location", new StringContent(serializedItem, Encoding.UTF8, "application/json"));

            return response.IsSuccessStatusCode;
        }

        //UWP Code
        public async Task<List<string>> GetExampleImages(bool forceRefresh = false)
        {
            List<string> list = new List<string>();
            if (forceRefresh)
            {
                var e = await client.GetStringAsync($"api/Upload"); //vom server event holen
                list = await Task.Run(() => JsonConvert.DeserializeObject<List<string>>(e));
            }
            return list;
        }


        public async Task<bool> DeleteItemAsync(Event e)
        {
            if (e.Equals(null))
            {
                return false;
            }

            var serializedItem = JsonConvert.SerializeObject(e);

            var response = await client.PostAsync($"api/Event/Remove", new StringContent(serializedItem, Encoding.UTF8, "application/json"));

            return response.IsSuccessStatusCode;

        }

        public async Task<bool> DeleteLocationAsync(Event e)
        {
            if (e.Equals(null))
            {
                return false;
            }

            var serializedItem = JsonConvert.SerializeObject(e);

            var response = await client.PostAsync($"api/Location/Remove", new StringContent(serializedItem, Encoding.UTF8, "application/json"));

            return response.IsSuccessStatusCode;
        }
    }
}
