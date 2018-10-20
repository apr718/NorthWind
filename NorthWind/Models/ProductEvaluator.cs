using Models;
using NorthWind.Core.Interfaces;

namespace NorthWind.Models
{
    public class ProductEvaluator
    {
        private readonly IProductNameValidator _nameValidator;
        private const decimal MinPrice = 0.1m;
        private const short MinUnitsinStock = 3;
        private const short MaxUnitsInStock = 100;

        public ProductEvaluator(IProductNameValidator nameValidator)
        {
            _nameValidator = nameValidator;
        }
        public ProductDecision Evaluate(Product prod)
        {
            if (!_nameValidator.IsValid(prod.ProductName))
            {
                return ProductDecision.WrongProductName;
            }
            if (prod.UnitPrice <= MinPrice)
            {
                return ProductDecision.MinPriceLimit;
            }
            if (prod.UnitsInStock <= 0)
            {
                return ProductDecision.OutOfStock;
            }
            if (prod.UnitsInStock <= MinUnitsinStock)
            {
                return ProductDecision.MinTreshhold;
            }
            if (prod.UnitsInStock > MaxUnitsInStock)
            {
                return ProductDecision.StockOverload;
            }
            return ProductDecision.AutoAccepted;
        }
    }
}


