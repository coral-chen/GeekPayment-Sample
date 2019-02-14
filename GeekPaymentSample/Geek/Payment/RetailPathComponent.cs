using GeekPaymentSample.Geek;

namespace GeekPaymentSample.Geek.Payment
{
    public class RetailPathComponents : PathComponents
    {
        private string pathPattern = "apps/{0}/retail_qr_orders/{1}";

        public RetailPathComponents() {

        } 

        private RetailPathComponents(string pathPattern)
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
                return new RetailPathComponents(pathPatternTo);
            }
            return this;
        }
    }
}