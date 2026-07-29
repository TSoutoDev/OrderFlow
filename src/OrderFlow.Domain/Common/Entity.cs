using System;
using System.Collections.Generic;
using System.Security.AccessControl;
using System.Text;

namespace OrderFlow.Domain.Common
{
    public class Entity
    {
        public Guid Id { get; protected set; }

        protected Entity()
        {
            Id = Guid.NewGuid();
        }

        protected Entity(Guid id)
        {
            if(id == Guid.Empty)
            {
                throw new ArgumentNullException( "O identificador da entidade nao pode ser vazio.", nameof(id));
            }

            Id = id;
        }


        public override bool Equals(object? obj)
        {
            if (obj is not Entity other)
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            if (obj.GetType() != other.GetType())
            {
                return false;
            }

            return Id == other.Id;
        }

        public override int GetHashCode() 
        {
            return HashCode.Combine(GetType(), Id);
        }

        public static bool operator ==(Entity? left, Entity? right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Entity? left, Entity? right)
        {
            return !Equals(left, right);
        }
    }
}
