using System.Security.AccessControl;
using Experiment.Core.Enums;

namespace Experiment.Core.Entities;

public class User
{
    public Guid Id { get; set; }
    public UserType UserType { get; set; }
    public DateTime RegisterationDate { get; set; }
}

//public abstract class User
//{
//    public DateTimeOffset RegisterationDate { get; set; }

//    public abstract decimal ApplyDiscount(Product product);
//}

//public class Affiliate : User
//{
//    public override decimal ApplyDiscount(Product product)
//    {
//        if (product.ProductType == ProductType.Grocery)
//            return product.Price;

//        return product.Price * 0.90m;
//    }
//}

//public class Employee : User
//{
//    public override decimal ApplyDiscount(Product product)
//    {
//        if (product.ProductType == ProductType.Grocery)
//            return product.Price;

//        return product.Price * 0.70m;
//    }
//}

//public class Customer : User
//{
//    public override decimal ApplyDiscount(Product product)
//    {
//        if (product.ProductType == ProductType.Grocery)
//            return product.Price;

//        return product.Price * 0.95m;
//    }
//}