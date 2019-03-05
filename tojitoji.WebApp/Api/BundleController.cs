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
    [RoutePrefix("api/bundle")]
    public class BundleController : ApiControllerBase
    {
        #region Initialize

        private IBundleService _bundleService;

        public BundleController(IErrorService errorService, IBundleService bundleService)
            : base(errorService)
        {
            this._bundleService = bundleService;
        }

        #endregion Initialize

        [Route("getallbundle")]
        [HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                var model = _bundleService.GetAll();
                var responseData = Mapper.Map<IEnumerable<Bundle>, IEnumerable<BundleViewModel>>(model);
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
                var model = _bundleService.GetAll(keyword);

                totalRow = model.Count();
                var query = model.OrderByDescending(x => x.ID).Skip(page * pageSize).Take(pageSize);

                var responseData = Mapper.Map<IEnumerable<Bundle>, IEnumerable<BundleViewModel>>(query);

                var paginationSet = new PaginationSet<BundleViewModel>()
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
                var model = _bundleService.GetById(id);
                var responseData = Mapper.Map<Bundle, BundleViewModel>(model);
                var response = request.CreateResponse(HttpStatusCode.OK, responseData);
                return response;
            });
        }

        [Route("create")]
        [HttpPost]
        public HttpResponseMessage Create(HttpRequestMessage request, BundleViewModel bundleVM)
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
                    var newBundle = new Bundle();
                    newBundle.UpdateBundle(bundleVM);
                    _bundleService.Add(newBundle);
                    _bundleService.SaveChanges();

                    var responseData = Mapper.Map<Bundle, BundleViewModel>(newBundle);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }

                return response;
            });
        }

        [Route("update")]
        [HttpPut]
        public HttpResponseMessage Update(HttpRequestMessage request, BundleViewModel bundleVM)
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
                    var dbBundle = _bundleService.GetById(bundleVM.ID);

                    dbBundle.UpdateBundle(bundleVM);

                    _bundleService.Update(dbBundle);
                    _bundleService.SaveChanges();

                    var responseData = Mapper.Map<Bundle, BundleViewModel>(dbBundle);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }

                return response;
            });
        }

        [Route("deletemulti")]
        [HttpDelete]
        public HttpResponseMessage DeleteMulti(HttpRequestMessage request, string checkedBundles)
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
                    var listBundle = new JavaScriptSerializer().Deserialize<List<int>>(checkedBundles);
                    foreach (var item in listBundle)
                    {
                        _bundleService.Delete(item);
                    }

                    _bundleService.SaveChanges();

                    response = request.CreateResponse(HttpStatusCode.OK, listBundle.Count);
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
                var listBundle = this.ReadBundleFromExcel(fullPath);
                if (listBundle.Count > 0)
                {
                    foreach (var Bundle in listBundle)
                    {
                        _bundleService.Add(Bundle);
                        addedCount++;
                    }
                    _bundleService.SaveChanges();
                }
            }
            return Request.CreateResponse(HttpStatusCode.OK, "Đã nhập thành công " + addedCount + " bundles");
        }

        private List<Bundle> ReadBundleFromExcel(string fullPath)
        {
            using (var package = new ExcelPackage(new FileInfo(fullPath)))
            {
                ExcelWorksheet workSheet = package.Workbook.Worksheets[1];
                List<Bundle> listBundle = new List<Bundle>();
                BundleViewModel bundleViewModel;
                Bundle bundle;

                int ProductID;
                int ProductQuantity;
                int ProductNo;
                decimal DiscountRate;
                DateTime SpecialFromTime;
                DateTime SpecialToTime;

                for (int i = workSheet.Dimension.Start.Row + 1; i <= workSheet.Dimension.End.Row; i++)
                {
                    bundleViewModel = new BundleViewModel();
                    bundle = new Bundle();

                    bundleViewModel.BundleType = workSheet.Cells[i, 1].Value.ToString();
                    bundleViewModel.SKUBundle = workSheet.Cells[i, 2].Value.ToString();
                    bundleViewModel.BundleName = workSheet.Cells[i, 3].Value.ToString();
                    int.TryParse(workSheet.Cells[i,4].Value.ToString(), out ProductID);
                    bundleViewModel.ProductID = ProductID;
                    int.TryParse(workSheet.Cells[i, 5].Value.ToString(), out ProductQuantity);
                    bundleViewModel.ProductQuantity = ProductQuantity;
                    int.TryParse(workSheet.Cells[i, 6].Value.ToString(), out ProductNo);
                    bundleViewModel.ProductNo = ProductNo;
                    decimal.TryParse(workSheet.Cells[i, 7].Value.ToString(), out DiscountRate);
                    bundleViewModel.DiscountRate = DiscountRate;
                    DateTime.TryParse(workSheet.Cells[i, 8].Value.ToString(), out SpecialFromTime);
                    bundleViewModel.SpecialFromTime = SpecialFromTime;
                    DateTime.TryParse(workSheet.Cells[i, 9].Value.ToString(), out SpecialToTime);
                    bundleViewModel.SpecialToTime = SpecialToTime;

                    bundle.UpdateBundle(bundleViewModel);
                    listBundle.Add(bundle);
                }
                return listBundle;
            }
        }

        [HttpGet]
        [Route("ExportXls")]
        public async Task<HttpResponseMessage> ExportXls(HttpRequestMessage request)
        {
            string fileName = string.Concat("Bundle_" + DateTime.Now.ToString("yyyyMMddhhmmsss") + ".xlsx");
            var folderReport = ConfigHelper.GetByKey("ReportFolder");
            string filePath = HttpContext.Current.Server.MapPath(folderReport);
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }
            string fullPath = Path.Combine(filePath, fileName);
            try
            {
                var data = _bundleService.GetAll().ToList();
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