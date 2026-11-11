using PLANAZO.Modelos;

namespace PLANAZO.Servicios
{
    public class GestorReservas
    {
        private List<Evento> _eventos;
        private List<Reserva> _reservas;
        private List<Usuario> _usuarios;
        private int _nextReservaId = 1;

        public GestorReservas()
        {
            _eventos = new List<Evento>();
            _reservas = new List<Reserva>();
            _usuarios = new List<Usuario>();
            CargarDatosIniciales();
        }

        private void CargarDatosIniciales()
        {
            // Usuarios de prueba
            _usuarios.Add(new Usuario(1, "Ana Gomez", "ana@mail.com"));
            _usuarios.Add(new Usuario(2, "Carlos Ruiz", "carlos@mail.com"));

            // 6 Eventos iniciales (2 Cine, 2 Concierto, 2 Taller)
            _eventos.Add(new Cine(1, "Inception", DateTime.Now.AddDays(5), 15000, 50));
            _eventos.Add(new Cine(2, "Matrix", DateTime.Now.AddDays(6), 12000, 40));
            _eventos.Add(new Concierto(3, "Rock Fest", DateTime.Now.AddDays(10), 50000, 100));
            _eventos.Add(new Concierto(4, "Jazz Night", DateTime.Now.AddDays(12), 40000, 80));
            _eventos.Add(new Taller(5, "C# Básico", DateTime.Now.AddDays(15), 30000, 20));
            _eventos.Add(new Taller(6, "Diseño UX", DateTime.Now.AddDays(18), 25000, 15));
            // Evento Reto 1
            _eventos.Add(new Festival(7, "Festival de Verano", DateTime.Now.AddDays(30), 80000, 200));
        }

        public List<Evento> ObtenerEventos() => _eventos;

        public Evento BuscarEvento(int id)
        {
            var evento = _eventos.FirstOrDefault(e => e.Id == id);
            if (evento == null) throw new KeyNotFoundException("Evento no encontrado.");
            return evento;
        }

        public void CrearReserva(int usuarioId, int eventoId, int cantidad)
        {
            var usuario = _usuarios.FirstOrDefault(u => u.Id == usuarioId);
            if (usuario == null) throw new Exception("Usuario no encontrado.");

            var evento = BuscarEvento(eventoId);

            // Validar y reservar cupos en el evento
            evento.ReservarCupos(cantidad);

            // Crear la reserva
            var nuevaReserva = new Reserva(_nextReservaId++, usuario, evento, cantidad);
            _reservas.Add(nuevaReserva);
        }

        public void CancelarReserva(int reservaId)
        {
            var reserva = _reservas.FirstOrDefault(r => r.Id == reservaId);
            if (reserva == null) throw new KeyNotFoundException("Reserva no encontrada.");
            
            reserva.Cancelar(); // El método interno valida si ya está cancelada
        }

        public List<Reserva> ObtenerReservas() => _reservas;

        public void MostrarResumen()
        {
            var activas = _reservas.Where(r => r.Estado == EstadoReserva.Activa).ToList();
            int totalEntradas = activas.Sum(r => r.CantidadEntradas);
            decimal totalDinero = activas.Sum(r => r.ValorTotal);

            Console.WriteLine("\n--- RESUMEN DE RESERVAS ACTIVAS ---");
            Console.WriteLine($"Cantidad de reservas activas: {activas.Count}");
            Console.WriteLine($"Total de entradas reservadas: {totalEntradas}");
            Console.WriteLine($"Valor total recaudado: {totalDinero:C}");
        }
        
        // Reto 2: Consultar reservas por usuario
        public void MostrarReservasPorUsuario(int usuarioId)
        {
            var reservasUsuario = _reservas.Where(r => r.Usuario.Id == usuarioId).ToList();
            if (!reservasUsuario.Any()) { Console.WriteLine("El usuario no tiene reservas."); return; }
            
            foreach (var r in reservasUsuario) Console.WriteLine(r);
        }
    }
}