using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Application.DTOs.LawyerDTO
{
    public class CreateLawyerDto
    {
        [Required]
        [StringLength(100)]
        public string AccountUsername { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 6)]
        public string AccountPassword { get; set; }

        [Required]
        [EmailAddress]
        public string AccountEmail { get; set; }

        [Required]
        [StringLength(200)]
        public string AccountFullName { get; set; }

        [Required]
        public DateOnly AccountDob { get; set; }

        [Required]
        [Range(0, 2)]
        public int AccountGender { get; set; }

        [Phone]
        public string? AccountPhone { get; set; }

        public string? AccountImage { get; set; }

        public string? AboutLawyer { get; set; }
    }
}
