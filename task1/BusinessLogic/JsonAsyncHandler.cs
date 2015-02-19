using System;
using System.Collections.Generic;
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
        private bool _completed;
        private Object _state;
        private AsyncCallback _callback;
        private HttpContext _context;

        bool IAsyncResult.IsCompleted { get { return _completed; } }
        WaitHandle IAsyncResult.AsyncWaitHandle { get { return null; } }
        Object IAsyncResult.AsyncState { get { return _state; } }
        bool IAsyncResult.CompletedSynchronously { get { return false; } }

        public AsynchOperation(AsyncCallback callback, HttpContext context, Object state)
        {
            _callback = callback;
            _context = context;
            _state = state;
            _completed = false;
        }

        public void StartAsyncWork()
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback(StartAsyncTask), null);
        }

        private void StartAsyncTask(Object workItemState)
        {
            _context.Response.ContentType = "application/json";
            _context.Response.WriteFile("~/App_Data/json1.json");
            _completed = true;
            _callback(this);
        }
    }
}