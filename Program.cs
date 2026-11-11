using PLANAZO.Servicios;

namespace PLANAZO
{
    class Program
    {
        static void Main(string[] args)
        {
            GestorReservas gestor = new GestorReservas();
            bool salir = false;

            while (!salir)
            {
                Console.WriteLine("\n================================");
                Console.WriteLine("        PLANAZO - EVENTOS       ");
                Console.WriteLine("================================");
                Console.WriteLine("1. Ver eventos disponibles");
                Console.WriteLine("2. Consultar evento");
                Console.WriteLine("3. Reservar entradas");
                Console.WriteLine("4. Ver reservas realizadas");
                Console.WriteLine("5. Cancelar una reserva");
                Console.WriteLine("6. Ver resumen de reservas");
                Console.WriteLine("0. Salir");
                Console.Write("Seleccione una opción: ");

                try
                {
                    string opcion = Console.ReadLine();
                    switch (opcion)
                    {
                        case "1":
                            foreach (var ev in gestor.ObtenerEventos()) Console.WriteLine(ev);
                            break;
                        case "2":
                            Console.Write("Ingrese ID del evento: ");
                            int idBuscar = int.Parse(Console.ReadLine());
                            Console.WriteLine(gestor.BuscarEvento(idBuscar));
                            break;
                        case "3":
                            Console.Write("ID Usuario (1 o 2): ");
                            int idUser = int.Parse(Console.ReadLine());
                            Console.Write("ID Evento: ");
                            int idEv = int.Parse(Console.ReadLine());
                            Console.Write("Cantidad entradas: ");
                            int cant = int.Parse(Console.ReadLine());
                            gestor.CrearReserva(idUser, idEv, cant);
                            Console.WriteLine("Reserva creada exitosamente.");
                            break;
                        case "4":
                            foreach (var res in gestor.ObtenerReservas()) Console.WriteLine(res);
                            break;
                        case "5":
                            Console.Write("ID de la reserva a cancelar: ");
                            int idRes = int.Parse(Console.ReadLine());
                            gestor.CancelarReserva(idRes);
                            Console.WriteLine("Reserva cancelada y cupos liberados.");
                            break;
                        case "6":
                            gestor.MostrarResumen();
                            break;
                        case "0":
                            salir = true;
                            break;
                        default:
                            Console.WriteLine("Opción inválida.");
                            break;
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Error: Ingrese un número válido.");
                }
                catch (Exception ex)
                {
                    // Captura de excepciones de lógica de negocio (cupos, cancelaciones dobles, etc.)
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }
    }
}