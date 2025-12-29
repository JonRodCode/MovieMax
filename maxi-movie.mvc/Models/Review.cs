using System.ComponentModel.DataAnnotations;

namespace maxi_movie.mvc.Models
{
    public class Review
    {
        public int Id { get; set; }

        public int PeliculaId { get; set; }

        public Pelicula? Pelicula { get; set; }

        public string UsuarioId { get; set; }

        public Usuario? Usuario { get; set; }

        [Range(1, 5)]
        public int Rating { get; set; }
        [Required]
        [StringLength(500)]
        public string Comentario { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime FechaReview { get; set; }


        // Control de concurrencia
        [Timestamp]
        public byte[] RowVersion { get; set; }

    }
}
