using Application.Dto;
using Application.DTO;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface
{
    public interface IAppointmentService
    {
        public Task<BookAppointmentRequest> BookAppointment(BookAppointmentRequest request);
        public Task<IEnumerable<Appointment>> GetUserAppointments(int userId);
        public Task<BookAppointmentResponse> CancelAppointment(int appointmentId);
    }
}
