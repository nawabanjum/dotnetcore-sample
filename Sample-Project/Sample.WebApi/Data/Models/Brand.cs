using System.ComponentModel.DataAnnotations;

namespace Sample.WebApi.Data.Models
{
    public class Brand
    {
        [Key]
        public int BrandId { get; set; }
        public string BrandName { get; set; }
        public string BrandDesc { get; set; }
    }
}
