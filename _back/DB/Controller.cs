using CJE.Component;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TestGuestbook.DB
{
    public static class Controller
    {
        public static List<Data.Message> LoadMessageList(IDBSession dbs)
        {
            IList<DB.Message> dbMessages = dbs.Session.QueryOver<DB.Message>().Fetch(x => x.Comments).Eager.TransformUsing(NHibernate.Transform.Transformers.DistinctRootEntity).List<DB.Message>();
            List<Data.Message> messages = dbMessages.Select(x => x.ToData(false, false)).ToList();
            return messages;
        }

        public static Data.Message LoadMessage(IDBSession dbs, Guid id)
        {
            DB.Message dbMessage = dbs.Session.QueryOver<DB.Message>().Fetch(x => x.Comments).Eager.Where(x => x.ID == id).SingleOrDefault<DB.Message>();
            Data.Message message = dbMessage?.ToData(true, true);
            return message;
        }

        public static Data.Message SaveMessage(IDBSession dbs, Data.Message message)
        {
            using (NHibernate.ITransaction transaction = dbs.Session.BeginTransaction())
            {
                DB.Message dbMessage = null;
                if (message.ID != Guid.Empty) dbMessage = dbs.Session.Get<DB.Message>(message.ID);
                if (dbMessage == null)
                {
                    dbMessage = new DB.Message(message);
                    dbMessage.Created = DateTime.UtcNow;
                }
                else
                {
                    dbMessage.Author = message.Author;
                    dbMessage.Title = message.Title;
                    dbMessage.Content = message.Content;
                }

                dbs.Session.SaveOrUpdate(dbMessage);
                transaction.Commit();
                message = dbMessage.ToData(false, true);
            }
            return message;
        }

        public static Data.Message DeleteMessage(IDBSession dbs, Guid id)
        {
            DB.Message dbMessage;
            using (NHibernate.ITransaction transaction = dbs.Session.BeginTransaction())
            {
                dbMessage = dbs.Session.Get<DB.Message>(id);
                if (dbMessage == null) return null;
                dbs.Session.Delete(dbMessage);
                transaction.Commit();
            }
            return dbMessage.ToData(false, false);
        }

        //___Oleg___

        public static Data.Rating AddRating(IDBSession dbs, Guid messageID, Data.Rating rating)
        {
            using (NHibernate.ITransaction transaction = dbs.Session.BeginTransaction())
            {
                var dbRating = new DB.Rating(rating);
                dbRating.MessageID = messageID;

                dbs.Session.SaveOrUpdate(dbRating);
                transaction.Commit();
                rating = dbRating.ToData();
            }
            return rating;
        }


        public static Data.Comment LoadComment(IDBSession dbs, Guid id)
        {
            DB.Comment dbComment = dbs.Session.QueryOver<DB.Comment>().Where(x => x.ID == id).SingleOrDefault<DB.Comment>();
            Data.Comment comment = dbComment?.ToData(true);
            return comment;
        }

        public static Data.Comment SaveComment(IDBSession dbs, Guid messageID, Data.Comment comment)
        {
            using (NHibernate.ITransaction transaction = dbs.Session.BeginTransaction())
            {
                DB.Comment dbComment = null;
                if (comment.ID != Guid.Empty) dbComment = dbs.Session.Get<DB.Comment>(comment.ID);
                if (dbComment == null)
                {
                    dbComment = new DB.Comment(comment);
                    dbComment.MessageID = messageID;
                    dbComment.Created = DateTime.UtcNow;
                }
                else
                {
                    dbComment.Author = comment.Author;
                    dbComment.Content = comment.Content;
                }

                dbs.Session.SaveOrUpdate(dbComment);
                transaction.Commit();
                comment = dbComment.ToData(true);
            }
            return comment;
        }

        public static Data.Comment DeleteComment(IDBSession dbs, Guid id)
        {
            DB.Comment dbComment;
            using (NHibernate.ITransaction transaction = dbs.Session.BeginTransaction())
            {
                dbComment = dbs.Session.Get<DB.Comment>(id);
                if (dbComment == null) return null;
                dbs.Session.Delete(dbComment);
                transaction.Commit();
            }
            return dbComment.ToData(false);
        }
    }
}
