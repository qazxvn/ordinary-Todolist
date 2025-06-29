using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Models;

namespace ToDoList.Services
{
    class FileIOService
    {
        private readonly string Path;

        public FileIOService(string path)
        {
            Path = path;
        }

        public BindingList<TodoModel> LoadData()
        {
            var fileExists = File.Exists(Path);
            if (fileExists)
            {
                File.CreateText(Path).Dispose();
                return new BindingList<TodoModel>();
            }
            using(var reader = File.OpenText(Path))
            {
                var fileText = reader.ReadToEnd();
                return JsonConvert.DeserializeObject<BindingList<TodoModel>>(fileText);
            }
        }

        public void SaveData(object dataList)
        {
            using(StreamWriter writer = File.CreateText(Path))
            {
                string output = JsonConvert.SerializeObject(dataList);
                writer.Write(output);
            }
        }
    }
}
