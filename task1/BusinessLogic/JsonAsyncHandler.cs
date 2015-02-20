using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;

namespace task1
{
    public class JsonAsyncHandler : IHttpAsyncHandler
    {
        public JsonAsyncHandler()
        {

        }
        public IAsyncResult BeginProcessRequest(HttpContext context, AsyncCallback cb, object extraData)
        {
            AsynchOperation asynch = new AsynchOperation(cb, context, extraData);
            asynch.StartAsyncWork();
            return asynch;
        }

        public void EndProcessRequest(IAsyncResult result)
        {
        }

        public bool IsReusable
        {
            get { return false; }
        }

        public void ProcessRequest(HttpContext context)
        {
            throw new InvalidOperationException();
        }
    }
    class AsynchOperation : IAsyncResult
    {
        private bool completed;
        private Object state;
        private AsyncCallback callback;
        private HttpContext context;

        bool IAsyncResult.IsCompleted { get { return completed; } }
        WaitHandle IAsyncResult.AsyncWaitHandle { get { return null; } }
        Object IAsyncResult.AsyncState { get { return state; } }
        bool IAsyncResult.CompletedSynchronously { get { return false; } }

        public AsynchOperation(AsyncCallback callback, HttpContext context, Object state)
        {
            this.callback = callback;
            this.context = context;
            this.state = state;
            this.completed = false;
        }

        public void StartAsyncWork()
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback(StartAsyncTask), null);
        }

        private void StartAsyncTask(Object workItemState)
        {
            context.Response.ContentType = "application/json";
            try
            {
                context.Response.WriteFile("~/App_Data/json1.json");
            }
            catch(IOException)
            {
                context.Response.StatusCode = 400;
            }
            completed = true;
            callback(this);
        }
    }
}