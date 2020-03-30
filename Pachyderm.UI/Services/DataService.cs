using LiteDB;
using Pachyderm.UI.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Linq;

namespace Pachyderm.UI.Services
{
    public class DataService
    {
        private readonly ConfigurationService _configurationService;

        private LiteDatabase _database;
        private MemoryStream _inMemoryDatabase;

        public DataService(ConfigurationService configurationService)
        {
            _configurationService = configurationService;
            var path = _configurationService.Get("DatabasePath");
            OpenDatabase(path);
        }

        ~DataService()
        {
            _database?.Dispose();
        }

        private LiteDatabase OpenDatabase(string path, bool inMemory = false)
        {
            _database?.Dispose();

            if (inMemory)
            {
                _inMemoryDatabase = new MemoryStream();
                _database = new LiteDatabase(_inMemoryDatabase);
            }
            else
            {
                _database = new LiteDatabase(path);
            }

            return _database;
        }

        public void Save<T>(IEnumerable<T> items) where T : Model
        {
            var collection = _database.GetCollection<T>();
            collection.Upsert(items);
            collection.EnsureIndex(x => x.Title);
        }

        public IEnumerable<T> GetAll<T>() where T : Model
        {
            var collection = _database.GetCollection<T>();
            return collection.FindAll();
        }

        public IEnumerable<T> GetByTitle<T>(string title) where T : Model
        {
            var collection = _database.GetCollection<T>();
            return collection.Find(Query.Contains("Title", title));
        }


        // =========== FILE STORAGE ==============

        public void SaveFile(Stream stream, string name, string path = "$/")
        {
            ValidatePath(path);

            var fs = _database.FileStorage;
            fs.Upload($"{path}{name}", name, stream);
        }

        public void DeleteFile(string name, string path = "$/")
        {
            ValidatePath(path);

            var fs = _database.FileStorage;
            fs.Delete($"{path}{name}");
        }

        public Stream GetFile(string name, string path = "$/")
        {
            ValidatePath(path);

            var fs = _database.FileStorage;
            var info = fs.FindById($"{path}{name}");
            return info.OpenRead();
        }

        public List<FileInfo> FindFiles(string path)
        {
            ValidatePath(path);

            var fs = _database.FileStorage;
            var infos = fs.Find(path);

            return infos.Select(x => new FileInfo { 
                Name = x.Filename,
                Path = x.Id.EndsWith(x.Filename) ? x.Id.Substring(0, x.Id.Length - x.Filename.Length) : x.Id,
                Size = x.Length
            }).ToList();
        }

        private void ValidatePath(string path)
        {
            var validates = Regex.IsMatch(path, @"^\$\/(([A-z0-9\-\%]+\/)+$)?");

            if (!validates)
                throw new FormatException("Path parameter must be in the '$/path/to/resource/' format.");
        }

        public class FileInfo
        {
            public string Name { get; set; }
            public string Path { get; set; }
            public long Size { get; set; }
        }


        // ========= TEST ===============

        public async Task PopulateTestData()
        {
            var json = await File.ReadAllTextAsync("wwwroot/sample-data/movies.json");
            var movies = System.Text.Json.JsonSerializer.Deserialize<MovieModel[]>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            Save(movies);
        }

    }
}
