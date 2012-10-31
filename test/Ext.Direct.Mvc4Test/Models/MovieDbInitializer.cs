using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Ext.Direct.Mvc4Test.Models {

    public class MovieDbInitializer : DropCreateDatabaseAlways<MovieDbContext> {

        protected override void Seed(MovieDbContext context) {
            var genres = new List<Genre> {
                new Genre {ID = 1, Name = "Action"},
                new Genre {ID = 2, Name = "Adventure"},
                new Genre {ID = 3, Name = "Fantasy"},
                new Genre {ID = 4, Name = "Sci-Fi"},
                new Genre {ID = 5, Name = "Mystery"},
                new Genre {ID = 6, Name = "Drama"},
                new Genre {ID = 7, Name = "War"}
            };

            var movies = new List<Movie> {
                new Movie {
                    ID = 1,
                    Title = "Avatar",
                    ReleaseDate = new DateTime(2009, 12, 18),
                    MovieGenres = new List<MovieGenre> {
                        new MovieGenre {GenreID = 1},
                        new MovieGenre {GenreID = 2}
                    }
                },
                new Movie {ID = 2, Title = "Inception", ReleaseDate = new DateTime(2010, 7, 16)},
                new Movie {ID = 3, Title = "Minority Report", ReleaseDate = new DateTime(2002, 6, 21)},
                new Movie {ID = 4, Title = "I, Robot", ReleaseDate = new DateTime(2004, 7, 16)},
                new Movie {ID = 5, Title = "Pan's Labyrinth", ReleaseDate = new DateTime(2007, 1, 19)}
            };

            genres.ForEach(x => context.Genres.Add(x));
            movies.ForEach(x => context.Movies.Add(x));

            context.SaveChanges();
        }
    }
}