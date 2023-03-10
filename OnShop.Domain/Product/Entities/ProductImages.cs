namespace OnShop.Domain.Product.Entities
{
    public class ProductImage
    {
        public int Id { get; set; }
        public long ProductId { get; set; }
        public bool? IsShow { get; set; } = true;
        public bool? IsBaseImage { get; set; }
        public string Src { get; set; }

        #region Relationships
        public virtual Product Product { get; set; }
        #endregion
    }
}