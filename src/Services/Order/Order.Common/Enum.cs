namespace Order.Common
{
    public class Enum
    {
        public enum OrderStatus
        {
            cancel,
            Peanding,
            Approved
        }
        public enum OrderPayment
        {
            CreditCard,
            PayPal,
            BankTransfer
        }
    }
}
