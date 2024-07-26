using System;
using System.Collections.Generic;

namespace medic_system.Models
{
    public partial class Catalogo
    {
        public Catalogo()
        {
            PacienteEstadocivilPacientesCaNavigations = new HashSet<Paciente>();
            PacienteFormacionprofesionalPacientesCaNavigations = new HashSet<Paciente>();
            PacienteSegurosaludPacientesCaNavigations = new HashSet<Paciente>();
            PacienteSexoPacientesCaNavigations = new HashSet<Paciente>();
            PacienteTipodocumentoPacientesCaNavigations = new HashSet<Paciente>();
            PacienteTiposangrePacientesCaNavigations = new HashSet<Paciente>();
        }

        public int IdCatalogo { get; set; }
        public DateTime? FechacreacionCatalogo { get; set; }
        public string? UsuariocreacionCatalogo { get; set; }
        public DateTime? FechamodificacionCatalogo { get; set; }
        public string? UsuariomodificacionCatalogo { get; set; }
        public string? DescripcionCatalogo { get; set; }
        public string? CategoriaCatalogo { get; set; }
        public Guid UuidCatalogo { get; set; }
        public int? EstadoCatalogo { get; set; }

        public virtual ICollection<Paciente> PacienteEstadocivilPacientesCaNavigations { get; set; }
        public virtual ICollection<Paciente> PacienteFormacionprofesionalPacientesCaNavigations { get; set; }
        public virtual ICollection<Paciente> PacienteSegurosaludPacientesCaNavigations { get; set; }
        public virtual ICollection<Paciente> PacienteSexoPacientesCaNavigations { get; set; }
        public virtual ICollection<Paciente> PacienteTipodocumentoPacientesCaNavigations { get; set; }
        public virtual ICollection<Paciente> PacienteTiposangrePacientesCaNavigations { get; set; }
    }
}
