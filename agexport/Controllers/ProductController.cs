using agexport.DAL;
using agexport.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace agexport.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly MyAppDbContext _context;
        public ProductController(MyAppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var products = _context.Products.ToList();
                if (products.Count == 0)
                {
                    return NotFound("El producto no se encuentra disponible");
                }
                return Ok(products);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var product = _context.Products.Find(id);
                if (product == null)
                {
                    return NotFound($"Detalle no encontrado de {id}");
                }
                return Ok(product);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public IActionResult Post(Product model)
        {
            try
            {
                _context.Add(model);
                _context.SaveChanges();
                return Ok("Producto Creado");
            }
	        catch (Exception ex)

             {

                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public IActionResult Put(Product model)
        {
            if(model == null || model.Id_producto == 0)
            {
                if (model == null)
                {
                    return BadRequest("Dato invalido.");
                }
                else if(model.Id_producto == 0)
                {
                    return BadRequest($"ID de producto es invalido {model.Id_producto} es invalido");

                }
                
            }

            try
            {
                var product = _context.Products.Find(model.Id_producto);
                if (product == null)
                {
                    return NotFound($"Producto no encontrado con el id {model.Id_producto}");
                }
                product.Producto = model.Producto;
                product.Precio = model.Precio;
                product.Cantidad = model.Cantidad;
                product.Total = model.Total;
                _context.SaveChanges();
                return Ok("Producto actualizado");
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }

        [HttpDelete]
        public IActionResult Delete(int id) 
        {

            try
            {
                var product = _context.Products.Find(id);
                if (product == null)
                {
                    return NotFound($"Producto no encontrado con el id {id}");
                }
                _context.Products.Remove(product);
                _context.SaveChanges();
                return BadRequest("Detalle del producto eliminado");
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }



        }




    }
}
