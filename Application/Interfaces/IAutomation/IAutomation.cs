using Application.Request.AutomationRequest;
using Application.Response.PersonalResponses;

namespace Application.Interfaces.IAutomation
{
    public interface IAutomation
    {
        bool HacerPedidosAutomatico();
        PersonalResponse SetPedidoAutomatico(AutomationRequest request);
    }
}
