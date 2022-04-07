using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using MasterClass.WebApi;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using PlanificationEntretien.Models;
using PlanificationEntretien.Repository;
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
            var recruteurDto = new Recruteur( Guid.NewGuid(), "C#", "recruteur@soat.fr", 4);

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
            var recruteurDto = new Recruteur( Guid.NewGuid(), "", "recruteur@soat.fr", 4);

            var content = JsonContent.Create(recruteurDto);
            var response = await _client.PostAsync("api/recruteur", content);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            
            var recruteurs = _recruteurRepository.FindAll();
            Assert.Equal(recruteurs.Count(), 0);
        }
        
        [Fact]
        public async Task CreerRecruteur_Should_Return_Status401_When_Missing_Email()
        {
            var recruteurDto = new Recruteur( Guid.NewGuid(), "C#", "", 4);

            var content = JsonContent.Create(recruteurDto);
            var response = await _client.PostAsync("api/recruteur", content);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            
            var recruteurs = _recruteurRepository.FindAll();
            Assert.Equal(recruteurs.Count(), 0);
        }
        
        [Fact]
        public async Task CreerRecruteur_Should_Return_Status401_When_Missing_Xp()
        {
            var recruteurDto = new Recruteur( Guid.NewGuid(), "C#", "recruteur@soat.fr", null);

            var content = JsonContent.Create(recruteurDto);
            var response = await _client.PostAsync("api/recruteur", content);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            
            var recruteurs = _recruteurRepository.FindAll();
            Assert.Equal(recruteurs.Count(), 0);
        }
        
        [Fact]
        public async Task CreerRecruteur_Should_Return_Status401_When_Invalid_Email()
        {
            var recruteurDto = new Recruteur( Guid.NewGuid(), "C#", "recruteur@mail", 10);

            var content = JsonContent.Create(recruteurDto);
            var response = await _client.PostAsync("api/recruteur", content);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            
            var recruteurs = _recruteurRepository.FindAll();
            Assert.Equal(recruteurs.Count(), 0);
        }
        
        [Fact]
        public async Task CreerRecruteur_Should_Return_Status401_When_Xp_Is_Negative()
        {
            var recruteurDto = new Recruteur( Guid.NewGuid(), "C#", "recruteur@soat.fr", -1);

            var content = JsonContent.Create(recruteurDto);
            var response = await _client.PostAsync("api/recruteur", content);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            
            var recruteurs = _recruteurRepository.FindAll();
            Assert.Equal(recruteurs.Count(), 0);
        }
        
    }
}