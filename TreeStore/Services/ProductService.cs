using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TreeStore.Data.Interface;
using TreeStore.Data.Repositories;
using TreeStore.Models;

namespace TreeStore.Services
{
    public interface IProductService
    {
     
        IEnumerable<Product> GetProducts();
        List<Product> GetProducts(string User, long id);
        Product GetProduct(long id);
        void CreateProduct(Product Product);
        void UpdateProduct(Product Product);
        void DeleteProduct(long id);
        int CountProduct();
        void SaveProduct();
    }

    public class ProductService : IProductService
    {
        private readonly IProductRepository ProductRepository;
        private readonly IUnitOfWork unitOfWork;

        #region IProductService Members
        public ProductService(IProductRepository ProductRepository, IUnitOfWork unitOfWork)
        {
            this.ProductRepository = ProductRepository;
            this.unitOfWork = unitOfWork;
        }
        public int CountProduct()
        {
            return ProductRepository.GetAll().Count();
        }

        public void CreateProduct(Product Product)
        {
            ProductRepository.Add(Product);
        }

        public void DeleteProduct(long id)
        {
            ProductRepository.Delete(c => c.Id == id);
        }

        public List<Product> GetProducts(string User, long id)
        {
            return ProductRepository.GetMany(c => c.CreatedBy == User && c.Id == id).ToList();
        }

        public IEnumerable<Product> GetProducts()
        {
            var Products = ProductRepository.GetAll();
            return Products;

        }

        public Product GetProduct(long id)
        {
            var Product = ProductRepository.GetById(id);
            return Product;
        }

        public void SaveProduct()
        {
            unitOfWork.Commit();
        }


        public void UpdateProduct(Product Product)
        {
            ProductRepository.Update(Product);
        }
        #endregion
    }
}
