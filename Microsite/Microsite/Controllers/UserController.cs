using AutoMapper;
using Azure.Storage.Blobs;
using Microsite.BusinessLogic;
using Microsite.Models;
using Microsite.Shared;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Microsite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    public class UserController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserBusinessContext userBusinessContext;
        private readonly IMapper mapper;
/*        private readonly string blobStorage = "blob storage access key";
        private readonly string containerName = "container name";*/

        public UserController(IConfiguration config, IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor)
        {
            _config = config;
            _webHostEnvironment = webHostEnvironment;
            _httpContextAccessor = httpContextAccessor;
            userBusinessContext = new UserBusinessContext();

            var userMappingConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<RegisterModel, UserRegisterDTO>();
                cfg.CreateMap<LoginModel, UserLoginDTO>();
                cfg.CreateMap<FollowModel, FollowModelDTO>();
            });
            mapper = new Mapper(userMappingConfig);
        }

        [HttpPost]
        [Route("CreateUser")]
        public async Task<IActionResult> Create([FromBody] RegisterModel user)
        {
            try
            {
                if (user == null)
                {
                    return Ok("Invalid Passed data");
                }

                if (!ModelState.IsValid)
                {
                    return Ok("Modelstate is not valid");
                }

                UserRegisterDTO userDTO = mapper.Map<RegisterModel, UserRegisterDTO>(user);
                UserRegisterDTO userCompleteDTO = new()
                {
                    Name = userDTO.Name,
                    Email = userDTO.Email,
                    Image = userDTO.Image,
                    Contact = userDTO.Contact,
                    Country = userDTO.Country,
                    Password = userDTO.Password
                };
                UserCompleteDTO newUser = await userBusinessContext.CreateNewUser(userCompleteDTO);
                return Ok("Success");
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginModel user)
        {
            try
            {
                if (user == null) { return Ok("Invalid passed data"); }

                if (!ModelState.IsValid)
                {
                    return Ok("Modelstate is not valid");
                }
                UserLoginDTO userLoginDTO = mapper.Map<LoginModel, UserLoginDTO>(user);
                UserCompleteDTO loginUser = await userBusinessContext.LoginUserCheck(userLoginDTO);
                if (loginUser != null)
                {
                    return Ok(new JwtService(_config).GenerateToken(
                        loginUser.Id.ToString(),
                        loginUser.Name,
                        loginUser.Email,
                        loginUser.Contact,
                        loginUser.Country
                        ));
                }
            }
            catch(Exception ex)
            {
                return Ok(ex.Message);
            }

            return Ok("Failure");
        }

        [HttpPost]
        [Route("follow")]
        public async Task<IActionResult> Follow([FromBody] FollowModel follow)
        {
            try
            {
                if (follow == null) { return Ok("Invalid passed data"); }

                if (follow.FollowerId == follow.UserToFollowId) { return Ok("Both are same user"); }

                FollowModelDTO followDTO = mapper.Map<FollowModel, FollowModelDTO>(follow);
                bool followed = await userBusinessContext.Follow(followDTO);
                if (followed)
                {
                    return Ok("Success");
                }
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }

            return Ok("Already Followed");
        }

        [HttpDelete]
        [Route("unfollow")]
        public async Task<IActionResult> UnFollow([FromBody] FollowModel unFollow)
        {
            try
            {
                if (unFollow == null) { return Ok("Invalid passed data"); }

                FollowModelDTO unFollowDTO = mapper.Map<FollowModel, FollowModelDTO>(unFollow);
                bool unFollowed = await userBusinessContext.Unfollow(unFollowDTO);
                if (unFollowed)
                {
                    return Ok("Success");
                }
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
            return Ok("Not Followed");
        }

        //Id is passed from URL
        [HttpGet]
        [Route("followers")]
        public IList<UserCompleteDTO> Followers(int Id)
        {
            try
            {
                IList<UserCompleteDTO> followers = userBusinessContext.Followers(Id);
                return followers;
            }
            catch (Exception)
            {
                throw;
            }
        }

        //Id is passed from URL
        [HttpGet]
        [Route("following")]
        public IList<UserCompleteDTO> Following(int Id)
        {
            try
            {
                IList<UserCompleteDTO> following = userBusinessContext.Following(Id);
                return following;
            }
            catch (Exception) { throw; }
        }

        [HttpPost]
        [Route("image")]
        public string Post()
        {
            HttpContext currentContext = _httpContextAccessor.HttpContext;
            var file = currentContext.Request.Form.Files.Count > 0 ? currentContext.Request.Form.Files[0] : null;
            if (file != null && file.Length > 0)
            {
                string folder = Guid.NewGuid().ToString() + file.FileName;
                var path = Path.Combine(_webHostEnvironment.ContentRootPath, "profileImage", folder);

                file.CopyToAsync(new FileStream(path, FileMode.Create));

                return "profileImage/" + folder;
            }
            return "No Image";
        }

        /* When image is to be uploaded in azure Blob
                [HttpPost]
                [Route("image")]
                public string Post()
                {
                    HttpContext currentContext = _httpContextAccessor.HttpContext;
                    var file = currentContext.Request.Form.Files.Count > 0 ? currentContext.Request.Form.Files[0] : null;
                    var fileUrl = "";
                    if (file != null && file.Length > 0)
                    {
                        string fileName = Guid.NewGuid().ToString() + file.FileName;
                        BlobContainerClient container = new BlobContainerClient(blobStorage, containerName);
                        try
                        {
                            BlobClient blob = container.GetBlobClient(fileName);
                            using Stream stream = file.OpenReadStream();
                            blob.Upload(stream);
                            fileUrl = blob.Uri.AbsoluteUri;

                        }catch(Exception ex) { return ex.Message; }

                        return fileUrl;
                    }
                    return "No Image";
                }*/

        [HttpGet]
        [Route("getAllUsers")]
        public IList<UserCompleteDTO> GetAllUsers()
        {
            try
            {
                IList<UserCompleteDTO> allUsers = userBusinessContext.GetAllUsers();
                return allUsers;
            }
            catch (Exception) { throw; }
        }
    }
}
