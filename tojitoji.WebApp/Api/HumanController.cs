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
    [RoutePrefix("api/human")]
    public class HumanController : ApiControllerBase
    {
        #region Initialize

        private IHumanService _humanService;

        public HumanController(IErrorService errorService, IHumanService humanService)
            : base(errorService)
        {
            this._humanService = humanService;
        }

        #endregion Initialize

        [Route("getallhumans")]
        [HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {               
                var model = _humanService.GetAll();
                var responseData = Mapper.Map<IEnumerable<Human>, IEnumerable<HumanViewModel>>(model);
                var response = request.CreateResponse(HttpStatusCode.OK, responseData);
                return response;
            });
        }

        [Route("getall")]
        [HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage request, int page, int pageSize = 20)
        {
            return CreateHttpResponse(request, () =>
            {
                int totalRow = 0;
                var model = _humanService.GetAll();

                totalRow = model.Count();
                var query = model.OrderByDescending(x => x.ID).Skip(page * pageSize).Take(pageSize);

                var responseData = Mapper.Map<IEnumerable<Human>, IEnumerable<HumanViewModel>>(query);

                var paginationSet = new PaginationSet<HumanViewModel>()
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
                var model = _humanService.GetById(id);
                var responseData = Mapper.Map<Human, HumanViewModel>(model);
                var response = request.CreateResponse(HttpStatusCode.OK, responseData);
                return response;
            });
        }

        [Route("create")]
        [HttpPost]
        public HttpResponseMessage Create(HttpRequestMessage request, HumanViewModel humanVM)
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
                    var newHuman = new Human();
                    newHuman.UpdateHuman(humanVM);
                    _humanService.Add(newHuman);
                    _humanService.SaveChanges();

                    var responseData = Mapper.Map<Human, HumanViewModel>(newHuman);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }

                return response;
            });
        }

        [Route("update")]
        [HttpPut]
        public HttpResponseMessage Update(HttpRequestMessage request, HumanViewModel humanVM)
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
                    var dbHuman = _humanService.GetById(humanVM.ID);

                    dbHuman.UpdateHuman(humanVM);

                    _humanService.Update(dbHuman);
                    _humanService.SaveChanges();

                    var responseData = Mapper.Map<Human, HumanViewModel>(dbHuman);
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
                    var oldHuman = _humanService.Delete(id);
                    _humanService.SaveChanges();

                    var responseData = Mapper.Map<Human, HumanViewModel>(oldHuman);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }

                return response;
            });
        }

        [Route("deletemulti")]
        [HttpDelete]
        [AllowAnonymous]
        public HttpResponseMessage DeleteMulti(HttpRequestMessage request, string checkedHumans)
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
                    var listBible = new JavaScriptSerializer().Deserialize<List<int>>(checkedHumans);
                    foreach (var item in listBible)
                    {
                        _humanService.Delete(item);
                    }

                    _humanService.SaveChanges();

                    response = request.CreateResponse(HttpStatusCode.OK, listBible.Count);
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
            
            if (result.FormData["humanType"] == null)
            {
                Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Bạn chưa chọn loại đối tượng");
            }

            int addedCount = 0;
            int humanType = 0;
            int.TryParse(result.FormData["humanType"], out humanType);

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
                
                var listHuman = this.ReadHumanFromExcel(fullPath, humanType);
                if (listHuman.Count > 0)
                {
                    foreach (var Human in listHuman)
                    {
                        _humanService.Add(Human);
                        addedCount++;
                    }
                    _humanService.SaveChanges();
                }
            }
            return Request.CreateResponse(HttpStatusCode.OK, "Đã nhập thành công " + addedCount + " đối tượng");
        }

        private List<Human> ReadHumanFromExcel(string fullPath, int humanTypeId)
        {
            using (var package = new ExcelPackage(new FileInfo(fullPath)))
            {
                ExcelWorksheet workSheet = package.Workbook.Worksheets[1];
                List<Human> listHuman = new List<Human>();
                HumanViewModel HumanViewModel;
                Human Human;

                int taxCode;
                DateTime dateOfBirth;
                DateTime dateOfEntry;                

                for (int i = workSheet.Dimension.Start.Row + 1; i <= workSheet.Dimension.End.Row; i++)
                {
                    HumanViewModel = new HumanViewModel();
                    Human = new Human();

                    HumanViewModel.LastName = workSheet.Cells[i, 1].Value.ToString();
                    HumanViewModel.FirstName = workSheet.Cells[i, 2].Value.ToString();
                    HumanViewModel.Company = workSheet.Cells[i, 3].Text.ToString();
                    HumanViewModel.Gender = workSheet.Cells[i, 4].Value.ToString();
                    HumanViewModel.Phone = workSheet.Cells[i, 5].Value.ToString();
                    HumanViewModel.Email = workSheet.Cells[i, 6].Text.ToString();
                    HumanViewModel.JobTitle = workSheet.Cells[i, 7].Text.ToString();
                    HumanViewModel.Address = workSheet.Cells[i, 8].Text.ToString();
                    HumanViewModel.Province = workSheet.Cells[i, 9].Text.ToString();
                    HumanViewModel.City = workSheet.Cells[i, 10].Text.ToString();
                    HumanViewModel.District = workSheet.Cells[i, 11].Text.ToString();
                    HumanViewModel.Ward = workSheet.Cells[i, 12].Text.ToString();
                    HumanViewModel.OtherContact = workSheet.Cells[i, 13].Text.ToString();
                    
                    int.TryParse(workSheet.Cells[i, 14].Text.ToString(), out taxCode);
                    HumanViewModel.TaxCode = taxCode;

                    HumanViewModel.Note = workSheet.Cells[i, 15].Text.ToString();

                    DateTime.TryParse(workSheet.Cells[i, 16].Value.ToString(), out dateOfBirth);
                    HumanViewModel.DateOfBirth = dateOfBirth;

                    DateTime.TryParse(workSheet.Cells[i, 17].Value.ToString(), out dateOfEntry);
                    HumanViewModel.DateOfEntry = dateOfEntry;

                    HumanViewModel.TypeCode = humanTypeId;

                    Human.UpdateHuman(HumanViewModel);
                    listHuman.Add(Human);
                }
                return listHuman;
            }
        }

        [HttpGet]
        [Route("ExportXls")]
        public async Task<HttpResponseMessage> ExportXls(HttpRequestMessage request)
        {
            string fileName = string.Concat("DoiTuong_" + DateTime.Now.ToString("yyyyMMddhhmmsss") + ".xlsx");
            var folderReport = ConfigHelper.GetByKey("ReportFolder");
            string filePath = HttpContext.Current.Server.MapPath(folderReport);
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }
            string fullPath = Path.Combine(filePath, fileName);
            try
            {
                var data = _humanService.GetListHuman().ToList();
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