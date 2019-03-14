using System;

namespace GeekPaymentSample.Geek
{
    [AttributeUsage(AttributeTargets.Field)]
    public class Path : Attribute
    {
        private PathComponents pathComponents;
        public Path(string value)
        {
            this.pathComponents = new GeekPathComponents(value);
        }

        public PathComponents PathComponents
        {
            get => pathComponents;
        }
    }
}