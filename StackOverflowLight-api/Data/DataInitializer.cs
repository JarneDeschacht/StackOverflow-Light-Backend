using Microsoft.AspNetCore.Identity;
using StackOverflowLight_api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StackOverflowLight_api.Data
{
    public class DataInitializer
    {
        private readonly StackOverflowContext _dbContext;
        private readonly UserManager<IdentityUser> _userManager;

        public DataInitializer(StackOverflowContext dbcontext, UserManager<IdentityUser> userManager)
        {
            _dbContext = dbcontext;
            _userManager = userManager;
        }
        public async Task InitializeData()
        {
            //_dbContext.Database.EnsureDeleted();
            if (_dbContext.Database.EnsureCreated())
            {
                User jarne = new User("Jarne", "Deschacht", "jarne.deschacht@hotmail.com");
                _dbContext.Users_Domain.Add(jarne);
                User ime = new User("Ime", "Van Daele", "imevandaele@gmail.com");
                _dbContext.Users_Domain.Add(ime);
                User tijs = new User("Tijs", "Martens", "tijs.martens@gmail.com");
                _dbContext.Users_Domain.Add(tijs);
                User robbe = new User("Robbe", "Dekien", "robbedekien@gmail.com");
                _dbContext.Users_Domain.Add(robbe);

                await CreateUser(jarne.Email, "P@ssword1111");
                await CreateUser(ime.Email, "P@ssword1111");
                await CreateUser(tijs.Email, "P@ssword1111");
                await CreateUser(robbe.Email, "P@ssword1111");

                Post post1 = new Post("Test 1", "Lorem Ipsum is slechts een proeftekst uit het drukkerij- en zetterijwezen. " +
                    "Lorem Ipsum is de standaard proeftekst in deze bedrijfstak sinds de 16e eeuw, toen een onbekende drukker een" +
                    " zethaak met letters nam en ze door elkaar husselde om een font-catalogus te maken. Het heeft niet alleen vijf " +
                    "eeuwen overleefd maar is ook, vrijwel onveranderd, overgenomen in elektronische letterzetting. Het is in de jaren " +
                    "'60 populair geworden met de introductie van Letraset vellen met Lorem Ipsum passages en meer recentelijk door desktop" +
                    " publishing software zoals Aldus PageMaker die versies van Lorem Ipsum bevatten.", jarne);
                post1.AddVote(new Vote(ime, VoteType.Downvote));
                post1.AddVote(new Vote(tijs, VoteType.Upvote));
                post1.AddVote(new Vote(robbe, VoteType.Upvote));
                post1.AddAnswer(new Answer("answer 1", ime));
                post1.AddAnswer(new Answer("answer 2", tijs));
                post1.AddAnswer(new Answer("answer 3", robbe));
                post1.AddAnswer(new Answer("answer 4", ime));
                _dbContext.Posts.Add(post1);

                Post post2 = new Post("Test 2","Het is al geruime tijd een bekend gegeven dat een lezer, tijdens het bekijken van de layout " +
                    "van een pagina, afgeleid wordt door de tekstuele inhoud. Het belangrijke punt van het gebruik van Lorem Ipsum is dat" +
                    " het uit een min of meer normale verdeling van letters bestaat, in tegenstelling tot 'Hier uw tekst, hier uw tekst' wat" +
                    " het tot min of meer leesbaar nederlands maakt. Veel desktop publishing pakketten en web pagina editors gebruiken " +
                    "tegenwoordig Lorem Ipsum als hun standaard model tekst, en een zoekopdracht naar 'lorem ipsum' ontsluit veel websites" +
                    " die nog in aanbouw zijn. Verscheidene versies hebben zich ontwikkeld in de loop van de jaren, soms per ongeluk soms expres" +
                    " (ingevoegde humor en dergelijke).", robbe);
                post2.AddVote(new Vote(jarne, VoteType.Downvote));
                post2.AddVote(new Vote(tijs, VoteType.Downvote));
                post2.AddVote(new Vote(ime, VoteType.Downvote));
                _dbContext.Posts.Add(post2);

                Post post3 = new Post("Test 3", "Lorem Ipsum is slechts een proeftekst uit het drukkerij- en zetterijwezen. " +
                    "Lorem Ipsum is de standaard proeftekst in deze bedrijfstak sinds de 16e eeuw, toen een onbekende drukker een" +
                    " zethaak met letters nam en ze door elkaar husselde om een font-catalogus te maken. Het heeft niet alleen vijf " +
                    "eeuwen overleefd maar is ook, vrijwel onveranderd, overgenomen in elektronische letterzetting. Het is in de jaren " +
                    "'60 populair geworden met de introductie van Letraset vellen met Lorem Ipsum passages en meer recentelijk door desktop" +
                    " publishing software zoals Aldus PageMaker die versies van Lorem Ipsum bevatten.", ime);
                post3.AddVote(new Vote(jarne, VoteType.Downvote));
                post3.AddVote(new Vote(tijs, VoteType.Downvote));
                post3.AddVote(new Vote(robbe, VoteType.Downvote));
                post3.AddAnswer(new Answer("answer 1", jarne));
                post3.AddAnswer(new Answer("answer 2", tijs));
                post3.AddAnswer(new Answer("answer 3", robbe));
                post3.AddAnswer(new Answer("answer 4", jarne));
                _dbContext.Posts.Add(post3);

                Post post4 = new Post("Test 4", "Het is al geruime tijd een bekend gegeven dat een lezer, tijdens het bekijken van de layout " +
                    "van een pagina, afgeleid wordt door de tekstuele inhoud. Het belangrijke punt van het gebruik van Lorem Ipsum is dat" +
                    " het uit een min of meer normale verdeling van letters bestaat, in tegenstelling tot 'Hier uw tekst, hier uw tekst' wat" +
                    " het tot min of meer leesbaar nederlands maakt. Veel desktop publishing pakketten en web pagina editors gebruiken " +
                    "tegenwoordig Lorem Ipsum als hun standaard model tekst, en een zoekopdracht naar 'lorem ipsum' ontsluit veel websites" +
                    " die nog in aanbouw zijn. Verscheidene versies hebben zich ontwikkeld in de loop van de jaren, soms per ongeluk soms expres" +
                    " (ingevoegde humor en dergelijke).", tijs);
                post4.AddVote(new Vote(jarne, VoteType.Upvote));
                post4.AddVote(new Vote(ime, VoteType.Upvote));
                post4.AddVote(new Vote(robbe, VoteType.Upvote));
                post4.AddVote(new Vote(robbe, VoteType.Downvote));
                post4.RemoveVote(robbe.UserId);
                post4.AddAnswer(new Answer("answer 1", jarne));
                _dbContext.Posts.Add(post4);

                _dbContext.SaveChanges();
            }
        }
        private async Task CreateUser(string email, string password)
        {
            var user = new IdentityUser { UserName = email, Email = email };
            await _userManager.CreateAsync(user, password);
        }
    }
}
