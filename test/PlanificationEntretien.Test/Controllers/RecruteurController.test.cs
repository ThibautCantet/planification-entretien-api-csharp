using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using PlanificationEntretien.Domain.Entities;
using PlanificationEntretien.Domain.Ports;
using PlanificationEntretien.Infrastructure.Controllers;
using Xunit;

namespace PlanificationEntretien.Test.Controllers
{
    public class RecruteurControllerTest : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;
        private readonly IRecruteurRepository _recruteurRepository;

        public RecruteurControllerTest(WebApplicationFactory<Startup> fixture)
        {
            var serviceClientProvider = fixture.Services;
            _recruteurRepository = serviceClientProvider.GetService<IRecruteurRepository>();
            _recruteurRepository.Clear();
            _client = fixture.CreateClient();
        }

        [Fact]
        public async Task CreerRecruteur_Should_Return_Status201()
        {
            var recruteurDto = new RecruteurDto( "C#", "recruteur@soat.fr", 4);

            var content = JsonContent.Create(recruteurDto);
            var response = await _client.PostAsync("api/recruteur", content);

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            
            var recruteurs = _recruteurRepository.FindAll();
            Assert.Equal(recruteurs.Count(), 1);
            var recruteur = recruteurs.First();
            Assert.Equal("recruteur@soat.fr", recruteur.Email);
            Assert.Equal("C#", recruteur.Language);
            Assert.Equal(4, recruteur.Xp);
        }

        [Fact]
        public async Task CreerRecruteur_Should_Return_Status401_When_Missing_Language()
        {
            var recruteurDto = new RecruteurDto("", "recruteur@soat.fr", 4);

            var content = JsonContent.Create(recruteurDto);
            var response = await _client.PostAsync("api/recruteur", content);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            
            var recruteurs = _recruteurRepository.FindAll();
            Assert.Equal(recruteurs.Count(), 0);
        }
        
        [Fact]
        public async Task CreerRecruteur_Should_Return_Status401_When_Missing_Email()
        {
            var recruteurDto = new RecruteurDto("C#", "", 4);

            var content = JsonContent.Create(recruteurDto);
            var response = await _client.PostAsync("api/recruteur", content);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            
            var recruteurs = _recruteurRepository.FindAll();
            Assert.Equal(recruteurs.Count(), 0);
        }
        
        [Fact]
        public async Task CreerRecruteur_Should_Return_Status401_When_Missing_Xp()
        {
            var recruteurDto = new RecruteurDto("C#", "recruteur@soat.fr", null);

            var content = JsonContent.Create(recruteurDto);
            var response = await _client.PostAsync("api/recruteur", content);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            
            var recruteurs = _recruteurRepository.FindAll();
            Assert.Equal(recruteurs.Count(), 0);
        }
        
        [Fact]
        public async Task CreerRecruteur_Should_Return_Status401_When_Invalid_Email()
        {
            var recruteurDto = new RecruteurDto("C#", "recruteur@mail", 10);

            var content = JsonContent.Create(recruteurDto);
            var response = await _client.PostAsync("api/recruteur", content);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            
            var recruteurs = _recruteurRepository.FindAll();
            Assert.Equal(recruteurs.Count(), 0);
        }
        
        [Fact]
        public async Task CreerRecruteur_Should_Return_Status401_When_Xp_Is_Negative()
        {
            var recruteurDto = new RecruteurDto("C#", "recruteur@soat.fr", -1);

            var content = JsonContent.Create(recruteurDto);
            var response = await _client.PostAsync("api/recruteur", content);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            
            var recruteurs = _recruteurRepository.FindAll();
            Assert.Equal(recruteurs.Count(), 0);
        }
        
        [Fact]
        public async Task ListerRecruteursExperimentes_Should_Return_Status200()
        {
            var recruteur9Id = Guid.NewGuid();
            var recruteur9 = new Recruteur(recruteur9Id, "Java", "recruteur9@soat.fr", 9);
            var recruteur10Id = Guid.NewGuid();
            var recruteur10 = new Recruteur(recruteur10Id, "Java", "recruteur10@soat.fr", 10);
            var recruteur11Id = Guid.NewGuid();
            var recruteur11 = new Recruteur(recruteur11Id, "Java", "recruteur11@soat.fr", 11);
            _recruteurRepository.Save(recruteur9);
            _recruteurRepository.Save(recruteur10);
            _recruteurRepository.Save(recruteur11);
            var response = await _client.GetAsync("api/recruteur");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            
            var recruteurExperimenteDtos = await response.Content.ReadFromJsonAsync<List<RecruteurExperimenteDto>>();
            Assert.Contains(new RecruteurExperimenteDto(recruteur10Id, "Java 10 ans XP", "recruteur10@soat.fr"), recruteurExperimenteDtos);
            Assert.Contains(new RecruteurExperimenteDto(recruteur11Id, "Java 11 ans XP", "recruteur11@soat.fr"), recruteurExperimenteDtos);
            Assert.DoesNotContain(new RecruteurExperimenteDto(recruteur9Id, "Java 9 ans XP", "recruteur9@soat.fr"), recruteurExperimenteDtos);
        }
        
    }
}