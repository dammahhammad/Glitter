using AutoMapper;
using Microsite.Data;
using Microsite.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsite.BusinessLogic
{
    public class SearchBusinessContext
    {
        private readonly SearchDbContext searchDbContext;
        private readonly IMapper mapper;
        public SearchBusinessContext()
        {
            searchDbContext = new SearchDbContext();

            var userMappingConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UserRegisterDTO, UserCompleteDTO>();
            });
            mapper = new Mapper(userMappingConfig);
        }

        //search users
        public List<UserCompleteDTO> GetAllUsers(SearchDTO searchDTO)
        {
            if (searchDTO.SearchString == null) { return null; }
            UserCompleteDTO user;
            List<UserCompleteDTO> AllUsers = new();
            List<UserRegisterDTO> users = searchDbContext.GetAllUsers(searchDTO);
            
            foreach(UserRegisterDTO us in users)
            {
                user = mapper.Map<UserRegisterDTO, UserCompleteDTO>(us);
                AllUsers.Add(user);
            }
            return AllUsers;
        }

        // search tags
        public List<GetAllTweetsDTO> GetAllTags(SearchDTO searchDTO)
        {
            if (searchDTO.SearchString == null) { return null; }
            List<GetAllTweetsDTO> users = searchDbContext.GetAllTags(searchDTO);
            return users;
        }
    }
}
