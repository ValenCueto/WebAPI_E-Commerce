using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using Application.Models;
using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces;

namespace Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public List<Product> GetAll()
        {
            return _productRepository.GetAll();
        }

        public Product? GetById(int id)
        {
            return _productRepository.GetById(id);
        }

        public Product Create(ProductToCreate productToCreate)
        {
            var existingProductName = _productRepository.GetByName(productToCreate.Name);
            if (existingProductName is not null)
            {
                throw new Exception("Ya existe un producto con ese nombre");
            }
            var product = new Product()
            {
                Name = productToCreate.Name,
                Price = productToCreate.Price,
                Stock = productToCreate.Stock,
                Description = productToCreate.Description,
            };


            return _productRepository.Create(product);
            
        }

        public void Delete(int id)
        {
            var product = _productRepository.GetById(id);
            _productRepository.Delete(product);
        }

        public void Update(ProductToUpdate productToUpdate, int id)
        {
            var product = _productRepository.GetById(id);
            product.Price = productToUpdate.Price;
            product.Stock = productToUpdate.Stock;
            _productRepository.Update(product);
        }
        public Product? GetByName(string name)
        {
            return _productRepository.GetByName(name);
        }
    }
}
