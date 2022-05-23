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
    class StudentService
    {
        static string BaseUrl = "https://eswipewebapi-1-6arxi3ufdq-as.a.run.app";
        static HttpClient client;

        static StudentService()
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

        public static Task<IEnumerable<Student>> GetStudent() =>
            GetAsync<IEnumerable<Student>>("Student", "getstudent");

        static async Task<T> GetAsync<T>(string url, string key, int mins = 1, bool forceRefresh = false)
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
        static int nextStudentNumber = 20220000;

        public static async Task AddStudent(string studentName, string studentYear, string studentSection, string username, string password, string userRole)
        {
            var student = new Student
            {
                Id = nextId++,
                StudentNumber = nextStudentNumber++,
                Name = studentName,
                Year = studentYear,
                Section = studentSection,
                Username = username,
                Password = nextStudentNumber.ToString(),
                UserRole = userRole
            };

            var json = JsonConvert.SerializeObject(student);
            var content =
                new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("Student", content);

            if (!response.IsSuccessStatusCode)
            {

            }
        }

        public static async Task RemoveStudent(int id)
        {
            var response = await client.DeleteAsync($"Student{id}");
            if (!response.IsSuccessStatusCode)
            {

            }
        }

    }
}
