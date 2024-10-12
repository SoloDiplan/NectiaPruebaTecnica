

using PruebaTecnica.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PruebaTecnica.DTO.Console
{
    public class UpdateConsoleDTO
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "El valor debe ser mayor a 0.")]
        public int BrandId { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Storage capacity must be a positive integer.")]
        public int StorageCapacity { get; set; }

        [Required]
        public string ReleaseDate { get; set; }

        [Required]
        [Range(0.01, 9999999999.99, ErrorMessage = "Price must be a positive decimal.")]
        public decimal Price { get; set; }

        [StringLength(150)]
        public string Comment { get; set; }
        public string username { get; set; }
        public List<BrandSelector> Brands { get; set; } = new List<BrandSelector>();
    }
}