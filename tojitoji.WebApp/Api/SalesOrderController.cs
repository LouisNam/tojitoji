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
    [RoutePrefix("api/salesorder")]
    public class SalesOrderController : ApiControllerBase
    {
        #region Initialize

        private ISalesOrderService _salesOrderService;

        public SalesOrderController(IErrorService errorService, ISalesOrderService salesOrderService)
            : base(errorService)
        {
            this._salesOrderService = salesOrderService;
        }

        #endregion Initialize

        [Route("getall")]
        [HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage request, int page, int pageSize = 20)
        {
            return CreateHttpResponse(request, () =>
            {
                int totalRow = 0;
                var model = _salesOrderService.GetAll();

                totalRow = model.Count();
                var query = model.OrderByDescending(x => x.ID).Skip(page * pageSize).Take(pageSize);

                var responseData = Mapper.Map<IEnumerable<SalesOrder>, IEnumerable<SalesOrderViewModel>>(query);

                var paginationSet = new PaginationSet<SalesOrderViewModel>()
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
                var model = _salesOrderService.GetById(id);
                var responseData = Mapper.Map<SalesOrder, SalesOrderViewModel>(model);
                var response = request.CreateResponse(HttpStatusCode.OK, responseData);
                return response;
            });
        }

        [Route("create")]
        [HttpPost]
        public HttpResponseMessage Create(HttpRequestMessage request, SalesOrderViewModel salesOrderVM)
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
                    var newSalesOrder = new SalesOrder();
                    newSalesOrder.UpdateSalesOrder(salesOrderVM);
                    _salesOrderService.Add(newSalesOrder);
                    _salesOrderService.SaveChanges();

                    var responseData = Mapper.Map<SalesOrder, SalesOrderViewModel>(newSalesOrder);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }

                return response;
            });
        }

        [Route("update")]
        [HttpPut]
        public HttpResponseMessage Update(HttpRequestMessage request, SalesOrderViewModel salesOrderVM)
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
                    var dbSalesOrder = _salesOrderService.GetById(salesOrderVM.ID);

                    dbSalesOrder.UpdateSalesOrder(salesOrderVM);

                    _salesOrderService.Update(dbSalesOrder);
                    _salesOrderService.SaveChanges();

                    var responseData = Mapper.Map<SalesOrder, SalesOrderViewModel>(dbSalesOrder);
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
                    var oldSalesOrder = _salesOrderService.Delete(id);
                    _salesOrderService.SaveChanges();

                    var responseData = Mapper.Map<SalesOrder, SalesOrderViewModel>(oldSalesOrder);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }

                return response;
            });
        }
    }
}
