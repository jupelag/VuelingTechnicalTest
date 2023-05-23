namespace GNB.SalesEnquiry.Shared
{
    public static class doubleExtenders
    {
        public static decimal ToDecimalBankersRouding(this double value)
        {
            var roundedValue = Math.Round(value, 2, MidpointRounding.ToEven);
            return Convert.ToDecimal(roundedValue);
        }
    }
}
