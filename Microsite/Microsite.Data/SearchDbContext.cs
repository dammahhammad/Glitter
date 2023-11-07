using Microsite.Shared;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace Microsite.Data
{
    public class SearchDbContext
    {
        readonly MicrositeDbContext DbContext;
        public SearchDbContext() 
        { 
            DbContext = new MicrositeDbContext();
        }
        public List<UserRegisterDTO> GetAllUsers(SearchDTO searchDTO)
        {
            try
            {
                    List<UserRegisterDTO> users = new();
                    users = DbContext.Users.Where(a => a.Name.Contains(searchDTO.SearchString) & (a.Id != searchDTO.UserId)).ToList();
                    return users;
            }
            catch
            {
                return null;
            }
        }
        public List<GetAllTweetsDTO> GetAllTags(SearchDTO searchDTO)
        {
            try
            {


                List<GetAllTweetsDTO> getAllTweets = new();

                getAllTweets = (from tweet in DbContext.Tweets
                                where DbContext.Tag.Any(tag => tag.TweetId == tweet.Id && tag.TagName == searchDTO.SearchString)
                                select new GetAllTweetsDTO
                                {
                                    Id = tweet.Id,
                                    Message = tweet.Message,
                                    UserId = tweet.UserId,
                                    Name = tweet.Name,
                                    CreatedAt = tweet.CreatedAt,
                                    Image = DbContext.Users.Where(a => a.Id == tweet.UserId).Select(u => u.Image).FirstOrDefault()
                                }).ToList();


                try
                {
                    TagDTO updateTag = DbContext.Tag.Where(tag => tag.TagName == searchDTO.SearchString).FirstOrDefault();
                    if (updateTag != null)
                    {
                        updateTag.SearchCount = (updateTag.SearchCount == 0) ? updateTag.SearchCount = 1 : updateTag.SearchCount + 1;
                        DbContext.SaveChanges();
                    }
                    
                } catch { return null; }

                return getAllTweets;
            }
            catch
            {
                return null;
            }
        }
    }
}
