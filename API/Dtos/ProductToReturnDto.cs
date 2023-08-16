using Core.Entities;

namespace API.Dtos
{
    public class ProductToReturnDto // This is a data transfer object // this object will show the final data that the customer wants to see
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public string PictureUrl { get; set; }

        public string ProductType { get; set; }

        public string ProductBrand { get; set; }

    }
}
