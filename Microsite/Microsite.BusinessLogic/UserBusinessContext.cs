using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsite.Shared;
using Microsite.Data;
using AutoMapper;

namespace Microsite.BusinessLogic
{
    public class UserBusinessContext
    {
        private readonly UserDbContext UserDBContext;
        private readonly IMapper UserMapper;

        public UserBusinessContext()
        {
            UserDBContext = new UserDbContext();
            var userMappingConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UserRegisterDTO, UserCompleteDTO>();
            });
            UserMapper = new Mapper(userMappingConfig);
        }
        public async Task<UserCompleteDTO> CreateNewUser(UserRegisterDTO userInput)
        {

            if (UserDBContext.EmailExists(userInput.Email))
            {
                userInput.Password = PasswordHasher.HashPassword(userInput.Password);
                userInput.Image ??= Constants.DEFAULT_USER_IMAGE;
                await UserDBContext.CreateNewUser(userInput);

                UserCompleteDTO user;
                user = UserMapper.Map<UserRegisterDTO, UserCompleteDTO>(userInput);
                return user;
            }
            else
            {
                throw new Exceptions.AlreadyExistsException("Email address already in use");
            }
        }

        public async Task<UserCompleteDTO> LoginUserCheck(UserLoginDTO userLoginDTO)
        {
            UserAuthDTO userAuthInfo = await UserDBContext.GetCredentialsByEmail(userLoginDTO.Email) ?? throw new Exceptions.InvalidCredentialsException("Email not found");
            if (PasswordHasher.ValidatePassword(userLoginDTO.Password, userAuthInfo.Password))
            {
                UserCompleteDTO userCompleteDTO = await UserDBContext.GetUserCompleteInfo(userAuthInfo);
                return userCompleteDTO;
            }
            return null;
        }

        public async Task<bool> Follow(FollowModelDTO follow)
        {
            if(await UserDBContext.FollowDb(follow))
            {
                return true;
            }
            return false;
        }

        public async Task<bool> Unfollow(FollowModelDTO unfollow)
        {
            if (await UserDBContext.UnfollowDb(unfollow))
            {
                return true;
            }
            return false;
        }

        public IList<UserCompleteDTO> Followers(int loggedinUserId)
        {
            IList<UserCompleteDTO> followers = UserDBContext.FollowersDb(loggedinUserId);
            return followers;
        }

        public IList<UserCompleteDTO> Following(int loggedinUserId)
        {
            IList<UserCompleteDTO> following = UserDBContext.FollowingDb(loggedinUserId);
            return following;
        }

        public IList<UserCompleteDTO> GetAllUsers()
        {
            IList<UserCompleteDTO> allUsers = UserDBContext.GetAllUsersDb();
            return allUsers;
        }
    }
}
