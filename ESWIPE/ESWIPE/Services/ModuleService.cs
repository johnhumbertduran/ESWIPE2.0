using System;
using System.Collections.Generic;
using System.Text;

using ESWIPE.Models;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Newtonsoft.Json;
using System.Diagnostics;
using SQLite;
using MonkeyCache.FileStore;
using ESWIPE.Views;

namespace ESWIPE.Services
{
    class ModuleService
    {
        static string BaseUrl = "https://eswipewebapi-1-6arxi3ufdq-as.a.run.app";
        //static string BaseUrl = "https://app.swaggerhub.com/apis-docs/johnhumbertduran/eswipe-web_api/v1";
        static HttpClient client;

        static ModuleService()
        {
            try
            {

                client = new HttpClient
                {
                    BaseAddress = new Uri(BaseUrl)
                };
            }
            catch
            {

            }
        }

        public static Task<IEnumerable<Module>> GetModule() =>
            GetAsync<IEnumerable<Module>>("Module", "getmodule");

        static async Task<T> GetAsync<T>(string url, string key, int mins = 5, bool forceRefresh = false)
        {
            var json = string.Empty;

            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
                json = Barrel.Current.Get<string>(key);
            else if (!forceRefresh && !Barrel.Current.IsExpired(key))
                json = Barrel.Current.Get<string>(key);

            try
            {
                if (string.IsNullOrWhiteSpace(json))
                {
                    json = await client.GetStringAsync(url);

                    Barrel.Current.Add(key, json, TimeSpan.FromMinutes(mins));
                }
                return JsonConvert.DeserializeObject<T>(json);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unable to get information from server {ex}");
                throw ex;
            }
        }

        static int nextId = 0;

        public static async Task AddModule(string createdBy, string subjectCode, string subjectQuizCode, string subjectQuizQty)
        {
            var module = new Module
            {
                Id = nextId++,
                CreatedBy = createdBy,
                SubjectCode = subjectCode,
                SubjectQuizCode = subjectQuizCode,
                SubjectQuizQty = subjectQuizQty
            };

            var json = JsonConvert.SerializeObject(module);
            var content =
                new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("Module", content);

            if (!response.IsSuccessStatusCode)
            {

            }
        }

        public static async Task RemoveModule(int id)
        {
            var response = await client.DeleteAsync($"Module{id}");
            if (!response.IsSuccessStatusCode)
            {

            }
        }
    }
}
