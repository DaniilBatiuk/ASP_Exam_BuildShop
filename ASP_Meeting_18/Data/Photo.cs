namespace ASP_Meeting_18.Data
{
    public class Photo
    {
        public int Id { get; set; }
        public byte[]? Image { get; set; }

        public int? ProductId { get; set; }

        public Product? Product { get; set; }

    }
}
