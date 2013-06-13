using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace DV.DataProvider
{
    public static class DBContextFactory
    {
        //TODO: this migh not be of great use..lets keep i9t for now
        public static dvradiusEntities GetDBContext()
        {
            return new dvradiusEntities();
        }
    }
}
