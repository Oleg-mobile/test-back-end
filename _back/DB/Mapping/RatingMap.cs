using FluentNHibernate.Mapping;

namespace TestGuestbook.DB.Mapping
{
    public class RatingMap : ClassMap<Rating>
    {
        public RatingMap()
        {
            Id(x => x.ID).GeneratedBy.GuidComb();
            Map(x => x.Value);
            Map(x => x.MessageID, "Message_id");
        }
    }
}
