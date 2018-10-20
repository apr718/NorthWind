using System;
using NorthWind.Core.Interfaces;

namespace NorthWind.Models
{
    public class ProductNameValidator : IProductNameValidator
    {
        private const int MaxProductNameLenth = 50;
        private const int MinProductName = 3;

        public bool IsValid(string productName)
        {
            if (productName is null)
            {
                throw new ArgumentNullException(nameof(productName));
            }
            if (productName.Length < MinProductName || productName.Length > MaxProductNameLenth)
            {
                return false;
            }
            return true;
        }
    }
}
