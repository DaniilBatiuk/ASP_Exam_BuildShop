namespace ASP_Meeting_18.Data
{
    public class Photo
    {
        public int Id { get; set; }
        public byte[]? Image { get; set; }
        //public string Filename { get; set; } = default!;
        //public string PhotoUrl { get; set; } = default!;

        public int? ProductId { get; set; }

        public Product? Product { get; set; }

    }
}
