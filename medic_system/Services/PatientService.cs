using medic_system.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace medic_system.Services
{
    public class PatientService
    {
        private readonly medical_systemContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        /// Siempre que se cree un servicio se tiene que instanciar el DbContext
        /// </summary>
        /// <param name="context"></param>
        public PatientService(medical_systemContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;

        }

        public async Task<List<Paciente>> GetAllPacientesAsync()
        {
            // Obtener el nombre de usuario de la sesión fuera del contexto asincrónico
            var NombreUsuario = _httpContextAccessor.HttpContext?.Session?.GetString("UsuarioNombre");

            // Validar que el nombre de usuario esté disponible
            if (string.IsNullOrEmpty(NombreUsuario))
            {
                throw new InvalidOperationException("El nombre de usuario no está disponible en la sesión.");
            }

            // Filtrar los pacientes por el usuario de creación
            try
            {
                var pacientes = await _context.Pacientes
                    .Where(p => p.UsuariocreacionPacientes == NombreUsuario)
                    .Include(p => p.NacionalidadPacientesPaNavigation)
                    .ToListAsync();

                return pacientes;
            }
            catch (Exception ex)
            {
                // Manejo de errores específico y con un mensaje claro
                throw new Exception("Ocurrió un error al obtener la lista de pacientes.", ex);
            }
        }


        public async Task<int> CreatePatientAsync(Paciente paciente)
        {
            using (var connection = new SqlConnection(_context.Database.GetDbConnection().ConnectionString))
            {
                using (var command = new SqlCommand("sp_CreatePatient", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@fechacreacion_pacientes", paciente.FechacreacionPacientes);
                    command.Parameters.AddWithValue("@usuariocreacion_pacientes", paciente.UsuariocreacionPacientes);
                    command.Parameters.AddWithValue("@fechamodificacion_pacientes", paciente.FechamodificacionPacientes);
                    command.Parameters.AddWithValue("@usuariomodificacion_pacientes", paciente.UsuariomodificacionPacientes);
                    command.Parameters.AddWithValue("@tipodocumento_pacientes_ca", paciente.TipodocumentoPacientesCa);
                    command.Parameters.AddWithValue("@ci_pacientes", paciente.CiPacientes);
                    command.Parameters.AddWithValue("@primernombre_pacientes", paciente.PrimernombrePacientes);
                    command.Parameters.AddWithValue("@segundonombre_pacientes", paciente.SegundonombrePacientes);
                    command.Parameters.AddWithValue("@primerapellido_pacientes", paciente.PrimerapellidoPacientes);
                    command.Parameters.AddWithValue("@segundoapellido_pacientes", paciente.SegundoapellidoPacientes);
                    command.Parameters.AddWithValue("@sexo_pacientes_ca", paciente.SexoPacientesCa);
                    command.Parameters.AddWithValue("@fechanacimiento_pacientes", paciente.FechanacimientoPacientes);
                    command.Parameters.AddWithValue("@edad_pacientes", paciente.EdadPacientes);
                    command.Parameters.AddWithValue("@tiposangre_pacientes_ca", paciente.TiposangrePacientesCa);
                    command.Parameters.AddWithValue("@donante_pacientes", paciente.DonantePacientes);
                    command.Parameters.AddWithValue("@estadocivil_pacientes_ca", paciente.EstadocivilPacientesCa);
                    command.Parameters.AddWithValue("@formacionprofesional_pacientes_ca", paciente.FormacionprofesionalPacientesCa);
                    command.Parameters.AddWithValue("@telefonofijo_pacientes", paciente.TelefonofijoPacientes);
                    command.Parameters.AddWithValue("@telefonocelular_pacientes", paciente.TelefonocelularPacientes);
                    command.Parameters.AddWithValue("@email_pacientes", paciente.EmailPacientes);
                    command.Parameters.AddWithValue("@nacionalidad_pacientes_pa", paciente.NacionalidadPacientesPa);
                    command.Parameters.AddWithValue("@provincia_pacientes_l", paciente.ProvinciaPacientesL);
                    command.Parameters.AddWithValue("@direccion_pacientes", paciente.DireccionPacientes);
                    command.Parameters.AddWithValue("@ocupacion_pacientes", paciente.OcupacionPacientes);
                    command.Parameters.AddWithValue("@empresa_pacientes", paciente.EmpresaPacientes);
                    command.Parameters.AddWithValue("@segurosalud_pacientes_ca", paciente.SegurosaludPacientesCa);

                    await connection.OpenAsync();
                    var result = await command.ExecuteScalarAsync();
                    return Convert.ToInt32(result);
                }
            }
        }


        public async Task EditPatientAsync(Paciente paciente)
        {
            using (var connection = new SqlConnection(_context.Database.GetDbConnection().ConnectionString))
            {
                using (var command = new SqlCommand("sp_EditPatient", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@id_pacientes", paciente.IdPacientes);
                    command.Parameters.AddWithValue("@fechamodificacion_pacientes", paciente.FechamodificacionPacientes);
                    command.Parameters.AddWithValue("@usuariomodificacion_pacientes", paciente.UsuariomodificacionPacientes);
                    command.Parameters.AddWithValue("@tipodocumento_pacientes_ca", paciente.TipodocumentoPacientesCa);
                    command.Parameters.AddWithValue("@ci_pacientes", paciente.CiPacientes);
                    command.Parameters.AddWithValue("@primernombre_pacientes", paciente.PrimernombrePacientes);
                    command.Parameters.AddWithValue("@segundonombre_pacientes", paciente.SegundonombrePacientes);
                    command.Parameters.AddWithValue("@primerapellido_pacientes", paciente.PrimerapellidoPacientes);
                    command.Parameters.AddWithValue("@segundoapellido_pacientes", paciente.SegundoapellidoPacientes);
                    command.Parameters.AddWithValue("@sexo_pacientes_ca", paciente.SexoPacientesCa);
                    command.Parameters.AddWithValue("@fechanacimiento_pacientes", paciente.FechanacimientoPacientes);
                    command.Parameters.AddWithValue("@edad_pacientes", paciente.EdadPacientes);
                    command.Parameters.AddWithValue("@tiposangre_pacientes_ca", paciente.TiposangrePacientesCa);
                    command.Parameters.AddWithValue("@donante_pacientes", paciente.DonantePacientes);
                    command.Parameters.AddWithValue("@estadocivil_pacientes_ca", paciente.EstadocivilPacientesCa);
                    command.Parameters.AddWithValue("@formacionprofesional_pacientes_ca", paciente.FormacionprofesionalPacientesCa);
                    command.Parameters.AddWithValue("@telefonofijo_pacientes", paciente.TelefonofijoPacientes);
                    command.Parameters.AddWithValue("@telefonocelular_pacientes", paciente.TelefonocelularPacientes);
                    command.Parameters.AddWithValue("@email_pacientes", paciente.EmailPacientes);
                    command.Parameters.AddWithValue("@nacionalidad_pacientes_pa", paciente.NacionalidadPacientesPa);
                    command.Parameters.AddWithValue("@provincia_pacientes_l", paciente.ProvinciaPacientesL);
                    command.Parameters.AddWithValue("@direccion_pacientes", paciente.DireccionPacientes);
                    command.Parameters.AddWithValue("@ocupacion_pacientes", paciente.OcupacionPacientes);
                    command.Parameters.AddWithValue("@empresa_pacientes", paciente.EmpresaPacientes);
                    command.Parameters.AddWithValue("@segurosalud_pacientes_ca", paciente.SegurosaludPacientesCa);

                    await connection.OpenAsync();
                    await command.ExecuteNonQueryAsync();
                }
            }
        }


        public async Task<Paciente> GetPacienteByIdAsync(int id)
        {
            return await _context.Pacientes.FindAsync(id);
        }

        public async Task DeletePatientAsync(int idPacientes)
        {
            using (var connection = new SqlConnection(_context.Database.GetDbConnection().ConnectionString))
            {
                using (var command = new SqlCommand("sp_DeletePatient", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@id_pacientes", idPacientes);

                    await connection.OpenAsync();
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

    }
}
