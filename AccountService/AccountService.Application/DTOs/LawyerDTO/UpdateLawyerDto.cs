using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Application.DTOs.LawyerDTO
{
    public class UpdateLawyerDto
    {
        [Required]
        public Guid AccountId { get; set; }

        [Required]
        [StringLength(200)]
        public string AccountFullName { get; set; }

        public DateOnly AccountDob { get; set; }

        [Range(0, 2)]
        public int AccountGender { get; set; }

        [Phone]
        public string? AccountPhone { get; set; }

        public string? AccountImage { get; set; }

        public string? AboutLawyer { get; set; }
    }
}
