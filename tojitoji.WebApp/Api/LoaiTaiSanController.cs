﻿using AutoMapper;
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
    [RoutePrefix("api/loaitaisan")]
    public class LoaiTaiSanController : ApiControllerBase
    {
        #region Initialize

        private ILoaiTaiSanService _loaiTaiSanService;

        public LoaiTaiSanController(IErrorService errorService, ILoaiTaiSanService loaiTaiSanService)
            : base(errorService)
        {
            this._loaiTaiSanService = loaiTaiSanService;
        }

        #endregion Initialize

        [Route("getalltype")]
        [HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                
                var model = _loaiTaiSanService.GetAll();
                var responseData = Mapper.Map<IEnumerable<LoaiTaiSan>, IEnumerable<LoaiTaiSanViewModel>>(model);
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
                var model = _loaiTaiSanService.GetAll(keyword);

                totalRow = model.Count();
                var query = model.OrderByDescending(x => x.ID).Skip(page * pageSize).Take(pageSize);

                var responseData = Mapper.Map<IEnumerable<LoaiTaiSan>, IEnumerable<LoaiTaiSanViewModel>>(query);

                var paginationSet = new PaginationSet<LoaiTaiSanViewModel>()
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
                var model = _loaiTaiSanService.GetById(id);
                var responseData = Mapper.Map<LoaiTaiSan, LoaiTaiSanViewModel>(model);
                var response = request.CreateResponse(HttpStatusCode.OK, responseData);
                return response;
            });
        }

        [Route("create")]
        [HttpPost]
        public HttpResponseMessage Create(HttpRequestMessage request, LoaiTaiSanViewModel loaiTaiSanVM)
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
                    var newLoaiTaiSan = new LoaiTaiSan();
                    newLoaiTaiSan.UpdateLoaiTaiSan(loaiTaiSanVM);
                    _loaiTaiSanService.Add(newLoaiTaiSan);
                    _loaiTaiSanService.SaveChanges();

                    var responseData = Mapper.Map<LoaiTaiSan, LoaiTaiSanViewModel>(newLoaiTaiSan);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }

                return response;
            });
        }

        [Route("update")]
        [HttpPut]
        public HttpResponseMessage Update(HttpRequestMessage request, LoaiTaiSanViewModel loaiTaiSanVM)
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
                    var dbLoaiTaiSan = _loaiTaiSanService.GetById(loaiTaiSanVM.ID);

                    dbLoaiTaiSan.UpdateLoaiTaiSan(loaiTaiSanVM);

                    _loaiTaiSanService.Update(dbLoaiTaiSan);
                    _loaiTaiSanService.SaveChanges();

                    var responseData = Mapper.Map<LoaiTaiSan, LoaiTaiSanViewModel>(dbLoaiTaiSan);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }

                return response;
            });
        }

        [Route("deletemulti")]
        [HttpDelete]
        public HttpResponseMessage DeleteMulti(HttpRequestMessage request, string checkedLoaiTaiSans)
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
                    var listLoaiTaiSan = new JavaScriptSerializer().Deserialize<List<int>>(checkedLoaiTaiSans);
                    foreach (var item in listLoaiTaiSan)
                    {
                        _loaiTaiSanService.Delete(item);
                    }

                    _loaiTaiSanService.SaveChanges();

                    response = request.CreateResponse(HttpStatusCode.OK, listLoaiTaiSan.Count);
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
                var listLoaiTaiSan = this.ReadLoaiTaiSanFromExcel(fullPath);
                if (listLoaiTaiSan.Count > 0)
                {
                    foreach (var LoaiTaiSan in listLoaiTaiSan)
                    {
                        _loaiTaiSanService.Add(LoaiTaiSan);
                        addedCount++;
                    }
                    _loaiTaiSanService.SaveChanges();
                }
            }
            return Request.CreateResponse(HttpStatusCode.OK, "Đã nhập thành công " + addedCount + " loại tài sản");
        }

        private List<LoaiTaiSan> ReadLoaiTaiSanFromExcel(string fullPath)
        {
            using (var package = new ExcelPackage(new FileInfo(fullPath)))
            {
                ExcelWorksheet workSheet = package.Workbook.Worksheets[1];
                List<LoaiTaiSan> listLoaiTaiSan = new List<LoaiTaiSan>();
                LoaiTaiSanViewModel LoaiTaiSanViewModel;
                LoaiTaiSan LoaiTaiSan;

                for (int i = workSheet.Dimension.Start.Row + 1; i <= workSheet.Dimension.End.Row; i++)
                {
                    LoaiTaiSanViewModel = new LoaiTaiSanViewModel();
                    LoaiTaiSan = new LoaiTaiSan();

                    LoaiTaiSanViewModel.Name = workSheet.Cells[i, 1].Value.ToString();

                    LoaiTaiSan.UpdateLoaiTaiSan(LoaiTaiSanViewModel);
                    listLoaiTaiSan.Add(LoaiTaiSan);
                }
                return listLoaiTaiSan;
            }
        }

        [HttpGet]
        [Route("ExportXls")]
        public async Task<HttpResponseMessage> ExportXls(HttpRequestMessage request)
        {
            string fileName = string.Concat("LoaiTaiSan_" + DateTime.Now.ToString("yyyyMMddhhmmsss") + ".xlsx");
            var folderReport = ConfigHelper.GetByKey("ReportFolder");
            string filePath = HttpContext.Current.Server.MapPath(folderReport);
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }
            string fullPath = Path.Combine(filePath, fileName);
            try
            {
                var data = _loaiTaiSanService.GetAll().ToList();
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
