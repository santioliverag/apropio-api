using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Apropio.API.Services;

namespace Apropio.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class FilesController : ControllerBase
    {
        private readonly IFileService _fileService;

        public FilesController(IFileService fileService)
        {
            _fileService = fileService;
        }

        /// <summary>
        /// Subir una imagen
        /// </summary>
        [HttpPost("upload")]
        public async Task<ActionResult<object>> UploadImage(
            IFormFile file,
            [FromQuery] string folder = "imagenes")
        {
            try
            {
                if (file == null || file.Length == 0)
                    return BadRequest("No se ha proporcionado ningún archivo");

                if (!_fileService.IsValidImageFile(file))
                    return BadRequest("El archivo no es una imagen válida o excede el tamaño máximo permitido");

                var imageUrl = await _fileService.UploadImageAsync(file, folder);

                return Ok(new
                {
                    success = true,
                    message = "Imagen subida exitosamente",
                    imageUrl = imageUrl,
                    fileName = Path.GetFileName(imageUrl),
                    size = file.Length
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    success = false,
                    message = "Error al subir la imagen",
                    error = ex.Message
                });
            }
        }

        /// <summary>
        /// Subir múltiples imágenes
        /// </summary>
        [HttpPost("upload-multiple")]
        public async Task<ActionResult<object>> UploadMultipleImages(
            List<IFormFile> files,
            [FromQuery] string folder = "imagenes")
        {
            try
            {
                if (files == null || !files.Any())
                    return BadRequest("No se han proporcionado archivos");

                var invalidFiles = files.Where(f => !_fileService.IsValidImageFile(f)).ToList();
                if (invalidFiles.Any())
                    return BadRequest($"Los siguientes archivos no son válidos: {string.Join(", ", invalidFiles.Select(f => f.FileName))}");

                var imageUrls = await _fileService.UploadMultipleImagesAsync(files, folder);

                return Ok(new
                {
                    success = true,
                    message = $"{imageUrls.Count} imágenes subidas exitosamente",
                    images = imageUrls.Select((url, index) => new
                    {
                        imageUrl = url,
                        fileName = Path.GetFileName(url),
                        originalName = files[index].FileName,
                        size = files[index].Length
                    }).ToList()
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    success = false,
                    message = "Error al subir las imágenes",
                    error = ex.Message
                });
            }
        }

        /// <summary>
        /// Eliminar una imagen
        /// </summary>
        [HttpDelete("delete")]
        public async Task<ActionResult<object>> DeleteImage([FromQuery] string imageUrl)
        {
            try
            {
                if (string.IsNullOrEmpty(imageUrl))
                    return BadRequest("URL de imagen requerida");

                var deleted = await _fileService.DeleteImageAsync(imageUrl);

                if (!deleted)
                    return NotFound("Imagen no encontrada");

                return Ok(new
                {
                    success = true,
                    message = "Imagen eliminada exitosamente"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    success = false,
                    message = "Error al eliminar la imagen",
                    error = ex.Message
                });
            }
        }

        /// <summary>
        /// Validar si un archivo es una imagen válida
        /// </summary>
        [HttpPost("validate")]
        public ActionResult<object> ValidateImageFile(IFormFile file)
        {
            if (file == null)
                return BadRequest("No se ha proporcionado ningún archivo");

            var isValid = _fileService.IsValidImageFile(file);

            return Ok(new
            {
                isValid = isValid,
                fileName = file.FileName,
                size = file.Length,
                contentType = file.ContentType,
                message = isValid ? "Archivo válido" : "Archivo inválido"
            });
        }
    }
} 