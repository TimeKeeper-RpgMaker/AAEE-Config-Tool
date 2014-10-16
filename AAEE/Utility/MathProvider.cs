using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AAEE.Utility
{
    /// <summary>Provides power calculation with integers.</summary>
    public class MathProvider
    {
        #region Methods

        /// <summary>Calculates the power of a number.</summary>
        /// <param name="x">Base.</param>
        /// <param name="y">Power.</param>
        /// <returns>x ^ y.</returns>
        public static long Pow(int x, int y)
        {
            long result = 1;
            for (int i = 0; i < y; i++) result *= x;
            return (long)result;
        }

        #endregion
    }
}
