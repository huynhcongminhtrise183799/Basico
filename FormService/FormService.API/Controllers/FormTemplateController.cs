using FormService.Application.Command;
using FormService.Application.DTOs.Request;
using FormService.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace FormService.API.Controllers
{
    [Route("api/")]
    [ApiController]
    public class FormTemplateController : ControllerBase
    {
        private readonly IMediator _mediator;
        public FormTemplateController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("template")]
        public async Task<IActionResult> CreateFormTemplate([FromBody] CreateFormTemplateRequest request)
        {
            if (!ModelState.IsValid)
            {
                var firstError = ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage)
                .FirstOrDefault();

                return BadRequest(new
                {
                    message = firstError
                });
            }
            var command = new CreateFormTemplateCommand(
                request.ServiceId,
                request.FormTemplateName,
                request.FormTemplateData,
				request.Price
			);
            var result = await _mediator.Send(command);
            if (result == null)
            {
                return BadRequest(new
                {
                    message = "Form template could not be created."
                });
            }
            return Ok(result);
        }
        [HttpGet("templates")]
        [SwaggerOperation(
    Summary = "Lấy tất cả template"

        )]
        public async Task<IActionResult> GetAllFormTemplates()
        {

            var result = await _mediator.Send(new GetAllFormTemplatesQuery());
            return Ok(result);
        }

        [HttpGet("templates-active")]
        [SwaggerOperation(
    Summary = "Lấy tất cả template có status = active"

        )]
        public async Task<IActionResult> GetAllFormTemplatesActive()
        {
            var result = await _mediator.Send(new GetAllFormTemplatesActiveQuery());
            return Ok(result);
        }
        [HttpGet("template/{id}")]
        [SwaggerOperation(
    Summary = "Lấy template theo template id "

        )]
        public async Task<IActionResult> GetAllFormTemplatesByTemplateId(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest(new
                {
                    message = "Template ID cannot be null or empty."
                });
            }
            var result = await _mediator.Send(new GetFormTemplateByIdQuery(Guid.Parse(id)));
            if (result == null)
            {
                return NotFound(new
                {
                    message = "Form template not found."
                });
            }
            return Ok(result);
        }
        [HttpPut("template/{id}")]
        [SwaggerOperation(
    Summary = "Update template"

        )]
        public async Task<IActionResult> UpdateFormTemplate(string id, [FromBody] UpdateFormTemplateRequest request)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest(new
                {
                    message = "Template ID cannot be null or empty."
                });
            }
            if (!ModelState.IsValid)
            {
                var firstError = ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage)
                .FirstOrDefault();

                return BadRequest(new
                {
                    message = firstError
                });
            }
            var command = new UpdateFormTemplateCommand(
                Guid.Parse(id),
                request.FormTemplateName,
                request.FormTemplateData,
                 request.ServiceId,

                request.Status,
				request.Price
			);
            var result = await _mediator.Send(command);
            if (result == null)
            {
                return NotFound(new
                {
                    message = "Form template not found."
                });
            }
            return Ok(result);
        }
        
        [HttpDelete("template/{id}")]
        [SwaggerOperation(
    Summary = "Xóa template"

        )]
        public async Task<IActionResult> DeleteFormTemplate(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest(new
                {
                    message = "Template ID cannot be null or empty."
                });
            }
            var command = new DeleteFormTemplateCommand(Guid.Parse(id));
            var result = await _mediator.Send(command);
            if (result == null)
            {
                return NotFound(new
                {
                    message = "Form template not found."
                });
            }
            return Ok(new
            {
                message = "Form template deleted successfully."
            });
        }
        [HttpGet("template")]
        [SwaggerOperation(
    Summary = "Lấy danh sách template có phân trang ",
    Description = "Trả về danh sách các form template theo số trang truyền vào. Số trang phải lớn hơn 0."
        )]
        [SwaggerResponse(200, "Thành công, trả về danh sách form template")]
        [SwaggerResponse(400, "Page number không hợp lệ")]
        [SwaggerResponse(404, "Không tìm thấy form template nào")]
        public async Task<IActionResult> GetFormTemplatesPagition([FromQuery] int page)
        {
            if (page < 1)
            {
                return BadRequest(new
                {
                    message = "Page number must be greater than 0."
                });
            }
            var result = await _mediator.Send(new GetFormTemplatesPagitionQuery(page));
            if (result == null || !result.Any())
            {
                return NotFound(new
                {
                    message = "No form templates found."
                });
            }
            return Ok(result);
        }
    }
}
