using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public interface IAppointmentRepository
    {
        public bool CheckAvailability(int HealthcareProfessionalId, DateTime StartTime, DateTime EndTime);
        public void AddAppointment(Appointment appointment);
        public Appointment GetAppointmentsByUser(int UserId);
        public Appointment GetAppointmentById(int AppointmentId);
        public void UpdateAppointment(Appointment appointment);
    }
}
