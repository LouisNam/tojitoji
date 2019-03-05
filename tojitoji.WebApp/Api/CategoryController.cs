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
    [RoutePrefix("api/category")]
    public class CategoryController : ApiControllerBase
    {
        #region Initialize

        private ICategoryService _categoryService;

        public CategoryController(IErrorService errorService, ICategoryService categoryService)
            : base(errorService)
        {
            this._categoryService = categoryService;
        }

        #endregion Initialize

        [Route("getalltype")]
        [HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                var model = _categoryService.GetAll();
                var responseData = Mapper.Map<IEnumerable<Category>, IEnumerable<CategoryViewModel>>(model);
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
                var model = _categoryService.GetAll(keyword);

                totalRow = model.Count();
                var query = model.OrderByDescending(x => x.ID).Skip(page * pageSize).Take(pageSize);

                var responseData = Mapper.Map<IEnumerable<Category>, IEnumerable<CategoryViewModel>>(query);

                var paginationSet = new PaginationSet<CategoryViewModel>()
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
                var model = _categoryService.GetById(id);
                var responseData = Mapper.Map<Category, CategoryViewModel>(model);
                var response = request.CreateResponse(HttpStatusCode.OK, responseData);
                return response;
            });
        }

        [Route("create")]
        [HttpPost]
        public HttpResponseMessage Create(HttpRequestMessage request, CategoryViewModel categoryVM)
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
                    var newCategory = new Category();
                    newCategory.UpdateCategory(categoryVM);
                    _categoryService.Add(newCategory);
                    _categoryService.SaveChanges();

                    var responseData = Mapper.Map<Category, CategoryViewModel>(newCategory);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }

                return response;
            });
        }

        [Route("update")]
        [HttpPut]
        public HttpResponseMessage Update(HttpRequestMessage request, CategoryViewModel categoryVM)
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
                    var dbCategory = _categoryService.GetById(categoryVM.ID);

                    dbCategory.UpdateCategory(categoryVM);

                    _categoryService.Update(dbCategory);
                    _categoryService.SaveChanges();

                    var responseData = Mapper.Map<Category, CategoryViewModel>(dbCategory);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }

                return response;
            });
        }        

        [Route("deletemulti")]
        [HttpDelete]
        public HttpResponseMessage DeleteMulti(HttpRequestMessage request, string checkedCategories)
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
                    var listCategory = new JavaScriptSerializer().Deserialize<List<int>>(checkedCategories);
                    foreach (var item in listCategory)
                    {
                        _categoryService.Delete(item);
                    }

                    _categoryService.SaveChanges();

                    response = request.CreateResponse(HttpStatusCode.OK, listCategory.Count);
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
                var listCategory = this.ReadCategoryFromExcel(fullPath);
                if (listCategory.Count > 0)
                {
                    foreach (var category in listCategory)
                    {
                        _categoryService.Add(category);
                        addedCount++;
                    }
                    _categoryService.SaveChanges();
                }
            }
            return Request.CreateResponse(HttpStatusCode.OK, "Đã nhập thành công " + addedCount + " categories");
        }

        private List<Category> ReadCategoryFromExcel(string fullPath)
        {
            using (var package = new ExcelPackage(new FileInfo(fullPath)))
            {
                ExcelWorksheet workSheet = package.Workbook.Worksheets[1];
                List<Category> listCategory = new List<Category>();
                CategoryViewModel categoryViewModel;
                Category category;

                int Code;
                int Categories_Type;

                for (int i = workSheet.Dimension.Start.Row + 1; i <= workSheet.Dimension.End.Row; i++)
                {
                    categoryViewModel = new CategoryViewModel();
                    category = new Category();

                    int.TryParse(workSheet.Cells[i, 1].Value.ToString(), out Code);
                    categoryViewModel.Code = Code;
                    
                    categoryViewModel.Categories = workSheet.Cells[i, 2].Value.ToString();

                    int.TryParse(workSheet.Cells[i, 3].Text.ToString(), out Categories_Type);
                    categoryViewModel.Categories_Type = Categories_Type;

                    categoryViewModel.MacroCategories = workSheet.Cells[i, 4].Value.ToString();
                    categoryViewModel.CommercialCate = workSheet.Cells[i, 5].Text.ToString();
                    categoryViewModel.Name_1 = workSheet.Cells[i, 6].Text.ToString();
                    categoryViewModel.Name_2 = workSheet.Cells[i, 7].Text.ToString();
                    categoryViewModel.Name_3 = workSheet.Cells[i, 8].Text.ToString();
                    categoryViewModel.Name_4 = workSheet.Cells[i, 9].Text.ToString();
                    categoryViewModel.Name_5 = workSheet.Cells[i, 10].Text.ToString();
                    categoryViewModel.Name_6 = workSheet.Cells[i, 11].Text.ToString();
                    categoryViewModel.NameEn_1 = workSheet.Cells[i, 12].Text.ToString();
                    categoryViewModel.NameEn_2 = workSheet.Cells[i, 13].Text.ToString();
                    categoryViewModel.NameEn_3 = workSheet.Cells[i, 14].Text.ToString();
                    categoryViewModel.NameEn_4 = workSheet.Cells[i, 15].Text.ToString();
                    categoryViewModel.NameEn_5 = workSheet.Cells[i, 16].Text.ToString();
                    categoryViewModel.NameEn_6 = workSheet.Cells[i, 17].Text.ToString();

                    category.UpdateCategory(categoryViewModel);
                    listCategory.Add(category);
                }
                return listCategory;
            }
        }

        [HttpGet]
        [Route("ExportXls")]
        public async Task<HttpResponseMessage> ExportXls(HttpRequestMessage request)
        {
            string fileName = string.Concat("Category_" + DateTime.Now.ToString("yyyyMMddhhmmsss") + ".xlsx");
            var folderReport = ConfigHelper.GetByKey("ReportFolder");
            string filePath = HttpContext.Current.Server.MapPath(folderReport);
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }
            string fullPath = Path.Combine(filePath, fileName);
            try
            {
                var data = _categoryService.GetListCategory().ToList();
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