var filaIndex = 1;
var medicamentoIndex = 1;
var laboratorioIndex = 1;
var imagenIndex = 1;

// Añadir nueva fila// Añadir nueva fila
$('#anadirFila').on('click', function () {
    var options;
    try {
        options = tiposDiagnosticoOptions;
    } catch (error) {
        console.error('Error al analizar el JSON:', error, 'Datos JSON:', tiposDiagnosticoOptions);
        return;
    }

    var nuevaFila = `<tr>
        <td>
            <div class="input-group">
                <select class="form-control" id="DiagnosticoId">
                    <option value="">Seleccione...</option>`;
    options.forEach(function (tiposDiagnostico) {
        nuevaFila += `<option value="${tiposDiagnostico.IdDiagnostico}">${tiposDiagnostico.NombreDiagnostico}</option>`;
    });
    nuevaFila += `</select>
                <div class="input-group-append">
                    <span class="input-group-text"><i class="fas fa-search"></i></span>
                </div>
            </div>
        </td>
        <td>
            <div class="btn-group btn-group-toggle" data-toggle="buttons">
                <label class="btn btn-outline-secondary">
                    <input type="checkbox" name="options${filaIndex}" id="presuntivo${filaIndex}" autocomplete="off"> Presuntivo
                </label>
                <label class="btn btn-outline-secondary">
                    <input type="checkbox" name="options${filaIndex}" id="definitivo${filaIndex}" autocomplete="off"> Definitivo
                </label>
            </div>
        </td>
        <td>
            <button type="button" class="btn btn-outline-secondary eliminar-fila"><i class="fas fa-times-circle"></i> Eliminar</button>
        </td>
    </tr>`;
    $('#diagnosticoTableBody').append(nuevaFila);
    filaIndex++;
});

$(document).on('click', '.eliminar-fila', function () {
    $(this).closest('tr').remove();
});// Añadir nueva fila de Medicamentos
$('#anadirFilaMedicamento').on('click', function () {
    var options = tiposMedicamentosOptions;
    var nuevaFila = `<tr>
        <td>
            <div class="input-group">
                <select class="form-control" id="MedicamentoId">
                    <option value="">Seleccione...</option>`;
    options.forEach(function (tiposMedicamento) {
        nuevaFila += `<option value="${tiposMedicamento.IdMedicamento}">${tiposMedicamento.NombreMedicamento}</option>`;
    });
    nuevaFila += `</select>
                <div class="input-group-append">
                    <span class="input-group-text"><i class="fas fa-search"></i></span>
                </div>
            </div>
        </td>
        <td>
            <input type="number" class="form-control" id="Cantidad" placeholder="Cantidad">
        </td>
        <td>
            <button type="button" class="btn btn-outline-secondary eliminar-fila"><i class="fas fa-times-circle"></i> Eliminar</button>
        </td>
    </tr>`;
    $('#medicamentosTableBody').append(nuevaFila);
    medicamentoIndex++;
});

// Añadir nueva fila de Laboratorio
$('#anadirFilaLaboratorio').on('click', function () {
    var options = tiposLaboratoriosOptions;
    var nuevaFila = `<tr>
        <td>
            <div class="input-group">
                <select class="form-control" id="LaboratorioId">
                    <option value="">Seleccione...</option>`;
    options.forEach(function (tiposLaboratorio) {
        nuevaFila += `<option value="${tiposLaboratorio.IdLaboratorio}">${tiposLaboratorio.NombreLaboratorio}</option>`;
    });
    nuevaFila += `</select>
                <div class="input-group-append">
                    <span class="input-group-text"><i class="fas fa-search"></i></span>
                </div>
            </div>
        </td>
        <td>
            <input type="text" class="form-control" id="Resultado" placeholder="Resultado">
        </td>
        <td>
            <button type="button" class="btn btn-outline-secondary eliminar-fila"><i class="fas fa-times-circle"></i> Eliminar</button>
        </td>
    </tr>`;
    $('#laboratorioTableBody').append(nuevaFila);
    laboratorioIndex++;
});

// Añadir nueva fila de Imágenes
$('#anadirFilaImagen').on('click', function () {
    var options = tiposImagenOptions;
    var nuevaFila = `<tr>
        <td>
            <div class="input-group">
                <select class="form-control" id="ImagenId">
                    <option value="">Seleccione...</option>`;
    options.forEach(function (tiposImagen) {
        nuevaFila += `<option value="${tiposImagen.IdImagen}">${tiposImagen.NombreImagen}</option>`;
    });
    nuevaFila += `</select>
                <div class="input-group-append">
                    <span class="input-group-text"><i class="fas fa-search"></i></span>
                </div>
            </div>
        </td>
        <td>
            <input type="text" class="form-control" id="Resultado" placeholder="Resultado">
        </td>
        <td>
            <button type="button" class="btn btn-outline-secondary eliminar-fila"><i class="fas fa-times-circle"></i> Eliminar</button>
        </td>
    </tr>`;
    $('#imagenesTableBody').append(nuevaFila);
    imagenIndex++;
});

$(document).on('click', '.eliminar-fila', function () {
    $(this).closest('tr').remove();
});

var navListItems = $('div.stepwizard-step button'),
    allWells = $('.setup-content');

allWells.hide();
// Manejo de clicks en los pasos del wizard
navListItems.click(function (e) {
    e.preventDefault();
    var $target = $('#step-' + $(this).data('step')),
        $item = $(this);

    if (!$item.hasClass('disabled')) {
        navListItems.removeClass('btn-primary').addClass('btn-secondary');
        $item.addClass('btn-primary');
        allWells.hide();
        $target.show();
        $target.find('input:eq(0)').focus();
    }
});

// Función para manejar el botón "Siguiente"
function goToNextStep() {
    var curStep = $(this).closest(".setup-content"),
        curStepBtn = curStep.attr("id"),
        nextStepWizard = $('div.stepwizard-step button[data-step="' + (parseInt(curStepBtn.split('-')[1]) + 1) + '"]'),
        curInputs = curStep.find("input[type='text'],input[type='url']"),
        isValid = true;

    $(".form-group").removeClass("has-error");
    for (var i = 0; i < curInputs.length; i++) {
        if (!curInputs[i].validity.valid) {
            isValid = false;
            $(curInputs[i]).closest(".form-group").addClass("has-error");
        }
    }

    if (isValid) {
        nextStepWizard.removeAttr('disabled').trigger('click');
    }
}

// Función para manejar el botón "Regresar"
$('div.setup-content button.previousBtn').click(function () {
    var curStep = $(this).closest(".setup-content"),
        curStepBtn = curStep.attr("id"),
        prevStepWizard = $('div.stepwizard-step button[data-step="' + (parseInt(curStepBtn.split('-')[1]) - 1) + '"]');

    navListItems.removeClass('btn-primary').addClass('btn-secondary');
    prevStepWizard.addClass('btn-primary');
    allWells.hide();
    $('#step-' + (parseInt(curStepBtn.split('-')[1]) - 1)).show();
});

// Mostrar u ocultar campos de observación al cambiar los switches
$('.consulta-antecedente-checked').change(function () {
    var isChecked = $(this).prop('checked');
    var $observacionField = $(this).closest('.fields').find('.consulta-antecedente-observacion');

    if (isChecked) {
        $observacionField.show();
        $observacionField.find('input').removeAttr('disabled');
    } else {
        $observacionField.hide();
        $observacionField.find('input').attr('disabled', 'disabled');
    }
});

// Attach the goToNextStep function to the buttons
$('div.setup-content button.nextBtn').on('click', goToNextStep);
