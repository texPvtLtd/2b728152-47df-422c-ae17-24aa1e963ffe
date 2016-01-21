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
    public class BrandController : Controller
    {
        // GET: /Brand/
        private DatabaseFactory _df = new DatabaseFactory();
        private UnitOfWork _unitOfWork;
        private BrandBusiness _brandBusiness;
      
        public BrandController()
        {
            this._unitOfWork = new UnitOfWork(_df);
            this._brandBusiness = new BrandBusiness(_df, this._unitOfWork);
          
        }
        //
        //
        // GET: /Brand/

        public ActionResult Index()
        {
            var BrandList = _brandBusiness.GetAllBrand().ToList();
            List<BrandViewModel> brandViewModelList = new List<BrandViewModel>();
            Mapper.CreateMap<Brand, BrandViewModel>()
                 .ForMember(dest => dest.EncId, opt => opt.MapFrom(src => Md5Encryption.Encrypt(src.BrandId.ToString())));
            brandViewModelList = Mapper.Map<List<Brand>, List<BrandViewModel>>(BrandList);
            return View(BrandList);           
        }
        public ActionResult Create()
        {
            BrandViewModel brandViewModel = new BrandViewModel();

            return View(brandViewModel);
        }

        //
        // POST: /Brand/Create
        [HttpPost]
        public ActionResult Create(BrandViewModel brandViewModel)
        {
            if (ModelState.IsValid)
            {
                Mapper.CreateMap<BrandViewModel, Brand>();
                Brand brand = Mapper.Map<BrandViewModel, Brand>(brandViewModel);

                var result = _brandBusiness.ValidateBrand(brand, "I");

                if (!string.IsNullOrEmpty(result))
                {
                    ModelState.AddModelError("", result);
                    return View(brandViewModel);
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
                    brand.EncId = Md5Encryption.Encrypt(brand.BrandId.ToString());
                    bool isSuccess = _brandBusiness.AddUpdateDeleteBrand(brand, "I");
                    if (isSuccess)
                    {
                        TempData["Success"] = "Brand Created Successfully!!";
                        TempData["isSuccess"] = "true";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["Success"] = "Failed to create Brand!!";
                        TempData["isSuccess"] = "false";
                    }
                }
            }
            return View(brandViewModel);

        }
        public ActionResult Edit(String Id)
        {
            int id = Convert.ToInt32(Md5Decryption.Decrypt(Id));
            var brand = _brandBusiness.GetBrandById(id);

            Mapper.CreateMap<Brand, BrandViewModel>();
            BrandViewModel brandViewModel = Mapper.Map<Brand, BrandViewModel>(brand);
            brandViewModel.EncId = Id;


            return View(brandViewModel);
            //  return View();
        }

        //
        // POST: /Brand/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(BrandViewModel brandViewModel, String Id)
        {

            int id = Convert.ToInt32(Md5Decryption.Decrypt(Id));
            brandViewModel.BrandId = id;
            if (ModelState.IsValid)
            {
                Mapper.CreateMap<BrandViewModel, Brand>();
                Brand brand = Mapper.Map<BrandViewModel, Brand>(brandViewModel);

                var result = _brandBusiness.ValidateBrand(brand, "U");
                if (!string.IsNullOrEmpty(result))
                {
                    ModelState.AddModelError("", result);
                    return View(brandViewModel);
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

                    bool isSuccess = _brandBusiness.AddUpdateDeleteBrand(brand, "U");
                    if (isSuccess)
                    {
                        TempData["Success"] = "Brand Updated Successfully!!";
                        TempData["isSuccess"] = "true";

                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["Success"] = "Failed to update Brand!!";
                        TempData["isSuccess"] = "false";
                    }
                }
            }
            return View(brandViewModel);
        }

    }
}
