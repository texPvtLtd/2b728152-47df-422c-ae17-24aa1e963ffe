using AutoMapper;
using Common.Cryptography;
using Entities.Models;
using Filters.ActionFilters;
using umartcms.Models;
using Repository.RepositoryFactoryCore;
using Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Filters.AuthenticationModel;
using System.Threading.Tasks;

namespace umartcms.Controllers
{
    public class UserController : Controller
    {
        private DatabaseFactory _df = new DatabaseFactory();
        private UnitOfWork _unitOfWork;
        private UserBusiness _userBusiness;
       
        public UserController()
        {
            this._unitOfWork = new UnitOfWork(_df);
            this._userBusiness = new UserBusiness(_df, this._unitOfWork);
           
        }
        //
        //
        // GET: /User/

        public ActionResult Login()
        {
            return View();
        }


        public ActionResult Login(User model)
        {
            return View();
        }

       
        public ActionResult Index()
        {
            var UserList = _userBusiness.GetAllUsers().ToList();
            List<UserViewModel> userViewModelList = new List<UserViewModel>();
            Mapper.CreateMap<User, UserViewModel>()
                 .ForMember(dest => dest.EncId, opt => opt.MapFrom(src => Md5Encryption.Encrypt(src.UserId.ToString())));
            userViewModelList = Mapper.Map<List<User>, List<UserViewModel>>(UserList);
            return View(userViewModelList);
        }

        public ActionResult Create()
        {
            UserViewModel userViewModel = new UserViewModel();
          //  ViewBag.gridfilters = gridfilters;
            // userViewModel.userTypeList = _userBusiness.GetUserType();
            //if (Filters.AuthenticationModel.GlobalUser.getGlobalUser().UserType != "Admin")
            //{
            //    userViewModel.RegionList = _regionBusiness.GetAllRegion().Select(x => new SelectListItem
            //    {
            //        Text = x.Name.ToString(),
            //        Value = x.RegionID.ToString()
            //    }).ToList();
            //}
            ////else
            //{
            //    var user = _userBusiness.GetUserById(Convert.ToInt32(GlobalUser.getGlobalUser().UserId));


          
            //}
            return View(userViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UserViewModel userViewModel)
        {
          
            if (ModelState.IsValid)
            {
                
                Mapper.CreateMap<UserViewModel, User>();
                User user = Mapper.Map<UserViewModel, User>(userViewModel);

                var result = _userBusiness.ValidateUser(user, "I");
                if (!string.IsNullOrEmpty(result))
                {
                    ModelState.AddModelError("Alert", result);
                    return View(userViewModel);
                }

                else
                {
                    //int vid = 0;
                    bool isSuccess = _userBusiness.AddUpdateDeleteUser(user, "I");
                    if (isSuccess)
                    {
                            TempData["Success"] = "User Created Successfully!!";
                            TempData["isSuccess"] = "true";
                            return RedirectToAction("Index");
                        
                    }
                    else
                    {
                       
                            TempData["Success"] = "Failed to create User!!";
                            TempData["isSuccess"] = "false";
                        
                    }
                }
            }
            
            return View(userViewModel);
        }

        // GET: /Event/Edit/5
        public ActionResult Edit( string gridfilters = "")
        {
            int Id = 1;
           // int id = Convert.ToInt32(Md5Decryption.Decrypt(Id));
            var user = _userBusiness.GetUserById(Id);
         
            Mapper.CreateMap<User, UserViewModel>();
            UserViewModel userViewModel = Mapper.Map<User, UserViewModel>(user);
            //userViewModel.EncId = Id;


            ViewBag.gridfilters = gridfilters;
            return View(userViewModel);
            //  return View();
        }

        //
        // POST: /Event/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UserViewModel userViewModel)
        {
            //ViewBag.gridfilters = gridfilters;

          //  int id = Convert.ToInt32(Md5Decryption.Decrypt(Id));
           // userViewModel.UserId = id;
            if (ModelState.IsValid)
            {
                Mapper.CreateMap<UserViewModel, User>();
                User user = Mapper.Map<UserViewModel, User>(userViewModel);

                var result = _userBusiness.ValidateUser(user, "U");
                if (!string.IsNullOrEmpty(result))
                {
                    ModelState.AddModelError("Alert", result);
                    return View(userViewModel);
                }
                //int vid = _versionDataBusiness.GetUnpublishedVersion();
                //if (vid == 0)
                //{
                //    ModelState.AddModelError("Alert", "New unpublished version not detected. Can not proceed. Create new version with out publishing it and try again.");
                //    TempData["Success"] = "New unpublished version not detected. Can not proceed. Create new version with out publishing it and try again.";
                //    TempData["isSuccess"] = "false";
                //    return RedirectToAction("Index", new { gridfilters = gridfilters });
                //}
                //else
                //{
                    bool isSuccess = _userBusiness.AddUpdateDeleteUser(user, "U");
                    if (isSuccess)
                    {
                        TempData["Success"] = "User Updated Successfully!!";
                        TempData["isSuccess"] = "true";

                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["Success"] = "Failed to update user!!";
                        TempData["isSuccess"] = "false";
                    }
                //}
            }
            return View(userViewModel);
        }


    }
}
