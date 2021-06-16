namespace MS.Contracts
{
    public interface Order
    {
        public int Id { get; }
        public string ProductName { get; }
        public int Qty { get; }
    }
}
