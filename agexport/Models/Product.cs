using System.ComponentModel.DataAnnotations;

namespace agexport.Models
{
    public class Product
    {
        [Key]
        public int Id_producto {  get; set; }
        [Required]
        public string Producto { get; set; }
        [Required]
        public float Precio { get; set; }
        [Required]
        public int Cantidad { get; set; }
        [Required]
        public int Total { get; set; }

    }
}
