using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.DynamicData;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DV.Admin.UI.DynamicData.Filters
{
    public partial class Text : QueryableFilterUserControl
    {
        public override Control FilterControl
        {
            get { return TextBox1; }
        }

        protected void TextBox1_Changed(object sender, EventArgs e)
        {
            OnFilterChanged();
        }

        public override IQueryable GetQueryable(IQueryable source)
        {
            var value = TextBox1.Text;
            if (String.IsNullOrWhiteSpace(value)) return source;

            if (DefaultValues != null)
            {
                DefaultValues[Column.Name] = value;
            }

            var parameter = Expression.Parameter(source.ElementType);
            var columnProperty = Expression.PropertyOrField(parameter, Column.Name);
            var likeValue = Expression.Constant(value, typeof(string));
            var condition = Expression.Call(
                columnProperty,
                typeof(string).GetMethod("Contains"),
                likeValue);
            var where = Expression.Call(
                typeof(Queryable),
                "Where",
                new[] { source.ElementType },
                source.Expression,
                Expression.Lambda(condition, parameter));
            return source.Provider.CreateQuery(where);
        }
    }
}