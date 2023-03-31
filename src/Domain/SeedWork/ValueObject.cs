﻿using System.Collections.Generic;
using System.Linq;

namespace Exercise_1.Domain.SeedWork
{
    public abstract class ValueObject<T> where T : ValueObject<T>
    {
        public static bool operator !=(ValueObject<T> a, ValueObject<T> b)
        {
            return !(a == b);
        }

        public static bool operator ==(ValueObject<T> a, ValueObject<T> b)
        {
            if (a is null && b is null)
            {
                return true;
            }

            if (a is null || b is null)
            {
                return false;
            }

            return a.Equals(b);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is T valueObject))
            {
                return false;
            }

            if (GetType() != obj.GetType())
            {
                return false;
            }

            return EqualsCore(valueObject);
        }

        public override int GetHashCode()
        {
            return GetHashCodeCore();
        }

        protected abstract bool EqualsCore(T other);

        protected abstract int GetHashCodeCore();
    }

    public abstract class ValueObject
    {
        public static bool operator !=(ValueObject a, ValueObject b)
        {
            return !(a == b);
        }

        public static bool operator ==(ValueObject a, ValueObject b)
        {
            if (a is null && b is null)
            {
                return true;
            }

            if (a is null || b is null)
            {
                return false;
            }

            return a.Equals(b);
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            if (GetType() != obj.GetType())
            {
                return false;
            }

            var valueObject = (ValueObject)obj;

            return GetEqualityComponents().SequenceEqual(valueObject.GetEqualityComponents());
        }

        public override int GetHashCode()
        {
            return GetEqualityComponents()
                .Aggregate(1, (current, obj) =>
                {
                    unchecked
                    {
                        return current * 23 + (obj?.GetHashCode() ?? 0);
                    }
                });
        }

        protected abstract IEnumerable<object> GetEqualityComponents();
    }
}