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
    [RoutePrefix("api/trialbalance")]
    public class TrialBalanceController : ApiControllerBase
    {
        #region Initialize

        private ITrialBalanceService _trialBalanceService;

        public TrialBalanceController(IErrorService errorService, ITrialBalanceService trialBalanceService)
            : base(errorService)
        {
            this._trialBalanceService = trialBalanceService;
        }

        #endregion Initialize

        //[Route("getallTrialBalance")]
        //[HttpGet]
        //public HttpResponseMessage GetAll(HttpRequestMessage request)
        //{
        //    return CreateHttpResponse(request, () =>
        //    {
        //        var model = _trialBalanceService.GetAll();
        //        var responseData = Mapper.Map<IEnumerable<TrialBalance>, IEnumerable<TrialBalanceViewModel>>(model);
        //        var response = request.CreateResponse(HttpStatusCode.OK, responseData);
        //        return response;
        //    });
        //}

        [Route("getall")]
        [HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage request, int page, int pageSize = 20)
        {
            return CreateHttpResponse(request, () =>
            {
                int totalRow = 0;
                var model = _trialBalanceService.GetAll();

                totalRow = model.Count();
                var query = model.OrderByDescending(x => x.ID).Skip(page * pageSize).Take(pageSize);

                var responseData = Mapper.Map<IEnumerable<TrialBalance>, IEnumerable<TrialBalanceViewModel>>(query);

                var paginationSet = new PaginationSet<TrialBalanceViewModel>()
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

        [Route("create")]
        [HttpPost]
        public HttpResponseMessage Create(HttpRequestMessage request, TrialBalanceViewModel TrialBalanceVM)
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
                    var newTrialBalance = new TrialBalance();
                    newTrialBalance.UpdateTrialBalance(TrialBalanceVM);                    

                    _trialBalanceService.Add(newTrialBalance);                    
                    _trialBalanceService.SaveChanges();

                    var responseData = Mapper.Map<TrialBalance, TrialBalanceViewModel>(newTrialBalance);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }

                return response;
            });
        }

        [Route("getbyid/{id}")]
        [HttpGet]
        public HttpResponseMessage GetById(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                var model = _trialBalanceService.GetById(id);
                var responseData = Mapper.Map<TrialBalance, TrialBalanceViewModel>(model);
                var response = request.CreateResponse(HttpStatusCode.OK, responseData);
                return response;
            });
        }

        [Route("update")]
        [HttpPut]
        public HttpResponseMessage Update(HttpRequestMessage request, TrialBalanceViewModel TrialBalanceVM)
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
                    var dbTrialBalance = _trialBalanceService.GetById(TrialBalanceVM.ID);

                    dbTrialBalance.UpdateTrialBalance(TrialBalanceVM);

                    _trialBalanceService.Update(dbTrialBalance);
                    _trialBalanceService.SaveChanges();

                    var responseData = Mapper.Map<TrialBalance, TrialBalanceViewModel>(dbTrialBalance);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }

                return response;
            });
        }

        //[Route("delete")]
        //[HttpDelete]
        //public HttpResponseMessage Delete(HttpRequestMessage request, int id)
        //{
        //    return CreateHttpResponse(request, () =>
        //    {
        //        HttpResponseMessage response = null;
        //        if (!ModelState.IsValid)
        //        {
        //            response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
        //        }
        //        else
        //        {
        //            var oldTrialBalance = _trialBalanceService.Delete(id);
        //            _trialBalanceService.SaveChanges();

        //            var responseData = Mapper.Map<TrialBalance, TrialBalanceViewModel>(oldTrialBalance);
        //            response = request.CreateResponse(HttpStatusCode.Created, responseData);
        //        }

        //        return response;
        //    });
        //}
    }
}
