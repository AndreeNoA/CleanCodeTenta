using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System;
using MovieLibrary;
using System.Net.Http;
using MovieLibrary.Services;
using System.Text.Json;
using MovieLibrary.Models;
using System.Collections.Generic;

namespace MovieLibraryTests
{
    [TestClass]
    public class MovieTests
    {
        [TestMethod]
        public void SortingTest()
        {
            HttpClient client = new HttpClient();
            MovieService movieService = new MovieService();
            var result = client.GetAsync("https://ithstenta2020.s3.eu-north-1.amazonaws.com/topp100.json").Result;
            var movieList = JsonSerializer.Deserialize<List<Movie>>(new StreamReader(result.Content.ReadAsStream()).ReadToEnd());

            var expected = movieService.SortLists(movieList, false)[0];
            var expectedTwo = movieService.SortLists(movieList, true)[0];

            Assert.AreEqual("The Shawshank Redemption", expected);
            Assert.AreEqual("Once Upon a Time in America", expectedTwo);
        }
    }
}
