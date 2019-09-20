using System.Collections.Generic;

namespace Nop.Web.Themes.PAA.Models
{
    public class EntryGroup<K, T>
    {
        public K Key;
        public IEnumerable<T> Values;
    }
}