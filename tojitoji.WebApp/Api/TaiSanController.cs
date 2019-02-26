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
    [RoutePrefix("api/taisan")]
    public class TaiSanController : ApiControllerBase
    {
        #region Initialize

        private ITaiSanService _taiSanService;

        public TaiSanController(IErrorService errorService, ITaiSanService taiSanService)
            : base(errorService)
        {
            this._taiSanService = taiSanService;
        }

        #endregion Initialize

        [Route("getall")]
        [HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage request, string keyword, int page, int pageSize = 20)
        {
            return CreateHttpResponse(request, () =>
            {
                int totalRow = 0;
                var model = _taiSanService.GetAll(keyword);

                totalRow = model.Count();
                var query = model.OrderByDescending(x => x.ID).Skip(page * pageSize).Take(pageSize);

                var responseData = Mapper.Map<IEnumerable<TaiSan>, IEnumerable<TaiSanViewModel>>(query);

                var paginationSet = new PaginationSet<TaiSanViewModel>()
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
                var model = _taiSanService.GetById(id);
                var responseData = Mapper.Map<TaiSan, TaiSanViewModel>(model);
                var response = request.CreateResponse(HttpStatusCode.OK, responseData);
                return response;
            });
        }

        [Route("create")]
        [HttpPost]
        public HttpResponseMessage Create(HttpRequestMessage request, TaiSanViewModel taiSanVM)
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
                    var newTaiSan = new TaiSan();
                    newTaiSan.UpdateTaiSan(taiSanVM);
                    _taiSanService.Add(newTaiSan);
                    _taiSanService.SaveChanges();

                    var responseData = Mapper.Map<TaiSan, TaiSanViewModel>(newTaiSan);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }

                return response;
            });
        }

        [Route("update")]
        [HttpPut]
        public HttpResponseMessage Update(HttpRequestMessage request, TaiSanViewModel TaiSanVM)
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
                    var dbTaiSan = _taiSanService.GetById(TaiSanVM.ID);

                    dbTaiSan.UpdateTaiSan(TaiSanVM);

                    _taiSanService.Update(dbTaiSan);
                    _taiSanService.SaveChanges();

                    var responseData = Mapper.Map<TaiSan, TaiSanViewModel>(dbTaiSan);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }

                return response;
            });
        }

        [Route("deletemulti")]
        [HttpDelete]
        public HttpResponseMessage DeleteMulti(HttpRequestMessage request, string checkedTaiSans)
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
                    var listTaiSan = new JavaScriptSerializer().Deserialize<List<int>>(checkedTaiSans);
                    foreach (var item in listTaiSan)
                    {
                        _taiSanService.Delete(item);
                    }

                    _taiSanService.SaveChanges();

                    response = request.CreateResponse(HttpStatusCode.OK, listTaiSan.Count);
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

            if (result.FormData["loaiTaiSan"] == null)
            {
                Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Bạn chưa chọn loại tài sản");
            }

            int addedCount = 0;
            int loaiTaiSan = 0;
            int.TryParse(result.FormData["loaiTaiSan"], out loaiTaiSan);

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
                var listTaiSan = this.ReadTaiSanFromExcel(fullPath, loaiTaiSan);
                if (listTaiSan.Count > 0)
                {
                    foreach (var TaiSan in listTaiSan)
                    {
                        _taiSanService.Add(TaiSan);
                        addedCount++;
                    }
                    _taiSanService.SaveChanges();
                }
            }
            return Request.CreateResponse(HttpStatusCode.OK, "Đã nhập thành công " + addedCount + " tài sản");
        }

        private List<TaiSan> ReadTaiSanFromExcel(string fullPath, int loaiTaiSan)
        {
            using (var package = new ExcelPackage(new FileInfo(fullPath)))
            {
                ExcelWorksheet workSheet = package.Workbook.Worksheets[1];
                List<TaiSan> listTaiSan = new List<TaiSan>();
                TaiSanViewModel TaiSanViewModel;
                TaiSan TaiSan;

                for (int i = workSheet.Dimension.Start.Row + 1; i <= workSheet.Dimension.End.Row; i++)
                {
                    TaiSanViewModel = new TaiSanViewModel();
                    TaiSan = new TaiSan();

                    TaiSanViewModel.Name = workSheet.Cells[i, 1].Value.ToString();
                    TaiSanViewModel.LoaiTaiSanID = loaiTaiSan;                    

                    TaiSan.UpdateTaiSan(TaiSanViewModel);
                    listTaiSan.Add(TaiSan);
                }
                return listTaiSan;
            }
        }

        [HttpGet]
        [Route("ExportXls")]
        public async Task<HttpResponseMessage> ExportXls(HttpRequestMessage request)
        {
            string fileName = string.Concat("TaiSan_" + DateTime.Now.ToString("yyyyMMddhhmmsss") + ".xlsx");
            var folderReport = ConfigHelper.GetByKey("ReportFolder");
            string filePath = HttpContext.Current.Server.MapPath(folderReport);
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }
            string fullPath = Path.Combine(filePath, fileName);
            try
            {
                var data = _taiSanService.GetAll().ToList();
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
