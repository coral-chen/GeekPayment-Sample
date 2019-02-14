namespace GeekPaymentSample.Geek
{
    public class GeekPathComponents : PathComponents
    {
        protected string pathPattern;

        public GeekPathComponents(string pathPattern)
        {
            this.pathPattern = pathPattern;
        }

        public string GetPath()
        {
            return string.Format(pathPattern, "", "");
        }

        public PathComponents Expand(params string[] variables)
        {
            if (variables.Length > 0)
            {
                string[] tempVariables = new string[] {variables[0], variables.Length == 1 ? "": variables[1]};
                string pathPatternTo = string.Format(pathPattern, tempVariables);
                return new GeekPathComponents(pathPatternTo);
            }
            return this;
        }
    }
}