using Application.Interfaces.ICostos;
using Application.Interfaces.IDescuento;
using Application.Interfaces.IPedido;
using Application.Interfaces.IPersonal;
using Application.Interfaces.IRecibo;
using Application.Response.CostoResponses;
using Domain.Entities;

namespace Application.UseCase.Costos
{
    public class CostoService : ICostoService
    {
        private readonly IPedidoQuery _pedidoQuery;
        private readonly IReciboQuery _reciboQuery;
        private readonly IDescuentoQuery _descuentoQuery;
        private readonly IPersonalQuery _personalQuery;

        public CostoService(IPedidoQuery pedidoQuery, IReciboQuery reciboQuery, IDescuentoQuery descuentoQuery, IPersonalQuery personalQuery)
        {
            _pedidoQuery = pedidoQuery;
            _reciboQuery = reciboQuery;
            _descuentoQuery = descuentoQuery;
            _personalQuery = personalQuery;
        }

        public CostoDiaResponse GetCostosDia(DateTime fecha)
        {
            List<Pedido> pedidosDelDia = _pedidoQuery.GetPedidosFiltrado(null, fecha, fecha, null);

            int CantidadDePedidos = 0;
            decimal Costototal = 0;
            decimal CostototalDescuento = 0;

            foreach (var pedido in pedidosDelDia)
            {
                Recibo reciboDelPedido = _reciboQuery.GetById(pedido.IdRecibo);
                decimal descuentoDelPedido = _descuentoQuery.GetById(reciboDelPedido.IdDescuento).Porcentaje;
                CostototalDescuento = CalcularDescuento(reciboDelPedido.precioTotal, descuentoDelPedido) + CostototalDescuento;
                Costototal = reciboDelPedido.precioTotal + Costototal;
                CantidadDePedidos++;
            }

            return new CostoDiaResponse
            {
                Fecha = fecha,
                CostoConDescuento = CostototalDescuento,
                CostoSinDescuento = Costototal,
                CantidadPedidos = CantidadDePedidos
            };
        }

        public CostoPeriodoResponse GetCostosPeriodo(DateTime fechaInicio, DateTime fechaFin)
        {
            List<Pedido> pedidosDelDia = _pedidoQuery.GetPedidosFiltrado(null, fechaInicio, fechaFin, null);

            if (pedidosDelDia.Count == 0)
            {
                return null;
            }

            int CantPedidos = pedidosDelDia.Count;
            decimal Costototal = 0;
            decimal CostototalDescuento = 0;

            foreach (var pedido in pedidosDelDia)
            {
                Recibo reciboDelPedido = _reciboQuery.GetById(pedido.IdRecibo);
                decimal descuentoDelPedido = _descuentoQuery.GetById(reciboDelPedido.IdDescuento).Porcentaje;
                CostototalDescuento = CalcularDescuento(reciboDelPedido.precioTotal, descuentoDelPedido) + CostototalDescuento;
                Costototal = reciboDelPedido.precioTotal + Costototal;
            }

            return new CostoPeriodoResponse
            {
                Inicio = fechaInicio,
                Fin = fechaFin,
                CostoTotal = Costototal,
                TotalDescuentos = CostototalDescuento,
                CantPedidos = CantPedidos
            };
        }

        public CostoPersonalResponse GetCostosPersonal(DateTime fechaInicio, DateTime fechaHasta, Guid idPersonal)
        {
            Personal persona = _personalQuery.GetPersonalById(idPersonal);
            List<Pedido> pedidosDelDia = _pedidoQuery.GetPedidosFiltrado(idPersonal, fechaInicio, fechaHasta, null);

            if (pedidosDelDia.Count == 0)
            {
                return null;
            }

            int CantidadPedidos = pedidosDelDia.Count;

            decimal Costototal = 0;
            decimal CostototalDescuento = 0;

            foreach (var pedido in pedidosDelDia)
            {
                Recibo reciboDelPedido = _reciboQuery.GetById(pedido.IdRecibo);
                decimal descuentoDelPedido = _descuentoQuery.GetById(reciboDelPedido.IdDescuento).Porcentaje;
                CostototalDescuento = CalcularDescuento(reciboDelPedido.precioTotal, descuentoDelPedido) + CostototalDescuento;
                Costototal = reciboDelPedido.precioTotal + Costototal;
            }

            return new CostoPersonalResponse
            {
                Id = idPersonal,
                Nombre = persona.Nombre,
                Apellido = persona.Apellido,
                Dni = persona.Dni,
                InicioPeriodo = fechaInicio,
                FinPeriodo = fechaHasta,
                CostoTotal = Costototal,
                Descuento = CostototalDescuento,
                CantidadPedidos = CantidadPedidos

            };
        }

        private decimal CalcularDescuento(decimal total, decimal porcentaje)
        {
            if (porcentaje < 0 || porcentaje > 100)
            {
                throw new ArgumentException("El porcentaje debe estar entre 0 y 100.");
            }

            decimal descuento = total * (porcentaje / 100);
            decimal resultado = total - descuento;

            return resultado;
        }
    }

}
