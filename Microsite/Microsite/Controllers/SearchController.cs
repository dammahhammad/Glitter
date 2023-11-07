using AutoMapper;
using Microsite.BusinessLogic;
using Microsite.Models;
using Microsite.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace Microsite.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("AllowOrigin")]
    public class SearchController : Controller
    {
        private readonly IMapper mapper;
        private readonly SearchBusinessContext searchBusinessContext;
        public SearchController()
        {
            searchBusinessContext = new SearchBusinessContext();
            var userMappingConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<SearchModel, SearchDTO>();
            });
            mapper = new Mapper(userMappingConfig);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("searchbyuser")]
        public List<UserCompleteDTO> SearchUser([FromBody] SearchModel search)
        {
            try
            {
                SearchDTO searchDTO = mapper.Map<SearchModel, SearchDTO>(search);
                List<UserCompleteDTO> AllUsers = new();
                AllUsers = searchBusinessContext.GetAllUsers(searchDTO);
                
                return AllUsers;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("searchbytag")]
        public List<GetAllTweetsDTO> SearchPost([FromBody] SearchModel search)
        {
            try
            {
                SearchDTO searchDTO = mapper.Map<SearchModel, SearchDTO>(search);
                List<GetAllTweetsDTO> AllUsers = new();
                AllUsers = searchBusinessContext.GetAllTags(searchDTO);

                return AllUsers;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
