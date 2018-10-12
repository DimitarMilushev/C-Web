using IRunesWebApp.Models;
using Services;
using SIS.HTTP.Exceptions;
using SIS.HTTP.Requests;
using SIS.HTTP.Responses;
using SIS.WebServer.Results;
using System;
using System.Linq;

namespace IRunesWebApp.Controllers
{
    public class UsersController : BaseController
    {
        private readonly HashService hashService;

        public UsersController()
        {
            hashService = new HashService();
        }
        public IHttpResponse Login(IHttpRequest request) => this.View();

        public IHttpResponse PostLogin(IHttpRequest request)
        {
            var username = request.FormData["username"].ToString();
            var password = request.FormData["password"].ToString();

            var hashedPassword = this.hashService.StrongHash(password);

            var user = this.Context.Users.FirstOrDefault(x => x.Username == username &&
                                                 x.HashedPassword == hashedPassword);

            if (user == null)
                return new RedirectResult("/login");

            var response = new RedirectResult("/index");
            this.SignInUser(username, response, request);
            return response;
        }

        public IHttpResponse Register(IHttpRequest request) => this.View();

        public IHttpResponse PostRegister(IHttpRequest request)
        {
            var username = request.FormData["username"].ToString().Trim();
            var password = request.FormData["password"].ToString();
            var confirmPassword = request.FormData["confirmPassword"].ToString();
            var email = request.FormData["email"].ToString();

            if (string.IsNullOrWhiteSpace(username))
            {
                throw new BadRequestException("Please provide valid username with length of 4 or more characters.");
            }

            if (this.Context.Users.Any(x => x.Username == username))
            {
                throw new BadRequestException("User with the same name already exists.");
            }

            //if (string.IsNullOrWhiteSpace(password) || password.Length < 6)
            //{
            //    throw new BadRequestException("Please provide password of length 6 or more.");
            //}

            //if (password != confirmPassword)
            //{
            //    throw new BadRequestException("Passwords do not match.");
            //}

            if (password != confirmPassword)
                throw new BadRequestException("Passwords do not match");
            // Hash password
            var hashedPassword = this.hashService.StrongHash(password);

            // Create user
            var user = new User
            {
                Username = username,
                HashedPassword = hashedPassword,
                Email = email
            };

            this.Context.Users.Add(user);

            try
            {
                this.Context.SaveChanges();
            }
            catch (Exception e)
            {
                // TODO: Log error
                throw new InternalServerErrorException(e.Message);
            }

            var response = new RedirectResult("/");
            // TODO: Login
            this.SignInUser(username, response, request);

            // Redirect
            return response;
        }
    }
}
