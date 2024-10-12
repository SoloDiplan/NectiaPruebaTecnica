
using PruebaTecnica.Models;
using System;
using System.Collections.Generic;

namespace PruebaTecnica.DTO.Console
{
    public class ConsoleDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
        public int BrandId { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int StorageCapacity { get; set; }
        public decimal Price { get; set; }

        public string Comment { get; set; }

        public List<BrandSelector> Brands = new List<BrandSelector>();
    }
}