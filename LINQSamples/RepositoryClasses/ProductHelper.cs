using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQSamples
{
    public static class ProductHelper
    {
        #region ByColor
        /// <summary>
        /// Filter Product By Color
        /// </summary>
        /// <param name="query">Query Syntax</param>
        /// <param name="color">Color</param>
        /// <returns>IEnumerable<Product></returns>
        public static IEnumerable<Product> ByColor(this IEnumerable<Product> query, string color)
        {
            return query.Where(prod => prod.Color == color);
        }
        #endregion
    }
}
