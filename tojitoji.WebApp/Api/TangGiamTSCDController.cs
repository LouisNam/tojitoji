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
    [RoutePrefix("api/tanggiamtscd")]
    public class TangGiamTSCDController : ApiControllerBase
    {
        #region Initialize

        private ITangGiamTSCDService _tangGiamTSCDService;

        public TangGiamTSCDController(IErrorService errorService, ITangGiamTSCDService tangGiamTSCDService)
            : base(errorService)
        {
            this._tangGiamTSCDService = tangGiamTSCDService;
        }

        #endregion Initialize

        [Route("getall")]
        [HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage request, string keyword, int page, int pageSize = 20)
        {
            return CreateHttpResponse(request, () =>
            {
                int totalRow = 0;
                var model = _tangGiamTSCDService.GetAll(keyword);

                totalRow = model.Count();
                var query = model.OrderByDescending(x => x.ID).Skip(page * pageSize).Take(pageSize);

                var responseData = Mapper.Map<IEnumerable<TangGiamTSCD>, IEnumerable<TangGiamTSCDViewModel>>(query);

                var paginationSet = new PaginationSet<TangGiamTSCDViewModel>()
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
                var model = _tangGiamTSCDService.GetById(id);
                var responseData = Mapper.Map<TangGiamTSCD, TangGiamTSCDViewModel>(model);
                var response = request.CreateResponse(HttpStatusCode.OK, responseData);
                return response;
            });
        }

        [Route("create")]
        [HttpPost]
        public HttpResponseMessage Create(HttpRequestMessage request, TangGiamTSCDViewModel tangGiamTSCDVM)
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
                    var newTangGiamTSCD = new TangGiamTSCD();
                    newTangGiamTSCD.UpdateTangGiamTSCD(tangGiamTSCDVM);
                    _tangGiamTSCDService.Add(newTangGiamTSCD);
                    _tangGiamTSCDService.SaveChanges();

                    var responseData = Mapper.Map<TangGiamTSCD, TangGiamTSCDViewModel>(newTangGiamTSCD);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }

                return response;
            });
        }

        [Route("update")]
        [HttpPut]
        public HttpResponseMessage Update(HttpRequestMessage request, TangGiamTSCDViewModel tangGiamTSCDVM)
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
                    var dbTangGiamTSCD = _tangGiamTSCDService.GetById(tangGiamTSCDVM.ID);

                    dbTangGiamTSCD.UpdateTangGiamTSCD(tangGiamTSCDVM);

                    _tangGiamTSCDService.Update(dbTangGiamTSCD);
                    _tangGiamTSCDService.SaveChanges();

                    var responseData = Mapper.Map<TangGiamTSCD, TangGiamTSCDViewModel>(dbTangGiamTSCD);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }

                return response;
            });
        }

        [Route("deletemulti")]
        [HttpDelete]
        public HttpResponseMessage DeleteMulti(HttpRequestMessage request, string checkedTangGiamTSCDs)
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
                    var listTangGiamTSCD = new JavaScriptSerializer().Deserialize<List<int>>(checkedTangGiamTSCDs);
                    foreach (var item in listTangGiamTSCD)
                    {
                        _tangGiamTSCDService.Delete(item);
                    }

                    _tangGiamTSCDService.SaveChanges();

                    response = request.CreateResponse(HttpStatusCode.OK, listTangGiamTSCD.Count);
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
                var listTangGiamTSCD = this.ReadTangGiamTSCDFromExcel(fullPath);
                if (listTangGiamTSCD.Count > 0)
                {
                    foreach (var TangGiamTSCD in listTangGiamTSCD)
                    {
                        _tangGiamTSCDService.Add(TangGiamTSCD);
                        addedCount++;
                    }
                    _tangGiamTSCDService.SaveChanges();
                }
            }
            return Request.CreateResponse(HttpStatusCode.OK, "Đã nhập thành công " + addedCount + " bản ghi.");
        }

        private List<TangGiamTSCD> ReadTangGiamTSCDFromExcel(string fullPath)
        {
            using (var package = new ExcelPackage(new FileInfo(fullPath)))
            {
                ExcelWorksheet workSheet = package.Workbook.Worksheets[1];
                List<TangGiamTSCD> listTangGiamTSCD = new List<TangGiamTSCD>();
                TangGiamTSCDViewModel tangGiamTSCDViewModel;
                TangGiamTSCD tangGiamTSCD;

                DateTime NgaySuDung;
                int SoLuong;
                decimal GiaTriBanDau;
                decimal GiaTriConLai;
                int ThoiGianSuDung;
                decimal GiaTriPhanBoTrongKy;
                int ThangSuDung;
                int NamSuDung;

                for (int i = workSheet.Dimension.Start.Row + 1; i <= workSheet.Dimension.End.Row; i++)
                {
                    tangGiamTSCDViewModel = new TangGiamTSCDViewModel();
                    tangGiamTSCD = new TangGiamTSCD();

                    tangGiamTSCDViewModel.MaTSCD = workSheet.Cells[i, 1].Value.ToString();
                    tangGiamTSCDViewModel.Type = workSheet.Cells[i, 2].Value.ToString();
                    tangGiamTSCDViewModel.Name = workSheet.Cells[i, 3].Value.ToString();
                    DateTime.TryParse(workSheet.Cells[i, 4].Value.ToString(), out NgaySuDung);
                    tangGiamTSCDViewModel.NgaySuDung = NgaySuDung;
                    int.TryParse(workSheet.Cells[i, 5].Value.ToString(), out SoLuong);
                    tangGiamTSCDViewModel.SoLuong = SoLuong;
                    decimal.TryParse(workSheet.Cells[i, 6].Value.ToString(), out GiaTriBanDau);
                    tangGiamTSCDViewModel.GiaTriBanDau = GiaTriBanDau;
                    decimal.TryParse(workSheet.Cells[i, 7].Value.ToString(), out GiaTriConLai);
                    tangGiamTSCDViewModel.GiaTriConLai = GiaTriConLai;
                    int.TryParse(workSheet.Cells[i, 8].Value.ToString(), out ThoiGianSuDung);
                    tangGiamTSCDViewModel.SoLuong = ThoiGianSuDung;
                    decimal.TryParse(workSheet.Cells[i, 9].Value.ToString(), out GiaTriPhanBoTrongKy);
                    tangGiamTSCDViewModel.GiaTriPhanBoTrongKy = GiaTriPhanBoTrongKy;
                    tangGiamTSCDViewModel.BoPhan = workSheet.Cells[i, 10].Value.ToString();
                    tangGiamTSCDViewModel.PhanBo = workSheet.Cells[i, 11].Value.ToString();
                    int.TryParse(workSheet.Cells[i, 12].Value.ToString(), out ThangSuDung);
                    tangGiamTSCDViewModel.ThangSuDung = ThangSuDung;
                    int.TryParse(workSheet.Cells[i, 13].Value.ToString(), out NamSuDung);
                    tangGiamTSCDViewModel.NamSuDung = NamSuDung;
                    tangGiamTSCDViewModel.Note = workSheet.Cells[i, 14].Value.ToString();

                    tangGiamTSCD.UpdateTangGiamTSCD(tangGiamTSCDViewModel);
                    listTangGiamTSCD.Add(tangGiamTSCD);
                }
                return listTangGiamTSCD;
            }
        }

        [HttpGet]
        [Route("ExportXls")]
        public async Task<HttpResponseMessage> ExportXls(HttpRequestMessage request)
        {
            string fileName = string.Concat("TangGiamTSCD_CCDC_" + DateTime.Now.ToString("yyyyMMddhhmmsss") + ".xlsx");
            var folderReport = ConfigHelper.GetByKey("ReportFolder");
            string filePath = HttpContext.Current.Server.MapPath(folderReport);
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }
            string fullPath = Path.Combine(filePath, fileName);
            try
            {
                var data = _tangGiamTSCDService.GetAll().ToList();
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
