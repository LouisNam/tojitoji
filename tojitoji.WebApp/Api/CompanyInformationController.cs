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
using tojitoji.Model.Models;
using tojitoji.Service;
using tojitoji.WebApp.Infrastructure.Core;
using tojitoji.WebApp.Infrastructure.Extensions;
using tojitoji.WebApp.Models;

namespace tojitoji.WebApp.Api
{
    [RoutePrefix("api/companyinformation")]
    public class CompanyInformationController : ApiControllerBase
    {
        #region Initialize

        private ICompanyInformationService _companyInformationService;

        public CompanyInformationController(IErrorService errorService, ICompanyInformationService companyInformationService)
            : base(errorService)
        {
            this._companyInformationService = companyInformationService;
        }

        #endregion Initialize

        [Route("getall")]
        [HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage request, int page, int pageSize = 20)
        {
            return CreateHttpResponse(request, () =>
            {
                int totalRow = 0;
                var model = _companyInformationService.GetAll();

                totalRow = model.Count();
                var query = model.OrderByDescending(x => x.ID).Skip(page * pageSize).Take(pageSize);

                var responseData = Mapper.Map<IEnumerable<CompanyInformation>, IEnumerable<CompanyInformationViewModel>>(query);

                var paginationSet = new PaginationSet<CompanyInformationViewModel>()
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
                var model = _companyInformationService.GetById(id);
                var responseData = Mapper.Map<CompanyInformation, CompanyInformationViewModel>(model);
                var response = request.CreateResponse(HttpStatusCode.OK, responseData);
                return response;
            });
        }

        [Route("create")]
        [HttpPost]
        public HttpResponseMessage Create(HttpRequestMessage request, CompanyInformationViewModel companyInformationVM)
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
                    var newCompanyInformation = new CompanyInformation();
                    newCompanyInformation.UpdateCompanyInformation(companyInformationVM);
                    _companyInformationService.Add(newCompanyInformation);
                    _companyInformationService.SaveChanges();

                    var responseData = Mapper.Map<CompanyInformation, CompanyInformationViewModel>(newCompanyInformation);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }

                return response;
            });
        }

        [Route("update")]
        [HttpPut]
        public HttpResponseMessage Update(HttpRequestMessage request, CompanyInformationViewModel companyInformationVM)
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
                    var dbCompanyInformation = _companyInformationService.GetById(companyInformationVM.ID);

                    dbCompanyInformation.UpdateCompanyInformation(companyInformationVM);

                    _companyInformationService.Update(dbCompanyInformation);
                    _companyInformationService.SaveChanges();

                    var responseData = Mapper.Map<CompanyInformation, CompanyInformationViewModel>(dbCompanyInformation);
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
                    var oldCompanyInformation = _companyInformationService.Delete(id);
                    _companyInformationService.SaveChanges();

                    var responseData = Mapper.Map<CompanyInformation, CompanyInformationViewModel>(oldCompanyInformation);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }

                return response;
            });
        }

        [Route("deletemulti")]
        [HttpDelete]
        [AllowAnonymous]
        public HttpResponseMessage DeleteMulti(HttpRequestMessage request, string checkedCompanyInformations)
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
                    var listCompanyInformation = new JavaScriptSerializer().Deserialize<List<int>>(checkedCompanyInformations);
                    foreach (var item in listCompanyInformation)
                    {
                        _companyInformationService.Delete(item);
                    }

                    _companyInformationService.SaveChanges();

                    response = request.CreateResponse(HttpStatusCode.OK, listCompanyInformation.Count);
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

            int addedCount = 0; ;
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

                var listCompanyInformation = this.ReadCompanyInformationFromExcel(fullPath);
                if (listCompanyInformation.Count > 0)
                {
                    foreach (var companyInformation in listCompanyInformation)
                    {
                        _companyInformationService.Add(companyInformation);
                        addedCount++;
                    }
                    _companyInformationService.SaveChanges();
                }
            }
            return Request.CreateResponse(HttpStatusCode.OK, "Đã nhập thành công " + addedCount + " thông tin công ty thành công.");
        }

        private List<CompanyInformation> ReadCompanyInformationFromExcel(string fullPath)
        {
            using (var package = new ExcelPackage(new FileInfo(fullPath)))
            {
                ExcelWorksheet workSheet = package.Workbook.Worksheets[1];
                List<CompanyInformation> listCompanyInformation = new List<CompanyInformation>();
                CompanyInformationViewModel CompanyInformationViewModel;
                CompanyInformation CompanyInformation;

                DateTime MSTDate;

                for (int i = workSheet.Dimension.Start.Row + 1; i <= workSheet.Dimension.End.Row; i++)
                {
                    CompanyInformationViewModel = new CompanyInformationViewModel();
                    CompanyInformation = new CompanyInformation();

                    CompanyInformationViewModel.CompanyName = workSheet.Cells[i, 1].Value.ToString();
                    CompanyInformationViewModel.ShortName = workSheet.Cells[i, 2].Value.ToString();
                    CompanyInformationViewModel.SoHuuVonType = workSheet.Cells[i, 3].Value.ToString();
                    CompanyInformationViewModel.Address = workSheet.Cells[i, 4].Value.ToString();
                    CompanyInformationViewModel.MaSoThue = workSheet.Cells[i, 5].Value.ToString();

                    DateTime.TryParse(workSheet.Cells[i, 6].Value.ToString(), out MSTDate);
                    CompanyInformationViewModel.MSTDate = MSTDate;

                    CompanyInformationViewModel.Phone = workSheet.Cells[i, 7].Value.ToString();
                    CompanyInformationViewModel.Fax = workSheet.Cells[i, 8].Value.ToString();
                    CompanyInformationViewModel.Email = workSheet.Cells[i, 9].Value.ToString();
                    CompanyInformationViewModel.BankAccount = workSheet.Cells[i, 10].Value.ToString();
                    CompanyInformationViewModel.CEO = workSheet.Cells[i, 11].Value.ToString();
                    CompanyInformationViewModel.ChiefAccountant = workSheet.Cells[i, 12].Value.ToString();
                    CompanyInformationViewModel.NguoiLapBieu = workSheet.Cells[i, 13].Value.ToString();
                    CompanyInformationViewModel.Cashier = workSheet.Cells[i, 14].Value.ToString();
                    CompanyInformationViewModel.CheDoKeToanApDung = workSheet.Cells[i, 15].Value.ToString();
                    CompanyInformationViewModel.HinhThucKeToan = workSheet.Cells[i, 16].Value.ToString();
                    CompanyInformationViewModel.PPThueGTGT = workSheet.Cells[i, 17].Value.ToString();
                    CompanyInformationViewModel.PPKhauHao = workSheet.Cells[i, 18].Value.ToString();
                    CompanyInformationViewModel.PPTinhGia = workSheet.Cells[i, 19].Value.ToString();
                    CompanyInformationViewModel.PPHachToanTonKho = workSheet.Cells[i, 20].Value.ToString();
                    CompanyInformationViewModel.PPTinhGiaTonKho = workSheet.Cells[i, 21].Value.ToString();
                    CompanyInformationViewModel.VonDieuLe = workSheet.Cells[i, 22].Value.ToString();
                    CompanyInformationViewModel.ThueSuat = workSheet.Cells[i, 23].Value.ToString();
                    CompanyInformationViewModel.FinancialYear = workSheet.Cells[i, 24].Value.ToString();
                    CompanyInformationViewModel.Website = workSheet.Cells[i, 25].Value.ToString();
                    CompanyInformationViewModel.Fanpage = workSheet.Cells[i, 26].Value.ToString();
                    CompanyInformationViewModel.Youtube = workSheet.Cells[i, 27].Value.ToString();
                    CompanyInformationViewModel.Group = workSheet.Cells[i, 28].Value.ToString();

                    CompanyInformation.UpdateCompanyInformation(CompanyInformationViewModel);
                    listCompanyInformation.Add(CompanyInformation);
                }
                return listCompanyInformation;
            }
        }
    }
}