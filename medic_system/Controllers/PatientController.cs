using Microsoft.AspNetCore.Mvc;
using medic_system.Models;
using medic_system.Services;
using System.Threading.Tasks;

namespace medic_system.Controllers
{
    public class PatientController : Controller
    {
        private readonly PatientService _pacienteService;
        private readonly CatalogService _catalogService;

        public PatientController(PatientService pacienteService, CatalogService catalogService)
        {
            _pacienteService = pacienteService;
            _catalogService = catalogService;
        }

        [HttpGet]
        public async Task<IActionResult> CrearPaciente()
        {
            ViewBag.UsuarioNombre = HttpContext.Session.GetString("UsuarioNombre");

            ViewBag.TiposDocumentos = await _catalogService.ObtenerTiposDocumentosAsync();
            ViewBag.TiposSangre = await _catalogService.ObtenerTiposDeSangreAsync();
            ViewBag.TiposGenero = await _catalogService.ObtenerTiposDeGeneroAsync();
            ViewBag.TiposEstadoCivil = await _catalogService.ObtenerTiposDeEstadoCivilAsync();
            ViewBag.TiposFormacion = await _catalogService.ObtenerTiposDeFormacionPAsync();
            ViewBag.TiposNacionalidad = await _catalogService.ObtenerTiposDeNacionalidadPAsync();
            ViewBag.TiposProvincia = await _catalogService.ObtenerTiposDeProvinciaPAsync();
            ViewBag.TiposSeguro = await _catalogService.ObtenerTiposDeSeguroAsync();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CrearPaciente(Paciente paciente)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    paciente.UsuariocreacionPacientes = HttpContext.Session.GetString("UsuarioNombre");
                    paciente.FechacreacionPacientes = DateTime.Now;
                    paciente.FechamodificacionPacientes = DateTime.Now;
                    paciente.UsuariomodificacionPacientes = HttpContext.Session.GetString("UsuarioNombre");
                    paciente.EstadoPacientes = 1;
                    await _pacienteService.CreatePatientAsync(paciente);
                    TempData["SuccessMessage"] = "Paciente creado exitosamente.";
                    return RedirectToAction("ListarPacientes");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, $"Error al crear el paciente: {ex.Message}");
                }
            }

            ViewBag.TiposDocumentos = await _catalogService.ObtenerTiposDocumentosAsync();
            ViewBag.TiposSangre = await _catalogService.ObtenerTiposDeSangreAsync();
            ViewBag.TiposGenero = await _catalogService.ObtenerTiposDeGeneroAsync();
            ViewBag.TiposEstadoCivil = await _catalogService.ObtenerTiposDeEstadoCivilAsync();
            ViewBag.TiposFormacion = await _catalogService.ObtenerTiposDeFormacionPAsync();
            ViewBag.TiposNacionalidad = await _catalogService.ObtenerTiposDeNacionalidadPAsync();
            ViewBag.TiposProvincia = await _catalogService.ObtenerTiposDeProvinciaPAsync();
            ViewBag.TiposSeguro = await _catalogService.ObtenerTiposDeSeguroAsync();

            return View(paciente);
        }

        [HttpGet]
        public async Task<IActionResult> EditarPaciente(int id)
        {
            var paciente = await _pacienteService.GetPacienteByIdAsync(id);
            if (paciente == null)
            {
                return NotFound();
            }

            await CargarListasDesplegables();
            return View(paciente);
        }

        private async Task CargarListasDesplegables()
        {
            ViewBag.TiposDocumentos = await _catalogService.ObtenerTiposDocumentosAsync();
            ViewBag.TiposSangre = await _catalogService.ObtenerTiposDeSangreAsync();
            ViewBag.TiposGenero = await _catalogService.ObtenerTiposDeGeneroAsync();
            ViewBag.TiposEstadoCivil = await _catalogService.ObtenerTiposDeEstadoCivilAsync();
            ViewBag.TiposFormacion = await _catalogService.ObtenerTiposDeFormacionPAsync();
            ViewBag.TiposNacionalidad = await _catalogService.ObtenerTiposDeNacionalidadPAsync();
            ViewBag.TiposProvincia = await _catalogService.ObtenerTiposDeProvinciaPAsync();
            ViewBag.TiposSeguro = await _catalogService.ObtenerTiposDeSeguroAsync();
        }

        [HttpPost]
        public async Task<IActionResult> EditarPaciente(Paciente paciente)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    paciente.UsuariomodificacionPacientes = HttpContext.Session.GetString("UsuarioNombre");
                    paciente.FechamodificacionPacientes = DateTime.Now;
                    await _pacienteService.EditPatientAsync(paciente);

                    TempData["SuccessMessage"] = "Paciente actualizado exitosamente.";
                    return RedirectToAction("ListarPacientes");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, $"Error al actualizar el paciente: {ex.Message}");
                }
            }

            await CargarListasDesplegables();
            return View(paciente);
        }


        [HttpPost]
        public async Task<IActionResult> EliminarPaciente(int id)
        {
            try
            {
                await _pacienteService.DeletePatientAsync(id);
                TempData["SuccessMessage"] = "Paciente eliminado exitosamente.";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error al eliminar el paciente: {ex.Message}";
            }

            return RedirectToAction("ListarPacientes");
        }
        [HttpGet]
        public async Task<IActionResult> ListarPacientes()
        {
            var pacientes = await _pacienteService.GetAllPacientesAsync();
            return View(pacientes);
        }
    }
}
