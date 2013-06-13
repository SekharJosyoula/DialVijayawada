using System.Collections.Generic;

namespace DV.Controls
{
    public interface IAutoAppendDataProvider
    {
        string GetAppendText(string textPattern, string firstMatch);
    }
}