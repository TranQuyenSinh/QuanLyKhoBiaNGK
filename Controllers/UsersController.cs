// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QuanLyKhoBiaNGK.Data;
using QuanLyKhoBiaNGK.Models;
using QuanLyKhoBiaNGK.ViewModels;

namespace App.Areas.Identity.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;

        public UsersController(RoleManager<IdentityRole> roleManager, ApplicationDbContext context, UserManager<User> userManager)
        {
            _roleManager = roleManager;
            _context = context;
            _userManager = userManager;
        }

        //
        // GET: /ManageUser/Index
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.OrderBy(u => u.UserName)
                   .ToListAsync();

            var model = new List<UserWithRoles>();
            foreach (var user in users)
            {
                var roles = (await _userManager.GetRolesAsync(user)).ToList();
                var isLocked = await _userManager.IsLockedOutAsync(user);
                model.Add(new UserWithRoles
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    FullName = user.FullName,
                    RoleNames = string.Join(",", roles),
                    IsLocked = isLocked,
                    Roles = roles
                });
            }

            return View(model);
        }

        // GET: /ManageUser/AddRole/id
        [HttpGet]
        public async Task<IActionResult> AddRoleAsync(string id)
        {
            if (!_roleManager.Roles.Any())
            {
                await _roleManager.CreateAsync(new IdentityRole("Admin"));
                await _roleManager.CreateAsync(new IdentityRole("Thủ kho"));
            }
            if (string.IsNullOrEmpty(id))
            {
                return NotFound("Không có user");
            }

            var user = await _userManager.Users.Select(x => new UserWithRoles
            {
                Id = x.Id,
                UserName = x.UserName,
                FullName = x.FullName,
            }).FirstOrDefaultAsync(x => x.Id == id);

            if (user == null)
            {
                return NotFound($"Không thấy user, id = {id}.");
            }

            // role of user
            var roles = await _userManager.GetRolesAsync(user);
            user.Roles = roles.ToList();

            // all role
            List<string> roleNames = await _roleManager.Roles.Select(r => r.Name).ToListAsync();
            ViewBag.allRoles = new SelectList(roleNames);

            return View(user);
        }

        // // GET: /ManageUser/AddRole/id
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddRoleAsync(string id, [Bind("Roles")] UserWithRoles model)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound($"Không có user");
            }

            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound($"Không thấy user, id = {id}.");
            }

            model.UserName = user.UserName;
            model.FullName = user.FullName;
            model.Id = user.Id;

            model.Roles ??= new List<string>();

            var OldRoleNames = (await _userManager.GetRolesAsync(user)).ToArray();

            var deleteRoles = OldRoleNames?.Where(r => !model.Roles.Contains(r));
            var addRoles = model.Roles?.Where(r => !OldRoleNames.Contains(r));

            var resultDelete = await _userManager.RemoveFromRolesAsync(user, deleteRoles);
            if (!resultDelete.Succeeded)
            {
                return View(model);
            }

            var resultAdd = await _userManager.AddToRolesAsync(user, addRoles);
            if (!resultAdd.Succeeded)
            {
                return View(model);
            }

            List<string> roleNames = await _roleManager.Roles.Select(r => r.Name).ToListAsync();
            ViewBag.allRoles = new SelectList(roleNames);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult CreateUser()
        {
            List<string> roleNames = _roleManager.Roles.Select(r => r.Name).ToList();
            ViewBag.allRoles = new SelectList(roleNames);
            return View();
        }
        //
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateUserAsync(CreateUserVM model)
        {
            if (ModelState.IsValid)
            {
                var user = new User { UserName = model.UserName, FullName = model.FullName };

                // Add user
                var result = await _userManager.CreateAsync(user, model.Password);

                // Add role
                var addRoleResult = await _userManager.AddToRolesAsync(user, model.Roles);

                return RedirectToAction(nameof(Index));
            }

            // error case -> back to previous page
            List<string> roleNames = await _roleManager.Roles.Select(r => r.Name).ToListAsync();
            ViewBag.allRoles = new SelectList(roleNames, model.Roles);

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteUserAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound("Không tìm thấy user");
            }

            await _userManager.DeleteAsync(user);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> ResetPasswordAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound($"Không có user");
            }

            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound($"Không thấy user, id = {id}.");
            }

            ViewBag.User = user;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPasswordAsync(string id, [Bind("NewPassword")] ResetPasswordVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
                return NotFound("Không tìm thấy user id = " + id);

            await _userManager.RemovePasswordAsync(user);
            await _userManager.AddPasswordAsync(user, model.NewPassword);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> LockOutUserAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
                return NotFound("Không tìm thấy user id = " + id);

            await _userManager.SetLockoutEnabledAsync(user, true);
            await _userManager.SetLockoutEndDateAsync(user, DateTimeOffset.MaxValue);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> UnlockUserAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
                return NotFound("Không tìm thấy user id = " + id);

            await _userManager.SetLockoutEnabledAsync(user, false);

            return RedirectToAction(nameof(Index));
        }
    }
}
