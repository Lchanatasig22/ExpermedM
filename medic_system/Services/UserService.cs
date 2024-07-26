using medic_system.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace medic_system.Services
{
    public class UserService
    {
        private readonly medical_systemContext _context;

        public UserService(medical_systemContext context)
        {
            _context = context;
        }

        // Método para listar usuarios
        public async Task<List<Usuario>> GetAllUserAsync()
        {
            return await _context.Usuarios
                .Include(u => u.Perfil)          // Incluye la relación con Perfil
                .Include(u => u.Especialidad)    // Incluye la relación con Especialidad
                .Include(u => u.Establecimiento) // Incluye la relación con Establecimiento
                .ToListAsync();
        }

        //Metodo para crear un usuario
        public async Task<int> CreateUserAsync(Usuario usuario)
        {
            using (var connection = new SqlConnection(_context.Database.GetDbConnection().ConnectionString))
            {
                using (var command = new SqlCommand("sp_CreateUser", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@ci_usuario", usuario.CiUsuario);
                    command.Parameters.AddWithValue("@nombres_usuario", usuario.NombresUsuario);
                    command.Parameters.AddWithValue("@apellidos_usuario", usuario.ApellidosUsuario);
                    command.Parameters.AddWithValue("@telefono_usuario", usuario.TelefonoUsuario);
                    command.Parameters.AddWithValue("@email_usuario", usuario.EmailUsuario);
                    command.Parameters.AddWithValue("@fechacreacion_usuario", usuario.FechacreacionUsuario);
                    command.Parameters.AddWithValue("@fechamodificacion_usuario", usuario.FechamodificacionUsuario);
                    command.Parameters.AddWithValue("@login_usuario", usuario.LoginUsuario);
                    command.Parameters.AddWithValue("@clave_usuario", usuario.ClaveUsuario);
                    command.Parameters.AddWithValue("@codigo_usuario", usuario.CodigoUsuario);
                    command.Parameters.AddWithValue("@estado_usuario", usuario.EstadoUsuario);
                    command.Parameters.AddWithValue("@perfil_id", usuario.PerfilId);
                    command.Parameters.AddWithValue("@establecimiento_id", usuario.EstablecimientoId);
                    command.Parameters.AddWithValue("@especialidad_id", usuario.EspecialidadId);

                    connection.Open();
                    var result = await command.ExecuteScalarAsync();
                    return Convert.ToInt32(result);
                }
            }
        }
        // Metodo para editar un usuario
        public async Task EditUserAsync(Usuario usuario)
        {
            using (var connection = new SqlConnection(_context.Database.GetDbConnection().ConnectionString))
            {
                using (var command = new SqlCommand("sp_EditUser", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@id_usuario", usuario.IdUsuario);
                    command.Parameters.AddWithValue("@ci_usuario", usuario.CiUsuario);
                    command.Parameters.AddWithValue("@nombres_usuario", usuario.NombresUsuario);
                    command.Parameters.AddWithValue("@apellidos_usuario", usuario.ApellidosUsuario);
                    command.Parameters.AddWithValue("@telefono_usuario", usuario.TelefonoUsuario);
                    command.Parameters.AddWithValue("@email_usuario", usuario.EmailUsuario);
                    command.Parameters.AddWithValue("@fechamodificacion_usuario", usuario.FechamodificacionUsuario);
                    command.Parameters.AddWithValue("@login_usuario", usuario.LoginUsuario);
                    command.Parameters.AddWithValue("@clave_usuario", usuario.ClaveUsuario);
                    command.Parameters.AddWithValue("@codigo_usuario", usuario.CodigoUsuario);
                    command.Parameters.AddWithValue("@estado_usuario", usuario.EstadoUsuario);
                    command.Parameters.AddWithValue("@perfil_id", usuario.PerfilId);
                    command.Parameters.AddWithValue("@establecimiento_id", usuario.EstablecimientoId);
                    command.Parameters.AddWithValue("@especialidad_id", usuario.EspecialidadId);

                    await connection.OpenAsync();
                    await command.ExecuteNonQueryAsync();
                }
            }
        }


        // Método para eliminar un usuario
        public async Task DeleteUserAsync(int idUsuario)
        {
            using (var connection = new SqlConnection(_context.Database.GetDbConnection().ConnectionString))
            {
                using (var command = new SqlCommand("sp_DeleteUser", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@id_usuario", idUsuario);

                    await connection.OpenAsync();
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

    }
}
