namespace GeekPaymentSample.Geek
{
    public class PathComponent
    {
        public string getPath()
        {
            return "";
        }

        public PathComponent expend(params string[] variables)
        {
            return new PathComponent();
        }
    }
}