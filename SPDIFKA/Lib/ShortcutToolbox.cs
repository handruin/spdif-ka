using IWshRuntimeLibrary;
using System;
using System.IO;
using File = System.IO.File;

namespace SPDIFKA.Lib {
    public static class ShortcutToolbox {
        /// <summary>
        /// Creates a shortcut in the startup folder.
        /// </summary>
        /// <param name="targetPath">The exe name e.g. test.exe as found in the current directory</param>
        /// <param name="startInFolderPath">The shortcut's "Start In" folder</param>
        /// <param name="description">The shortcut's description</param>
        /// <returns>The folder path where created</returns>
        public static string CreateShortcutInStartUpFolder(string targetPath, string startInFolderPath, string description) {
            var linkPath = Environment.GetFolderPath(Environment.SpecialFolder.Startup) + @"\" + Path.GetFileName(targetPath) + "-Shortcut.lnk";
            if (File.Exists(linkPath)) {
                File.Delete(linkPath);
            }
            Create(linkPath, targetPath, startInFolderPath, description);
            return linkPath;
        }

        /// <summary>
        /// Create a shortcut
        /// </summary>
        /// <param name="fullPathToLink">the full path to the shortcut to be created</param>
        /// <param name="fullPathToTargetExe">the full path to the exe to 'really execute'</param>
        /// <param name="startIn">Start in this folder</param>
        /// <param name="description">Description for the link</param>
        public static void Create(string fullPathToLink, string fullPathToTargetExe, string startIn, string description) {
            var shell = new WshShell();
            var link = (IWshShortcut)shell.CreateShortcut(fullPathToLink);
            link.IconLocation = fullPathToTargetExe;
            link.TargetPath = fullPathToTargetExe;
            link.Description = description;
            link.WorkingDirectory = startIn;
            link.Save();
        }

        public static bool DeleteShortcutInStartUpFolder(string targetPath) {
            var linkPath = Environment.GetFolderPath(Environment.SpecialFolder.Startup) + @"\" + Path.GetFileName(targetPath) + "-Shortcut.lnk";
            if (File.Exists(linkPath)) {
                File.Delete(linkPath);
                return true;
            }
            return false;
        }
    }
}