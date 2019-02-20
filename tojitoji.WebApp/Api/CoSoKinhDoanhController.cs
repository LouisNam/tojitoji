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
    [RoutePrefix("api/cosokinhdoanh")]
    public class CoSoKinhDoanhController : ApiControllerBase
    {
        #region Initialize

        private ICoSoKinhDoanhService _coSoKinhDoanhService;

        public CoSoKinhDoanhController(IErrorService errorService, ICoSoKinhDoanhService coSoKinhDoanhService)
            : base(errorService)
        {
            this._coSoKinhDoanhService = coSoKinhDoanhService;
        }

        #endregion Initialize

        [Route("getall")]
        [HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage request, int page, int pageSize = 20)
        {
            return CreateHttpResponse(request, () =>
            {
                int totalRow = 0;
                var model = _coSoKinhDoanhService.GetAll();

                totalRow = model.Count();
                var query = model.OrderByDescending(x => x.ID).Skip(page * pageSize).Take(pageSize);

                var responseData = Mapper.Map<IEnumerable<CoSoKinhDoanh>, IEnumerable<CoSoKinhDoanhViewModel>>(query);

                var paginationSet = new PaginationSet<CoSoKinhDoanhViewModel>()
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
                var model = _coSoKinhDoanhService.GetById(id);
                var responseData = Mapper.Map<CoSoKinhDoanh, CoSoKinhDoanhViewModel>(model);
                var response = request.CreateResponse(HttpStatusCode.OK, responseData);
                return response;
            });
        }

        [Route("create")]
        [HttpPost]
        public HttpResponseMessage Create(HttpRequestMessage request, CoSoKinhDoanhViewModel CoSoKinhDoanhVM)
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
                    var newCoSoKinhDoanh = new CoSoKinhDoanh();
                    newCoSoKinhDoanh.UpdateCoSoKinhDoanh(CoSoKinhDoanhVM);
                    _coSoKinhDoanhService.Add(newCoSoKinhDoanh);
                    _coSoKinhDoanhService.SaveChanges();

                    var responseData = Mapper.Map<CoSoKinhDoanh, CoSoKinhDoanhViewModel>(newCoSoKinhDoanh);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }

                return response;
            });
        }

        [Route("update")]
        [HttpPut]
        public HttpResponseMessage Update(HttpRequestMessage request, CoSoKinhDoanhViewModel CoSoKinhDoanhVM)
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
                    var dbCoSoKinhDoanh = _coSoKinhDoanhService.GetById(CoSoKinhDoanhVM.ID);

                    dbCoSoKinhDoanh.UpdateCoSoKinhDoanh(CoSoKinhDoanhVM);

                    _coSoKinhDoanhService.Update(dbCoSoKinhDoanh);
                    _coSoKinhDoanhService.SaveChanges();

                    var responseData = Mapper.Map<CoSoKinhDoanh, CoSoKinhDoanhViewModel>(dbCoSoKinhDoanh);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }

                return response;
            });
        }

        [Route("delete")]
        [HttpDelete]
        public HttpResponseMessage Delete(HttpRequestMessage request, int id)
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
                    var oldCoSoKinhDoanh = _coSoKinhDoanhService.Delete(id);
                    _coSoKinhDoanhService.SaveChanges();

                    var responseData = Mapper.Map<CoSoKinhDoanh, CoSoKinhDoanhViewModel>(oldCoSoKinhDoanh);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }

                return response;
            });
        }

        [Route("deletemulti")]
        [HttpDelete]
        public HttpResponseMessage DeleteMulti(HttpRequestMessage request, string checkedCoSoKinhDoanhs)
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
                    var listCoSoKinhDoanh = new JavaScriptSerializer().Deserialize<List<int>>(checkedCoSoKinhDoanhs);
                    foreach (var item in listCoSoKinhDoanh)
                    {
                        _coSoKinhDoanhService.Delete(item);
                    }

                    _coSoKinhDoanhService.SaveChanges();

                    response = request.CreateResponse(HttpStatusCode.OK, listCoSoKinhDoanh.Count);
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
                
                var listCoSoKinhDoanh = this.ReadCoSoKinhDoanhFromExcel(fullPath);
                if (listCoSoKinhDoanh.Count > 0)
                {
                    foreach (var CoSoKinhDoanh in listCoSoKinhDoanh)
                    {
                        _coSoKinhDoanhService.Add(CoSoKinhDoanh);
                        addedCount++;
                    }
                    _coSoKinhDoanhService.SaveChanges();
                }
            }
            return Request.CreateResponse(HttpStatusCode.OK, "Đã nhập thành công " + addedCount + " cơ sở kinh doanh");
        }

        private List<CoSoKinhDoanh> ReadCoSoKinhDoanhFromExcel(string fullPath)
        {
            using (var package = new ExcelPackage(new FileInfo(fullPath)))
            {
                ExcelWorksheet workSheet = package.Workbook.Worksheets[1];
                List<CoSoKinhDoanh> listCoSoKinhDoanh = new List<CoSoKinhDoanh>();
                CoSoKinhDoanhViewModel CoSoKinhDoanhViewModel;
                CoSoKinhDoanh CoSoKinhDoanh;

                bool status = false;

                for (int i = workSheet.Dimension.Start.Row + 1; i <= workSheet.Dimension.End.Row; i++)
                {
                    CoSoKinhDoanhViewModel = new CoSoKinhDoanhViewModel();
                    CoSoKinhDoanh = new CoSoKinhDoanh();

                    CoSoKinhDoanhViewModel.Place_1 = workSheet.Cells[i, 1].Value.ToString();
                    CoSoKinhDoanhViewModel.Place_2 = workSheet.Cells[i, 2].Value.ToString();
                    CoSoKinhDoanhViewModel.Place_3 = workSheet.Cells[i, 3].Value.ToString();
                    CoSoKinhDoanhViewModel.Place_4 = workSheet.Cells[i, 4].Value.ToString();

                    bool.TryParse(workSheet.Cells[i, 5].Value.ToString(), out status);
                    CoSoKinhDoanhViewModel.Status = status;

                    CoSoKinhDoanhViewModel.Note = workSheet.Cells[i, 6].Value.ToString();

                    CoSoKinhDoanh.UpdateCoSoKinhDoanh(CoSoKinhDoanhViewModel);
                    listCoSoKinhDoanh.Add(CoSoKinhDoanh);
                }
                return listCoSoKinhDoanh;
            }
        }

        [HttpGet]
        [Route("ExportXls")]
        public async Task<HttpResponseMessage> ExportXls(HttpRequestMessage request)
        {
            string fileName = string.Concat("CoSoKinhDoanh_" + DateTime.Now.ToString("yyyyMMddhhmmsss") + ".xlsx");
            var folderReport = ConfigHelper.GetByKey("ReportFolder");
            string filePath = HttpContext.Current.Server.MapPath(folderReport);
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }
            string fullPath = Path.Combine(filePath, fileName);
            try
            {
                var data = _coSoKinhDoanhService.GetListCoSoKinhDoanh().ToList();
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
