using Book_Store.Data;
using Book_Store.Models.Domains;
using Book_Store.Models.ViewModels;
using Book_Store.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Book_Store.Controllers
{
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class AdminTagController : Controller
    {
        private readonly ITagRepository _tagRepository;
        public AdminTagController(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }

        [HttpGet]
        public IActionResult Tag()
        {
            TagVM vm = new TagVM();
            vm.Tag = new TagModel();
            vm.TagList = new List<TagModel>();

            vm.TagList = SelectAllTags();
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Tag(TagVM tagVM) 
        {
            if (tagVM.TagRequest.Action == RequestAction.Add)
                await AddTag(tagVM.TagRequest);
            else
                await EditTag(tagVM.TagRequest);
            return RedirectToAction("Tag");
        }

        private async Task<TagModel> AddTag(TagRequest newTag) 
        {
            var tag = new TagModel{
                Name = newTag.Name,
                DisplayName = newTag.DisplayName
            };
            return await _tagRepository.AddAsync(tag);
        }

        private async Task<TagModel> EditTag(TagRequest modifiedTag) 
        {
            var tag = new TagModel {
                ID = modifiedTag.ID,
                Name = modifiedTag.Name,
                DisplayName = modifiedTag.DisplayName
            };
            if (modifiedTag.Action == RequestAction.Delete)
                return await _tagRepository.DeleteAsync(tag);
            else
                return await _tagRepository.UpdateAsync(tag);
        }

        private List<TagModel> SelectAllTags() 
        {
            var tags = _tagRepository.GetAll().ToList();
            return tags;
        }
    }
}
