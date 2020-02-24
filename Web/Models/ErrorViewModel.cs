using System;

namespace Web.Models
{
    public class ErrorViewModel
    {
        private Order model;

        public ErrorViewModel(Order model)
        {
            this.model = model;
        }

        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
