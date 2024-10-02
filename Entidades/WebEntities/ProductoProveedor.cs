
namespace com.msc.infraestructure.entities
{
    public class ProductoProveedor
    {
       
        public int IdPedido { get; set; }
       
        public int IdProveedor { get; set; }
       
        public string Proveedor { get; set; }
       
        public int IdProducto { get; set; }
       
        public string Producto { get; set; }
       
        public int Cantidad { get; set; }
       
        public decimal Precio { get; set; }
       
        public decimal Total { get; set; }
       
        public string Observacion { get; set; }

    }
}
