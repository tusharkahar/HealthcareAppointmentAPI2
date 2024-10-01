using Application.Dto;
using Application.DTO;
using Application.Interface;
using Domain.Entities;
using Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository _appointmentRepository;

        public AppointmentService(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }

        public async Task<BookAppointmentResponse> BookAppointment(BookAppointmentRequest request)
        {
            bool isAvailable = await _appointmentRepository.CheckAvailability(request.HealthcareProfessionalId, request.StartTime, request.EndTime);
            if (!isAvailable)
            {
                return new BookAppointmentResponse { Success = false, Message = "The healthcare professional is already booked during this time." };
            }

            var appointment = new Appointment
            {
                UserId = request.UserId,
                HealthcareProfessionalId = request.HealthcareProfessionalId,
                StartTime = request.StartTime,
                EndTime = request.EndTime,
                Status = "booked"
            };

            await _appointmentRepository.AddAppointment(appointment);

            return new BookAppointmentResponse { Success = true };
        }

        public async Task<IEnumerable<Appointment>> GetUserAppointments(int userId)
        {
            return await _appointmentRepository.GetAppointmentsByUser(userId);
        }

        public async Task<BookAppointmentResponse> CancelAppointment(int appointmentId)
        {
            var appointment = await _appointmentRepository.GetAppointmentById(appointmentId);
            if (appointment == null || appointment.Status == "cancelled")
            {
                return new BookAppointmentResponse { Success = false, Message = "Appointment not found or already cancelled." };
            }

            appointment.Status = "cancelled";
            await _appointmentRepository.UpdateAppointment(appointment);

            return new BookAppointmentResponse { Success = true };
        }
    }
}
