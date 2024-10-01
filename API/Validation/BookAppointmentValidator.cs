using API.DTO;
using FluentValidation;

namespace API.Validation
{
    public class BookAppointmentValidator: AbstractValidator<BookAppointmentRequest>
    {
        public BookAppointmentValidator()
        {
            RuleFor(a => a.UserId).NotEmpty().WithMessage("User ID is required");
            RuleFor(a => a.HealthcareProfessionalId).NotEmpty().WithMessage("Healthcare professional ID is required");
            RuleFor(a => a.StartTime).GreaterThan(DateTime.Now).WithMessage("Start time must be in the future");
            RuleFor(a => a.EndTime).GreaterThan(a => a.StartTime).WithMessage("End time must be after start time");
        }
    }
}
