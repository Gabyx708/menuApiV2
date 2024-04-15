using Application.Interfaces.IPagos;
using Domain.Entities;
using Infraestructure.Persistence;

namespace Infraestructure.Commands
{
    public class PagoCommand : IPagoCommand
    {
        private readonly MenuAppContext _context;

        public PagoCommand(MenuAppContext context)
        {
            _context = context;
        }

        public Pago InsertPago(Pago NuevoPago, List<Guid> idRecibos)
        {
            decimal montoPagado = 0;
            _context.Add(NuevoPago);
            _context.SaveChanges(); // Guardar el pago en la base de datos

            foreach (var recibo in idRecibos)
            {
                var reciboEncontrado = _context.Recibos.FirstOrDefault(r => r.IdRecibo == recibo);

                if (reciboEncontrado == null) { return null; }

                montoPagado = montoPagado + reciboEncontrado.precioTotal;
                reciboEncontrado.NumeroPago = NuevoPago.NumeroPago;
                _context.Update(reciboEncontrado);
            }

            NuevoPago.MontoPagado = montoPagado;
            _context.SaveChanges(); // Actualizar los recibos con el número de pago
            return NuevoPago;
        }


        public Pago ModificarEstadoAnulado(long NPago, bool estadoAnulado)
        {
            var pagoOriginal = _context.Pagos.FirstOrDefault(p => p.NumeroPago == NPago);

            if (pagoOriginal == null) { return null; }

            pagoOriginal.IsAnulado = estadoAnulado;

            if (estadoAnulado == true)
            {
                var recibosDeEsePago = _context.Recibos.Where(r => r.NumeroPago == NPago).ToList();

                foreach (var recibo in recibosDeEsePago)
                {
                    recibo.NumeroPago = null;
                }
            }


            _context.Update(pagoOriginal);
            _context.SaveChanges();
            return pagoOriginal;
        }
    }
}
