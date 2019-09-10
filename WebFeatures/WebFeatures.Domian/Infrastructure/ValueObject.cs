using System.Collections.Generic;

namespace WebFeatures.Domian.Infrastructure
{
    public abstract class ValueObject
    {
        protected abstract IEnumerable<object> GetAtomicValues();

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }
    }
}
