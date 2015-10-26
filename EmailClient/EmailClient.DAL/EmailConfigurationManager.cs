using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmailClient.Data;
using Newtonsoft.Json;

namespace EmailClient.DAL
{
    public class EmailConfigurationManager
    {
        private static readonly string _pathToConfigurationFile = 
            AppDomain.CurrentDomain.BaseDirectory + @"bin\EmailConfiguration.json";    
     
        private static EmailConfigurationManager _instance = null;
        private static object _lockSaveObj = new object();

        private JsonSerializer _serializer;
        private ConcurrentDictionary<string, EmailProvider> _emailProviders; 

        private EmailConfigurationManager()
        {
            using (StreamReader file = File.OpenText(_pathToConfigurationFile))
            {
                _serializer = new JsonSerializer();
                _emailProviders =
                    _serializer.Deserialize<ConcurrentDictionary<string, EmailProvider>>(new JsonTextReader(file));
            }
        }

        public static EmailConfigurationManager Manager
        {
            get
            {
                if (_instance == null)
                    _instance = new EmailConfigurationManager();
                return _instance;
            }
        }

        public EmailProvider this[string provider]
        {
            get
            {
                return _emailProviders[provider];
            }

            set
            {
                if(_emailProviders.TryAdd(provider, value))
                    lock (_lockSaveObj)
                        SaveProviders();              
            }
        }

        public bool ContainKey(string key)
        {
            return _emailProviders.ContainsKey(key);
        }

        private void SaveProviders()
        {
            using (FileStream fs = File.Open(_pathToConfigurationFile, FileMode.Open))
            using (StreamWriter sw = new StreamWriter(fs))
            using (JsonWriter jw = new JsonTextWriter(sw))
            {
                jw.Formatting = Formatting.Indented;
                _serializer.Serialize(jw, _emailProviders);
            }
        }
    }
}
