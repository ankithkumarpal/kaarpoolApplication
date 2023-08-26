using AutoMapper;
using Google.Apis.Auth;
using Interfaces;
using Microsoft.Identity.Client;
using Models;
using System.Security.Cryptography;
using ViewLayer;

namespace CarPool.Services
{
    public class AuthService : IAuthService
    {
        private ITokenGeneration _tokenGenerationService;
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper { get; set; }
        private ILog _logger { get; set; }
        private IConfiguration _config { get ; set; }
        public AuthService(ITokenGeneration tokenGenerationService , IUnitOfWork unitOfWork , IMapper mapper , ILog logger , IConfiguration config)
        {
            _tokenGenerationService = tokenGenerationService;
            _unitOfWork= unitOfWork;
            _mapper = mapper;
            _config= config;
            _logger = logger;
        }

        public bool SignUp(UserDTO user)
        {
            try
            {
                User existingUser = _unitOfWork.authRepository.CheckUserExist(user.Email);
                if (existingUser != null)
                {
                    _logger.Log("Email Alreadey Exists");
                    return false;
                }
                User userDetail = _mapper.Map<User>(user);
                string passwordHash = BCrypt.Net.BCrypt.HashPassword(user.Password);
                userDetail.Password = passwordHash;
                User res = _unitOfWork.authRepository.AddUser(userDetail);
                _unitOfWork.SaveChanges();
                if (res == null) { return false; }
                return true;
            }
            catch(Exception ex) {
                _logger.Log(ex.Message);
                throw ex;
            }
           
        }

        public bool VerifyHashedPassword(string password , string hashedPassword)
        {
            return  BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }

        public  async Task<string> GoogleLogin(string token)
        {

            var settings = new GoogleJsonWebSignature.ValidationSettings
            {
                Audience = new[] { _config["Authentication:clientId"] }
            };

            try
            {
                var userData = await GoogleJsonWebSignature.ValidateAsync(token, settings);
                User user = _unitOfWork.authRepository.CheckUserExist(userData.Email);
                if (user == null)
                {
                    User userDetail = new User();
                    userDetail.Email = userData.Email;
                    userDetail.Name = userData.Name;
                    User res = _unitOfWork.authRepository.AddUser(userDetail);
                    _unitOfWork.SaveChanges();
                    if (res == null) { return null;}
                    return _tokenGenerationService.GenerateToken(userDetail);
                }
                string responseToken = _tokenGenerationService.GenerateToken(user);
                _unitOfWork.authRepository.Update(user);
                _unitOfWork.SaveChanges();
                return responseToken;
            }
            catch (Exception ex)
            {
                _logger.Log(ex.Message);
                throw ex;
            }
        }
        public string  Login(LoginRequestDTO requestDetails)
        {
            try
            {
                User user = _unitOfWork.authRepository.CheckUserExist(requestDetails.Email);
                if (user == null || !BCrypt.Net.BCrypt.Verify(requestDetails.Password, user.Password)) {
                    if (user == null) _logger.Log("Email Does not exists");
                    else _logger.Log("Incorrect Password");
                    return null;
                }
                string responseToken = _tokenGenerationService.GenerateToken(user);
                _unitOfWork.authRepository.Update(user);
                _unitOfWork.SaveChanges();
                return responseToken;
            }catch(Exception ex) {
                _logger.Log(ex.Message);
                throw ex;
            }
        }
    }
}
