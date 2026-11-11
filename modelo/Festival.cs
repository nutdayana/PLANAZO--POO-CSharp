public class Festival : Evento
    {
        public Festival(int id, string nombre, DateTime fecha, decimal precioBase, int capacidad)
            : base(id, nombre, fecha, precioBase, capacidad) { }

        public override decimal CalcularPrecioFinal() => PrecioBase * 1.20m; // +20% por ser festival
    }
