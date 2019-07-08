using System;

namespace Final.Domain.Entities
{
    public class Produto : BaseEntity
    {
        public string Name { get; set; }
        public string Codigo { get; set; }
        public double PrecoCompra { get; set; }
        public double PrecoVenda { get; set; }
        public string Sku { get; set; }
    }
}