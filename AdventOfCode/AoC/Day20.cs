using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2021.AoC
{
    record Pixel(int X, int Y);

    public class Day20 : AdventBase
    {
        readonly char[] _algorithm;
        readonly int _size;
        Dictionary<Pixel, char> _image;

        public Day20()
        {
            var input = Common.GetInput(20);

            _algorithm = input.First().ToCharArray();
            _size = input.Skip(2).First().Length;
            _image = ReadImage(input.Skip(2));
        }

        static Dictionary<Pixel, char> ReadImage(IEnumerable<string> imageData)
        {
            var image = new Dictionary<Pixel, char>();
            int row = 0;

            foreach (var line in imageData)
            {
                int col = 0;
                foreach (var c in line)
                {
                    image.Add(new Pixel(row, col++), c);
                }
                row++;
            }

            return image;
        }

        Dictionary<Pixel, char> Enhance(Dictionary<Pixel, char> image, int steps)
        {
            int step = 0;

            while (++step <= steps)
            {
                var left = 0 - 3 * step;
                var top = 0 - 3 * step;
                var right = _size + 3 * step;
                var bottom = _size + 3 * step;

                var enhancedImage = new Dictionary<Pixel, char>();

                for (int i = top; i < bottom; i++)
                    for (int j = left; j < right; j++)
                    {
                        var surroundingPixels = SurroundingPixels(i, j);

                        List<int> bits = surroundingPixels
                            .Select(x => FindPixel(x))
                            .Select(x => x == '#' ? 1 : 0)
                            .ToList();

                        var index = BitsToInt(bits);
                        var pixel = _algorithm[index];

                        enhancedImage.Add(new Pixel(i, j), pixel);
                    }

                _image = enhancedImage;
            }

            return _image;
        }

        static int BitsToInt(List<int> bits)
        {
            int result = 0;
            int doubling = 1;

            for (int i = bits.Count - 1; i >= 0; i--)
            {
                if (bits[i] == 1)
                    result += doubling;

                doubling *= 2;
            }

            return result;
        }

        char FindPixel(Pixel pixel)
        {
            if (_image.ContainsKey(pixel))
                return _image[pixel];

            return '.';
        }

        static IEnumerable<Pixel> SurroundingPixels(int row, int col)
        {
            yield return new Pixel(row - 1, col - 1);
            yield return new Pixel(row - 1, col);
            yield return new Pixel(row - 1, col + 1);
            yield return new Pixel(row, col - 1);
            yield return new Pixel(row, col);
            yield return new Pixel(row, col + 1);
            yield return new Pixel(row + 1, col - 1);
            yield return new Pixel(row + 1, col);
            yield return new Pixel(row + 1, col + 1);
        }

        int Process(int steps)
        {
            var lightPixels = 0;
            var enhancedImage = Enhance(_image, steps);
            foreach (var pixel in enhancedImage.Keys)
            {
                if (enhancedImage[pixel] == '#')
                    lightPixels++;
            }

            return lightPixels;
        }

        public override void Solution1()
        {
            var result = Process(2);

        }
    }
}
