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
            _dbContext.Database.EnsureDeleted();
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
                User geert = new User("Geert", "Deschacht", "geert.deschacht1@telenet.be");
                _dbContext.Users_Domain.Add(geert);
                User jelle = new User("Jelle", "Deschacht", "jelledeschacht@gmail.com");
                _dbContext.Users_Domain.Add(jelle);
                User ruben = new User("Ruben", "Vanhee", "ruben.vanhee@hotmail.com");
                _dbContext.Users_Domain.Add(ruben);
                User timon = new User("Timon", "Batsleer", "timonbatseer@gmail.com");
                _dbContext.Users_Domain.Add(timon);

                await CreateUser(jarne.Email, "P@ssword1111");
                await CreateUser(ime.Email, "Password1111");
                await CreateUser(tijs.Email, "P@ssword1111");
                await CreateUser(robbe.Email, "P@ssword1111");
                await CreateUser(geert.Email, "P@ssword1111");
                await CreateUser(ruben.Email, "P@ssword1111");
                await CreateUser(jelle.Email, "P@ssword1111");
                await CreateUser(timon.Email, "P@ssword1111");

                Post post1 = new Post("Problems setting proxy on HttpClient","In my C# application I'm trying to send an HTTP request" +
                    " to an external company outside of our firewall. When I use the below code, I'm getting back an error that" +
                    " the remote site has forcibly closed the connection. I'm assuming I'm doing something incorrectly with the " +
                    "proxy setting but I'm not sure what. Our proxy does not require authentication. I've printed out the encoded " +
                    "JSON that gets sent and if I just do things manually via curl the request goes right though, so I'm sure that" +
                    " my URL, bearer token and JSON are all correct.", jelle);
                post1.AddVote(new Vote(ime, VoteType.Downvote));
                post1.AddVote(new Vote(tijs, VoteType.Upvote));
                post1.AddVote(new Vote(robbe, VoteType.Upvote));
                post1.AddVote(new Vote(jarne, VoteType.Downvote));
                post1.AddVote(new Vote(timon, VoteType.Upvote));
                post1.AddAnswer(new Answer("answer 1", ime));
                post1.AddAnswer(new Answer("answer 2", tijs));
                post1.AddAnswer(new Answer("answer 3", robbe));
                post1.AddAnswer(new Answer("answer 4", ime));
                _dbContext.Posts.Add(post1);

                Post post2 = new Post("Can comments be used in JSON?", "Can I use comments inside a JSON file? If so, how?", robbe);
                post2.AddVote(new Vote(jarne, VoteType.Downvote));
                post2.AddVote(new Vote(tijs, VoteType.Downvote));
                post2.AddVote(new Vote(ime, VoteType.Downvote));
                post2.AddAnswer(new Answer("No. The JSON should all be data," +
                    " and if you include a comment, then it will be data too. You could have a designated" +
                    " data element called '_comment'(or something)' that would be ignored by apps that use" +
                    " the JSON data. You would probably be better having the comment in the processes that" +
                    " generates/ receives the JSON, as they are supposed to know what the JSON data will be " +
                    "in advance, or at least the structure of it.", ime));
                _dbContext.Posts.Add(post2);

                Post post3 = new Post("What is the most efficient way to deep clone an object in JavaScript?", "What is the most efficient way to clone a JavaScript object? " +
                    "I've seen obj = eval(uneval(o)); being used, but that's non-standard and only supported by " +
                    "Firefox. I've done things like obj = JSON.parse(JSON.stringify(o)); but question the efficiency.  " +
                    "I've also seen recursive copying functions with various flaws. I'm surprised no canonical solution" +
                    " exists.", ime);
                
                post3.AddVote(new Vote(jarne, VoteType.Downvote));
                post3.AddVote(new Vote(tijs, VoteType.Downvote));
                post3.AddVote(new Vote(robbe, VoteType.Downvote));
                post3.AddVote(new Vote(jelle, VoteType.Upvote));
                post3.AddVote(new Vote(ruben, VoteType.Upvote));
                post3.AddVote(new Vote(geert, VoteType.Upvote));
                post3.AddVote(new Vote(timon, VoteType.Downvote));
                post3.AddAnswer(new Answer("Note that JSON method will loose any Javascript types that have no equivalent in JSON. For example: JSON.parse(JSON.stringify({a:null,b:NaN,c:Infinity,d:undefined,e:function(){},f:Number,g:false})) will generate {a: null, b: null, c: null, g: false}", jarne));
                post3.AddAnswer(new Answer("eval() is generally a bad idea because many Javascript engine's optimisers have to turn off when dealing with variables that are set via eval. Just having eval() in your code can lead to worse performance", robbe));
                post3.AddAnswer(new Answer("Cloning objects is a tricky business, especially with custom objects of arbitrary collections. Which probably why there is no out-of-the box way to do it.", tijs));
                post3.AddAnswer(new Answer("Eval is not evil. Using eval poorly is. If you are afraid of its side effects you are using it wrong. The side effects you fear are the reasons to use it. Did any one by the way actually answer your question?", jarne));

                _dbContext.Posts.Add(post3);

                Post post4 = new Post("How to disable text selection highlighting?", "For anchors that act like buttons (for example, Questions, Tags, Users," +
                    " etc. at the top of the Stack Overflow page) or tabs, is there a CSS standard way to disable the " +
                    "highlighting effect if the user accidentally selects the text? I realize this could be done with JavaScript, " +
                    "and a little googling yielded the Mozilla - only - moz - user - select option. Is there a standard - " +
                    "compliant way to accomplish this with CSS, and if not, what is the 'best practice' approach ? ", tijs);
                post4.AddVote(new Vote(jarne, VoteType.Upvote));
                post4.AddVote(new Vote(ime, VoteType.Upvote));
                post4.AddVote(new Vote(robbe, VoteType.Upvote));
                post4.AddVote(new Vote(robbe, VoteType.Downvote));
                post4.RemoveVote(robbe.UserId);
                post4.AddVote(new Vote(timon, VoteType.Upvote));
                post4.AddVote(new Vote(ruben, VoteType.Upvote));
                post4.AddAnswer(new Answer("answer 1", jarne));
                _dbContext.Posts.Add(post4);

                Post post5 = new Post("How do I return the response from an asynchronous call?", "I have a function " +
                    "foo which makes an Ajax request.How can I return the response from foo?I tried returning the value " +
                    "from the success callback as well as assigning the response to a local variable inside the function and " +
                    "returning that one, but none of those ways actually return the response.", timon);
                post5.AddVote(new Vote(ruben, VoteType.Upvote));
                post5.AddVote(new Vote(geert, VoteType.Upvote));
                _dbContext.Posts.Add(post5);

                Post post6 = new Post("What is the maximum length of a URL in different browsers?", "What is the maximum" +
                    " length of a URL in different browsers? Does it differ among browsers? Does the HTTP protocol dictate it " +
                    "? What is the maximum length of a URL in different browsers ? ", geert);
                post6.AddVote(new Vote(ruben, VoteType.Downvote));
                post6.AddAnswer(new Answer("answer 2", jarne));
                post6.AddAnswer(new Answer("answer 1", robbe));
                
                _dbContext.Posts.Add(post6);

                Post post7 = new Post("How to make Git “forget” about a file that was tracked but is now in .gitignore?", 
                    "There is a file that was being tracked by git, but now the file is on the .gitignore list. However," +
                    " that file keeps showing up in git status after it's edited. How do you force git to completely forget " +
                    "about it?", ruben);
                post7.AddVote(new Vote(timon, VoteType.Upvote));
                post7.AddAnswer(new Answer("answer 1", jarne));
                _dbContext.Posts.Add(post7);

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
