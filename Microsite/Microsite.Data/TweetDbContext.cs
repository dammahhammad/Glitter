using AutoMapper;
using Microsite.Shared;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Microsite.Data
{
    public class TweetDbContext
    {
        readonly MicrositeDbContext DBContext;

        public TweetDbContext()
        {
            DBContext = new MicrositeDbContext();
        }
        public async Task<NewTweetDTO> NewTweetDb(NewTweetDTO newTweet)
        {
            await DBContext.Tweets.AddAsync(newTweet);
            await DBContext.SaveChangesAsync();

            return newTweet;
        }
        public IList<GetAllTweetsDTO> AllTweetsDb(int userId)
        {
            IList<GetAllTweetsDTO> allTweets = new List<GetAllTweetsDTO>();
            allTweets = (from u in DBContext.Users
                         from t in DBContext.Tweets.Where(tw => tw.UserId == u.Id)
                         select new GetAllTweetsDTO
                         {
                             Id = t.Id,
                             Message = t.Message,
                             UserId = t.UserId,
                             Name = t.Name,
                             CreatedAt = t.CreatedAt,
                             Image = u.Image,
                         }).ToList();

            foreach (var tweet in allTweets)
            {
                LikeTweetDTO t = DBContext.LikeTweet.Where(a => (a.TweetId == tweet.Id) && (a.UserId == userId)).FirstOrDefault();
                if(t is not null)
                {
                    tweet.IsLiked = true;
                }
                else tweet.IsLiked = false;
            }
            return allTweets;
        }
        public async Task<NewTweetDTO> UpdatedTweetDb(NewTweetDTO updateTweet)
        {
            try
            {
                NewTweetDTO newTweetDTO = DBContext.Tweets.Where(tweet => tweet.Id == updateTweet.Id).FirstOrDefault();
                newTweetDTO.Message = updateTweet.Message;
                newTweetDTO.CreatedAt = System.DateTime.Now;
                await DBContext.SaveChangesAsync();
                return updateTweet;
            }
            catch (Exception)
            {
                return null;
            }

        }
        public async Task<NewTweetDTO> DeleteTweetDb(NewTweetDTO deleteTweet)
        { 
            try
            {
                NewTweetDTO deleteTweetDTO = DBContext.Tweets.Where(tweet => tweet.Id == deleteTweet.Id).FirstOrDefault();
                DBContext.Tweets.Remove(deleteTweetDTO);
                await DBContext.SaveChangesAsync();
                return deleteTweet;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public async Task<LikeTweetDTO> LikeTweet(LikeTweetDTO likeTweet)
        {
            await DBContext.LikeTweet.AddRangeAsync(likeTweet);
            await DBContext.SaveChangesAsync();
            return likeTweet;
        }
        public async Task<LikeTweetDTO> DislikeTweet(LikeTweetDTO dislikeTweet)
        {
            try
            {
                LikeTweetDTO dislike = DBContext.LikeTweet.Where(tweet => (tweet.TweetId == dislikeTweet.TweetId) && (tweet.UserId == dislikeTweet.UserId)).FirstOrDefault();
                DBContext.LikeTweet.Remove(dislike);
                await DBContext.SaveChangesAsync();
                return dislike;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public string MostTrending()
        {
            TagDTO tagByName = DBContext.Tag.OrderByDescending(re => re.SearchCount).ThenByDescending(re => re.TagName).FirstOrDefault();
            if (tagByName is not null)
            {
                return tagByName.TagName;
            }
            else
            {
                return "No Tags Found";
            }
        }

        public string MostLiked()
        {
            var tweetIdWithMostLikes = DBContext.LikeTweet
                                        .GroupBy(x => x.TweetId)
                                        .Select(g => new
                                        {
                                            TweetId = g.Key,
                                            LikeCount = g.Count()
                                        })
                                        .OrderByDescending(g => g.LikeCount)
                                        .FirstOrDefault();

            if (tweetIdWithMostLikes != null)
            {
                int maxid = tweetIdWithMostLikes.TweetId;
                var t = DBContext.Tweets.FirstOrDefault(ds => ds.Id == maxid);

                if (t != null)
                {
                    return t.Message;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        public int TotalTweetsToday()
        {
            DateTime sysDate = DateTime.Today;
            int count = DBContext.Tweets.Count(i => i.CreatedAt.Date == sysDate);
            return count;
        }
    }
}
