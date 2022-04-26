using Experiment.Core.Entities;
using Experiment.Core.Enums;

namespace Experiment.Core.QueryExtensions
{
    /// <summary>
    /// NOTE: Normally I would write these extension methods against IQueryable interface.
    /// </summary>
    public static class ProductQueryExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="products"></param>
        /// <returns></returns>
        public static List<Product> FilterNonGroceries(this IEnumerable<Product> products)
        {
            return products.Where(t => t.ProductType != ProductType.Grocery).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="products"></param>
        /// <returns></returns>
        public static decimal SumNonGroceriesTotalCost(this IEnumerable<Product> products)
        {
            return products.FilterNonGroceries().Sum(t => t.Price);
        }
    }
}
