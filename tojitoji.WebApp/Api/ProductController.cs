using AutoMapper;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Script.Serialization;
using tojitoji.Common;
using tojitoji.Model.Models;
using tojitoji.Service;
using tojitoji.WebApp.Infrastructure.Core;
using tojitoji.WebApp.Infrastructure.Extensions;
using tojitoji.WebApp.Models;

namespace tojitoji.WebApp.Api
{
    [RoutePrefix("api/product")]
    public class ProductController : ApiControllerBase
    {
        #region Initialize

        private IProductService _productService;

        public ProductController(IErrorService errorService, IProductService productService)
            : base(errorService)
        {
            this._productService = productService;
        }

        #endregion Initialize

        [Route("getallproduct")]
        [HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                var model = _productService.GetAll();
                var responseData = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(model);
                var response = request.CreateResponse(HttpStatusCode.OK, responseData);
                return response;
            });
        }

        [Route("getall")]
        [HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage request, string keyword, int page, int pageSize = 20)
        {
            return CreateHttpResponse(request, () =>
            {
                int totalRow = 0;
                var model = _productService.GetAll(keyword);

                totalRow = model.Count();
                var query = model.OrderByDescending(x => x.ID).Skip(page * pageSize).Take(pageSize);

                var responseData = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(query);

                var paginationSet = new PaginationSet<ProductViewModel>()
                {
                    Items = responseData,
                    Page = page,
                    TotalCount = totalRow,
                    TotalPages = (int)Math.Ceiling((decimal)totalRow / pageSize)
                };
                var response = request.CreateResponse(HttpStatusCode.OK, paginationSet);
                return response;
            });
        }

        [Route("getbyid/{id}")]
        [HttpGet]
        public HttpResponseMessage GetById(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                var model = _productService.GetDetail(id);
                var responseData = Mapper.Map<Product, ProductViewModel>(model);
                var response = request.CreateResponse(HttpStatusCode.OK, responseData);
                return response;
            });
        }

        [Route("create")]
        [HttpPost]
        public HttpResponseMessage Create(HttpRequestMessage request, ProductViewModel productVM)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (!ModelState.IsValid)
                {
                    response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    var newProduct = new Product();
                    newProduct.UpdateProduct(productVM);
                    _productService.Add(newProduct);
                    _productService.SaveChanges();

                    var responseData = Mapper.Map<Product, ProductViewModel>(newProduct);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }

                return response;
            });
        }

        [Route("update")]
        [HttpPut]
        public HttpResponseMessage Update(HttpRequestMessage request, ProductViewModel productVM)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (!ModelState.IsValid)
                {
                    response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    var dbProduct = _productService.GetById(productVM.ID);

                    dbProduct.UpdateProduct(productVM);

                    _productService.Update(dbProduct);
                    _productService.SaveChanges();

                    var responseData = Mapper.Map<Product, ProductViewModel>(dbProduct);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }

                return response;
            });
        }

        [Route("deletemulti")]
        [HttpDelete]
        public HttpResponseMessage DeleteMulti(HttpRequestMessage request, string checkedProducts)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (!ModelState.IsValid)
                {
                    response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    var listProduct = new JavaScriptSerializer().Deserialize<List<int>>(checkedProducts);
                    foreach (var item in listProduct)
                    {
                        _productService.Delete(item);
                    }

                    _productService.SaveChanges();

                    response = request.CreateResponse(HttpStatusCode.OK, listProduct.Count);
                }

                return response;
            });
        }

        [Route("import")]
        [HttpPost]
        public async Task<HttpResponseMessage> Import()
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                Request.CreateErrorResponse(HttpStatusCode.UnsupportedMediaType, "Định dạng không được server hỗ trợ");
            }

            var root = HttpContext.Current.Server.MapPath("~/UploadedFiles/Excels");
            if (!Directory.Exists(root))
            {
                Directory.CreateDirectory(root);
            }

            var provider = new MultipartFormDataStreamProvider(root);
            var result = await Request.Content.ReadAsMultipartAsync(provider);

            int addedCount = 0;

            foreach (MultipartFileData fileData in result.FileData)
            {
                if (string.IsNullOrEmpty(fileData.Headers.ContentDisposition.FileName))
                {
                    return Request.CreateResponse(HttpStatusCode.NotAcceptable, "Yêu cầu không đúng định dạng");
                }
                string fileName = fileData.Headers.ContentDisposition.FileName;
                if (fileName.StartsWith("\"") && fileName.EndsWith("\""))
                {
                    fileName = fileName.Trim('"');
                }
                if (fileName.Contains(@"/") || fileName.Contains(@"\"))
                {
                    fileName = Path.GetFileName(fileName);
                }

                var fullPath = Path.Combine(root, fileName);
                File.Copy(fileData.LocalFileName, fullPath, true);

                //insert to DB
                var listProduct = this.ReadProductFromExcel(fullPath);
                if (listProduct.Count > 0)
                {
                    foreach (var Product in listProduct)
                    {
                        _productService.Add(Product);
                        addedCount++;
                    }
                    _productService.SaveChanges();
                }
            }
            return Request.CreateResponse(HttpStatusCode.OK, "Đã nhập thành công " + addedCount + " sản phẩm.");
        }

        private List<Product> ReadProductFromExcel(string fullPath)
        {
            using (var package = new ExcelPackage(new FileInfo(fullPath)))
            {
                ExcelWorksheet workSheet = package.Workbook.Worksheets[1];
                List<Product> listProduct = new List<Product>();
                ProductViewModel productViewModel;
                Product product;

                decimal RRP;
                decimal SP;
                DateTime SpecialFromTime;
                DateTime SpecialToTime;
                bool Status;
                int ProductLifeTime;
                int Warranty;
                int PackageWeight;
                int PackageLength;
                int PackageWidth;
                int PackageHeight;
                int CategoryID;

                for (int i = workSheet.Dimension.Start.Row + 1; i <= workSheet.Dimension.End.Row; i++)
                {
                    productViewModel = new ProductViewModel();
                    product = new Product();

                    productViewModel.Name = workSheet.Cells[i, 1].Value.ToString();
                    decimal.TryParse(workSheet.Cells[i, 2].Value.ToString(), out RRP);
                    productViewModel.RRP = RRP;
                    decimal.TryParse(workSheet.Cells[i, 3].Value.ToString(), out SP);
                    productViewModel.SP = SP;
                    DateTime.TryParse(workSheet.Cells[i, 4].Value.ToString(), out SpecialFromTime);
                    productViewModel.SpecialFromTime = SpecialFromTime;
                    DateTime.TryParse(workSheet.Cells[i, 5].Value.ToString(), out SpecialToTime);
                    productViewModel.SpecialToTime = SpecialToTime;
                    bool.TryParse(workSheet.Cells[i, 6].Value.ToString(), out Status);
                    productViewModel.Status = Status;
                    productViewModel.NameEn = workSheet.Cells[i, 7].Value.ToString();
                    productViewModel.Brand = workSheet.Cells[i, 8].Value.ToString();
                    productViewModel.Model = workSheet.Cells[i, 9].Value.ToString();
                    productViewModel.ProductCode = workSheet.Cells[i, 10].Value.ToString();
                    productViewModel.ColorFamily = workSheet.Cells[i, 11].Value.ToString();
                    productViewModel.Size = workSheet.Cells[i, 12].Value.ToString();
                    int.TryParse(workSheet.Cells[i, 13].Value.ToString(), out ProductLifeTime);
                    productViewModel.ProductLifeTime = ProductLifeTime;
                    int.TryParse(workSheet.Cells[i, 14].Value.ToString(), out Warranty);
                    productViewModel.Warranty = Warranty;
                    productViewModel.WarrantyType = workSheet.Cells[i, 15].Value.ToString();
                    productViewModel.Unit = workSheet.Cells[i, 16].Value.ToString();
                    productViewModel.PackageContent = workSheet.Cells[i, 17].Value.ToString();
                    int.TryParse(workSheet.Cells[i, 18].Value.ToString(), out PackageWeight);
                    productViewModel.PackageWeight = PackageWeight;
                    int.TryParse(workSheet.Cells[i, 19].Value.ToString(), out PackageLength);
                    productViewModel.PackageLength = PackageLength;
                    int.TryParse(workSheet.Cells[i, 20].Value.ToString(), out PackageWidth);
                    productViewModel.PackageWidth = PackageWidth;
                    int.TryParse(workSheet.Cells[i, 21].Value.ToString(), out PackageHeight);
                    productViewModel.PackageHeight = PackageHeight;
                    productViewModel.ShortDescription = workSheet.Cells[i, 22].Value.ToString();
                    productViewModel.Description = workSheet.Cells[i, 23].Value.ToString();
                    productViewModel.Origin = workSheet.Cells[i, 24].Value.ToString();
                    productViewModel.Video = workSheet.Cells[i, 25].Text.ToString();
                    productViewModel.MainImage = workSheet.Cells[i, 26].Text.ToString();
                    productViewModel.MoreImage = workSheet.Cells[i, 27].Text.ToString();
                    productViewModel.Note = workSheet.Cells[i, 28].Text.ToString();
                    int.TryParse(workSheet.Cells[i, 21].Value.ToString(), out CategoryID);
                    productViewModel.CategoryID = CategoryID;

                    product.UpdateProduct(productViewModel);
                    listProduct.Add(product);
                }
                return listProduct;
            }
        }

        [HttpGet]
        [Route("ExportXls")]
        public async Task<HttpResponseMessage> ExportXls(HttpRequestMessage request)
        {
            string fileName = string.Concat("SanPham_" + DateTime.Now.ToString("yyyyMMddhhmmsss") + ".xlsx");
            var folderReport = ConfigHelper.GetByKey("ReportFolder");
            string filePath = HttpContext.Current.Server.MapPath(folderReport);
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }
            string fullPath = Path.Combine(filePath, fileName);
            try
            {
                var data = _productService.GetAll().ToList();
                await ReportHelper.GenerateXls(data, fullPath);
                return request.CreateErrorResponse(HttpStatusCode.OK, Path.Combine(folderReport, fileName));
            }
            catch (Exception ex)
            {
                return request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }
    }
}