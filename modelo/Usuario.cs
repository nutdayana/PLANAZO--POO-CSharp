namespace PLANAZO.Modelos
{
    public class Usuario
    {
        public int Id { get; private set; }
        public string Nombre { get; private set; }
        public string Email { get; private set; }

        public Usuario(int id, string nombre, string email)
        {
            Id = id;
            Nombre = nombre;
            Email = email;
        }
    }
}