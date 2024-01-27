using NetworkMegaConfigurator.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Configuration.Internal;
using System.Windows.Media.Media3D;
using System.Collections.ObjectModel;

namespace NetworkMegaConfigurator.Stores
{
  class RecentsStore
  {
    public static event Action OnStoreUpdated = delegate { };

    const string k_folderName = "NetworkMegaConfigurator";
    const string k_fileName = "RecentActions.json";
    const int k_capacity = 5;

    static string FilePath => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), k_folderName, k_fileName);

    static List<ConfigurationModel> _configs = new();
    public static IEnumerable<ConfigurationModel> Configs => _configs;

    public static bool Contains(ConfigurationModel config) => Configs.Contains(config);

    public static void Add(ConfigurationModel config)
    {
      _configs.Remove(config);

      _configs.Insert(0, config);

      if (_configs.Count > k_capacity) _configs.RemoveAt(_configs.Count - 1);
      OnStoreUpdated.Invoke();
    }

    public RecentsStore()
    {
      _configs.Clear();

      if (!File.Exists(FilePath)) return;

      Load();
      OnStoreUpdated.Invoke();
    }

    static void Load()
    {
      string json = File.ReadAllText(FilePath);
      _configs = JsonConvert.DeserializeObject<List<ConfigurationModel>>(json);
    }

    public void Save()
    {
      string saveJson = JsonConvert.SerializeObject(_configs, Formatting.Indented);
      Directory.CreateDirectory(k_folderName);
      File.WriteAllText(FilePath, saveJson);
    }
  }
}
