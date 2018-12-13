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
    [RoutePrefix("api/campaignsku")]
    public class CampaignSKUController : ApiControllerBase
    {
        #region Initialize

        private ICampaignSKUService _campaignSKUService;

        public CampaignSKUController(IErrorService errorService, ICampaignSKUService campaignSKUService)
            : base(errorService)
        {
            this._campaignSKUService = campaignSKUService;
        }

        #endregion Initialize

        [Route("getallcampaign")]
        [HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                var model = _campaignSKUService.GetAll();
                var responseData = Mapper.Map<IEnumerable<CampaignSKU>, IEnumerable<CampaignSKUViewModel>>(model);
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
                var model = _campaignSKUService.GetAll();

                totalRow = model.Count();
                var query = model.OrderByDescending(x => x.ID).Skip(page * pageSize).Take(pageSize);

                var responseData = Mapper.Map<IEnumerable<CampaignSKU>, IEnumerable<CampaignSKUViewModel>>(query);

                var paginationSet = new PaginationSet<CampaignSKUViewModel>()
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
                var model = _campaignSKUService.GetById(id);
                var responseData = Mapper.Map<CampaignSKU, CampaignSKUViewModel>(model);
                var response = request.CreateResponse(HttpStatusCode.OK, responseData);
                return response;
            });
        }

        [Route("create")]
        [HttpPost]
        public HttpResponseMessage Create(HttpRequestMessage request, CampaignSKUViewModel CampaignSKUVM)
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
                    var newCampaignSKU = new CampaignSKU();
                    newCampaignSKU.UpdateCampaignSKU(CampaignSKUVM);
                    _campaignSKUService.Add(newCampaignSKU);
                    _campaignSKUService.SaveChanges();

                    var responseData = Mapper.Map<CampaignSKU, CampaignSKUViewModel>(newCampaignSKU);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }

                return response;
            });
        }

        [Route("update")]
        [HttpPut]
        public HttpResponseMessage Update(HttpRequestMessage request, CampaignSKUViewModel CampaignSKUVM)
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
                    var dbCampaignSKU = _campaignSKUService.GetById(CampaignSKUVM.ID);

                    dbCampaignSKU.UpdateCampaignSKU(CampaignSKUVM);

                    _campaignSKUService.Update(dbCampaignSKU);
                    _campaignSKUService.SaveChanges();

                    var responseData = Mapper.Map<CampaignSKU, CampaignSKUViewModel>(dbCampaignSKU);
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
                    var oldCampaignSKU = _campaignSKUService.Delete(id);
                    _campaignSKUService.SaveChanges();

                    var responseData = Mapper.Map<CampaignSKU, CampaignSKUViewModel>(oldCampaignSKU);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }

                return response;
            });
        }
    }
}