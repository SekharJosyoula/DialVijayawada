using Microsoft.Practices.Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DV.TeleCallerHelper.Common
{
    public class AvalonDockRegionAdapter : RegionAdapterBase<DocumentPane>
    {
        public AvalonDockRegionAdapter(IRegionBehaviorFactory factory)
            : base(factory)
        {

        }
    }
}
