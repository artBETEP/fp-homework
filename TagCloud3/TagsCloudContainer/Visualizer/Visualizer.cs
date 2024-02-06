﻿using ResultOf;
using System.Drawing;

namespace TagsCloudContainer.Drawer
{
    public static class Visualizer
    {

        public static Result<Image> Draw(Size size, IEnumerable<Result<TextImage>> textImages, Color? bgColor = null)
        {
            var image = new Bitmap(size.Width, size.Height);
            var gr = Graphics.FromImage(image);

            gr.Clear(bgColor ?? Color.Black);

            foreach (var textImage in textImages)
            {
                if (textImage.IsSuccess)
                {
                    gr.DrawString(textImage.Value.Text, textImage.Value.Font, new SolidBrush(textImage.Value.Color), textImage.Value.Position);
                }
                else
                {
                    return Result.Fail<Image>(textImage.Error);
                    Console.WriteLine(textImage.Error);
                }
            }

            return Result.Ok<Image>(image);
        }
    }
}