using CJE.Http.RequestAnswer;
using System;

namespace TestGuestbook.Handler
{
    public class Comment : CJE.Http.RequestHandlers.RequestHandlerBase
    {
        public Comment(CJE.Http.HttpServer server, System.Net.HttpListenerContext context, CJE.Http.RequestHandlerData data) : base(server, context, data) { }

        public override IAnswer HandleGET()
        {
            Guid id = Data.Get.GetGuid("id");
            Data.Comment comments = DB.Controller.LoadComment(Server.DBSession, id);
            return new CJE.Http.RequestAnswer.Data(comments);
        }

        public override IAnswer HandlePOST()
        {
            Form.Comment inputData = new Form.Comment(Data.Post.Input);
            Data.Comment inputComment = inputData.ToData();

            Data.Comment comments = DB.Controller.SaveComment(Server.DBSession, inputData.MessageID, inputComment);

            return new CJE.Http.RequestAnswer.Json(comments);
        }

        public override IAnswer HandleDELETE()
        {
            Guid id = Data.Get.GetGuid("id");

            Data.Comment comments = DB.Controller.DeleteComment(Server.DBSession, id);

            return new CJE.Http.RequestAnswer.Json(comments);
        }
    }
}
