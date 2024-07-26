using System.Data;
using System.Data.SqlClient;
using medic_system.Models;
using medic_system.Services;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
public class ConsultationService
{
    private readonly medical_systemContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;
    public ConsultationService(medical_systemContext context, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _httpContextAccessor = httpContextAccessor;
    }
    public async Task<List<Consultum>> GetAllConsultasAsync()
    {
        // Obtener el nombre de usuario de la sesión
        var loginUsuario = _httpContextAccessor.HttpContext.Session.GetString("UsuarioNombre");

        if (string.IsNullOrEmpty(loginUsuario))
        {
            throw new Exception("El nombre de usuario no está disponible en la sesión.");
        }

        // Filtrar las consultas por el usuario de creación y el estado igual a 0
        var consultas = await _context.Consulta
            .Where(c => c.UsuariocreacionConsulta == loginUsuario)
            .Include(c => c.ConsultaDiagnostico)
            .Include(c => c.ConsultaImagen)
            .Include(c => c.ConsultaLaboratorio)
            .Include(c => c.ConsultaMedicamento)
            .Include(c => c.PacienteConsultaPNavigation)
            .OrderBy(c => c.FechacreacionConsulta) // Ordenar por fecha de la consulta Ocupar esto mismo para cualquier tabla 

            .ToListAsync();

        return consultas;
    }

    public async Task DeleteConsultasByPacienteIdAsync(int pacienteId)
    {
        var consultas = await _context.Consulta.Where(c => c.PacienteConsultaP == pacienteId).ToListAsync();
        _context.Consulta.RemoveRange(consultas);
        await _context.SaveChangesAsync();
    }
    public async Task<Consultum> GetConsultaByIdAsync(int id)
    {
        return await _context.Consulta
            .Include(c => c.ConsultaMedicamento) // Incluye las relaciones necesarias
            .Include(c => c.ConsultaLaboratorio)
            .Include(c => c.ConsultaImagen)
            .Include(c => c.ConsultaDiagnostico)
            .Include(c => c.ConsultaAntecedentesFamiliares)
            .Include(c => c.ConsultaOrganosSistemas)
            .Include(c => c.ConsultaExamenFisico)
            .FirstOrDefaultAsync(c => c.IdConsulta == id);
    }
    //Creacion consulta
    public async Task<int> CreateConsultationAsync(
            DateTime fechacreacionConsulta,
            string usuariocreacionConsulta,
            string historialConsulta,
            string secuencialConsulta,
            int pacienteConsultaP,
            string motivoConsulta,
            string enfermedadConsulta,
            string nombreparienteConsulta,
            string alergiasConsulta,
            string reconofarmacologicas,
            int tipoparienteConsulta,
            string telefonoConsulta,
            string temperaturaConsulta,
            string frecuenciarespiratoriaConsulta,
            string presionarterialsistolicaConsulta,
            string presionarterialdiastolicaConsulta,
            string pulsoConsulta,
            string pesoConsulta,
            string tallaConsulta,
            string plantratamientoConsulta,
            string observacionConsulta,
            string antecedentespersonalesConsulta,
            int diasincapacidadConsulta,
            int medicoConsultaD,
            int especialidadId,
            int estadoConsultaC,
            int tipoConsultaC,
            string notasevolucionConsulta,
            string consultaprincipalConsulta,
            int activoConsulta,
            DateTime fechacreacionMedicamento,
            int medicamentoId,
            int dosisMedicamento,
            string observacionMedicamento,
            int estadoMedicamento,
            int cantidadLaboratorio,
            string observacionLaboratorio,
            int catalogoLaboratorioId,
            int estadoLaboratorio,
            int imagenId,
            string observacionImagen,
            int cantidadImagen,
            int estadoImagen,
            int diagnosticoId,
            string observacionDiagnostico,
            bool presuntivoDiagnosticos,
            bool definitivoDiagnosticos,
            int estadoDiagnostico,
            bool cardiopatia,
            string obserCardiopatia,
            bool diabetes,
            string obserDiabetes,
            bool enfCardiovascular,
            string obserEnfCardiovascular,
            bool hipertension,
            string obserHipertension,
            bool cancer,
            string obserCancer,
            bool tuberculosis,
            string obserTuberculosis,
            bool enfMental,
            string obserEnfMental,
            bool enfInfecciosa,
            string obserEnfInfecciosa,
            bool malFormacion,
            string obserMalFormacion,
            bool otro,
            string obserOtro,
            bool alergiasAntecedentes,
            string obserAlergias,
            bool cirugias,
            string obserCirugias,
            bool orgSentidos,
            string obserOrgSentidos,
            bool respiratorio,
            string obserRespiratorio,
            bool cardioVascular,
            string obserCardioVascular,
            bool digestivo,
            string obserDigestivo,
            bool genital,
            string obserGenital,
            bool urinario,
            string obserUrinario,
            bool mEsqueletico,
            string obserMEsqueletico,
            bool endocrino,
            string obserEndocrino,
            bool linfatico,
            string obserLinfatico,
            bool nervioso,
            string obserNervioso,
            bool cabeza,
            string obserCabeza,
            bool cuello,
            string obserCuello,
            bool torax,
            string obserTorax,
            bool abdomen,
            string obserAbdomen,
            bool pelvis,
            string obserPelvis,
            bool extremidades,
            string obserExtremidades)
    {
        using (var connection = new SqlConnection(_context.Database.GetConnectionString()))
        {
            using (var command = new SqlCommand("sp_Create_Consultation", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@fechacreacion_consulta", fechacreacionConsulta);
                command.Parameters.AddWithValue("@usuariocreacion_consulta", usuariocreacionConsulta);
                command.Parameters.AddWithValue("@historial_consulta", historialConsulta);
                command.Parameters.AddWithValue("@secuencial_consulta", secuencialConsulta);
                command.Parameters.AddWithValue("@paciente_consulta_p", pacienteConsultaP);
                command.Parameters.AddWithValue("@motivo_consulta", motivoConsulta);
                command.Parameters.AddWithValue("@enfermedad_consulta", enfermedadConsulta);
                command.Parameters.AddWithValue("@nombrepariente_consulta", nombreparienteConsulta);
                command.Parameters.AddWithValue("@alergias_consulta", alergiasConsulta);
                command.Parameters.AddWithValue("@reconofarmacologicas", reconofarmacologicas);
                command.Parameters.AddWithValue("@tipopariente_consulta", tipoparienteConsulta);
                command.Parameters.AddWithValue("@telefono_consulta", telefonoConsulta);
                command.Parameters.AddWithValue("@temperatura_consulta", temperaturaConsulta);
                command.Parameters.AddWithValue("@frecuenciarespiratoria_consulta", frecuenciarespiratoriaConsulta);
                command.Parameters.AddWithValue("@presionarterialsistolica_consulta", presionarterialsistolicaConsulta);
                command.Parameters.AddWithValue("@presionarterialdiastolica_consulta", presionarterialdiastolicaConsulta);
                command.Parameters.AddWithValue("@pulso_consulta", pulsoConsulta);
                command.Parameters.AddWithValue("@peso_consulta", pesoConsulta);
                command.Parameters.AddWithValue("@talla_consulta", tallaConsulta);
                command.Parameters.AddWithValue("@plantratamiento_consulta", plantratamientoConsulta);
                command.Parameters.AddWithValue("@observacion_consulta", observacionConsulta);
                command.Parameters.AddWithValue("@antecedentespersonales_consulta", antecedentespersonalesConsulta);
                command.Parameters.AddWithValue("@diasincapacidad_consulta", diasincapacidadConsulta);
                command.Parameters.AddWithValue("@medico_consulta_d", medicoConsultaD);
                command.Parameters.AddWithValue("@especialidad_id", especialidadId);
                command.Parameters.AddWithValue("@estado_consulta_c", estadoConsultaC);
                command.Parameters.AddWithValue("@tipo_consulta_c", tipoConsultaC);
                command.Parameters.AddWithValue("@notasevolucion_consulta", notasevolucionConsulta);
                command.Parameters.AddWithValue("@consultaprincipal_consulta", consultaprincipalConsulta);
                command.Parameters.AddWithValue("@activo_consulta", activoConsulta);

                command.Parameters.AddWithValue("@fechacreacion_medicamento", fechacreacionMedicamento);
                command.Parameters.AddWithValue("@medicamento_id", medicamentoId);
                command.Parameters.AddWithValue("@dosis_medicamento", dosisMedicamento);
                command.Parameters.AddWithValue("@observacion_medicamento", observacionMedicamento);
                command.Parameters.AddWithValue("@estado_medicamento", estadoMedicamento);

                command.Parameters.AddWithValue("@cantidad_laboratorio", cantidadLaboratorio);
                command.Parameters.AddWithValue("@observacion_laboratorio", observacionLaboratorio);
                command.Parameters.AddWithValue("@catalogo_laboratorio_id", catalogoLaboratorioId);
                command.Parameters.AddWithValue("@estado_laboratorio", estadoLaboratorio);

                command.Parameters.AddWithValue("@imagen_id", imagenId);
                command.Parameters.AddWithValue("@observacion_imagen", observacionImagen);
                command.Parameters.AddWithValue("@cantidad_imagen", cantidadImagen);
                command.Parameters.AddWithValue("@estado_imagen", estadoImagen);

                command.Parameters.AddWithValue("@diagnostico_id", diagnosticoId);
                command.Parameters.AddWithValue("@observacion_diagnostico", observacionDiagnostico);
                command.Parameters.AddWithValue("@presuntivo_diagnosticos", presuntivoDiagnosticos);
                command.Parameters.AddWithValue("@definitivo_diagnosticos", definitivoDiagnosticos);
                command.Parameters.AddWithValue("@estado_diagnostico", estadoDiagnostico);

                command.Parameters.AddWithValue("@cardiopatia", cardiopatia);
                command.Parameters.AddWithValue("@obser_cardiopatia", obserCardiopatia);
                command.Parameters.AddWithValue("@diabetes", diabetes);
                command.Parameters.AddWithValue("@obser_diabetes", obserDiabetes);
                command.Parameters.AddWithValue("@enf_cardiovascular", enfCardiovascular);
                command.Parameters.AddWithValue("@obser_enf_cardiovascular", obserEnfCardiovascular);
                command.Parameters.AddWithValue("@hipertension", hipertension);
                command.Parameters.AddWithValue("@obser_hipertension", obserHipertension);
                command.Parameters.AddWithValue("@cancer", cancer);
                command.Parameters.AddWithValue("@obser_cancer", obserCancer);
                command.Parameters.AddWithValue("@tuberculosis", tuberculosis);
                command.Parameters.AddWithValue("@obser_tuberculosis", obserTuberculosis);
                command.Parameters.AddWithValue("@enf_mental", enfMental);
                command.Parameters.AddWithValue("@obser_enf_mental", obserEnfMental);
                command.Parameters.AddWithValue("@enf_infecciosa", enfInfecciosa);
                command.Parameters.AddWithValue("@obser_enf_infecciosa", obserEnfInfecciosa);
                command.Parameters.AddWithValue("@mal_formacion", malFormacion);
                command.Parameters.AddWithValue("@obser_mal_formacion", obserMalFormacion);
                command.Parameters.AddWithValue("@otro", otro);
                command.Parameters.AddWithValue("@obser_otro", obserOtro);
                command.Parameters.AddWithValue("@alergias_antecedentes", alergiasAntecedentes);
                command.Parameters.AddWithValue("@obser_alergias", obserAlergias);
                command.Parameters.AddWithValue("@cirugias", cirugias);
                command.Parameters.AddWithValue("@obser_cirugias", obserCirugias);

                command.Parameters.AddWithValue("@org_sentidos", orgSentidos);
                command.Parameters.AddWithValue("@obser_org_sentidos", obserOrgSentidos);
                command.Parameters.AddWithValue("@respiratorio", respiratorio);
                command.Parameters.AddWithValue("@obser_respiratorio", obserRespiratorio);
                command.Parameters.AddWithValue("@cardio_vascular", cardioVascular);
                command.Parameters.AddWithValue("@obser_cardio_vascular", obserCardioVascular);
                command.Parameters.AddWithValue("@digestivo", digestivo);
                command.Parameters.AddWithValue("@obser_digestivo", obserDigestivo);
                command.Parameters.AddWithValue("@genital", genital);
                command.Parameters.AddWithValue("@obser_genital", obserGenital);
                command.Parameters.AddWithValue("@urinario", urinario);
                command.Parameters.AddWithValue("@obser_urinario", obserUrinario);
                command.Parameters.AddWithValue("@m_esqueletico", mEsqueletico);
                command.Parameters.AddWithValue("@obser_m_esqueletico", obserMEsqueletico);
                command.Parameters.AddWithValue("@endocrino", endocrino);
                command.Parameters.AddWithValue("@obser_endocrino", obserEndocrino);
                command.Parameters.AddWithValue("@linfatico", linfatico);
                command.Parameters.AddWithValue("@obser_linfatico", obserLinfatico);
                command.Parameters.AddWithValue("@nervioso", nervioso);
                command.Parameters.AddWithValue("@obser_nervioso", obserNervioso);

                command.Parameters.AddWithValue("@cabeza", cabeza);
                command.Parameters.AddWithValue("@obser_cabeza", obserCabeza);
                command.Parameters.AddWithValue("@cuello", cuello);
                command.Parameters.AddWithValue("@obser_cuello", obserCuello);
                command.Parameters.AddWithValue("@torax", torax);
                command.Parameters.AddWithValue("@obser_torax", obserTorax);
                command.Parameters.AddWithValue("@abdomen", abdomen);
                command.Parameters.AddWithValue("@obser_abdomen", obserAbdomen);
                command.Parameters.AddWithValue("@pelvis", pelvis);
                command.Parameters.AddWithValue("@obser_pelvis", obserPelvis);
                command.Parameters.AddWithValue("@extremidades", extremidades);
                command.Parameters.AddWithValue("@obser_extremidades", obserExtremidades);
                // Añadir un parámetro de salida para el ID de la consulta
                var idConsultaParam = new SqlParameter("@NewConsultaID", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };
                command.Parameters.Add(idConsultaParam);

                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();

                // Obtener el valor del parámetro de salida
                int idConsulta = (int)idConsultaParam.Value;
                return idConsulta;
            }
        }
    }

    public async Task UpdateConsultationAsync(Consultum consultation)
    {
        using (var connection = new SqlConnection(_context.Database.GetConnectionString()))
        {
            var command = new SqlCommand("sp_Update_Consultation", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            // Add parameters for consultation
            command.Parameters.AddWithValue("@consulta_id", consultation.IdConsulta);
            command.Parameters.AddWithValue("@fechacreacion_consulta", consultation.FechacreacionConsulta ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@usuariocreacion_consulta", consultation.UsuariocreacionConsulta ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@historial_consulta", consultation.HistorialConsulta ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@secuencial_consulta", consultation.SecuencialConsulta ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@paciente_consulta_p", consultation.PacienteConsultaP);
            command.Parameters.AddWithValue("@motivo_consulta", consultation.MotivoConsulta ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@enfermedad_consulta", consultation.EnfermedadConsulta ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@nombrepariente_consulta", consultation.NombreparienteConsulta ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@alergias_consulta", consultation.AlergiasConsulta ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@reconofarmacologicas", consultation.Reconofarmacologicas ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@tipopariente_consulta", consultation.TipoparienteConsulta);
            command.Parameters.AddWithValue("@telefono_consulta", consultation.TelefonoConsulta ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@temperatura_consulta", consultation.TemperaturaConsulta ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@frecuenciarespiratoria_consulta", consultation.FrecuenciarespiratoriaConsulta ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@presionarterialsistolica_consulta", consultation.PresionarterialsistolicaConsulta ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@presionarterialdiastolica_consulta", consultation.PresionarterialdiastolicaConsulta ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@pulso_consulta", consultation.PulsoConsulta ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@peso_consulta", consultation.PesoConsulta ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@talla_consulta", consultation.TallaConsulta ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@plantratamiento_consulta", consultation.PlantratamientoConsulta ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@observacion_consulta", consultation.ObservacionConsulta ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@antecedentespersonales_consulta", consultation.AntecedentespersonalesConsulta ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@diasincapacidad_consulta", consultation.DiasincapacidadConsulta);
            command.Parameters.AddWithValue("@medico_consulta_d", consultation.MedicoConsultaD);
            command.Parameters.AddWithValue("@especialidad_id", consultation.EspecialidadId);
            command.Parameters.AddWithValue("@estado_consulta_c", consultation.EstadoConsultaC);
            command.Parameters.AddWithValue("@tipo_consulta_c", consultation.TipoConsultaC);
            command.Parameters.AddWithValue("@notasevolucion_consulta", consultation.NotasevolucionConsulta ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@consultaprincipal_consulta", consultation.ConsultaprincipalConsulta ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@activo_consulta", consultation.ActivoConsulta);

            // Add parameters for medication
            var medication = consultation.ConsultaMedicamento;
            command.Parameters.AddWithValue("@fechacreacion_medicamento", medication.FechacreacionMedicamento ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@medicamento_id", medication.MedicamentoId);
            command.Parameters.AddWithValue("@dosis_medicamento", medication.DosisMedicamento);
            command.Parameters.AddWithValue("@observacion_medicamento", medication.ObservacionMedicamento ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@estado_medicamento", medication.EstadoMedicamento);

            // Add parameters for laboratory
            var laboratory = consultation.ConsultaLaboratorio;
            command.Parameters.AddWithValue("@cantidad_laboratorio", laboratory.CantidadLaboratorio);
            command.Parameters.AddWithValue("@observacion_laboratorio", laboratory.Observacion ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@catalogo_laboratorio_id", laboratory.CatalogoLaboratorioId);
            command.Parameters.AddWithValue("@estado_laboratorio", laboratory.EstadoLaboratorio);

            // Add parameters for image
            var image = consultation.ConsultaImagen;
            command.Parameters.AddWithValue("@imagen_id", image.ImagenId);
            command.Parameters.AddWithValue("@observacion_imagen", image.ObservacionImagen ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@cantidad_imagen", image.CantidadImagen);
            command.Parameters.AddWithValue("@estado_imagen", image.EstadoImagen);

            // Add parameters for diagnosis
            var diagnosis = consultation.ConsultaDiagnostico;
            command.Parameters.AddWithValue("@diagnostico_id", diagnosis.DiagnosticoId);
            command.Parameters.AddWithValue("@observacion_diagnostico", diagnosis.ObservacionDiagnostico ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@presuntivo_diagnosticos", diagnosis.PresuntivoDiagnosticos);
            command.Parameters.AddWithValue("@definitivo_diagnosticos", diagnosis.DefinitivoDiagnosticos);
            command.Parameters.AddWithValue("@estado_diagnostico", diagnosis.EstadoDiagnostico);

            // Add parameters for family history
            var familyHistory = consultation.ConsultaAntecedentesFamiliares;
            command.Parameters.AddWithValue("@cardiopatia", familyHistory.Cardiopatia);
            command.Parameters.AddWithValue("@obser_cardiopatia", familyHistory.ObserCardiopatia ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@diabetes", familyHistory.Diabetes);
            command.Parameters.AddWithValue("@obser_diabetes", familyHistory.ObserDiabetes ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@enf_cardiovascular", familyHistory.EnfCardiovascular);
            command.Parameters.AddWithValue("@obser_enf_cardiovascular", familyHistory.ObserEnfCardiovascular ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@hipertension", familyHistory.Hipertension);
            command.Parameters.AddWithValue("@obser_hipertension", familyHistory.ObserHipertension ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@cancer", familyHistory.Cancer);
            command.Parameters.AddWithValue("@obser_cancer", familyHistory.ObserCancer ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@tuberculosis", familyHistory.Tuberculosis);
            command.Parameters.AddWithValue("@obser_tuberculosis", familyHistory.ObserTuberculosis ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@enf_mental", familyHistory.EnfMental);
            command.Parameters.AddWithValue("@obser_enf_mental", familyHistory.ObserEnfMental ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@enf_infecciosa", familyHistory.EnfInfecciosa);
            command.Parameters.AddWithValue("@obser_enf_infecciosa", familyHistory.ObserEnfInfecciosa ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@mal_formacion", familyHistory.MalFormacion);
            command.Parameters.AddWithValue("@obser_mal_formacion", familyHistory.ObserMalFormacion ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@otro", familyHistory.Otro );
            command.Parameters.AddWithValue("@obser_otro", familyHistory.ObserOtro ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@alergias_antecedentes", familyHistory.Alergias);
            command.Parameters.AddWithValue("@obser_alergias", familyHistory.ObserAlergias ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@cirugias", familyHistory.Cirugias);
            command.Parameters.AddWithValue("@obser_cirugias", familyHistory.ObserCirugias ?? (object)DBNull.Value);

            // Add parameters for organ systems
            var organSystems = consultation.ConsultaOrganosSistemas;
            command.Parameters.AddWithValue("@org_sentidos", organSystems.OrgSentidos);
            command.Parameters.AddWithValue("@obser_org_sentidos", organSystems.ObserOrgSentidos ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@respiratorio", organSystems.Respiratorio);
            command.Parameters.AddWithValue("@obser_respiratorio", organSystems.ObserRespiratorio ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@cardio_vascular", organSystems.CardioVascular);
            command.Parameters.AddWithValue("@obser_cardio_vascular", organSystems.ObserCardioVascular ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@digestivo", organSystems.Digestivo);
            command.Parameters.AddWithValue("@obser_digestivo", organSystems.ObserDigestivo ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@genital", organSystems.Genital);
            command.Parameters.AddWithValue("@obser_genital", organSystems.ObserGenital ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@urinario", organSystems.Urinario);
            command.Parameters.AddWithValue("@obser_urinario", organSystems.ObserUrinario ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@m_esqueletico", organSystems.MEsqueletico);
            command.Parameters.AddWithValue("@obser_m_esqueletico", organSystems.ObserMEsqueletico ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@endocrino", organSystems.Endocrino);
            command.Parameters.AddWithValue("@obser_endocrino", organSystems.ObserEndocrino ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@linfatico", organSystems.Linfatico);
            command.Parameters.AddWithValue("@obser_linfatico", organSystems.ObserLinfatico ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@nervioso", organSystems.Nervioso);
            command.Parameters.AddWithValue("@obser_nervioso", organSystems.ObserNervioso ?? (object)DBNull.Value);

            // Add parameters for physical exam
            var physicalExam = consultation.ConsultaExamenFisico;
            command.Parameters.AddWithValue("@cabeza", physicalExam.Cabeza);
            command.Parameters.AddWithValue("@obser_cabeza", physicalExam.ObserCabeza ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@cuello", physicalExam.Cuello);
            command.Parameters.AddWithValue("@obser_cuello", physicalExam.ObserCuello ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@torax", physicalExam.Torax);
            command.Parameters.AddWithValue("@obser_torax", physicalExam.ObserTorax ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@abdomen", physicalExam.Abdomen);
            command.Parameters.AddWithValue("@obser_abdomen", physicalExam.ObserAbdomen ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@pelvis", physicalExam.Pelvis);
            command.Parameters.AddWithValue("@obser_pelvis", physicalExam.ObserPelvis ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@extremidades", physicalExam.Extremidades);
            command.Parameters.AddWithValue("@obser_extremidades", physicalExam.ObserExtremidades ?? (object)DBNull.Value);

            await connection.OpenAsync();
            await command.ExecuteNonQueryAsync();
        }
    }

    public async Task<Paciente> BuscarPacientePorCiAsync(int ci)
    {
        if (ci <= 0)
        {
            throw new ArgumentException("El número de identificación debe ser un entero positivo.", nameof(ci));
        }

        try
        {
            return await _context.Pacientes
                .SingleOrDefaultAsync(p => p.CiPacientes == ci);
        }
        catch (Exception ex)
        {
            // Manejo de la excepción, puedes registrar el error o lanzar una excepción personalizada
            throw new Exception("Error al buscar el paciente por número de identificación.", ex);
        }
    }






}



