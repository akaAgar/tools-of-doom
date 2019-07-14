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
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace WadPacker
{
    public sealed class WadFile : IDisposable
    {
        /// <summary>
        /// A list of all the lumps in the wad file.
        /// </summary>
        private readonly List<WadLump> Lumps = new List<WadLump>();

        /// <summary>
        /// The number of lumps in the file.
        /// </summary>
        public int LumpCount { get { return Lumps.Count; } }

        /// <summary>
        /// Constructor.
        /// </summary>
        public WadFile() { }

        /// <summary>
        /// IDispose implementation.
        /// </summary>
        public void Dispose() { Clear(); }

        /// <summary>
        /// Remove all lumps.
        /// </summary>
        public void Clear() { Lumps.Clear(); }

        /// <summary>
        /// Saves the content of the .wad to a file.
        /// </summary>
        public void SaveToFile(string wadFilePath)
        {
            int directoryOffset = 12;
            foreach (WadLump l in Lumps) directoryOffset += l.Bytes.Length;

            // Write the 12-bytes wad file header.
            // 4 bytes: an ASCII string which must be either "IWAD" or "PWAD"
            // 4 bytes: an integer which is the number of lumps in the wad
            // 4 bytes: an integer which is the file offset to the start of the directory
            List<byte> headerBytes = new List<byte>();
            headerBytes.AddRange(Encoding.ASCII.GetBytes("IWAD"));
            headerBytes.AddRange(BitConverter.GetBytes(Lumps.Count));
            headerBytes.AddRange(BitConverter.GetBytes(directoryOffset));

            // Writes the file directory
            List<byte> directoryBytes = new List<byte>();
            int byteOffset = 12;
            foreach (WadLump l in Lumps)
            {
                directoryBytes.AddRange(BitConverter.GetBytes(byteOffset));
                directoryBytes.AddRange(BitConverter.GetBytes(l.Bytes.Length));
                byte[] nameBytes = Encoding.ASCII.GetBytes(l.LumpName);
                Array.Resize(ref nameBytes, 8);
                directoryBytes.AddRange(nameBytes);
                byteOffset += l.Bytes.Length;
            }

            List<byte> wadBytes = new List<byte>();
            wadBytes.AddRange(headerBytes);
            foreach (WadLump l in Lumps) wadBytes.AddRange(l.Bytes);
            wadBytes.AddRange(directoryBytes);
            File.WriteAllBytes(wadFilePath, wadBytes.ToArray());

            Console.WriteLine($"Saved wad to {Path.GetFileName(wadFilePath)}, {LumpCount} lumps, {wadBytes.Count} bytes.");
        }

        /// <summary>
        /// Adds a lump to the wad.
        /// </summary>
        /// <param name="lumpName">The name of the lump.</param>
        /// <param name="bytes">The content of the lump, as a byte array.</param>
        public void AddLump(string lumpName, byte[] bytes)
        {
            Lumps.Add(new WadLump(lumpName, bytes));
        }
    }
}
