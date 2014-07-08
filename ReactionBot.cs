using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Net;
using Newtonsoft.Json;
using TweetSharp;
using Newtonsoft.Json.Linq;

namespace ReactionBot
{
    class ReactionBot
    {
        private TwitterService service;
        private CommonWordsList words;
        private Random rand;

        public ReactionBot()
        {
            this.rand = new Random();
            this.service = authenticate();
            this.words = new CommonWordsList();
        }

        #region Twitter Methods

        public TwitterService authenticate()
        {
            string SERVICE_KEY = ConfigurationManager.AppSettings["serviceKey"],
                   SERVICE_SECRET = ConfigurationManager.AppSettings["serviceSecret"],
                   ACCESS_TOKEN = ConfigurationManager.AppSettings["accessToken"],
                   ACCESS_TOKEN_SECRET = ConfigurationManager.AppSettings["accessTokenSecret"];

            var serv = new TwitterService(SERVICE_KEY, SERVICE_SECRET);
            serv.AuthenticateWith(ACCESS_TOKEN, ACCESS_TOKEN_SECRET);
            return serv;
        }

        public TwitterStatus tweetBasedOnWord(string word)
        {
            var options = new SearchOptions { Q = word, Count = 10 };
            var tweets = service.Search(options).Statuses.ToList();
            return tweets[Utility.randomInt(rand, 0, tweets.Count)];
        }

        public TwitterStatus randomTweet()
        {
            return tweetBasedOnWord(words.randomWord());
        }

        public TwitterStatus reactToTweet(TwitterStatus tweet, string gif)
        {
            string user = "@" + tweet.User.ScreenName;
            Console.WriteLine(tweet.Id);
            var options = new SendTweetOptions { Status = user + " " + gif, InReplyToStatusId = tweet.Id};
            return service.SendTweet(options);
        }

        public void run()
        {
            var tweet = randomTweet();
            string gif = getRandomReaction();

            System.Console.WriteLine(tweet.Text);
            System.Console.WriteLine(gif);

            var reply = reactToTweet(tweet, gif);
        }

        #endregion

        #region Reddit Methods

        public string getRandomReaction()
        {
            JToken post;
            using (WebClient wc = new WebClient())
            {
                var json = wc.DownloadString(ConfigurationManager.AppSettings["reactionGifJson"]);
                var posts = (JArray)JToken.Parse(json).SelectToken("data.children");
                post = posts[Utility.randomInt(rand, 0, posts.Count)];
            }
            return (string)post.SelectToken("data.url");
        }

        #endregion
    }
}
