using System;

namespace Task1
{
    public static class Utilities
    {
        /// <summary>
        /// Sorts an array in ascending order using bubble sort.
        /// </summary>
        /// <param name="numbers">Numbers to sort.</param>
        public static void Sort(int[] numbers)
        {
            if (IsUnValidForSort(numbers))
            {
                return;
            }

            for (var i = 0; i < numbers.Length - 1; i++)
            {
                var swapped = false;
                for (var j = 0; j < numbers.Length - i - 1; j++)
                {
                    if (numbers[j + 1] < numbers[j])
                    {
                        Swap(ref numbers[j + 1], ref numbers[j]);
                        swapped = true;
                    }
                }

                if (swapped == false) break;
            }
        }

        /// <summary>
        /// Searches for the index of a product in an <paramref name="products"/>
        /// based on a <paramref name="predicate"/>.
        /// </summary>
        /// <param name="products">Products used for searching.</param>
        /// <param name="predicate">Product predicate.</param>
        /// <returns>If match found then returns index of product in <paramref name="products"/>
        /// otherwise -1.</returns>
        public static int IndexOf(Product[] products, Predicate<Product> predicate)
        {
            if (products == null)
            {
                throw new ArgumentNullException(nameof(products), "Products can't be null");
            }

            if (predicate == null)
            {
                throw new ArgumentNullException(nameof(predicate), "Predicate can't be null");
            }

            for (var i = 0; i < products.Length; i++)
            {
                Product product = products[i];
                if (predicate(product))
                {
                    return i;
                }
            }

            return -1;
        }

        private static bool IsUnValidForSort(int[] numbers)
        {
            if (numbers == null)
            {
                throw new ArgumentNullException(nameof(numbers), "Array can't be null");
            }

            return numbers.Length <= 1;
        }

        private static void Swap(ref int lhs, ref int rhs)
        {
            (lhs, rhs) = (rhs, lhs);
        }
    }
}