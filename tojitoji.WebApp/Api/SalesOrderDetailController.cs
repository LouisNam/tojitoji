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
    [RoutePrefix("api/salesorderdetail")]
    public class SalesOrderDetailController : ApiControllerBase
    {
        #region Initialize

        private ISalesOrderDetailService _salesOrderDetailService;

        public SalesOrderDetailController(IErrorService errorService, ISalesOrderDetailService salesOrderDetailService)
            : base(errorService)
        {
            this._salesOrderDetailService = salesOrderDetailService;
        }

        #endregion Initialize

        [Route("getdetail/{id}")]
        [HttpGet]
        public HttpResponseMessage GetDetailById(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                var model = _salesOrderDetailService.GetAll(id);
                var responseData = Mapper.Map<IEnumerable<SalesOrderDetail>, IEnumerable<SalesOrderDetailViewModel>>(model);
                var response = request.CreateResponse(HttpStatusCode.OK, responseData);
                return response;
            });
        }

        [Route("getall")]
        [HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                var model = _salesOrderDetailService.GetAll(id);
                var responseData = Mapper.Map<IEnumerable<SalesOrderDetail>, IEnumerable<SalesOrderDetailViewModel>>(model);
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
                var model = _salesOrderDetailService.GetById(id);
                var responseData = Mapper.Map<SalesOrderDetail, SalesOrderDetailViewModel>(model);
                var response = request.CreateResponse(HttpStatusCode.OK, responseData);
                return response;
            });
        }

        [Route("create")]
        [HttpPost]
        public HttpResponseMessage Create(HttpRequestMessage request, SalesOrderDetailViewModel salesOrderDetailVM)
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
                    var newSalesOrderDetail = new SalesOrderDetail();
                    newSalesOrderDetail.UpdateSalesOrderDetail(salesOrderDetailVM);
                    _salesOrderDetailService.Add(newSalesOrderDetail);
                    _salesOrderDetailService.SaveChanges();

                    var responseData = Mapper.Map<SalesOrderDetail, SalesOrderDetailViewModel>(newSalesOrderDetail);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }

                return response;
            });
        }

        [Route("update")]
        [HttpPut]
        public HttpResponseMessage Update(HttpRequestMessage request, SalesOrderDetailViewModel salesOrderDetailVM)
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
                    var dbSalesOrderDetail = _salesOrderDetailService.GetById(salesOrderDetailVM.ID);

                    dbSalesOrderDetail.UpdateSalesOrderDetail(salesOrderDetailVM);

                    _salesOrderDetailService.Update(dbSalesOrderDetail);
                    _salesOrderDetailService.SaveChanges();

                    var responseData = Mapper.Map<SalesOrderDetail, SalesOrderDetailViewModel>(dbSalesOrderDetail);
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
                    var oldSalesOrderDetail = _salesOrderDetailService.Delete(id);
                    _salesOrderDetailService.SaveChanges();

                    var responseData = Mapper.Map<SalesOrderDetail, SalesOrderDetailViewModel>(oldSalesOrderDetail);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }

                return response;
            });
        }
    }
}