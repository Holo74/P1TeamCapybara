using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Data.Globals
{

    /// <summary>
    /// Contains all tags that are used in game.
    /// When referencing tag strings, only use this class.
    /// </summary>
    public static class StaticTagStrings
    {
        // This is the tag that will be used to declare an object is a small block
        public const string SMALL_BLOCK = "SmallBlock";

        // Player Tag
        public const string PLAYER = "Player";

        // Environment Triggers
        public const string TRIGGER = "Trigger";
    }

}
