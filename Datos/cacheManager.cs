using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public static class cacheManager
    {
        private static Dictionary<string, object> cache = new Dictionary<string, object>();
        private static DateTime cacheExpiration = DateTime.Now;

        // Método para agregar o actualizar un objeto en el caché
        public static void Set(string key, object data, TimeSpan duration)
        {
            cache[key] = data;
            cacheExpiration = DateTime.Now.Add(duration); // Duración del caché
        }

        // Método para obtener un objeto del caché
        public static T Get<T>(string key)
        {
            if (cache.ContainsKey(key) && DateTime.Now <= cacheExpiration)
            {
                return (T)cache[key];
            }
            else
            {
                return default;
            }
        }
        // Método para invalidar el caché
        public static void Invalidate(string key)
        {
            if (cache.ContainsKey(key))
            {
                cache.Remove(key);
            }
        }
    }
}
