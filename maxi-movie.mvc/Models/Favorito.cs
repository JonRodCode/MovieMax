using System.ComponentModel.DataAnnotations;

namespace maxi_movie.mvc.Models
{
    public class Favorito
    {
        public int Id { get; set; }
        public string UsuarioId { get; set; }
        public Usuario? Usuario { get; set; }
        public int PeliculaId { get; set; }
        public Pelicula? Pelicula { get; set; }
        [DataType(DataType.Date)]
        public DateTime Fecha { get; set; }
    }
}
