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
using System.Globalization;
using System.Text.RegularExpressions;

namespace DoomPacker
{
    /// <summary>
    /// A lump in a Doom wad file.
    /// </summary>
    public struct WadLump
    {
        /// <summary>
        /// Max length of the lump name entry.
        /// </summary>
        private const int MAX_LUMP_NAME_LENGTH = 8;

        /// <summary>
        /// Regex pattern used to remove invalid characters from the lump name.
        /// </summary>
        private const string LUMP_REGEX_PATTERN = "[^A-Z0-9]";

        /// <summary>
        /// Lump name.
        /// </summary>
        public readonly string LumpName;

        /// <summary>
        /// Lump content, as an array of bytes.
        /// </summary>
        public readonly byte[] Bytes;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="name">The name of the lump.</param>
        /// <param name="bytes">The content of the lump, as an array of bytes.</param>
        public WadLump(string name, byte[] bytes)
        {
            Bytes = bytes ?? new byte[0]; // Make sure bytes is not null
            if (string.IsNullOrEmpty(name)) name = "NULL";
            LumpName = Regex.Replace(name.ToUpperInvariant(), LUMP_REGEX_PATTERN, "");
            if (LumpName.Length > MAX_LUMP_NAME_LENGTH) LumpName = LumpName.Substring(0, MAX_LUMP_NAME_LENGTH);

            Console.WriteLine($"Added lump {LumpName} ({Bytes.Length.ToString("N0", NumberFormatInfo.InvariantInfo)} bytes).");
        }
    }
}
