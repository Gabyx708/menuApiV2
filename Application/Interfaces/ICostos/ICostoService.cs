using Application.Response.CostoResponses;

namespace Application.Interfaces.ICostos
{
    public interface ICostoService
    {
        CostoDiaResponse GetCostosDia(DateTime fecha);
        CostoPersonalResponse GetCostosPersonal(DateTime fechaInicio, DateTime fechaHasta, Guid idPersonal);
        CostoPeriodoResponse GetCostosPeriodo(DateTime fechaInicio, DateTime fechaFin);
    }
}
