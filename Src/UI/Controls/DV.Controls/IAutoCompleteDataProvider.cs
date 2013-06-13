using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DV.Controls
{
    public interface IAutoCompleteDataProvider
    {
        IEnumerable<string> GetItems(string textPattern);
    }
}
