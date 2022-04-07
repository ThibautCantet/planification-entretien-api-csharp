using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using MasterClass.WebApi;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using PlanificationEntretien.Controllers;
using PlanificationEntretien.Models;
using PlanificationEntretien.Repository;
using Xunit;

namespace PlanificationEntretien.Test.Controllers
{
    public class EntretienControllerTest : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;
        private readonly IEntretienRepository _entretienRepository;

        public EntretienControllerTest(WebApplicationFactory<Startup> fixture)
        {
            var serviceClientProvider = fixture.Services;
            _entretienRepository = serviceClientProvider.GetService<IEntretienRepository>();
            _entretienRepository.Clear();
            
            _client = fixture.CreateClient();
        }

        [Fact]
        public async Task PlanifierEntretien_Should_Return_Status201()
        {
            var dateEtHeure = new DateTime(2022, 4, 5, 18, 0, 0);
            var planificationDto = new PlanificationDto(
                new Candidat( Guid.NewGuid(), "C#", "candidat@mail.com", 4),
                new Recruteur( Guid.NewGuid(), "C#", "recruteur@soat.fr", 5),
                dateEtHeure,
                dateEtHeure);
            
            var content = JsonContent.Create(planificationDto);
            var response = await _client.PostAsync("api/entretien", content);

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            
            var entretien = await response.Content.ReadFromJsonAsync<Entretien>();
            Assert.Equal(dateEtHeure, entretien.DateEtHeure);
            Assert.Equal("candidat@mail.com", entretien.EmailCandidat);
            Assert.Equal("recruteur@soat.fr", entretien.EmailRecruteur);

            
            var entretiens = _entretienRepository.FindAll();
            Assert.Equal(entretiens.Count(), 1);
            entretien = entretiens.First();
            Assert.Equal(dateEtHeure, entretien.DateEtHeure);
            Assert.Equal("candidat@mail.com", entretien.EmailCandidat);
            Assert.Equal("recruteur@soat.fr", entretien.EmailRecruteur);
        }
        
        [Fact]
        public async Task PlanifierEntretien_Should_Return_Status404_When_Recruteur_Less_Experienced()
        {
            var dateEtHeure = new DateTime(2022, 4, 5, 18, 0, 0);
            var planificationDto = new PlanificationDto(
                new Candidat(Guid.NewGuid(), "C#", "candidat@mail.com", 4),
                new Recruteur( Guid.NewGuid(), "C#", "recruteur@soat.fr", 3),
                dateEtHeure,
                dateEtHeure);
            
            var content = JsonContent.Create(planificationDto);
            var response = await _client.PostAsync("api/entretien", content);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            
            var entretiens = _entretienRepository.FindAll();
            Assert.Equal(entretiens.Count(), 0);
        }
        
        [Fact]
        public async Task PlanifierEntretien_Should_Return_Status404_When_Different_Tech()
        {
            var dateEtHeure = new DateTime(2022, 4, 5, 18, 0, 0);
            var planificationDto = new PlanificationDto(
                new Candidat(Guid.NewGuid(), "C#", "candidat@mail.com", 4),
                new Recruteur( Guid.NewGuid(), "Java", "recruteur@soat.fr", 5),
                dateEtHeure,
                dateEtHeure);
            
            var content = JsonContent.Create(planificationDto);
            var response = await _client.PostAsync("api/entretien", content);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            
            var entretiens = _entretienRepository.FindAll();
            Assert.Equal(entretiens.Count(), 0);
        }
        
        [Fact]
        public async Task PlanifierEntretien_Should_Return_Status404_When_Not_Available_At_The_Same_Moment()
        {
            var dateEtHeure = new DateTime(2022, 4, 5, 18, 0, 0);
            var planificationDto = new PlanificationDto(
                new Candidat(Guid.NewGuid(), "C#", "candidat@mail.com", 4),
                new Recruteur( Guid.NewGuid(), "Java", "recruteur@soat.fr", 5),
                dateEtHeure.AddDays(1),
                dateEtHeure);
            
            var content = JsonContent.Create(planificationDto);
            var response = await _client.PostAsync("api/entretien", content);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            
            var entretiens = _entretienRepository.FindAll();
            Assert.Equal(entretiens.Count(), 0);
        }
    }
}