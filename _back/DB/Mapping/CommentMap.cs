using FluentNHibernate.Mapping;

namespace TestGuestbook.DB.Mapping
{
    public class CommentMap : ClassMap<Comment>
    {
        public CommentMap()
        {
            Id(x => x.ID).GeneratedBy.GuidComb();
            Map(x => x.Created);
            Map(x => x.Author);
            Map(x => x.Content);
            Map(x => x.MessageID, "Message_id");
        }
    }
}
