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
    [RoutePrefix("api/purchaseorder")]
    public class PurchaseOrderController : ApiControllerBase
    {
        #region Initialize

        private IPurchaseOrderService _purchaseOrderService;

        public PurchaseOrderController(IErrorService errorService, IPurchaseOrderService purchaseOrderService)
            : base(errorService)
        {
            this._purchaseOrderService = purchaseOrderService;
        }

        #endregion Initialize

        //[Route("getallPurchaseOrder")]
        //[HttpGet]
        //public HttpResponseMessage GetAll(HttpRequestMessage request)
        //{
        //    return CreateHttpResponse(request, () =>
        //    {
        //        var model = _PurchaseOrderService.GetAll();
        //        var responseData = Mapper.Map<IEnumerable<PurchaseOrder>, IEnumerable<PurchaseOrderViewModel>>(model);
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
                var model = _purchaseOrderService.GetAll();

                totalRow = model.Count();
                var query = model.OrderByDescending(x => x.ID).Skip(page * pageSize).Take(pageSize);

                var responseData = Mapper.Map<IEnumerable<PurchaseOrder>, IEnumerable<PurchaseOrderViewModel>>(query);

                var paginationSet = new PaginationSet<PurchaseOrderViewModel>()
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
        public HttpResponseMessage Create(HttpRequestMessage request, PurchaseOrderViewModel PurchaseOrderVM)
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
                    var newPurchaseOrder = new PurchaseOrder();
                    newPurchaseOrder.UpdatePurchaseOrder(PurchaseOrderVM);
                    newPurchaseOrder.CreatedDate = DateTime.Now;
                    _purchaseOrderService.Add(newPurchaseOrder);
                    _purchaseOrderService.SaveChanges();

                    var responseData = Mapper.Map<PurchaseOrder, PurchaseOrderViewModel>(newPurchaseOrder);
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
                var model = _purchaseOrderService.GetById(id);
                var responseData = Mapper.Map<PurchaseOrder, PurchaseOrderViewModel>(model);
                var response = request.CreateResponse(HttpStatusCode.OK, responseData);
                return response;
            });
        }

        [Route("update")]
        [HttpPut]
        public HttpResponseMessage Update(HttpRequestMessage request, PurchaseOrderViewModel PurchaseOrderVM)
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
                    var dbPurchaseOrder = _purchaseOrderService.GetById(PurchaseOrderVM.ID);

                    dbPurchaseOrder.UpdatePurchaseOrder(PurchaseOrderVM);

                    _purchaseOrderService.Update(dbPurchaseOrder);
                    _purchaseOrderService.SaveChanges();

                    var responseData = Mapper.Map<PurchaseOrder, PurchaseOrderViewModel>(dbPurchaseOrder);
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
        //            var oldPurchaseOrder = _PurchaseOrderService.Delete(id);
        //            _PurchaseOrderService.SaveChanges();

        //            var responseData = Mapper.Map<PurchaseOrder, PurchaseOrderViewModel>(oldPurchaseOrder);
        //            response = request.CreateResponse(HttpStatusCode.Created, responseData);
        //        }

        //        return response;
        //    });
        //}
    }
}