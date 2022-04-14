using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using PlanificationEntretien.Infrastructure.Controllers;
using PlanificationEntretien.Domain;
using PlanificationEntretien.Domain.Entities;
using PlanificationEntretien.Domain.Ports;
using PlanificationEntretien.UserCase;
using Xunit;

namespace PlanificationEntretien.Test.Controllers
{
    public class EntretienControllerTest : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;
        private readonly IEntretienRepository _entretienRepository;
        private readonly ICandidatRepository _candidatRepository;
        private readonly IRecruteurRepository _recruteurRepository;

        private static readonly Guid RECRUTEUR_CSHARP_5 = Guid.Parse("6c246f1c-cff3-41d4-b076-dc7c62fcf4aa");
        private static readonly Guid RECRUTEUR_CSHARP_3 = Guid.Parse("6c246f1c-cff3-41d4-b076-dc7c62fcf4ab");
        private static readonly Guid CANDIDAT_CSHARP_4 = Guid.Parse("6c246f1c-cff3-41d4-b076-dc7c62fcf4ac");
        private static readonly Guid CANDIDAT_JAVA_4 = Guid.Parse("6c246f1c-cff3-41d4-b076-dc7c62fcf4ad");
        public EntretienControllerTest(WebApplicationFactory<Startup> fixture)
        {
            var serviceClientProvider = fixture.Services;
            _entretienRepository = serviceClientProvider.GetService<IEntretienRepository>();
            _candidatRepository = serviceClientProvider.GetService<ICandidatRepository>();
            _recruteurRepository = serviceClientProvider.GetService<IRecruteurRepository>();
            _entretienRepository.Clear();
            _candidatRepository.Clear();
            _recruteurRepository.Clear();
            _recruteurRepository.Save(new Recruteur(RECRUTEUR_CSHARP_5, "C#", "recruteur@soat.fr", 5));
            _recruteurRepository.Save(new Recruteur(RECRUTEUR_CSHARP_3, "C#", "recruteur@soat.fr", 3));
            _candidatRepository.Save(new Candidat( CANDIDAT_CSHARP_4, "C#", "candidat@mail.com", 4));
            _candidatRepository.Save(new Candidat( CANDIDAT_JAVA_4, "Java", "candidat@mail.com", 4));

            _client = fixture.CreateClient();
        }

        [Fact]
        public async Task PlanifierEntretien_Should_Return_Status201()
        {
            var dateEtHeure = new DateTime(2022, 4, 5, 18, 0, 0);
            var planificationDto = new PlanificationDto(
                 CANDIDAT_CSHARP_4,
                 RECRUTEUR_CSHARP_5,
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
                CANDIDAT_CSHARP_4,
                RECRUTEUR_CSHARP_3,
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
                CANDIDAT_JAVA_4,
                RECRUTEUR_CSHARP_5,
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
                CANDIDAT_CSHARP_4,
                RECRUTEUR_CSHARP_5,
                dateEtHeure.AddDays(1),
                dateEtHeure);
            
            var content = JsonContent.Create(planificationDto);
            var response = await _client.PostAsync("api/entretien", content);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            
            var entretiens = _entretienRepository.FindAll();
            Assert.Equal(entretiens.Count(), 0);
        }
        
        [Fact]
        public async Task ListerEntretiens_Should_Return_Status200()
        {
            var entretien = new Entretien(Guid.NewGuid(),
                new DateTime(2022, 4, 5, 18, 0, 0),
                "candidat@mail.com",
                "recruteur@soat.fr");
            _entretienRepository.Save(entretien);
            
            var response = await _client.GetAsync("api/entretien");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            
            var entretiens = await response.Content.ReadFromJsonAsync<List<Entretien>>();
            Assert.Contains(entretien, entretiens);
        }
    }
}