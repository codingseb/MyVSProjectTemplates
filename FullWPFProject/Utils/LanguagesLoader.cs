using CodingSeb.Localization.Loaders;
using System;
using System.IO;
using System.Windows;

namespace $safeprojectname$
{
    /// <summary>
    /// A common place to initialize the Localization of the application.
    /// </summary>
    public static class LanguagesLoader
    {
        /// <summary>
        /// The directory where to find languages (localization) files.
        /// By Default : <c>Path.Combine(PathUtils.StartupPath, "lang")</c>
        /// </summary>
        public static string LanguagesDirectoryPath { get; set; } = Path.Combine(PathUtils.StartupPath, "lang");

        /// <summary>
        /// Initialize the Localization of the application.
        /// Define supported translations files
        /// Load translations files
        /// </summary>
        public static void Init()
        {
            try
            {
                LocalizationLoader.Instance.FileLanguageLoaders.Add(new JsonFileLoader());

                LocalizationLoader.Instance.AddDirectory(LanguagesDirectoryPath);
            }
            catch (Exception exception)
            {
                MessageBox.Show($"Can't load languages files :\r\n{exception}", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
