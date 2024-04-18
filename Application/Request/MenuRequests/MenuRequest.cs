using Application.Request.
    
    Requests;

namespace Application.Request.MenuRequests
{
    public class MenuRequest
    {
        public DateTime fecha_consumo { get; set; }
        public DateTime fecha_cierre { get; set; }
        public List<MenuOptionRequest> platillosDelMenu { get; set; }
    }
}
