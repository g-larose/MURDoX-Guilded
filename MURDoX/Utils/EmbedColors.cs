using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MURDoX.Utils
{
    public class EmbedColors
    {
        public static string GenerateRandomEmbedColor()
        {
            var rng = new Random();
            var color = String.Format("#{0:X6}", rng.Next(0x1000000));
            return color;
        }

        public static Dictionary<string, Color> Colors { get; set; } = new Dictionary<string, Color>()
        {
            { "teal", Color.Teal }, { "red", Color.Red}, { "blue", Color.Blue },
            { "black", Color.Black }, { "yellow", Color.Yellow}, { "green", Color.Green },
        };

        public static Color GetColor(string colorName, Color defaultColor)
        {
            if (Colors.TryGetValue(colorName, out Color value)) return value;
            return defaultColor;
        }
    }
}
