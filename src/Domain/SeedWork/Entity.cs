using System;

namespace Exercise_1.Domain.SeedWork
{
    public abstract class Entity
    {
        public Guid Id { get; protected set; }

        protected object Actual => this;

        public static bool operator !=(Entity a, Entity b)
        {
            return !(a == b);
        }

        public static bool operator ==(Entity a, Entity b)
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
            if (!(obj is Entity other))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            if (Actual.GetType() != other.Actual.GetType())
            {
                return false;
            }

            if (Id == Guid.Empty || other.Id == Guid.Empty)
            {
                return false;
            }

            return Id == other.Id;
        }

        public override int GetHashCode()
        {
            return (Actual.GetType().ToString() + Id).GetHashCode();
        }
    }
}