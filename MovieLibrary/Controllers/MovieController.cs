using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using MovieLibrary.Models;
using MovieLibrary.Services;

namespace MovieLibrary.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class MovieController
    {
        static readonly HttpClient client = new HttpClient();
        MovieService movieService = new MovieService();
        
        [HttpGet]
        [Route("/movieById")]
        public Movie GetMovieById(string inputId) 
        {
            var resultOne = client.GetAsync("https://ithstenta2020.s3.eu-north-1.amazonaws.com/detailedMovies.json").Result;
            var resultTwo = client.GetAsync("https://ithstenta2020.s3.eu-north-1.amazonaws.com/topp100.json").Result;
            var joinedMovieList = movieService.JoinLists(resultTwo, resultOne);
            foreach (var movie in joinedMovieList) {
                if (movie.id.Equals(inputId))
                {
                    return movie;
                }
            }
            return null;
        }

        [HttpGet]
        [Route("/AllMoviesSorted")]
        public IEnumerable<string> GetJoinedMovieListSorted(bool ascending)
        {
            var resultOne = client.GetAsync("https://ithstenta2020.s3.eu-north-1.amazonaws.com/detailedMovies.json").Result;
            var resultTwo = client.GetAsync("https://ithstenta2020.s3.eu-north-1.amazonaws.com/topp100.json").Result;

            var joinedMovieList = movieService.JoinLists(resultTwo, resultOne);

            return movieService.SortLists(joinedMovieList, ascending);
        }
    }
}