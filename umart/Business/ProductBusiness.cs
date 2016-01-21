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
  public class ProductBusiness
    {
           public ProductRepository _productRepository;
        private FormsAuthenticationFactory _formsAuthenticationFactory;
        private UnitOfWork _unitOfWork;
        public ProductBusiness(DatabaseFactory df = null, UnitOfWork uow = null)
        {
            DatabaseFactory dfactory = df == null ? new DatabaseFactory() : df;
            _unitOfWork = uow == null ? new UnitOfWork(dfactory) : uow;
            _productRepository = new ProductRepository(dfactory);
            this._formsAuthenticationFactory = new FormsAuthenticationFactory();
        }

        public Product Insert(Product Product)
        {
            _productRepository.Insert(Product);
            return Product;
        }

        public Product Update(Product Product)
        {
            _productRepository.Update(Product);
            return Product;
        }

        public void Delete(long? id)
        {
            _productRepository.Delete(id);
        }

        public void Delete(Product Product)
        {
            _productRepository.Delete(Product);
        }

        public Product Find(long? id)
        {
            return _productRepository.Find(id);
        }
        public IEnumerable<Product> GetAllProduct()
        {
            return _productRepository.Query().Select();
        }
        //public IEnumerable<Brand> GetAllBrandByRegion()
        //{
        //    return _brandRepository.Query().Select().Where(u => u.RegionID == GlobalUser.getGlobalUser().RegionID).ToList();
        //}
        public Product GetProductById(int id)
        {
            return _productRepository.Query(u => u.ProductID == id).Select().FirstOrDefault();
        }
        public bool AddUpdateDeleteProduct(Product product, string action)
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
                    Insert(product);
                }
                else if (action == "U")
                {
                    Update(product);
                }
                else if (action == "D")
                {
                    Delete(product);
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

        public string ValidateProduct(Product productchk, string action)
        {
            string result = string.Empty;
            if (action == "I")
            {
                var productList = _productRepository.Query(u => u.ProductName == productchk.ProductName).Select();
                if (productList.ToList().Count > 0)
                {
                    if (productList.Where(u => u.ProductName == productchk.ProductName).FirstOrDefault() != null)
                    {
                        result = "product Name already exists!";
                        return result;
                    }

                }
            }
            else if (action == "U")
            {
                var productList = _productRepository.Query(u => u.ProductID != productchk.ProductID && (u.ProductName == productchk.ProductName)).Select();
                if (productList.ToList().Count > 0)
                {

                    return result;

                }
            }
            return result;
        }
    }
}
