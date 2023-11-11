using System;

namespace TestGuestbook.Form
{
    public class Rating : CJE.Form.FormData
    {
        public Rating(CJE.Form.DataRaw data) : base(data, true) { }

        [CJE.Form.Value("Value", typeof(CJE.Form.Values.DoubleParser))]
        public double Value;

        [CJE.Form.Value("MessageID", typeof(CJE.Form.Values.GuidParser))]
        public Guid MessageID;

        public Data.Rating ToData()
        {
            return new Data.Rating()
            {
                Value = this.Value,
            };
        }
    }
}
