using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Ext.Direct.Mvc4Test.Models {
    public class ContactsInitializer : DropCreateDatabaseAlways<ContactsContext> {
        protected override void Seed(ContactsContext context) {
            var contacts = new List<Contact> {
                new Contact {FirstName = "Carson",    LastName = "Alexander",    BirthDate = DateTime.Parse("1976-09-01"), Employed = false},
                new Contact {FirstName = "Meredith",  LastName = "Alonso",       BirthDate = DateTime.Parse("1980-02-15"), Employed = false},
                new Contact {FirstName = "Arturo",    LastName = "Anand",        BirthDate = DateTime.Parse("1969-05-24"), Employed = false},
                new Contact {FirstName = "Gytis",     LastName = "Barzdukas",    BirthDate = DateTime.Parse("1973-09-20"), Employed = false},
                new Contact {FirstName = "Yan",       LastName = "Li",           BirthDate = DateTime.Parse("1975-11-01"), Employed = false},
                new Contact {FirstName = "Peggy",     LastName = "Justice",      BirthDate = DateTime.Parse("1964-01-14"), Employed = false},
                new Contact {FirstName = "Laura",     LastName = "Norman",       BirthDate = DateTime.Parse("2001-08-03"), Employed = false},
                new Contact {FirstName = "Nino",      LastName = "Olivetto",     BirthDate = DateTime.Parse("1984-03-17"), Employed = false},
                new Contact {FirstName = "Julio",     LastName = "Benesh",       BirthDate = DateTime.Parse("1972-11-23"), Employed = true},
                new Contact {FirstName = "Julio",     LastName = "Minich",       BirthDate = DateTime.Parse("1972-10-29"), Employed = true},
                new Contact {FirstName = "Tania",     LastName = "Ricco",        BirthDate = DateTime.Parse("1991-09-24"), Employed = true},
                new Contact {FirstName = "Odessa",    LastName = "Steuck",       BirthDate = DateTime.Parse("1986-06-10"), Employed = true},
                new Contact {FirstName = "Nelson",    LastName = "Raber",        BirthDate = DateTime.Parse("1982-08-09"), Employed = false},
                new Contact {FirstName = "Tyrone",    LastName = "Scannell",     BirthDate = DateTime.Parse("1987-11-29"), Employed = true},
                new Contact {FirstName = "Allan",     LastName = "Disbrow",      BirthDate = DateTime.Parse("1991-08-08"), Employed = true},
                new Contact {FirstName = "Cody",      LastName = "Herrell",      BirthDate = DateTime.Parse("1983-11-05"), Employed = true},
                new Contact {FirstName = "Julio",     LastName = "Burgoyne",     BirthDate = DateTime.Parse("1976-01-20"), Employed = false},
                new Contact {FirstName = "Jessie",    LastName = "Boedeker",     BirthDate = DateTime.Parse("1992-08-14"), Employed = true},
                new Contact {FirstName = "Allan",     LastName = "Leyendecker",  BirthDate = DateTime.Parse("1971-05-16"), Employed = true},
                new Contact {FirstName = "Javier",    LastName = "Lockley",      BirthDate = DateTime.Parse("1973-02-20"), Employed = true},
                new Contact {FirstName = "Guy",       LastName = "Reasor",       BirthDate = DateTime.Parse("1987-04-01"), Employed = true},
                new Contact {FirstName = "Jamie",     LastName = "Brummer",      BirthDate = DateTime.Parse("1986-04-11"), Employed = true},
                new Contact {FirstName = "Jessie",    LastName = "Casa",         BirthDate = DateTime.Parse("1992-11-16"), Employed = true},
                new Contact {FirstName = "Marcie",    LastName = "Ricca",        BirthDate = DateTime.Parse("1985-10-28"), Employed = true},
                new Contact {FirstName = "Gay",       LastName = "Lamoureaux",   BirthDate = DateTime.Parse("1976-06-02"), Employed = true},
                new Contact {FirstName = "Althea",    LastName = "Sturtz",       BirthDate = DateTime.Parse("1993-11-22"), Employed = false},
                new Contact {FirstName = "Kenya",     LastName = "Morocco",      BirthDate = DateTime.Parse("1974-11-30"), Employed = true},
                new Contact {FirstName = "Rae",       LastName = "Pasquariello", BirthDate = DateTime.Parse("1986-05-30"), Employed = true},
                new Contact {FirstName = "Ted",       LastName = "Abundis",      BirthDate = DateTime.Parse("1989-01-26"), Employed = false},
                new Contact {FirstName = "Jessie",    LastName = "Schacherer",   BirthDate = DateTime.Parse("1970-07-13"), Employed = true},
                new Contact {FirstName = "Jamie",     LastName = "Gleaves",      BirthDate = DateTime.Parse("1994-12-31"), Employed = true},
                new Contact {FirstName = "Hillary",   LastName = "Spiva",        BirthDate = DateTime.Parse("1979-12-09"), Employed = true},
                new Contact {FirstName = "Elinor",    LastName = "Rockefeller",  BirthDate = DateTime.Parse("1994-01-28"), Employed = true},
                new Contact {FirstName = "Dona",      LastName = "Clauss",       BirthDate = DateTime.Parse("1992-02-07"), Employed = true},
                new Contact {FirstName = "Ashlee",    LastName = "Kennerly",     BirthDate = DateTime.Parse("1989-03-13"), Employed = false},
                new Contact {FirstName = "Alana",     LastName = "Wiersma",      BirthDate = DateTime.Parse("1973-09-17"), Employed = true},
                new Contact {FirstName = "Kelly",     LastName = "Holdman",      BirthDate = DateTime.Parse("1982-06-11"), Employed = true},
                new Contact {FirstName = "Mathew",    LastName = "Lofthouse",    BirthDate = DateTime.Parse("1980-03-25"), Employed = true},
                new Contact {FirstName = "Dona",      LastName = "Tatman",       BirthDate = DateTime.Parse("1983-11-12"), Employed = false},
                new Contact {FirstName = "Clayton",   LastName = "Clear",        BirthDate = DateTime.Parse("1977-01-12"), Employed = true},
                new Contact {FirstName = "Rosalinda", LastName = "Urman",        BirthDate = DateTime.Parse("1980-04-25"), Employed = true},
                new Contact {FirstName = "Cody",      LastName = "Sayler",       BirthDate = DateTime.Parse("1980-10-14"), Employed = true},
                new Contact {FirstName = "Odessa",    LastName = "Averitt",      BirthDate = DateTime.Parse("1990-01-17"), Employed = true},
                new Contact {FirstName = "Ted",       LastName = "Poage",        BirthDate = DateTime.Parse("1987-09-23"), Employed = true},
                new Contact {FirstName = "Penelope",  LastName = "Gayer",        BirthDate = DateTime.Parse("1987-01-12"), Employed = false},
                new Contact {FirstName = "Katy",      LastName = "Bluford",      BirthDate = DateTime.Parse("1971-05-17"), Employed = true},
                new Contact {FirstName = "Kelly",     LastName = "Mchargue",     BirthDate = DateTime.Parse("1994-11-19"), Employed = true},
                new Contact {FirstName = "Kathrine",  LastName = "Gustavson",    BirthDate = DateTime.Parse("1987-06-03"), Employed = true},
                new Contact {FirstName = "Kelly",     LastName = "Hartson",      BirthDate = DateTime.Parse("1983-02-20"), Employed = true},
                new Contact {FirstName = "Carlene",   LastName = "Summitt",      BirthDate = DateTime.Parse("1976-06-23"), Employed = false},
                new Contact {FirstName = "Kathrine",  LastName = "Vrabel",       BirthDate = DateTime.Parse("1978-12-08"), Employed = false},
                new Contact {FirstName = "Roxie",     LastName = "Mcconn",       BirthDate = DateTime.Parse("1976-06-22"), Employed = true},
                new Contact {FirstName = "Margery",   LastName = "Pullman",      BirthDate = DateTime.Parse("1982-03-13"), Employed = true},
                new Contact {FirstName = "Avis",      LastName = "Bueche",       BirthDate = DateTime.Parse("1984-12-05"), Employed = true},
                new Contact {FirstName = "Esmeralda", LastName = "Katzer",       BirthDate = DateTime.Parse("1983-05-27"), Employed = true},
                new Contact {FirstName = "Tania",     LastName = "Belmonte",     BirthDate = DateTime.Parse("1978-04-01"), Employed = false},
                new Contact {FirstName = "Malinda",   LastName = "Kwak",         BirthDate = DateTime.Parse("1979-09-30"), Employed = true},
                new Contact {FirstName = "Tanisha",   LastName = "Jobin",        BirthDate = DateTime.Parse("1972-05-06"), Employed = false},
                new Contact {FirstName = "Kelly",     LastName = "Dziedzic",     BirthDate = DateTime.Parse("1982-01-25"), Employed = true},
                new Contact {FirstName = "Darren",    LastName = "Devalle",      BirthDate = DateTime.Parse("1987-01-22"), Employed = true},
                new Contact {FirstName = "Julio",     LastName = "Buchannon",    BirthDate = DateTime.Parse("1987-12-04"), Employed = true},
                new Contact {FirstName = "Darren",    LastName = "Schreier",     BirthDate = DateTime.Parse("1994-05-16"), Employed = false},
                new Contact {FirstName = "Jamie",     LastName = "Pollman",      BirthDate = DateTime.Parse("1996-01-06"), Employed = true},
                new Contact {FirstName = "Karina",    LastName = "Pompey",       BirthDate = DateTime.Parse("1992-06-10"), Employed = true},
                new Contact {FirstName = "Hugh",      LastName = "Snover",       BirthDate = DateTime.Parse("1978-08-14"), Employed = true},
                new Contact {FirstName = "Zebra",     LastName = "Evilias",      BirthDate = DateTime.Parse("1971-09-10"), Employed = true}
            };
            contacts.ForEach(c => context.Contacts.Add(c));
            context.SaveChanges();
        }
    }
}