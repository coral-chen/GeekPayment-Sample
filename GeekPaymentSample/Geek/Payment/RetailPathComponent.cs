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
            string pathPatternTo = string.Format(pathPattern, variables);
            return new RetailPathComponents(pathPatternTo);
        }
    }
}