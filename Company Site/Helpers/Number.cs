namespace Company_Site.Helpers
{
    public static class Number
    {
        public static string Round(double value) => Math.Round(value, 2).ToString();
    }
}
