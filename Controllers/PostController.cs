using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using HivePSTL.Models;
using HivePSTL.Services;

namespace HivePSTL.Controllers
{
    [Authorize]
    public class PostController : Controller
    {
        private readonly PostService _postSvc;

        public PostController (PostService postService)
        {
            _postSvc = postService;
        }

        [AllowAnonymous]
        public ActionResult<IList<Post>> Index() => View(_postSvc.Read());

        [HttpGet]
        public ActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult<Post> Create (Post post)
        {
            post.Created = post.LastUpdated = DateTime.Now;
            post.UserId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            post.UserName = User.Identity.Name;
            if(ModelState.IsValid)
            {
                _postSvc.Create(post);
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult<Post> Edit(string id) => View(_postSvc.Find(id));

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit (Post post)
        {
            post.LastUpdated = DateTime.Now;
            post.Created = post.Created.ToLocalTime();
            if(ModelState.IsValid)
            {
                if(User.Claims.FirstOrDefault(c=>c.Type == ClaimTypes.NameIdentifier).Value != post.UserId)
                {
                    return Unauthorized();
                }
                _postSvc.Update(post);
                return RedirectToAction("Index");
            }
            return View(post);
        }

        [HttpGet]
        public ActionResult Delete (string id)
        {
            _postSvc.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
