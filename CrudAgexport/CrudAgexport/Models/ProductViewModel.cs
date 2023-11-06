using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace CrudAgexport.Models
{
    public class ProductViewModel
    {
        [Key]
        public int Id_producto { get; set; }
        [Required]
        [DisplayName("Nombre del producto")]
        public string Producto { get; set; }
        [Required]
        public float Precio { get; set; }
        [Required]
        public int Cantidad { get; set; }
        [Required]
        public int Total { get; set; }
    }
}
