using Microsite.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microsite.Data
{
    public class UserDbContext
    {
        readonly MicrositeDbContext DBContext;
        public UserDbContext()
        {
            DBContext = new MicrositeDbContext();
        }

        public async Task<UserRegisterDTO> CreateNewUser(UserRegisterDTO userInput)
        {
            await DBContext.Users.AddAsync(userInput);
            await DBContext.SaveChangesAsync();
            return userInput;
        }

        public bool EmailExists(string email)
        {
           if (DBContext.Users.Where(u => u.Email == email).Any())
                return false;
           return true;
        }

        public async Task<UserCompleteDTO> GetUserCompleteInfo(UserAuthDTO userAuthInfo)
        {
            UserRegisterDTO userRegisterDTO = DBContext.Users.Where(user => user.Email == userAuthInfo.Email).FirstOrDefault();
            await DBContext.SaveChangesAsync();
            UserCompleteDTO userCompleteDTO = new()
            {
                Id = userRegisterDTO.Id,
                Contact = userRegisterDTO.Contact,
                Country = userRegisterDTO.Country,
                Email = userRegisterDTO.Email,
                Image = userRegisterDTO.Image,
                Name = userRegisterDTO.Name
            };

            if (userRegisterDTO.Email != null)
            {
                return userCompleteDTO;
            }
            return null;
        }
        public async Task<UserAuthDTO> GetCredentialsByEmail(string email)
        {
            UserRegisterDTO userRegisterDTO = new();

            userRegisterDTO = DBContext.Users.Where(user => user.Email == email).FirstOrDefault();
            await DBContext.SaveChangesAsync();

            if (userRegisterDTO != null)
            {
                UserAuthDTO userAuthInfo = new()
                {
                    Email = userRegisterDTO.Email,
                    Password = userRegisterDTO.Password
                };


                if (userAuthInfo.Email != null)
                {
                    return userAuthInfo;
                }
            }
            return null;
        }

        public async Task<bool> FollowDb(FollowModelDTO follow)
        {
            FollowModelDTO follow1 = DBContext.Follow.Where(UF => UF.FollowerId == follow.FollowerId && UF.UserToFollowId == follow.UserToFollowId).FirstOrDefault();
            if(follow1 != null) { return false; }
            else
            {
                try
                {
                    await DBContext.Follow.AddAsync(follow);
                    await DBContext.SaveChangesAsync();
                    return true;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public async Task<bool> UnfollowDb(FollowModelDTO follow)
        {
            FollowModelDTO follow1 = DBContext.Follow.Where(UF => UF.FollowerId == follow.FollowerId && UF.UserToFollowId == follow.UserToFollowId).FirstOrDefault();
            if (follow1 != null) 
            {
                try
                {
                    DBContext.Follow.Remove(follow1);
                    await DBContext.SaveChangesAsync();
                    return true;
                }
                catch (Exception)
                {
                    throw;
                }
            }
            else
            {
                return false;
            }
        }

        public IList<UserCompleteDTO> FollowersDb(int loggedinUserId)
        {
            IList<UserCompleteDTO> followerData = new List<UserCompleteDTO>();
            IList<FollowModelDTO> followers = DBContext.Follow.Where(follow => follow.UserToFollowId == loggedinUserId).ToList();

            foreach(FollowModelDTO follower in followers)
            {
                UserCompleteDTO user = new();
                UserRegisterDTO users = DBContext.Users.Where(user => user.Id == follower.FollowerId).FirstOrDefault();
                user.Id = users.Id;
                user.Name = users.Name;
                user.Email = users.Email;
                user.Contact = users.Contact;
                user.Country = users.Country;
                user.Image = users.Image;
                followerData.Add(user);
            }
            return followerData;
        }

        public IList<UserCompleteDTO> FollowingDb(int loggedinUserId)
        {
            IList<UserCompleteDTO> followingUserData = new List<UserCompleteDTO>();
            IList<FollowModelDTO> followingUserList = DBContext.Follow.Where(follow => follow.FollowerId == loggedinUserId).ToList();

            foreach (FollowModelDTO followingUser in followingUserList)
            {
                UserCompleteDTO user = new();
                UserRegisterDTO users = DBContext.Users.Where(user => user.Id == followingUser.UserToFollowId).FirstOrDefault();
                user.Id = users.Id;
                user.Name = users.Name;
                user.Email = users.Email;
                user.Contact = users.Contact;
                user.Country = users.Country;
                user.Image = users.Image;
                followingUserData.Add(user);
            }
            return followingUserData;
        }

        public IList<UserCompleteDTO> GetAllUsersDb()
        {
            IList<UserCompleteDTO> getAllUsers = new List<UserCompleteDTO>();
            IList<UserRegisterDTO> allUsers = DBContext.Users.ToList();

            foreach(UserRegisterDTO user in allUsers)
            {
                UserCompleteDTO userComplete = new()
                {
                    Id = user.Id,
                    Name = user.Name,
                    Email = user.Email,
                    Contact = user.Contact,
                    Country = user.Country,
                    Image = user.Image
                };
                getAllUsers.Add(userComplete);
            }
            return getAllUsers;
        }

        public string MostTweetsBy()
        {
            var userIdWithMostTweets = DBContext.Tweets
                                        .GroupBy(x => x.UserId)
                                        .Select(g => new
                                        {
                                            UserId = g.Key,
                                            TweetCount = g.Count()
                                        })
                                        .OrderByDescending(g => g.TweetCount)
                                        .FirstOrDefault();

            if (userIdWithMostTweets != null)
            {
                int maxUserId = userIdWithMostTweets.UserId;
                var userWithMostTweets = DBContext.Users
                    .FirstOrDefault(u => u.Id == maxUserId);

                if (userWithMostTweets != null)
                {
                    return userWithMostTweets.Name;
                }
                else
                {
                    return "No user found with the most tweets";
                }
            }
            else
            {
                return "No tweets found";                                   
            }

        }
    }
}
