using System;

namespace Example.App.ViewsModels

{
    public class ErrorViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        public int ErroCode { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
    }
}
