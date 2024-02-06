using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Response
{
    public class ServerResponse<T>
    {
        public bool Ok { get => Errors.Count == 0; }
        public List<Error> Errors { get; set; } = new();
        public T Data { get; set; }
    }
}
