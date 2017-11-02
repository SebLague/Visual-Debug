using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VisualDebugging
{
    public static class Colours
    {

        public const string lightRed = "F94040FF";
        public const string darkRed = "870C0CFF";

        public const string lightBlue = "1D92F4FF";
        public const string darkBlue = "1B369AFF";

        public const string lightGreen = "19F43FFF";
        public const string darkGreen = "116720FF";

        public const string lightGrey = "A7A7A7FF";
        public const string darkGrey = "5C5C5CFF";
        public const string veryDarkGrey = "1C1C1CFF";

        public const string white = "FFFFFFFF";
        public const string black = "000000FF";

        public static Color HexStringToColour(string hex)
        {
            if (hex[0] != '#')
            {
                hex = "#" + hex;
            }

            Color col = Color.white;
            ColorUtility.TryParseHtmlString(hex, out col);
            return col;
        }
    }
}