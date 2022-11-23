using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movie_Core.Models.ResponseModels
{
    public class Status
    {
        public int StatusCode { get; set; }
        public string? Message { get; set; }
        public string? Token { get; set; }
    }
}
