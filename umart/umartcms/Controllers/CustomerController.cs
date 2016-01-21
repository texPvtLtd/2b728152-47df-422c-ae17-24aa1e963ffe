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

namespace umartcms.Models
{
    public class CustomerController : Controller
    {
      
        private DatabaseFactory _df = new DatabaseFactory();
        private UnitOfWork _unitOfWork;
        private CustomerDetails _customerBusiness;
        //
        // GET: /Product/

        public CustomerController()
        {
            this._unitOfWork = new UnitOfWork(_df);
            this._customerBusiness = new CustomerDetails(_df, this._unitOfWork);
          
        }
        //
        // GET: /Customer/

        public ActionResult Index()
        {
            var custList = _customerBusiness.GetAllCustomer().ToList();
            List<CustomerViewModel> productViewModelList = new List<CustomerViewModel>();
            Mapper.CreateMap<CustomerDetail, CustomerViewModel>()
                 .ForMember(dest => dest.Encid, opt => opt.MapFrom(src => Md5Encryption.Encrypt(src.CustomerId.ToString())));
            productViewModelList = Mapper.Map<List<CustomerDetail>, List<CustomerViewModel>>(custList);
            return View(custList);  
        }

    }
}
