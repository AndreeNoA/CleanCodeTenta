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
            MovieService movieService = new MovieService();
            var mockMovieList = new List<Movie>
            {
                new Movie { title = "MovieOne", id = "1", rated = "6" },
                new Movie { title = "MovieTwo", id = "2", rated = "9" },
                new Movie { title = "MovieThree", id = "3", rated = "4" }
            };

            var expectedDecending = movieService.SortLists(mockMovieList, false)[0];
            var expectedAscending = movieService.SortLists(mockMovieList, true)[0];

            Assert.AreEqual("MovieTwo", expectedDecending);
            Assert.AreEqual("MovieThree", expectedAscending);
        }
    }
}
