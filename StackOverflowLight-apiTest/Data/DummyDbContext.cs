using StackOverflowLight_api.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace StackOverflowLight_apiTest.Data
{
    class DummyDbContext
    {
        public IEnumerable<User> Users { get; set; }
        public User Jarne { get; set; }
        public IEnumerable<Post> Posts { get; set; }
        public Post Post1 { get; set; }

        public DummyDbContext()
        {
            Jarne = new User("Jarne", "Deschacht", "jarne.deschacht@hotmail.com");
            User ime = new User("Ime", "Van Daele", "imevandaele@gmail.com");
            User tijs = new User("Tijs", "Martens", "tijs.martens@gmail.com");
            User ruben = new User("Ruben", "Vanhee", "ruben.vanhee@hotmail.com");

            Users = new List<User>
            {
                Jarne,ime,tijs,ruben
            };

            Post1 = new Post("Problems setting proxy on HttpClient", "In my C# application I'm trying to send an HTTP request" +
                    " to an external company outside of our firewall. When I use the below code, I'm getting back an error that" +
                    " the remote site has forcibly closed the connection. I'm assuming I'm doing something incorrectly with the " +
                    "proxy setting but I'm not sure what. Our proxy does not require authentication. I've printed out the encoded " +
                    "JSON that gets sent and if I just do things manually via curl the request goes right though, so I'm sure that" +
                    " my URL, bearer token and JSON are all correct.", Jarne);
            Post1.AddVote(new Vote(ime, VoteType.Downvote));
            Post1.AddVote(new Vote(tijs, VoteType.Upvote));
            Post1.AddVote(new Vote(Jarne, VoteType.Downvote));
            Post1.AddAnswer(new Answer("answer 1", ime));
            Post1.AddAnswer(new Answer("answer 2", tijs));
            Post1.AddAnswer(new Answer("answer 3", ruben));
            Post1.AddAnswer(new Answer("answer 4", ime));

            Post post2 = new Post("Can comments be used in JSON?", "Can I use comments inside a JSON file? If so, how?", ruben);
            post2.AddVote(new Vote(Jarne, VoteType.Downvote));
            post2.AddVote(new Vote(tijs, VoteType.Downvote));
            post2.AddVote(new Vote(ime, VoteType.Downvote));
            post2.AddAnswer(new Answer("No. The JSON should all be data," +
                " and if you include a comment, then it will be data too. You could have a designated" +
                " data element called '_comment'(or something)' that would be ignored by apps that use" +
                " the JSON data. You would probably be better having the comment in the processes that" +
                " generates/ receives the JSON, as they are supposed to know what the JSON data will be " +
                "in advance, or at least the structure of it.", ime));


            Post post3 = new Post("What is the most efficient way to deep clone an object in JavaScript?", "What is the most efficient way to clone a JavaScript object? " +
                "I've seen obj = eval(uneval(o)); being used, but that's non-standard and only supported by " +
                "Firefox. I've done things like obj = JSON.parse(JSON.stringify(o)); but question the efficiency.  " +
                "I've also seen recursive copying functions with various flaws. I'm surprised no canonical solution" +
                " exists.", ime);

            post3.AddVote(new Vote(Jarne, VoteType.Downvote));
            post3.AddVote(new Vote(tijs, VoteType.Downvote));
            post3.AddVote(new Vote(ruben, VoteType.Upvote));
            post3.AddAnswer(new Answer("Note that JSON method will loose any Javascript types that have no equivalent in JSON. For example: JSON.parse(JSON.stringify({a:null,b:NaN,c:Infinity,d:undefined,e:function(){},f:Number,g:false})) will generate {a: null, b: null, c: null, g: false}", Jarne));
            post3.AddAnswer(new Answer("eval() is generally a bad idea because many Javascript engine's optimisers have to turn off when dealing with variables that are set via eval. Just having eval() in your code can lead to worse performance", ruben));
            post3.AddAnswer(new Answer("Cloning objects is a tricky business, especially with custom objects of arbitrary collections. Which probably why there is no out-of-the box way to do it.", tijs));
            post3.AddAnswer(new Answer("Eval is not evil. Using eval poorly is. If you are afraid of its side effects you are using it wrong. The side effects you fear are the reasons to use it. Did any one by the way actually answer your question?", Jarne));

            Posts = new List<Post>
            {
                Post1,post2,post3
            };
        }
    }
}
