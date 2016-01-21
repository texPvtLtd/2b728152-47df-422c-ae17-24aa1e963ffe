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
    public class AddtoCartController : Controller
    {
      
        // GET: /Brand/
        private DatabaseFactory _df = new DatabaseFactory();
        private UnitOfWork _unitOfWork;
        private AddToCartBusiness _cartBusiness;

        public AddtoCartController()
        {
            this._unitOfWork = new UnitOfWork(_df);
            this._cartBusiness = new AddToCartBusiness(_df, this._unitOfWork);
          
        }
        //
        //
        // GET: /Brand/

        public ActionResult Index()
        {
            var CartList = _cartBusiness.GetAllCart().ToList();
            List<AddtoCartViewModel> brandViewModelList = new List<AddtoCartViewModel>();
            Mapper.CreateMap<AddToCart, AddtoCartViewModel>()
                 .ForMember(dest => dest.encid, opt => opt.MapFrom(src => Md5Encryption.Encrypt(src.ID.ToString())));
            brandViewModelList = Mapper.Map<List<AddToCart>, List<AddtoCartViewModel>>(CartList);
            return View(CartList);           
        }

    }
}
