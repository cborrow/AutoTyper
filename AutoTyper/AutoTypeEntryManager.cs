using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using Newtonsoft.Json;

namespace AutoTyper
{
    public class AutoTypeEntryManager : Collection<AutoTypeEntry>
    {
        static string entriesDataPath = Path.Combine(Environment.CurrentDirectory, "Entries.db");

        static AutoTypeEntryManager instance;
        public static AutoTypeEntryManager Instance
        {
            get
            {
                if (instance == null)
                    instance = Load();
                return instance;
            }
        }

        public void Init()
        {

        }

        public static AutoTypeEntryManager FromFile(string path)
        {
            if (!File.Exists(path))
                return new AutoTypeEntryManager();

            string data = File.ReadAllText(path);
            AutoTypeEntryManager entryManager = JsonConvert.DeserializeObject<AutoTypeEntryManager>(data);
            return entryManager;
        }

        public static AutoTypeEntryManager Load()
        {
            return AutoTypeEntryManager.FromFile(entriesDataPath);
        }

        public void Save()
        {
            string json = JsonConvert.SerializeObject(this);
            File.WriteAllText(entriesDataPath, json);
        }
    }
}
