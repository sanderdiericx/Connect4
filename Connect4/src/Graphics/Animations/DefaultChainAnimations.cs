using System.Collections.Generic;
using Connect4.src.Graphics.Sprites;
using System;
using System.Drawing;
using System.Runtime.CompilerServices;

namespace Connect4.src.Graphics.Animations
{
    internal static class DefaultChainAnimations
    {
        internal static List<ColorAnimation> GetRainbowAnimation(AnimationTarget animationTarget)
        {
            List <ColorAnimation> animations = new List<ColorAnimation>();

            List<Color> rainbowColors = new List<Color>()
            {
                Color.Red,
                Color.Orange,
                Color.Yellow,
                Color.Green,
                Color.Blue,
                Color.Indigo,
                Color.Violet
            };

            // Loop through the colors and create a new animation between them all
            foreach (var color in rainbowColors)
            {
                animations.Add(new ColorAnimation(animationTarget, color));
            }

            return animations;
        }
    }
}
