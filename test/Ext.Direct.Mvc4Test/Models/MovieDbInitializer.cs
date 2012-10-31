using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Ext.Direct.Mvc4Test.Models {

    public class MovieDbInitializer : DropCreateDatabaseAlways<MovieDbContext> {

        protected override void Seed(MovieDbContext context) {
            var movies = new List<Movie> {
                new Movie {Title = "Avatar", ReleaseDate = new DateTime(2009, 12, 18)},
                new Movie {Title = "Inception", ReleaseDate = new DateTime(2010, 7, 16)},
                new Movie {Title = "Minority Report", ReleaseDate = new DateTime(2002, 6, 21)},
                new Movie {Title = "I, Robot", ReleaseDate = new DateTime(2004, 7, 16)},
                new Movie {Title = "Pan's Labyrinth", ReleaseDate = new DateTime(2007, 1, 19)}
            };

            movies.ForEach(x => context.Movies.Add(x));
            context.SaveChanges();
        }
    }
}