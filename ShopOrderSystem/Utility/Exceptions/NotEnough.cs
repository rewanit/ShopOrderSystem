using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ShopOrderSystem.Utility.Exceptions
{
    public class NotEnoughException : Exception
    {
        private readonly IEnumerable<string> list;

        public NotEnoughException(IEnumerable<string> list) : base("Not enough")
        {
            this.list = list;
        }
        public override string Message
        {
            get
            {
                return $"{base.Message}: {string.Join(", ", list)}";
            }
        }
    }
}
