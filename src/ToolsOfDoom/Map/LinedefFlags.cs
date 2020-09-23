/*
==========================================================================
This file is part of Tools of Doom, a library providing a collection of
classes to load/edit/save Doom maps and wad archives, created by @akaAgar
(https://github.com/akaAgar/tools-of-doom).

Tools of Doom is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

Tools of Doom is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with Tools of Doom. If not, see https://www.gnu.org/licenses/
==========================================================================
*/


using System;

namespace ToolsOfDoom.Map
{
    /// <summary>
    /// Special flags for a Doom map linedef.
    /// </summary>
    [Flags]
    public enum LinedefFlags
    {
        /// <summary>
        /// Cannot be crossed.
        /// </summary>
        Impassible = 1,
        /// <summary>
        /// Blocks monsters.
        /// </summary>
        BlocksMonsters = 2,
        /// <summary>
        /// Has two sides.
        /// </summary>
        TwoSided = 4,
        /// <summary>
        /// Draw lower texture from the bottom.
        /// </summary>
        UpperUnpegged = 8,
        /// <summary>
        /// Draw upper texture from the top.
        /// </summary>
        LowerUnpegged = 16,
        /// <summary>
        /// Show as a wall on the automap, used to hide secret passages.
        /// </summary>
        Secret = 32,
        /// <summary>
        /// Sound can only cross one linedef with this flag (not two).
        /// </summary>
        BlocksSound = 64,
        /// <summary>
        /// Does not appear on the automap.
        /// </summary>
        NotOnMap = 128,
        /// <summary>
        /// Drawn on the automap at the beginning of the level.
        /// </summary>
        AlreadyOnMap = 126
    }
}
