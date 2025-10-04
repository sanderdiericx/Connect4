using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect4.src.Graphics
{
    internal static class EasingFunctions
    {
        internal static Func<float, float> GetEaseOutBounce()
        {
            Func<float, float> easeOutBounce = x =>
            {
                float n1 = 7.5625f;
                float d1 = 2.75f;

                if (x < 1 / d1)
                {
                    return n1 * x * x;
                }
                else if (x < 2 / d1)
                {
                    x -= 1.5f / d1;
                    return n1 * x * x + 0.75f;
                }
                else if (x < 2.5 / d1)
                {
                    x -= 2.25f / d1;
                    return n1 * x * x + 0.9375f;
                }
                else
                {
                    x -= 2.625f / d1;
                    return n1 * x * x + 0.984375f;
                }
            };

            return easeOutBounce;
        }
    }
}
