/*
==========================================================================
This file is part of WadPacker, a command-line wad packing tool
by @akaAgar (https://github.com/akaAgar/WadPacker)

WadPacker is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

WadPacker is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with WadPacker. If not, see https://www.gnu.org/licenses/
==========================================================================
*/

using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace DoomPacker
{
    /// <summary>
    /// Main class for the application. Basically just an ugly static classes adding all files from the application directory
    /// to a WadFile.
    /// </summary>
    public sealed class WadPackerCommandLineTool
    {
        /// <summary>
        /// Files with these extensions won't be added to the wad file
        /// </summary>
        private static readonly string[] IGNORED_EXTENSIONS = new string[] { ".exe", ".wad" };

        /// <summary>
        /// Entrypoint of the application.
        /// </summary>
        /// <param name="args">Command-line parameters. Unused.</param>
        private static void Main(string[] args)
        {
            using (WadFile wad = new WadFile())
            {
                string appDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

                AddFilesAsLumps(wad, appDirectory, 0);
                Console.WriteLine();
                wad.SaveToFile(appDirectory + "\\" + Path.GetFileName(appDirectory) + ".wad");
            }

#if DEBUG
            Console.WriteLine();
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
#endif
        }

        /// <summary>
        /// Adds add files in a directory to a WadFile. Recursively called to search subdirectories.
        /// </summary>
        /// <param name="wad">The WadFile.</param>
        /// <param name="directory">The directory to search for files.</param>
        /// <param name="depth">How deep this directory is from the root directory (depth=0).</param>
        private static void AddFilesAsLumps(WadFile wad, string directory, int depth)
        {
            // Directory is not the root directory and directory map is in the ExMx or MAPxxxxx format (where x is a digit).
            // It means directory is a map: add a 0-byte "map name" lump.
            if (depth > 0)
            {
                string dirName = Path.GetFileName(directory).ToUpperInvariant();

                if ((Regex.IsMatch(dirName, "MAP[0-9].")) || (Regex.IsMatch(dirName, "E[0-9]M[0-9]")))
                    wad.AddLump(dirName, null);
            }

            // Add all files in the directory as lumps
            foreach (string f in Directory.GetFiles(directory))
            {
                if (IGNORED_EXTENSIONS.Contains(Path.GetExtension(f).ToLowerInvariant())) continue;
                wad.AddLump(Path.GetFileNameWithoutExtension(f), File.ReadAllBytes(f));
            }

            foreach (string subDirectory in Directory.GetDirectories(directory))
                AddFilesAsLumps(wad, subDirectory, depth + 1);
        }
    }
}
