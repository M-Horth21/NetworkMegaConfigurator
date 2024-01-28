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
  class FavoritesStore
  {
    public static event Action OnStoreUpdated = delegate { };

    const string k_folderName = "NetworkMegaConfigurator";
    const string k_fileName = "Favorites.json";

    static string FilePath => Path.Combine(FolderPath, k_fileName);
    static string FolderPath => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), k_folderName);

    static List<ConfigurationModel> _configFavorites = new();

    public static IEnumerable<ConfigurationModel> GetFavorites => _configFavorites;
    public static bool Contains(ConfigurationModel config) => GetFavorites.Contains(config);

    public static void AddFavorite(ConfigurationModel config)
    {
      if (_configFavorites.Contains(config)) return;
      _configFavorites.Add(config);

      OnStoreUpdated.Invoke();
    }

    public static void RemoveFavorite(ConfigurationModel config)
    {
      _configFavorites.Remove(config);
      OnStoreUpdated.Invoke();
    }

    public FavoritesStore()
    {
      _configFavorites.Clear();

      if (!File.Exists(FilePath)) return;

      Load();
      OnStoreUpdated.Invoke();
    }

    static void Load()
    {
      string json = File.ReadAllText(FilePath);
      _configFavorites = JsonConvert.DeserializeObject<List<ConfigurationModel>>(json);
    }

    public void Save()
    {
      string saveJson = JsonConvert.SerializeObject(_configFavorites, Formatting.Indented);
      Directory.CreateDirectory(FolderPath);
      File.WriteAllText(FilePath, saveJson);
    }
  }
}
