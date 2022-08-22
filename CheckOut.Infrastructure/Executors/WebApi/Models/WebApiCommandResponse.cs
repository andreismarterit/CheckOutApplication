using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CheckOut.Infrastructure.Executors.WebApi.Models
{
    public class WebApiCommandResponse<TOutput>
    {
        public HttpStatusCode HttpStatusCode { get; set; }

        public TOutput ResponseObject { get; set; }

        public List<WebApiCommandValidationError> Errors { get; set; }
    }
}
