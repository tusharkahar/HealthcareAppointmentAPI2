namespace Application.DTO
{
    public class BookAppointmentRequest
    {
        public int UserId { get; set; }
        public int HealthcareProfessionalId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
