using System;
using System.IO;
using System.Reflection;

namespace $safeprojectname$
{
    /// <summary>
    /// A common place to group io paths relative to the application and io utils functions
    /// </summary>
    public static class PathUtils
    {
        /// <summary>
        /// The directory where the current application started
        /// </summary>
        public static string StartupPath
        {
            get
            {
                return Path.GetDirectoryName((Assembly.GetEntryAssembly() ?? Assembly.GetExecutingAssembly()).Location);
            }
        }

        /// <summary>
        /// The directory of the application in AppData\Roaming
        /// </summary>
        public static string AppDataPath
        {
            get
            {
                return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), Assembly.GetExecutingAssembly().GetName().Name);
            }
        }

        /// <summary>
        /// The directory of the application in AppData\Local
        /// </summary>
        public static string LocalAppDataPath
        {
            get
            {
                return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), Assembly.GetExecutingAssembly().GetName().Name);
            }
        }

        /// <summary>
        /// Search for the specified file in the specified directory if not found go up a directory at a time until it reach the root path.
        /// </summary>
        /// <param name="startDirectory">The first directory where to start searching</param>
        /// <param name="fileName">The file name (without full path) of the file to find</param>
        /// <returns>If found return the directory path where it is found. It return null otherwise.</returns>
        public static string FindFileInDirectoryAncestors(string startDirectory, string fileName)
        {
            if (File.Exists(Path.Combine(startDirectory, fileName)))
            {
                return startDirectory;
            }
            else if (startDirectory.Equals(Path.GetPathRoot(startDirectory)))
            {
                return null;
            }
            else
            {
                return FindFileInDirectoryAncestors(Path.GetDirectoryName(startDirectory), fileName);
            }
        }

        /// <summary>
        /// Clean the specified filename of all invalids characters
        /// </summary>
        /// <param name="fileName">The file name to clean</param>
        /// <param name="replacement">The text to use to replace all invalids characters (by default : "_")</param>
        public static string GetSafeFilename(string filename, string replacement = "_")
        {
            return string.Join(replacement, filename.Split(Path.GetInvalidFileNameChars()));
        }
    }
}
