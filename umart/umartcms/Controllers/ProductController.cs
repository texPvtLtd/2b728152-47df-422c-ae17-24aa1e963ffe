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
    public class ProductController : Controller
    {
        private DatabaseFactory _df = new DatabaseFactory();
        private UnitOfWork _unitOfWork;
        private ProductBusiness _productBusiness;
        //
        // GET: /Product/

        public ProductController()
        {
            this._unitOfWork = new UnitOfWork(_df);
            this._productBusiness = new ProductBusiness(_df, this._unitOfWork);
          
        }
        //
        // GET: /Category/

        public ActionResult Index()
        {
            var productList = _productBusiness.GetAllProduct().ToList();
            List<ProductViewModel> productViewModelList = new List<ProductViewModel>();
            Mapper.CreateMap<Product, ProductViewModel>()
                 .ForMember(dest => dest.EncId, opt => opt.MapFrom(src => Md5Encryption.Encrypt(src.CategoryId.ToString())));
            productViewModelList = Mapper.Map<List<Product>, List<ProductViewModel>>(productList);
            return View(productList);  
        }
        public ActionResult Create()
        {
            ProductViewModel productViewModel = new ProductViewModel();

            return View(productViewModel);
        }

        //
        // POST: /Brand/Create
        [HttpPost]
        public ActionResult Create(ProductViewModel productViewModel)
        {
            if (ModelState.IsValid)
            {
                Mapper.CreateMap<ProductViewModel, Product>();
                Product product = Mapper.Map<ProductViewModel, Product>(productViewModel);

                var result = _productBusiness.ValidateProduct(product, "I");

                if (!string.IsNullOrEmpty(result))
                {
                    ModelState.AddModelError("", result);
                    return View(productViewModel);
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
                    product.EncId = Md5Encryption.Encrypt(product.ProductID.ToString());
                    bool isSuccess = _productBusiness.AddUpdateDeleteProduct(product, "I");
                    if (isSuccess)
                    {
                        TempData["Success"] = "product Created Successfully!!";
                        TempData["isSuccess"] = "true";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["Success"] = "Failed to create product!!";
                        TempData["isSuccess"] = "false";
                    }
                }
            }
            return View(productViewModel);

        }
        public ActionResult Edit(String Id)
        {
            int id = Convert.ToInt32(Md5Decryption.Decrypt(Id));
            var product = _productBusiness.GetProductById(id);

            Mapper.CreateMap<Product, ProductViewModel>();
            ProductViewModel brandViewModel = Mapper.Map<Product, ProductViewModel>(product);
            brandViewModel.EncId = Id;


            return View(brandViewModel);
            //  return View();
        }

        //
        // POST: /Brand/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProductViewModel productViewModel, String Id)
        {

            int id = Convert.ToInt32(Md5Decryption.Decrypt(Id));
            productViewModel.ProductID = id;
            if (ModelState.IsValid)
            {
                Mapper.CreateMap<ProductViewModel, Product>();
                Product product = Mapper.Map<ProductViewModel, Product>(productViewModel);

                var result = _productBusiness.ValidateProduct(product, "U");
                if (!string.IsNullOrEmpty(result))
                {
                    ModelState.AddModelError("", result);
                    return View(productViewModel);
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

                    bool isSuccess = _productBusiness.AddUpdateDeleteProduct(product, "U");
                    if (isSuccess)
                    {
                        TempData["Success"] = "Product Updated Successfully!!";
                        TempData["isSuccess"] = "true";

                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["Success"] = "Failed to update Product!!";
                        TempData["isSuccess"] = "false";
                    }
                }
            }
            return View(productViewModel);
        }


    }
}
