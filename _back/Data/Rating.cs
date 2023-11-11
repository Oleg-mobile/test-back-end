using System;
using System.Runtime.InteropServices;

namespace TestGuestbook.Data
{
    [Guid("bd586114-4d37-4582-8b86-bd7d2a878b86")]
    [CJE.Serializable(CJE.Serializable.Politics.AllExceptExcluded)]
    public class Rating : CJE.ISerializable
    {
        public Guid ID { get; set; }
        public double Value { get; set; }
    }
}
