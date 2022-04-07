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
    public class CandidatControllerTest : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;
        private readonly ICandidatRepository _candidatRepository;

        public CandidatControllerTest(WebApplicationFactory<Startup> fixture)
        {
            var serviceClientProvider = fixture.Services;
            _candidatRepository = serviceClientProvider.GetService<ICandidatRepository>();
            _candidatRepository.Clear();
            _client = fixture.CreateClient();
        }

        [Fact]
        public async Task CreerCandidat_Should_Return_Status201()
        {
            var candidatDto = new Candidat(  Guid.NewGuid(), "C#", "candidat@mail.com", 4);

            var content = JsonContent.Create(candidatDto);
            var response = await _client.PostAsync("api/candidat", content);

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            
            var candidats = _candidatRepository.FindAll();
            Assert.Equal(candidats.Count(), 1);
            var candidat = candidats.First();
            Assert.Equal("candidat@mail.com", candidat.Email);
            Assert.Equal("C#", candidat.Language);
            Assert.Equal(4, candidat.Xp);
        }
        
    }
}