using Microsite.Data;
using Microsite.Shared;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsite.BusinessLogic
{
    public class TweetBusinessContext
    {
        private readonly TweetDbContext tweetDbContext;

        public TweetBusinessContext()
        {
            tweetDbContext = new TweetDbContext();
        }

        public async Task<NewTweetDTO> NewTweet(NewTweetDTO tweetInput)
        {
            NewTweetDTO newTweet = await tweetDbContext.NewTweetDb(tweetInput);

            return newTweet;
        }

        public IList<GetAllTweetsDTO> GetAllTweets(int userId)
        {
            IList<GetAllTweetsDTO> allTweets = tweetDbContext.AllTweetsDb(userId);
            return allTweets;
        }

        public async Task<NewTweetDTO> UpdateTweet(NewTweetDTO updatedTweet)
        {
            NewTweetDTO tweet = await tweetDbContext.UpdatedTweetDb(updatedTweet);
            return tweet;
        }

        public async Task<NewTweetDTO> DeleteTweet(NewTweetDTO deleteTweet)
        {
            NewTweetDTO tweet = await tweetDbContext.DeleteTweetDb(deleteTweet);
            return tweet;
        }

        public async Task<LikeTweetDTO> LikeTweet(LikeTweetDTO likeTweet)
        {
            LikeTweetDTO like = await tweetDbContext.LikeTweet(likeTweet);
            return like;
        }
        public async Task<LikeTweetDTO> DislikeTweet(LikeTweetDTO dislikeTweet)
        {
            LikeTweetDTO dislike = await tweetDbContext.DislikeTweet(dislikeTweet);
            return dislike;
        }
    }
}
