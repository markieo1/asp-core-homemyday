//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc;
//using System.Threading.Tasks;
//using Xunit;
//using System;
//using HomeMyDay.Controllers;
//using HomeMyDay.ViewModels;
//using Moq;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Authentication;
//using Microsoft.Extensions.Options;

//namespace HomeMyDay.Tests
//{
//    public class AccountControllerTest
//    {
//        [Fact]
//        public void TestLogin()
//        {
//            LoginViewModel loginModel = new LoginViewModel{ ReturnUrl = "/" };
            
//            RedirectResult result = new RedirectResult(loginModel?.ReturnUrl);

//            var userStore = new Mock<IUserStore<IdentityUser>>();

//            var userManager = new UserManager<IdentityUser>(userStore.Object, null, null, null, null, null, null, null, null);
//            var signInManager = new SignInManager<IdentityUser>(userManager, new HttpContextAccessor { HttpContext = new Mock<HttpContext>().Object }, new Mock<IUserClaimsPrincipalFactory<IdentityUser>>().Object, null, null, null);
            
//            var controller = new AccountController(userManager, signInManager);

//            Assert.Equal(result, controller.Login(loginModel).Result as RedirectResult);
                   

                       


            
//            //vergelijken of dat de expected view gelijkj is aan wat er wordt terug gegeven.
//           // Assert.Equal(ExpectedViewModel, ResultViewModel);
//        }

//        [Fact]
//        public async Task Test_if_Correct_view_returned_when_user_Register_succesfull()
//        {

//        }
//    }
//}