using MySql.Data.MySqlClient.Memcached;
using NUnit.Framework;
using RestSharp;
using Roomies.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace SpecFlowProject4.Steps
{
    [Binding]
    public class PostSteps
    {
        
        private static Post post;
        public static RestClient restClient;
        public static RestRequest restRequest;
        public static IRestResponse response;



        [Given(@"Landlord wants to add post")]
        public void GivenLandlordWantsToAddPost(int postId)
        {
            restClient = new RestClient("https://localhost:44356/");
            restRequest = new RestRequest("api/posts", Method.POST);
            restRequest.RequestFormat = DataFormat.Json;
        }

        [When(@"landlord adds post")]
         public void WhenLandlordAddsPost(Table dto)
         {
           
            post = dto.CreateInstance<Post>();
            post = new Post()
            {
                Title="titulo"
            };
            restRequest.AddJsonBody(post);
            response = restClient.Execute(restRequest);
            
         }

         [Then(@"post is added successfully")]
         public void ThenPostIsAddedSuccessfully()
         {
            Assert.That("Titulo", Is.EqualTo(post.Title));
         }
        
    }
}
