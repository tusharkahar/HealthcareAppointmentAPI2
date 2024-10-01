using Application.Services;
using Application.DTO;
using Infrastructure.Repositories;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Tests
{
    public class AppointmentServiceTests
    {
        private readonly Mock<IAppointmentRepository> _mockRepo;
        private readonly AppointmentService _service;

        public AppointmentServiceTests()
        {
            _mockRepo = new Mock<IAppointmentRepository>();
            _service = new AppointmentService(_mockRepo.Object);
        }

        [Fact]
        public async Task BookAppointment_ShouldReturnSuccess_WhenAvailable()
        {
            var request = new BookAppointmentRequest
            {
                UserId = 1,
                HealthcareProfessionalId = 2,
                StartTime = DateTime.Now.AddDays(1),
                EndTime = DateTime.Now.AddDays(1).AddHours(1)
            };

            _mockRepo.Setup(repo => repo.CheckAvailability(It.IsAny<int>(), It.IsAny<DateTime>(), It.IsAny<DateTime>())).ReturnsAsync(true);

            var result = await _service.BookAppointment(request);

            Assert.True(result.Success);
            _mockRepo.Verify(repo => repo.AddAppointment(It.IsAny<Appointment>()), Times.Once);
        }
    }
}
