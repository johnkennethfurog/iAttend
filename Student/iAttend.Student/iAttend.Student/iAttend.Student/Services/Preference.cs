using iAttend.Student.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

namespace iAttend.Student.Services
{
    class Preference : IPreferences
    {
        public void Clear()
        {
            Preferences.Clear();
        }

        public string Get(string key)
        {
            var myValue = Preferences.Get(key, string.Empty);
            return myValue;
        }

        public T Get<T>(string key)
        {
            var json = Preferences.Get(key, string.Empty);

            if (!string.IsNullOrEmpty(json))
                return JsonConvert.DeserializeObject<T>(json);
            else
                return default;
        }

        public void Remove(string key)
        {
            Preferences.Remove(key);
        }

        public void Set(string key, string value)
        {
            Preferences.Set(key, value);
        }

        public void Set<T>(string key, T value)
        {
            var json = JsonConvert.SerializeObject(value);
            Preferences.Set(key, json);
        }
    }
}
