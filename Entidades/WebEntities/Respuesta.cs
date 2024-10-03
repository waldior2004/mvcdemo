namespace com.msc.infraestructure.entities
{
    public class Respuesta
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public string Message { get; set; }
        public string Aplicacion { get; set; }
        public string Metodo { get; set; }
        public string TipoError { get; set; }
        public string PilaError { get; set; }
    }
}
