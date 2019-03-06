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
    [RoutePrefix("api/timetrichkhauhaotscd")]
    public class TimeTrichKhauHaoTSCDController : ApiControllerBase
    {
        #region Initialize

        private ITimeTrichKhauHaoTSCDService _timeTrichKhauHaoTSCDService;

        public TimeTrichKhauHaoTSCDController(IErrorService errorService, ITimeTrichKhauHaoTSCDService timeTrichKhauHaoTSCDService)
            : base(errorService)
        {
            this._timeTrichKhauHaoTSCDService = timeTrichKhauHaoTSCDService;
        }

        #endregion Initialize

        [Route("getall")]
        [HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage request, string keyword, int page, int pageSize = 20)
        {
            return CreateHttpResponse(request, () =>
            {
                int totalRow = 0;
                var model = _timeTrichKhauHaoTSCDService.GetAll(keyword);

                totalRow = model.Count();
                var query = model.OrderByDescending(x => x.ID).Skip(page * pageSize).Take(pageSize);

                var responseData = Mapper.Map<IEnumerable<TimeTrichKhauHaoTSCD>, IEnumerable<TimeTrichKhauHaoTSCDViewModel>>(query);

                var paginationSet = new PaginationSet<TimeTrichKhauHaoTSCDViewModel>()
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
                var model = _timeTrichKhauHaoTSCDService.GetById(id);
                var responseData = Mapper.Map<TimeTrichKhauHaoTSCD, TimeTrichKhauHaoTSCDViewModel>(model);
                var response = request.CreateResponse(HttpStatusCode.OK, responseData);
                return response;
            });
        }

        [Route("create")]
        [HttpPost]
        public HttpResponseMessage Create(HttpRequestMessage request, TimeTrichKhauHaoTSCDViewModel timeTrichKhauHaoTSCDVM)
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
                    var newTimeTrichKhauHaoTSCD = new TimeTrichKhauHaoTSCD();
                    newTimeTrichKhauHaoTSCD.UpdateTimeTrichKhauHaoTSCD(timeTrichKhauHaoTSCDVM);
                    _timeTrichKhauHaoTSCDService.Add(newTimeTrichKhauHaoTSCD);
                    _timeTrichKhauHaoTSCDService.SaveChanges();

                    var responseData = Mapper.Map<TimeTrichKhauHaoTSCD, TimeTrichKhauHaoTSCDViewModel>(newTimeTrichKhauHaoTSCD);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }

                return response;
            });
        }

        [Route("update")]
        [HttpPut]
        public HttpResponseMessage Update(HttpRequestMessage request, TimeTrichKhauHaoTSCDViewModel timeTrichKhauHaoTSCDVM)
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
                    var dbTimeTrichKhauHaoTSCD = _timeTrichKhauHaoTSCDService.GetById(timeTrichKhauHaoTSCDVM.ID);

                    dbTimeTrichKhauHaoTSCD.UpdateTimeTrichKhauHaoTSCD(timeTrichKhauHaoTSCDVM);

                    _timeTrichKhauHaoTSCDService.Update(dbTimeTrichKhauHaoTSCD);
                    _timeTrichKhauHaoTSCDService.SaveChanges();

                    var responseData = Mapper.Map<TimeTrichKhauHaoTSCD, TimeTrichKhauHaoTSCDViewModel>(dbTimeTrichKhauHaoTSCD);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }

                return response;
            });
        }

        [Route("deletemulti")]
        [HttpDelete]
        public HttpResponseMessage DeleteMulti(HttpRequestMessage request, string checkedTimeTrichKhauHaoTSCDs)
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
                    var listTimeTrichKhauHaoTSCD = new JavaScriptSerializer().Deserialize<List<int>>(checkedTimeTrichKhauHaoTSCDs);
                    foreach (var item in listTimeTrichKhauHaoTSCD)
                    {
                        _timeTrichKhauHaoTSCDService.Delete(item);
                    }

                    _timeTrichKhauHaoTSCDService.SaveChanges();

                    response = request.CreateResponse(HttpStatusCode.OK, listTimeTrichKhauHaoTSCD.Count);
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
            //Upload files
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
                var listTimeTrichKhauHaoTSCD = this.ReadTimeTrichKhauHaoTSCDFromExcel(fullPath);
                if (listTimeTrichKhauHaoTSCD.Count > 0)
                {
                    foreach (var TimeTrichKhauHaoTSCD in listTimeTrichKhauHaoTSCD)
                    {
                        _timeTrichKhauHaoTSCDService.Add(TimeTrichKhauHaoTSCD);
                        addedCount++;
                    }
                    _timeTrichKhauHaoTSCDService.SaveChanges();
                }
            }
            return Request.CreateResponse(HttpStatusCode.OK, "Đã nhập thành công " + addedCount + " bản ghi.");
        }

        private List<TimeTrichKhauHaoTSCD> ReadTimeTrichKhauHaoTSCDFromExcel(string fullPath)
        {
            using (var package = new ExcelPackage(new FileInfo(fullPath)))
            {
                ExcelWorksheet workSheet = package.Workbook.Worksheets[1];
                List<TimeTrichKhauHaoTSCD> listTimeTrichKhauHaoTSCD = new List<TimeTrichKhauHaoTSCD>();
                TimeTrichKhauHaoTSCDViewModel timeTrichKhauHaoTSCDViewModel;
                TimeTrichKhauHaoTSCD timeTrichKhauHaoTSCD;

                int TimeMin;
                int TimeMax;

                for (int i = workSheet.Dimension.Start.Row + 1; i <= workSheet.Dimension.End.Row; i++)
                {
                    timeTrichKhauHaoTSCDViewModel = new TimeTrichKhauHaoTSCDViewModel();
                    timeTrichKhauHaoTSCD = new TimeTrichKhauHaoTSCD();

                    timeTrichKhauHaoTSCDViewModel.DanhMucNhomTSCD = workSheet.Cells[i, 1].Value.ToString();
                    timeTrichKhauHaoTSCDViewModel.NhomTSCD = workSheet.Cells[i, 2].Value.ToString();
                    int.TryParse(workSheet.Cells[i, 3].Value.ToString(), out TimeMin);
                    timeTrichKhauHaoTSCDViewModel.TimeMin = TimeMin;
                    int.TryParse(workSheet.Cells[i, 4].Value.ToString(), out TimeMax);
                    timeTrichKhauHaoTSCDViewModel.TimeMax = TimeMax;

                    timeTrichKhauHaoTSCD.UpdateTimeTrichKhauHaoTSCD(timeTrichKhauHaoTSCDViewModel);
                    listTimeTrichKhauHaoTSCD.Add(timeTrichKhauHaoTSCD);
                }
                return listTimeTrichKhauHaoTSCD;
            }
        }

        [HttpGet]
        [Route("ExportXls")]
        public async Task<HttpResponseMessage> ExportXls(HttpRequestMessage request)
        {
            string fileName = string.Concat("ThoiGianTrichKhauHaoTSCD_" + DateTime.Now.ToString("yyyyMMddhhmmsss") + ".xlsx");
            var folderReport = ConfigHelper.GetByKey("ReportFolder");
            string filePath = HttpContext.Current.Server.MapPath(folderReport);
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }
            string fullPath = Path.Combine(filePath, fileName);
            try
            {
                var data = _timeTrichKhauHaoTSCDService.GetAll().ToList();
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
