using CodingSeb.Localization;
using Newtonsoft.Json;
using System;
using System.IO;
using CodingSeb.Mvvm;

namespace $safeprojectname$
{
    /// <summary>
    /// This class is the serializable root object of the configuration of the application
    /// </summary>
    public sealed class Config : NotifyPropertyChangedBaseClass
    {
        private bool firstInitOK;
        
        private static readonly string fileName = Path.Combine(PathUtils.AppDataPath, "Configuration.json");

        /// <summary>
        /// To memorize the selected language for the application
        /// </summary>
        public string SelectedLanguage
        {
            get => Loc.Instance.CurrentLanguage;
            set
            {
                Loc.Instance.CurrentLanguage = value;
                if (firstInitOK)
                    Save();
            }
        }

        #region Json singleton

        private static Config instance;

        /// <summary>
        /// The single instance of Config
        /// Get the backup of the cofig if it exists
        /// </summary>
        public static Config Instance
        {
            get
            {
                if (instance == null)
                {
                    if (File.Exists(fileName))
                    {
                        try
                        {
                            instance = JsonConvert.DeserializeObject<Config>(File.ReadAllText(fileName));
                        }
                        catch { }
                    }

                    if (instance == null)
                    {
                        instance = new Config();
                        instance.Save();
                    }

                    instance.firstInitOK = true;
                }

                return instance;
            }
        }

        /// <summary>
        /// To save a backup of this config to restore it at the next instance of the application
        /// </summary>
        public void Save()
        {
            Directory.CreateDirectory(Path.GetDirectoryName(fileName));

            try
            {
                File.WriteAllText(fileName, JsonConvert.SerializeObject(this));
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.ToString());
            }
        }

        private Config()
        {
            PropertyChanged += Config_PropertyChanged;
        }

        private void Config_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            Save();
        }

        #endregion

    }
}
