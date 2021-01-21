using MovieLibrary.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace MovieLibrary.Services
{
    public class MovieService
    {
        public MovieService()
        {

        }
        public List<Movie> JoinLists(HttpResponseMessage movies, HttpResponseMessage moviesDetailed)
        {
            var movieList = JsonSerializer.Deserialize<List<Movie>>(new StreamReader(movies.Content.ReadAsStream()).ReadToEnd());
            var movieListDetailed = JsonSerializer.Deserialize<List<DetailedMovie>>(new StreamReader(moviesDetailed.Content.ReadAsStream()).ReadToEnd());
            List<Movie> detailedMovieAvgCounted = new List<Movie>();

            foreach (var movie in movieListDetailed)
            {
                double avg = Queryable.Average(movie.ratings.AsQueryable());
                detailedMovieAvgCounted.Add(new Movie { title = movie.title, id = movie.id, rated = avg.ToString() });
            }

            var joinedMovieList = movieList.Union(detailedMovieAvgCounted).ToList();
            return joinedMovieList;
        }

        public List<string> SortLists(List<Movie> movies, bool ascending)
        {
            List<string> sortedMovieList = new List<string>();
            if (ascending)
            {
                var ascendingList = movies.OrderBy(e => e.rated).ToList();
                foreach (var movie in ascendingList)
                {
                    sortedMovieList.Add(movie.title);
                }
            }
            else
            {
                var descendingList = movies.OrderByDescending(e => e.rated).ToList();
                foreach (var movie in descendingList)
                {
                    sortedMovieList.Add(movie.title);
                }
            }
            return sortedMovieList;
        }
    }
}
