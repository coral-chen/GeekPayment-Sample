namespace GeekPaymentSample.Geek
{
    public interface PathComponents
    {
        string GetPath();
        PathComponents Expand(params string[] variables);
    }
}