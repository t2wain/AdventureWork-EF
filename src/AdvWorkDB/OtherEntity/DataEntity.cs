namespace AdvWorkDB.OtherEntity
{
    #pragma warning disable CS8618

    public record BillOfMaterialSP
    {
        public int ProductAssemblyID { get; set; }
        public int ComponentID { get; set; }
        public string ComponentDesc { get; set; }
        public decimal TotalQuantity { get; set; }
        public decimal StandardCost { get; set; }
        public decimal ListPrice { get; set; }
        public short BOMLevel { get; set; }
        public int RecursionLevel { get; set; }

    }

    public record ContactInfoSP
    {
        public int PersonID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string JobTitle { get; set; }
        public string BusinessEntityType { get; set; }
    }

    #pragma warning restore CS8618
}
