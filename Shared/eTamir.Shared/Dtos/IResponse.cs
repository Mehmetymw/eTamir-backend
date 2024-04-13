using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTamir.Shared.Dtos
{
    public interface IResponse<T>
    {
        int StatusCode { get; set; }
        bool IsSuccessful { get; set; }
        T Data { get; set; }
    }
}
