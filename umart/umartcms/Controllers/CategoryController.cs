using AutoMapper;
using Business;
using Common.Cryptography;
using Entities.Models;
using Repository.RepositoryFactoryCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using umartcms.Models;
namespace umartcms.Controllers
{
    public class CategoryController : Controller
    {
          private DatabaseFactory _df = new DatabaseFactory();
        private UnitOfWork _unitOfWork;
        private CategoryBusiness _categoryBusiness;

        public CategoryController()
        {
            this._unitOfWork = new UnitOfWork(_df);
            this._categoryBusiness = new CategoryBusiness(_df, this._unitOfWork);
          
        }
        //
        // GET: /Category/

        public ActionResult Index()
        {
            var categoryList = _categoryBusiness.GetAllCategory().ToList();
            List<CategoryViewModel> categoryViewModelList = new List<CategoryViewModel>();
            Mapper.CreateMap<Category, CategoryViewModel>()
                 .ForMember(dest => dest.EncId, opt => opt.MapFrom(src => Md5Encryption.Encrypt(src.CategoryId.ToString())));
            categoryViewModelList = Mapper.Map<List<Category>, List<CategoryViewModel>>(categoryList);
            return View(categoryList);  
        }

        public ActionResult Create()
        {
            CategoryViewModel categoryViewModel = new CategoryViewModel();

            return View(categoryViewModel);
        }

        //
        // POST: /Brand/Create
        [HttpPost]
        public ActionResult Create(CategoryViewModel categoryViewModel)
        {
            if (ModelState.IsValid)
            {
                Mapper.CreateMap<CategoryViewModel, Category>();
                Category category = Mapper.Map<CategoryViewModel, Category>(categoryViewModel);

                var result = _categoryBusiness.ValidateCategory(category, "I");

                if (!string.IsNullOrEmpty(result))
                {
                    ModelState.AddModelError("", result);
                    return View(categoryViewModel);
                }
                //int vid = _versionDataBusiness.GetUnpublishedVersion();
                //if (vid == 0)
                //{
                //    ModelState.AddModelError("", "New unpublished version not detected. Can not proceed. Create new version with out publishing it and try again.");
                //    TempData["Success"] = "New unpublished version not detected. Can not proceed. Create new version with out publishing it and try again.";
                //    TempData["isSuccess"] = "false";
                //    return RedirectToAction("Index");
                //}
                //else
                {
                    bool isSuccess = _categoryBusiness.AddUpdateDeleteCategory(category, "I");
                    if (isSuccess)
                    {
                        TempData["Success"] = "Category Created Successfully!!";
                        TempData["isSuccess"] = "true";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["Success"] = "Failed to create category!!";
                        TempData["isSuccess"] = "false";
                    }
                }
            }
            return View(categoryViewModel);

        }
        public ActionResult Edit(String Id)
        {
            int id = Convert.ToInt32(Md5Decryption.Decrypt(Id));
            var category = _categoryBusiness.GetCategoryById(id);

            Mapper.CreateMap<Category, CategoryViewModel>();
            CategoryViewModel categoryViewModel = Mapper.Map<Category, CategoryViewModel>(category);
            categoryViewModel.EncId = Id;


            return View(categoryViewModel);
            //  return View();
        }

        //
        // POST: /Brand/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CategoryViewModel categoryViewModel, String Id)
        {

            int id = Convert.ToInt32(Md5Decryption.Decrypt(Id));
            categoryViewModel.CategoryId = id;
            if (ModelState.IsValid)
            {
                Mapper.CreateMap<CategoryViewModel, Category>();
                Category brand = Mapper.Map<CategoryViewModel, Category>(categoryViewModel);

                var result = _categoryBusiness.ValidateCategory(brand, "U");
                if (!string.IsNullOrEmpty(result))
                {
                    ModelState.AddModelError("", result);
                    return View(categoryViewModel);
                }
                //int vid = _versionDataBusiness.GetUnpublishedVersion();
                //if (vid == 0)
                //{
                //    ModelState.AddModelError("", "New unpublished version not detected. Can not proceed. Create new version with out publishing it and try again.");
                //    TempData["Success"] = "New unpublished version not detected. Can not proceed. Create new version with out publishing it and try again.";
                //    TempData["isSuccess"] = "false";
                //    return RedirectToAction("Index");
                //}
                //else
                {

                    bool isSuccess = _categoryBusiness.AddUpdateDeleteCategory(brand, "U");
                    if (isSuccess)
                    {
                        TempData["Success"] = "Category Updated Successfully!!";
                        TempData["isSuccess"] = "true";

                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["Success"] = "Failed to update Category!!";
                        TempData["isSuccess"] = "false";
                    }
                }
            }
            return View(categoryViewModel);
        }

    }
}
