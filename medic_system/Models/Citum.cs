using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace medic_system.Models
{
    public partial class Citum
    {
        public Citum()
        {
            Facturacions = new HashSet<Facturacion>();
        }

        public int IdCita { get; set; }

        [DataType(DataType.Date)]
        public DateTime? FechacreacionCita { get; set; }

        public string? UsuariocreacionCita { get; set; }

        [Required(ErrorMessage = "La fecha de la cita es obligatoria.")]
        [DataType(DataType.Date)]
        public DateTime? FechadelacitaCita { get; set; }

        [Required(ErrorMessage = "La hora de la cita es obligatoria.")]
        public TimeSpan? HoradelacitaCita { get; set; }

        [Required]
        public int? UsuarioId { get; set; }

        [Required]
        public int? PacienteId { get; set; }

        public int? ConsultaId { get; set; }

        [Required(ErrorMessage = "El motivo de la consulta es obligatorio.")]
        public string Motivo { get; set; } = null!;

        public virtual Consultum? Consulta { get; set; }
        public virtual Paciente? Paciente { get; set; }
        public virtual Usuario? Usuario { get; set; }
        public virtual ICollection<Facturacion> Facturacions { get; set; }
    }
}
