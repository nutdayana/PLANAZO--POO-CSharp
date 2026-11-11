namespace PLANAZO.Modelos
{
    public enum EstadoReserva { Activa, Cancelada }

    public class Reserva
    {
        public int Id { get; private set; }
        public Usuario Usuario { get; private set; }
        public Evento Evento { get; private set; }
        public int CantidadEntradas { get; private set; }
        public decimal ValorTotal { get; private set; }
        public EstadoReserva Estado { get; private set; }

        public Reserva(int id, Usuario usuario, Evento evento, int cantidad)
        {
            if (cantidad <= 0) throw new ArgumentException("La cantidad debe ser mayor a cero.");
            
            Id = id;
            Usuario = usuario;
            Evento = evento;
            CantidadEntradas = cantidad;
            ValorTotal = evento.CalcularPrecioFinal() * cantidad;
            Estado = EstadoReserva.Activa;
        }

        public void Cancelar()
        {
            if (Estado == EstadoReserva.Cancelada)
                throw new InvalidOperationException("Esta reserva ya está cancelada.");
            
            Estado = EstadoReserva.Cancelada;
            Evento.LiberarCupos(CantidadEntradas);
        }

        public override string ToString()
        {
            return $"Reserva #{Id} - {Usuario.Nombre} - Evento: {Evento.Nombre} - Entradas: {CantidadEntradas} - Total: {ValorTotal:C} - Estado: {Estado}";
        }
    }
}