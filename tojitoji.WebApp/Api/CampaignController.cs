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
    [RoutePrefix("api/campaign")]
    public class CampaignController : ApiControllerBase
    {
        #region Initialize

        private ICampaignService _campaignService;

        public CampaignController(IErrorService errorService, ICampaignService campaignService)
            : base(errorService)
        {
            this._campaignService = campaignService;
        }

        #endregion Initialize

        [Route("getall")]
        [HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage request, int page, int pageSize = 20)
        {
            return CreateHttpResponse(request, () =>
            {
                int totalRow = 0;
                var model = _campaignService.GetAll();

                totalRow = model.Count();
                var query = model.OrderByDescending(x => x.CampaignID).Skip(page * pageSize).Take(pageSize);

                var responseData = Mapper.Map<IEnumerable<Campaign>, IEnumerable<CampaignViewModel>>(query);

                var paginationSet = new PaginationSet<CampaignViewModel>()
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
                var model = _campaignService.GetById(id);
                var responseData = Mapper.Map<Campaign, CampaignViewModel>(model);
                var response = request.CreateResponse(HttpStatusCode.OK, responseData);
                return response;
            });
        }

        [Route("create")]
        [HttpPost]
        public HttpResponseMessage Create(HttpRequestMessage request, CampaignViewModel campaignVM)
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
                    var newCampaign = new Campaign();
                    newCampaign.UpdateCampaign(campaignVM);
                    _campaignService.Add(newCampaign);
                    _campaignService.SaveChanges();

                    var responseData = Mapper.Map<Campaign, CampaignViewModel>(newCampaign);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }

                return response;
            });
        }

        [Route("update")]
        [HttpPut]
        public HttpResponseMessage Update(HttpRequestMessage request, CampaignViewModel campaignVM)
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
                    var dbCampaign = _campaignService.GetById(campaignVM.CampaignID);

                    dbCampaign.UpdateCampaign(campaignVM);

                    _campaignService.Update(dbCampaign);
                    _campaignService.SaveChanges();

                    var responseData = Mapper.Map<Campaign, CampaignViewModel>(dbCampaign);
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
                    var oldCampaign = _campaignService.Delete(id);
                    _campaignService.SaveChanges();

                    var responseData = Mapper.Map<Campaign, CampaignViewModel>(oldCampaign);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }

                return response;
            });
        }
    }
}