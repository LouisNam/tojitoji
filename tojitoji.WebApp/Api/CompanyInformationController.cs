using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
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
    }
}