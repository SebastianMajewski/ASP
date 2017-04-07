namespace ASPProject.Tests.Fakes
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    public class SetMap : KeyedCollection<Type, object>
    {
        public HashSet<T> Use<T>(IEnumerable<T> sourceData)
        {
            var set = new HashSet<T>(sourceData);
            if (this.Contains(typeof(T)))
            {
                this.Remove(typeof(T));
            }
            this.Add(set);
            return set;
        }

        public HashSet<T> Get<T>()
        {
            if (!this.Contains(typeof(T)))
            {
                this.Use(new List<T>());
            }
            return (HashSet<T>)this[typeof(T)];
        }

        protected override Type GetKeyForItem(object item)
        {
            return item.GetType().GetGenericArguments().Single();
        }
    }
}