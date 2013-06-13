using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DV.Manager.Interfaces
{
    public interface ISearch<T>
    {
        IEnumerable<T> Get(string request);
    }
}
