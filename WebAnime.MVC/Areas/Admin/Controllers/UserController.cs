using AutoMapper;
using DataModels.EF.Identity;
using DataModels.Helpers;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using ViewModels.Admin;
using Newtonsoft.Json;

namespace WebAnime.MVC.Areas.Admin.Controllers
{
    //[OnlyAdminAuthorize]
    public class UserController(UserManager userManager, IMapper mapper, RoleManager roleManager)
        : Controller
    {
        public async Task<ActionResult> Index()
        {
            var users = userManager.Users.Where(x => !x.IsDeleted);
            var usersViewModel = mapper.Map<IQueryable<Users>, IEnumerable<UserViewModel>>(users);
            return await Task.FromResult(View(usersViewModel));
        }
        [HttpGet]
        public async Task<ActionResult> Create()
        {
            var roleList = roleManager.Roles;
            ViewBag.Roles = roleList;

            return await Task.FromResult(View());
        }

        [HttpPost]
        public async Task<ActionResult> Create(UserViewModel model)
        {
            var roleList = roleManager.Roles.ToList();
            ViewBag.Roles = roleList;

            if (ModelState.IsValid)
            {
                if (!model.Password.Equals(model.ReTypePassword))
                {
                    ModelState.AddModelError("ErrorConfirmPassword", @"Mật khẩu xác nhận không đúng, hãy thử lại");
                    return View(model);
                }

                var existUsername = await userManager.FindByNameAsync(model.UserName);
                if (existUsername != null)
                {
                    ModelState.AddModelError("ExistUsername", @"Tài khoản đã tồn tại");
                    return View(model);
                }

                var existEmail = await userManager.FindByEmailAsync(model.Email);
                if (existEmail != null)
                {
                    ModelState.AddModelError("ExistEmail", @"Địa chỉ đã tồn tại");
                    return View(model);
                }


                var user = mapper.Map<Users>(model);

                var insertRoleList = roleList.Where(x => model.RoleListIds.Contains(x.Id)).Select(x => x.Name).ToArray();

                user.CreatedBy = User.Identity.GetUserId<int>();

                user.CreatedDate = DateTime.Now;

                user.EmailConfirmed = true;
                user.PhoneNumberConfirmed = true;

                IdentityResult result = await userManager.CreateAsync(user, model.Password);
                int x;
                if (result.Succeeded)
                {
                    IdentityResult roleResult = await userManager.AddToRolesAsync(user.Id, insertRoleList);

                    if (!roleResult.Succeeded)
                    {
                        x = 0;
                        foreach (var error in roleResult.Errors)
                        {
                            ModelState.AddModelError($"Error_{++x}", error);
                            return View(model);
                        }
                    }
                    return RedirectToAction("Index");
                }

                x = 0;
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError($"Error_{++x}", error);
                }

                return View(model);
            }
            ModelState.AddModelError("TotalError", @"Lỗi dữ liệu đầu vào, hãy kiểm tra lại");
            return View(model);
        }
        [HttpGet]
        public async Task<ActionResult> Update(int id)
        {
            var roleList = roleManager.Roles.ToList();
            ViewBag.Roles = roleList;
            var user = await userManager.FindByIdAsync(id);
            if (user == null)
            {
                return new HttpNotFoundResult("Cannot find user");
            }

            var usersViewModel = mapper.Map<Users, UserViewModel>(user);
            usersViewModel.Password = usersViewModel.ReTypePassword = "abc";//fake
            return await Task.FromResult(View(usersViewModel));
        }

        [HttpPost]
        public async Task<ActionResult> Update(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var roleList = roleManager.Roles.ToArray();
                ViewBag.Roles = roleList;
                var user = await userManager.FindByIdAsync(model.Id);
                if (user == null)
                {
                    return new HttpNotFoundResult("Cannot find user");
                }
                user.BirthDay = model.BirthDay;
                user.UserName = model.UserName;
                user.Email = model.Email;
                user.PhoneNumber = model.PhoneNumber;
                user.FullName = model.FullName;
                user.AvatarUrl = model.AvatarUrl;

                var oldRoleIds = roleManager.GetRoleIdsFromUser(userManager, user.Id).ToArray();
                var newRoleIds = model.RoleListIds ?? Array.Empty<int>();
                var removeUserRoleIds = oldRoleIds.Except(newRoleIds);
                var insertUserRoleIds = newRoleIds.Except(oldRoleIds);

                var roleListIds = roleList.Select(x => x.Id).ToArray();

                int countError = 0;
                foreach (var removeRoleId in removeUserRoleIds)
                {
                    if (roleListIds.Contains(removeRoleId))
                    {
                        var removeRole = roleList.FirstOrDefault(x => x.Id == removeRoleId);
                        if (removeRole != null)
                        {
                            IdentityResult removeResult = await userManager.RemoveFromRoleAsync(user.Id, removeRole.Name);
                            if (!removeResult.Succeeded)
                            {
                                foreach (var removeResultError in removeResult.Errors)
                                {
                                    countError++;
                                    ModelState.AddModelError(removeResultError, removeResultError);
                                }
                            }
                        }
                    }
                }
                foreach (var insertRoleId in insertUserRoleIds)
                {
                    if (roleListIds.Contains(insertRoleId))
                    {
                        var insertRole = roleList.FirstOrDefault(x => x.Id == insertRoleId);

                        if (insertRole != null)
                        {
                            var insertRoleResult = await userManager.AddToRoleAsync(user.Id, insertRole.Name);
                            if (!insertRoleResult.Succeeded)
                            {
                                foreach (var insertResultError in insertRoleResult.Errors)
                                {
                                    countError++;
                                    ModelState.AddModelError(insertResultError, insertResultError);
                                }
                            }
                        }
                    }
                }

                user.ModifiedBy = User.Identity.GetUserId<int>();
                user.ModifiedDate = DateTime.Now;

                IdentityResult updateUserResult = await userManager.UpdateAsync(user);

                foreach (var userError in updateUserResult.Errors)
                {
                    countError++;
                    ModelState.AddModelError(userError, userError);
                }
                if (countError > 0)
                {
                    return View(model);
                }

                if (updateUserResult.Succeeded)
                {
                    return RedirectToAction("Index");

                }
            }
            return View(model);
        }
        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            var user = await userManager.FindByIdAsync(id);
            if (user == null)
            {
                return new HttpNotFoundResult("Cannot find user");
            }

            var usersViewModel = mapper.Map<Users, UserViewModel>(user);
            return await Task.FromResult(View(usersViewModel));
        }

        [HttpPost]
        public async Task<ActionResult> Delete(UserViewModel model)
        {
            var user = await userManager.FindByIdAsync(model.Id);
            if (user == null)
            {
                return new HttpNotFoundResult("Cannot find user");
            }

            user.IsDeleted = true;
            user.DeletedBy = User.Identity.GetUserId<int>();

            IdentityResult result = await userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error);
                }
                return View(model);
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<ActionResult> GetPaging(string textSearch, int pageNumber, int pageSize,
            string selectedPropertyName)
        {
            var result = Enumerable.Empty<Users>().AsQueryable();

            switch (selectedPropertyName)
            {
                case "UserName": 
                    result = userManager.Users
                    .Where(x => (!x.IsDeleted) &&
                                (x.UserName.Contains(textSearch) || String.IsNullOrEmpty(textSearch)));
                    break;
                case "FullName":
                    result = userManager.Users
                    .Where(x => (!x.IsDeleted) &&
                                (x.FullName.Contains(textSearch) || String.IsNullOrEmpty(textSearch)));
                    break;
                case "PhoneNumber":
                    result = userManager.Users
                    .Where(x => (!x.IsDeleted) &&
                                (x.PhoneNumber.Contains(textSearch) || String.IsNullOrEmpty(textSearch)));
                    break;
                case "Email":
                    result = userManager.Users
                    .Where(x => (!x.IsDeleted) &&
                                (x.Email.Contains(textSearch) || String.IsNullOrEmpty(textSearch)));
                    break;
            }
            var resultCount = await result.CountAsync();

            var searchResult = (await result
                .OrderBy(x => x.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync())
                .Select(x => new
                {
                    x.Id,
                    x.UserName,
                    x.FullName,
                    x.PhoneNumber,
                    BirthDay = x.BirthDay?.ToString("dd/MM/yyyy"),
                    x.Email,
                    x.AvatarUrl,
                    RoleList = x.Roles.Select(t => new
                    {
                        t.Roles.Name,
                        t.Roles.Id
                    })
                });


            return Json(JsonConvert.SerializeObject(new
            {
                data = searchResult,
                totalPage= Math.Ceiling((resultCount*1.0)/pageSize),
            }), JsonRequestBehavior.AllowGet);
        }
    }
}