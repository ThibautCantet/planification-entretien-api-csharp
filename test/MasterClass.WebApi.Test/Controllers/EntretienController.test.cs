using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using MasterClass.WebApi.Controllers;
using Microsoft.AspNetCore.Mvc.Testing;
using PlanificationEntretien;
using Xunit;

namespace MasterClass.WebApi.Test.Controllers
{
    public class EntretienControllerTest : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;

        public EntretienControllerTest(WebApplicationFactory<Startup> fixture)
        {
            _client = fixture.CreateClient();
        }

        [Fact]
        public async Task PlanifierEntretien_Should_Return_Status201()
        {
            var dateEtHeure = new DateTime(2022, 4, 5, 18, 0, 0);
            var planificationDto = new PlanificationDto(
                new Candidat("C#", "candidat@mail.com", 4),
                new Recruteur("C#", "recruteur@soat.fr", 5),
                dateEtHeure,
                dateEtHeure);
            
            var content = JsonContent.Create(planificationDto);
            var response = await _client.PostAsync("api/entretien", content);

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            
            var readFromJsonAsync = await response.Content.ReadFromJsonAsync<Entretien>();
            Assert.Equal(new Entretien(dateEtHeure, "candidat@mail.com", "recruteur@soat.fr"), readFromJsonAsync);
        }
        
        [Fact]
        public async Task PlanifierEntretien_Should_Return_Status404_When_Recruteur_Less_Experienced()
        {
            var dateEtHeure = new DateTime(2022, 4, 5, 18, 0, 0);
            var planificationDto = new PlanificationDto(
                new Candidat("C#", "candidat@mail.com", 4),
                new Recruteur("C#", "recruteur@soat.fr", 3),
                dateEtHeure,
                dateEtHeure);
            
            var content = JsonContent.Create(planificationDto);
            var response = await _client.PostAsync("api/entretien", content);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
        
        [Fact]
        public async Task PlanifierEntretien_Should_Return_Status404_When_Different_Tech()
        {
            var dateEtHeure = new DateTime(2022, 4, 5, 18, 0, 0);
            var planificationDto = new PlanificationDto(
                new Candidat("C#", "candidat@mail.com", 4),
                new Recruteur("Java", "recruteur@soat.fr", 5),
                dateEtHeure,
                dateEtHeure);
            
            var content = JsonContent.Create(planificationDto);
            var response = await _client.PostAsync("api/entretien", content);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
        
        [Fact]
        public async Task PlanifierEntretien_Should_Return_Status404_When_Not_Available_At_The_Same_Moment()
        {
            var dateEtHeure = new DateTime(2022, 4, 5, 18, 0, 0);
            var planificationDto = new PlanificationDto(
                new Candidat("C#", "candidat@mail.com", 4),
                new Recruteur("Java", "recruteur@soat.fr", 5),
                dateEtHeure.AddDays(1),
                dateEtHeure);
            
            var content = JsonContent.Create(planificationDto);
            var response = await _client.PostAsync("api/entretien", content);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}