using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MURDoX.Utils
{
    public class ConsoleUtil
    {
        public static ConsoleColor FromHex(string hex)
        {
            int argb = Int32.Parse(hex.Replace("#", ""), NumberStyles.HexNumber);
            Color c = Color.FromArgb(argb);

            int index = (c.R > 128 | c.G > 128 | c.B > 128) ? 8 : 0; // Bright bit
            index |= (c.R > 64) ? 4 : 0; // Red bit
            index |= (c.G > 64) ? 2 : 0; // Green bit
            index |= (c.B > 64) ? 1 : 0; // Blue bit

            return (System.ConsoleColor)index;
        }

        public static async Task<string> GetRandomEmbedColorAsync()
        {
            var random = new Random();
            var color = EmbedColors.GenerateRandomEmbedColor();

            return color;

        }
    }
}
