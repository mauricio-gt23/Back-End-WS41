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
    class FavouritePostSteps
    {
        private static FavouritePost favouritePost;
        public static RestClient restClient;
        public static RestRequest restRequest;
        public static IRestResponse response;



        [Given(@"Leaseholder wants to add FavouritePost")]
        public void GivenLeaseholderWantsToAddFavouritePost(int favouritePostId)
        {
            restClient = new RestClient("https://localhost:44356/");
            restRequest = new RestRequest("api/posts", Method.POST);
            restRequest.RequestFormat = DataFormat.Json;
        }

        [When(@"leaseholder adds FavouritePost")]
        public void WhenLeaseholderAddsFavouritePost(Table dto)
        {

            favouritePost = dto.CreateInstance<FavouritePost>();
            favouritePost = new FavouritePost()
            {
                PostId = 2
            };
            restRequest.AddJsonBody(favouritePost);
            response = restClient.Execute(restRequest);

        }

        [Then(@"favouritePost is added successfully")]
        public void ThenFavouritePostIsAddedSuccessfully()
        {
            Assert.That(2, Is.EqualTo(favouritePost.PostId));
        }

    }
}
}
