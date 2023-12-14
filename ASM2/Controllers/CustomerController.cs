using ASM2.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ASM2.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CustomerController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public CustomerController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var customers = _userManager.GetUsersInRoleAsync("Customer").Result;
            return View(customers);
        }

        // Phương thức thêm customer account
        [HttpPost]
        public async Task<IActionResult> Create(string username, string password)
        {
            var customer = new ApplicationUser { UserName = username };
            var result = await _userManager.CreateAsync(customer, password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(customer, "Customer");
                return RedirectToAction("Index");
            }

            return View("Error");
        }

        // Phương thức xóa customer account
        [HttpPost]
        public async Task<IActionResult> Delete(string userId)
        {
            var customer = await _userManager.FindByIdAsync(userId);

            if (customer != null)
            {
                var result = await _userManager.DeleteAsync(customer);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }

                return View("Error");
            }

            return View("NotFound");
        }
    }
}
