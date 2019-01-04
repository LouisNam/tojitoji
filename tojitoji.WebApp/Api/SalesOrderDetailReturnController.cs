using AutoMapper;
using System.Collections.Generic;
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
    [RoutePrefix("api/salesorderdetailreturn")]
    public class SalesOrderDetailReturnController : ApiControllerBase
    {
        #region Initialize

        private ISalesOrderDetailReturnService _salesOrderDetailReturnService;

        public SalesOrderDetailReturnController(IErrorService errorService, ISalesOrderDetailReturnService salesOrderDetailReturnService)
            : base(errorService)
        {
            this._salesOrderDetailReturnService = salesOrderDetailReturnService;
        }

        #endregion Initialize

        [Route("getall")]
        [HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                var model = _salesOrderDetailReturnService.GetAll();
                var responseData = Mapper.Map<IEnumerable<SalesOrderDetailReturn>, IEnumerable<SalesOrderDetailReturnViewModel>>(model);
                var response = request.CreateResponse(HttpStatusCode.OK, responseData);
                return response;
            });
        }

        [Route("getbyid/{id}")]
        [HttpGet]
        public HttpResponseMessage GetById(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                var model = _salesOrderDetailReturnService.GetById(id);
                var responseData = Mapper.Map<SalesOrderDetailReturn, SalesOrderDetailReturnViewModel>(model);
                var response = request.CreateResponse(HttpStatusCode.OK, responseData);
                return response;
            });
        }

        [Route("create")]
        [HttpPost]
        public HttpResponseMessage Create(HttpRequestMessage request, SalesOrderDetailReturnViewModel salesOrderDetailReturnVM)
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
                    var newSalesOrderDetailReturn = new SalesOrderDetailReturn();
                    newSalesOrderDetailReturn.UpdateSalesOrderDetailReturn(salesOrderDetailReturnVM);
                    _salesOrderDetailReturnService.Add(newSalesOrderDetailReturn);
                    _salesOrderDetailReturnService.SaveChanges();

                    var responseData = Mapper.Map<SalesOrderDetailReturn, SalesOrderDetailReturnViewModel>(newSalesOrderDetailReturn);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }

                return response;
            });
        }

        [Route("update")]
        [HttpPut]
        public HttpResponseMessage Update(HttpRequestMessage request, SalesOrderDetailReturnViewModel salesOrderDetailReturnVM)
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
                    var dbSalesOrderDetailReturn = _salesOrderDetailReturnService.GetById(salesOrderDetailReturnVM.ID);

                    dbSalesOrderDetailReturn.UpdateSalesOrderDetailReturn(salesOrderDetailReturnVM);

                    _salesOrderDetailReturnService.Update(dbSalesOrderDetailReturn);
                    _salesOrderDetailReturnService.SaveChanges();

                    var responseData = Mapper.Map<SalesOrderDetailReturn, SalesOrderDetailReturnViewModel>(dbSalesOrderDetailReturn);
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
                    var oldSalesOrderDetailReturn = _salesOrderDetailReturnService.Delete(id);
                    _salesOrderDetailReturnService.SaveChanges();

                    var responseData = Mapper.Map<SalesOrderDetailReturn, SalesOrderDetailReturnViewModel>(oldSalesOrderDetailReturn);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }

                return response;
            });
        }
    }
}