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
    public class webController : Controller
    {
        private DatabaseFactory _df = new DatabaseFactory();
        private UnitOfWork _unitOfWork;
        private CategoryBusiness _categoryBusiness;
        private BrandBusiness _brandBusiness;
        private ProductBusiness _productBusiness;

        public webController()
        {
            this._unitOfWork = new UnitOfWork(_df);
            this._categoryBusiness = new CategoryBusiness(_df, this._unitOfWork);
          
        }
        public ActionResult Index()
        {
            return View();
        }
         
        public ActionResult DispalyCategory()
        {

            CategoryViewModel cData = new CategoryViewModel();
            var list = _categoryBusiness.GetAllCategory().ToList();
            return PartialView("_categoryList", list);
        }

    }
}
