using Common.Cryptography;
using Common.GlobalData;
using Entities.Models;
using Filters.AuthenticationCore;
using Filters.AuthenticationModel;
using Repository.RepositoryFactoryCore;
using Repository.RepositoryModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Business
{
  public  class CustomerDetails
    {
        public CustomerDetailsRepository _customerdetailsRepository;
        private FormsAuthenticationFactory _formsAuthenticationFactory;
        private UnitOfWork _unitOfWork;
        public CustomerDetails(DatabaseFactory df = null, UnitOfWork uow = null)
        {
            DatabaseFactory dfactory = df == null ? new DatabaseFactory() : df;
            _unitOfWork = uow == null ? new UnitOfWork(dfactory) : uow;
            _customerdetailsRepository = new CustomerDetailsRepository(dfactory);
            this._formsAuthenticationFactory = new FormsAuthenticationFactory();
        }

        public CustomerDetail Insert(CustomerDetail Customer)
        {
            _customerdetailsRepository.Insert(Customer);
            return Customer;
        }

        public CustomerDetail Update(CustomerDetail Customer)
        {
            _customerdetailsRepository.Update(Customer);
            return Customer;
        }

        public void Delete(long? id)
        {
            _customerdetailsRepository.Delete(id);
        }

        public void Delete(CustomerDetail Customer)
        {
            _customerdetailsRepository.Delete(Customer);
        }

        public CustomerDetail Find(long? id)
        {
            return _customerdetailsRepository.Find(id);
        }
        public IEnumerable<CustomerDetail> GetAllCustomer()
        {
            return _customerdetailsRepository.Query().Select();
        }
        //public IEnumerable<Brand> GetAllBrandByRegion()
        //{
        //    return _brandRepository.Query().Select().Where(u => u.RegionID == GlobalUser.getGlobalUser().RegionID).ToList();
        //}
        public CustomerDetail GetCustomerById(int id)
        {
            return _customerdetailsRepository.Query(u => u.CustomerId == id).Select().FirstOrDefault();
        }
        public bool AddUpdateDeleteCustomer(CustomerDetail Customer, string action)
        {
            bool isSuccess = true;
            try
            {

                //brand.App_User = ReadConfigData.GetDBLoginUser();
                //brand.Audit_User = GlobalUser.getGlobalUser().UserName;
                //brand.RegionID = Convert.ToInt32(GlobalUser.getGlobalUser().RegionID);
                //brand.DisplayOrder = 1;
                //brand.VersionDataID = vid;
                //brand.VersionAction = action;
                if (action == "I")
                {
                    Insert(Customer);
                }
                else if (action == "U")
                {
                    Update(Customer);
                }
                else if (action == "D")
                {
                    Delete(Customer);
                }
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                isSuccess = false;
                throw ex;
            }
            return isSuccess;
        }

        public string ValidateCustomer(CustomerDetail Customerchk, string action)
        {
            string result = string.Empty;
            if (action == "I")
            {
                var custList = _customerdetailsRepository.Query(u => u.CustomerName == Customerchk.CustomerName).Select();
                if (custList.ToList().Count > 0)
                {
                    if (custList.Where(u => u.CustomerName == Customerchk.CustomerName).FirstOrDefault() != null)
                    {
                        result = "Customer Name already exists!";
                        return result;
                    }

                }
            }
            else if (action == "U")
            {
                var custList = _customerdetailsRepository.Query(u => u.CustomerName != Customerchk.CustomerName).Select();
                if (custList.ToList().Count > 0)
                {

                    return result;

                }
            }
            return result;
        }

    }
}
