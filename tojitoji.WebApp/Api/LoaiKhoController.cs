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
    [RoutePrefix("api/loaikho")]
    public class LoaiKhoController : ApiControllerBase
    {
        #region Initialize

        private ILoaiKhoService _loaiKhoService;

        public LoaiKhoController(IErrorService errorService, ILoaiKhoService loaiKhoService)
            : base(errorService)
        {
            this._loaiKhoService = loaiKhoService;
        }

        #endregion Initialize

        [Route("getall")]
        [HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage request, string keyword, int page, int pageSize = 20)
        {
            return CreateHttpResponse(request, () =>
            {
                int totalRow = 0;
                var model = _loaiKhoService.GetAll(keyword);

                totalRow = model.Count();
                var query = model.OrderByDescending(x => x.ID).Skip(page * pageSize).Take(pageSize);

                var responseData = Mapper.Map<IEnumerable<LoaiKho>, IEnumerable<LoaiKhoViewModel>>(query);

                var paginationSet = new PaginationSet<LoaiKhoViewModel>()
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
                var model = _loaiKhoService.GetById(id);
                var responseData = Mapper.Map<LoaiKho, LoaiKhoViewModel>(model);
                var response = request.CreateResponse(HttpStatusCode.OK, responseData);
                return response;
            });
        }

        [Route("create")]
        [HttpPost]
        public HttpResponseMessage Create(HttpRequestMessage request, LoaiKhoViewModel loaiKhoVM)
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
                    var newLoaiKho = new LoaiKho();
                    newLoaiKho.UpdateLoaiKho(loaiKhoVM);
                    _loaiKhoService.Add(newLoaiKho);
                    _loaiKhoService.SaveChanges();

                    var responseData = Mapper.Map<LoaiKho, LoaiKhoViewModel>(newLoaiKho);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }

                return response;
            });
        }

        [Route("update")]
        [HttpPut]
        public HttpResponseMessage Update(HttpRequestMessage request, LoaiKhoViewModel loaiKhoVM)
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
                    var dbLoaiKho = _loaiKhoService.GetById(loaiKhoVM.ID);

                    dbLoaiKho.UpdateLoaiKho(loaiKhoVM);

                    _loaiKhoService.Update(dbLoaiKho);
                    _loaiKhoService.SaveChanges();

                    var responseData = Mapper.Map<LoaiKho, LoaiKhoViewModel>(dbLoaiKho);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }

                return response;
            });
        }

        [Route("deletemulti")]
        [HttpDelete]
        public HttpResponseMessage DeleteMulti(HttpRequestMessage request, string checkedLoaiKhos)
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
                    var listLoaiKho = new JavaScriptSerializer().Deserialize<List<int>>(checkedLoaiKhos);
                    foreach (var item in listLoaiKho)
                    {
                        _loaiKhoService.Delete(item);
                    }

                    _loaiKhoService.SaveChanges();

                    response = request.CreateResponse(HttpStatusCode.OK, listLoaiKho.Count);
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
                var listLoaiKho = this.ReadLoaiKhoFromExcel(fullPath);
                if (listLoaiKho.Count > 0)
                {
                    foreach (var LoaiKho in listLoaiKho)
                    {
                        _loaiKhoService.Add(LoaiKho);
                        addedCount++;
                    }
                    _loaiKhoService.SaveChanges();
                }
            }
            return Request.CreateResponse(HttpStatusCode.OK, "Đã nhập thành công " + addedCount + " loại kho.");
        }

        private List<LoaiKho> ReadLoaiKhoFromExcel(string fullPath)
        {
            using (var package = new ExcelPackage(new FileInfo(fullPath)))
            {
                ExcelWorksheet workSheet = package.Workbook.Worksheets[1];
                List<LoaiKho> listLoaiKho = new List<LoaiKho>();
                LoaiKhoViewModel LoaiKhoViewModel;
                LoaiKho LoaiKho;

                for (int i = workSheet.Dimension.Start.Row + 1; i <= workSheet.Dimension.End.Row; i++)
                {
                    LoaiKhoViewModel = new LoaiKhoViewModel();
                    LoaiKho = new LoaiKho();

                    LoaiKhoViewModel.Name = workSheet.Cells[i, 1].Value.ToString();
                    LoaiKhoViewModel.Description = workSheet.Cells[i, 2].Value.ToString();

                    LoaiKho.UpdateLoaiKho(LoaiKhoViewModel);
                    listLoaiKho.Add(LoaiKho);
                }
                return listLoaiKho;
            }
        }

        [HttpGet]
        [Route("ExportXls")]
        public async Task<HttpResponseMessage> ExportXls(HttpRequestMessage request)
        {
            string fileName = string.Concat("LoaiKho_" + DateTime.Now.ToString("yyyyMMddhhmmsss") + ".xlsx");
            var folderReport = ConfigHelper.GetByKey("ReportFolder");
            string filePath = HttpContext.Current.Server.MapPath(folderReport);
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }
            string fullPath = Path.Combine(filePath, fileName);
            try
            {
                var data = _loaiKhoService.GetAll().ToList();
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
