﻿/*
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
    /// Bit flags for a Doom map thing
    /// </summary>
    [Flags]
    public enum ThingOptions
    {
        /// <summary>
        /// Thing is present in skill levels 1 & 2.
        /// </summary>
        Skill12 = 1,

        /// <summary>
        /// Thing is present in skill level 3.
        /// </summary>
        Skill3 = 2,

        /// <summary>
        /// Thing is present in skill levels 4 & 5.
        /// </summary>
        Skill45 = 4,

        /// <summary>
        /// Monster is deaf.
        /// </summary>
        Deaf = 8,

        /// <summary>
        /// Thing is only present in multiplayer.
        /// </summary>
        MultiplayerOnly = 16,

        /// <summary>
        /// Shortcut: this is present in all skill levels.
        /// (AllSkills = Skill12 | Skill3 | Skill45)
        /// </summary>
        AllSkills = Skill12 | Skill3 | Skill45
    }
}
