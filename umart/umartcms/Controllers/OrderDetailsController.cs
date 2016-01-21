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
    public class OrderDetailsController : Controller
    {
        //
        // GET: /OrderDetails/
          private DatabaseFactory _df = new DatabaseFactory();
        private UnitOfWork _unitOfWork;
        private OrderBusiness _orderBusiness;

        public OrderDetailsController()
        {
            this._unitOfWork = new UnitOfWork(_df);
            this._orderBusiness = new OrderBusiness(_df, this._unitOfWork);
          
        }
        public ActionResult Index()
        {
            return View();
        }

    }
}
