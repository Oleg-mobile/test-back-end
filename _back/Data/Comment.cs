using System;
using System.Runtime.InteropServices;

namespace TestGuestbook.Data
{
    [Guid("3455f310-d4a4-41f6-a3d7-1e85063bd4e3")]
    [CJE.Serializable(CJE.Serializable.Politics.AllExceptExcluded)]
    public class Comment : CJE.ISerializable
    {
        public Guid ID { get; set; }
        public DateTime Created { get; set; }
        public string Author { get; set; }
        public string Content { get; set; }
    }
}
