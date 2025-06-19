using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormService.Application.DTOs.Request
{
    public class CreateFormTemplateRequest
    {
        [Required(ErrorMessage = "ServiceId is required")]
        public Guid ServiceId { get; set; }

        [Required(ErrorMessage = "FormTemplateName is required")]
        public string FormTemplateName { get; set; }

        [Required(ErrorMessage = "FormTemplateData is required")]
        public string FormTemplateData { get; set; }

		[Required(ErrorMessage = "Price is required")]
		[Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than zero")]
		public double Price { get; set; }
	}
}
