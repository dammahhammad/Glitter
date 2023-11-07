using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Microsite.BusinessLogic;
using Microsite.Models;
using Microsite.Shared;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Microsite.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("AllowOrigin")]
    public class TweetController : Controller
    {
        private readonly TweetBusinessContext tweetBusinessContext;
        private readonly TagBusinessContext TagBusinessContext;

        public TweetController()
        {
            tweetBusinessContext = new TweetBusinessContext();
            TagBusinessContext = new TagBusinessContext();
        }

        // Post a New Tweet
        [AllowAnonymous]
        [HttpPost]
        [Route("newTweet")]
        public async Task<ActionResult> NewTweet([FromBody] NewTweetModel newTweet)
        {
            try
            {
                if (newTweet == null) { return Ok("Invalid passed data"); }

                NewTweetDTO tweetInput = new()
                {
                    Message = newTweet.Message,
                    UserId = newTweet.UserId,
                    CreatedAt = DateTime.Now,
                    Name = newTweet.Name,
                };
                tweetInput = await tweetBusinessContext.NewTweet(tweetInput);
                bool tag = TagBusinessContext.NewTag(tweetInput);
                return Ok("Success");
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("allTweet")]
        public IList<GetAllTweetsDTO> GetAllTweets(int userId)
        {
            try
            {
                IList<GetAllTweetsDTO> allTweets = tweetBusinessContext.GetAllTweets(userId);
                return allTweets;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        // POST: TweetController/Edit/5
        [AllowAnonymous]
        [HttpPut]
        [Route("editTweet")]
        public async Task<ActionResult> EditTweet([FromBody] NewTweetModel updatedTweet)
        {
            try
            {
                NewTweetDTO newTweetDTO = new()
                {
                    Id = updatedTweet.Id,
                    Message = updatedTweet.Message,
                    UserId = updatedTweet.UserId,
                    Name = updatedTweet.Name,
                    CreatedAt = DateTime.Now
                };

                NewTweetDTO tweet = await tweetBusinessContext.UpdateTweet(newTweetDTO);
                return Ok("Success");
            }
            catch(Exception ex)
            {
                return Ok(ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpDelete]
        [Route("deleteTweet")]
        public async Task<ActionResult> DeleteTweet([FromBody] NewTweetModel deletedTweet)
        {
            try
            {
                NewTweetDTO deleteTweet = new()
                {
                    Id = deletedTweet.Id,
                    Message = deletedTweet.Message,
                    UserId = deletedTweet.UserId,
                    Name = deletedTweet.Name,
                };

                await tweetBusinessContext.DeleteTweet(deleteTweet);
                return Ok("Success");
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("liketweet")]
        public async Task<ActionResult> LikeTweet([FromBody] LikeTweetDTO likeTweet)
        {
            try
            {
                if (likeTweet is not null)
                {
                    await tweetBusinessContext.LikeTweet(likeTweet);
                }
                return Ok("Success");
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpDelete]
        [Route("disliketweet")]
        public async Task<ActionResult> DislikeTweet([FromBody] LikeTweetDTO dislikeTweet)
        {
            try
            {
                if (dislikeTweet is not null)
                {
                    await tweetBusinessContext.DislikeTweet(dislikeTweet);
                }
                return Ok("Success");
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }
    }
}
