using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Demo1.Models;
using Demo.Data;

namespace Demo1.Controllers
{
    public class AdminController : Controller
        {
            private readonly UserManager<IdentityUser> userManager;
            private readonly IPasswordValidator<IdentityUser> passwordValidator;
            private readonly IUserValidator<IdentityUser> userValidator;
            private readonly IPasswordHasher<IdentityUser> passwordHasher;
        private readonly ICustomerRepository customerRepository;

        public AdminController(UserManager<IdentityUser> userManager,
                IPasswordValidator<IdentityUser> passwordValidator,
                IUserValidator<IdentityUser> userValidator,
                IPasswordHasher<IdentityUser> passwordHasher,
                ICustomerRepository customerRepository)
            {
                this.userManager = userManager;
                this.passwordValidator = passwordValidator;
                this.userValidator = userValidator;
                this.passwordHasher = passwordHasher;
            this.customerRepository = customerRepository;
        }
            public IActionResult Index()
            {
                var users = userManager.Users.ToList();

                List<UserViewModel> userViewModels = new List<UserViewModel>();
                foreach (var item in users)
                {
                    UserViewModel userViewModel = new UserViewModel()
                    {
                        Id = item.Id,
                        Email = item.Email,
                        Password = item.PasswordHash,
                        Username = item.UserName
                    };
                    userViewModels.Add(userViewModel);

                }

                return View(userViewModels);
            }

            public IActionResult Create()
            {
                return View();
            }

            [HttpPost]
            public async Task<IActionResult> Create(UserViewModel model)
            {
                if (ModelState.IsValid)
                {
                    IdentityUser appUser = new IdentityUser()
                    {
                        UserName = model.Username,
                        Email = model.Email

                    };

                    IdentityResult result = await userManager.CreateAsync(appUser, model.Password);
                    if (result.Succeeded)
                    {
             
                       customerRepository.Add(new Customer() { Name = model.Name ,IdentityId=appUser.Id});

                        await userManager.AddToRoleAsync(appUser, "guest");

                        return RedirectToAction("Index");
                    }
                    else
                    {
                        foreach (var item in result.Errors)
                        {
                            ModelState.AddModelError("", item.Description);
                        }
                    }


                }
                return View(model);
            }

         
            public void AddModelError(IdentityResult result)
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
            }


        }
    
}
