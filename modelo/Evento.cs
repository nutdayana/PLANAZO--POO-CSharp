namespace PLANAZO.Modelos
{
    public abstract class Evento
    {
        public int Id { get; private set; }
        public string Nombre { get; private set; }
        public DateTime Fecha { get; private set; }
        public decimal PrecioBase { get; protected set; }
        public int CapacidadMaxima { get; private set; }
        public int CuposDisponibles { get; private set; }

        public Evento(int id, string nombre, DateTime fecha, decimal precioBase, int capacidadMaxima)
        {
            Id = id;
            Nombre = nombre;
            Fecha = fecha;
            PrecioBase = precioBase;
            CapacidadMaxima = capacidadMaxima;
            CuposDisponibles = capacidadMaxima;
        }

        // Método abstracto para Polimorfismo
        public abstract decimal CalcularPrecioFinal();

        public void ReservarCupos(int cantidad)
        {
            if (cantidad > CuposDisponibles)
                throw new InvalidOperationException("No hay suficientes cupos disponibles.");
            
            CuposDisponibles -= cantidad;
        }

        public void LiberarCupos(int cantidad)
        {
            CuposDisponibles += cantidad;
            if (CuposDisponibles > CapacidadMaxima) CuposDisponibles = CapacidadMaxima;
        }

        public override string ToString()
        {
            return $"[{Id}] {Nombre} - {Fecha:dd/MM/yyyy} - Precio: {CalcularPrecioFinal():C} - Cupos: {CuposDisponibles}";
        }
    }
}