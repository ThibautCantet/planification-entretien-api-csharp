using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using MasterClass.WebApi.Controllers;
using Microsoft.AspNetCore.Mvc.Testing;
using Moq;
using PlanificationEntretien;
using Xunit;

namespace MasterClass.WebApi.Test.Controllers
{
    public class EntretienControllerTest : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;
        private Candidat _candidat = new("C#", "candidat@mail.com", 4);
        private Recruteur _recruteur= new("C#", "recruteur@soat.fr", 5);
        private static IEntretienRepository _entretienRepository = new EntretienRepository();
        private static IEmailService _emailService = new Mock<IEmailService>().Object;
        private EntretienService _entretienService = new(_entretienRepository, _emailService);
        private DateTime _disponibiliteCandidat = new(2022, 4, 5, 18, 0, 0);
        private DateTime _disponibiliteRecruteur = new(2022, 4, 5, 18, 0, 0);
        private Entretien _entretien;

        public EntretienControllerTest(WebApplicationFactory<Startup> fixture)
        {
            _client = fixture.CreateClient();
        }

        [Fact]
        public async Task PlanifierEntretien_Status201()
        {
            _entretien = _entretienService.planifier(_candidat, _recruteur, _disponibiliteCandidat, _disponibiliteRecruteur);

            var dateEtHeure = new DateTime(2022, 4, 5, 18, 0, 0);
            var planificationDto = new PlanificationDto(
                new Candidat("C#", "candidat@mail.com", 4),
                new Recruteur("C#", "recruteur@soat.fr", 4),
                dateEtHeure,
                dateEtHeure);
            
            var content = JsonContent.Create(planificationDto);
            var response = await _client.PostAsync("api/entretien", content);

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            
            var readFromJsonAsync = await response.Content.ReadFromJsonAsync<Entretien>();
            Assert.Equal(new Entretien(dateEtHeure, "candidat@mail.com", "recruteur@soat.fr"), readFromJsonAsync);
        }
    }
}