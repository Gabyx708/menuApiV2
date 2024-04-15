using Application.Request.MenuPlatilloRequests;

namespace Application.Request.MenuRequests
{
    public class MenuRequest
    {
        public DateTime fecha_consumo { get; set; }
        public DateTime fecha_cierre { get; set; }
        public List<MenuPlatilloRequest> platillosDelMenu { get; set; }
    }
}
