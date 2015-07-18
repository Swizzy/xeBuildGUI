using System;
using System.Windows.Markup;

namespace xeBuild_GUI.Converters {

    public abstract class BaseConverter : MarkupExtension
    {
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }

}