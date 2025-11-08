

using System.ComponentModel.DataAnnotations;

namespace SimpleStock.Application.DTOs
{
    public class ProductDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Ürün adı boş olamaz")]
        public string Name { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Birim fiyat 0’dan büyük olmalı")]
        public decimal UnitPrice { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Stok negatif olamaz")]
        public int StockQuantity { get; set; }

        [Required(ErrorMessage = "Kategori seçmelisiniz")]
        public int CategoryId { get; set; }
    }
}
