using System;

namespace Task1
{
    public class Product : IEquatable<Product>
    {
        public Product(string name, double price)
        {
            Name = name;
            Price = price;
        }

        public string Name { get; }

        public double Price { get; }

        public bool Equals(Product other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return Name == other.Name && Price.Equals(other.Price);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            return obj.GetType() == GetType() && Equals((Product)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Price);
        }

        public static bool operator ==(Product left, Product right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Product left, Product right)
        {
            return !Equals(left, right);
        }
    }
}
