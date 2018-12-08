using System;
using System.Collections.Generic;
using System.Text;

namespace iAttend.Student.Interfaces
{
    public interface IPreferences
    {
        T Get<T>(string key);
        string Get(string key);

        void Set(string key, string value);
        void Set<T>(string key, T value);

        void Remove(string key);
        void Clear();
    }
}
