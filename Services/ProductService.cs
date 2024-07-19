using OfficeOpenXml;
using Repositories;
using Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ProductService
    {
        private ProductRepository _repo = new();
        public List<Product> GetAllProducts()
        {
            return _repo.GetProducts();
        }

        public void CreateProduct(Product product)
        {
            _repo.Create(product);
        }
        public void DeleteProduct(Product product)
        {
            _repo.Delete(product);
        }
        public List<Product> SearchProduct(String productName, int? supplierId, int? maxPrice, int? minPrice, Boolean checkIsInStock)
        {
            List<Product> productList = _repo.GetProducts();
            productList = productList.Where(x => x.ProductName.Contains(productName)).ToList();
            if (supplierId != null)
            {
                productList = productList.Where(x => x.SupplierId.Equals(supplierId)).ToList();
            }
            if (maxPrice != null)
            {
                productList = productList.Where(x => x.Price <= maxPrice).ToList();
            }
            if (minPrice != null)
            {
                productList = productList.Where(x => x.Price >= minPrice).ToList();
            }
            if (checkIsInStock)
            {
                productList = productList.Where(x => x.Quantity > 0).ToList();
            }
            return productList;
        }

        public void ImportProducts(string excelFilePath)
        {
            FileInfo fileInfo = new FileInfo(excelFilePath);
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (ExcelPackage package = new ExcelPackage(fileInfo))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                int rowCount = worksheet.Dimension.Rows;

                for (int row = 2; row <= rowCount; row++)
                {
                    string productName = worksheet.Cells[row, 1].Text.Trim();
                    int supplierId = int.Parse(worksheet.Cells[row, 2].Text.Trim());
                    int categoryId = int.Parse(worksheet.Cells[row, 3].Text.Trim());
                    int price = int.Parse(worksheet.Cells[row, 4].Text.Trim());
                    int quantity = int.Parse(worksheet.Cells[row, 5].Text.Trim());
                    int yearOfManufacture = int.Parse(worksheet.Cells[row, 6].Text.Trim());
                    string warranty = worksheet.Cells[row, 7].Text.Trim();

                    var existingProduct = _repo.GetByName(productName);

                    if (existingProduct != null)
                    {
                        existingProduct.Quantity += quantity;
                        _repo.Update(existingProduct);
                    }
                    else
                    {
                        Product newProduct = new Product
                        {
                            ProductName = productName,
                            SupplierId = supplierId,
                            CategoryId = categoryId,
                            Price = price,
                            Quantity = quantity,
                            YearOfManufacture = yearOfManufacture,
                            Warranty = warranty
                        };
                        _repo.Create(newProduct);
                    }
                }
            }

        }
    }
}
