namespace DemoTests.Models
{
    public class Order
    {
        public int orderId { get; set; }
        public string referenceNumber { get; set; }
        public Name name { get; set; }

        public Address address { get; set; }
    }
    public class Name
    {
        public string title { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
    }
    public class Address
    {
        public string type { get; set; }
        public string address1 { get; set; }
        public string address2 { get; set; }
        public string city { get; set; }
        public string stateCode { get; set; }
        public string postalCode { get; set; }
    }
}