using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace GameClicker.Services
{
    public class FileManager : IFileManager
    {
        public bool Load<T>(out T objectLoaded)
        {
            objectLoaded = default;

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            openFileDialog.Filter = "GameClicker file (*.gcf)|*.gcf";
            if (openFileDialog.ShowDialog() == true)
            {
                JsonSerializer serializer = new JsonSerializer();
                using (StreamReader sr = new StreamReader(openFileDialog.FileName))
                {
                    using (JsonReader reader = new JsonTextReader(sr))
                    {
                        objectLoaded = serializer.Deserialize<T>(reader);
                        return true;
                    }
                }
            }
            return false;
        }

        public bool Save<T>(T objectToSave)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            saveFileDialog.Filter = "GameClicker file (*.gcf)|*.gcf";
            if (saveFileDialog.ShowDialog() == true)
            {
                JsonSerializer serializer = JsonSerializer.CreateDefault(new JsonSerializerSettings
                {
                    Formatting = Formatting.Indented
                });

                using (StreamWriter sw = new StreamWriter(saveFileDialog.FileName))
                {
                    using (JsonWriter writer = new JsonTextWriter(sw))
                    {
                        serializer.Serialize(writer, objectToSave);
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
