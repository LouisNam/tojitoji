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
    [RoutePrefix("api/purchaseorderdetail")]
    public class PurchaseOrderDetailController : ApiControllerBase
    {
        #region Initialize

        private IPurchaseOrderDetailService _purchaseOrderDetailService;

        public PurchaseOrderDetailController(IErrorService errorService, IPurchaseOrderDetailService purchaseOrderDetailService)
            : base(errorService)
        {
            this._purchaseOrderDetailService = purchaseOrderDetailService;
        }

        #endregion Initialize

        //[Route("getallPurchaseOrderDetail")]
        //[HttpGet]
        //public HttpResponseMessage GetAll(HttpRequestMessage request)
        //{
        //    return CreateHttpResponse(request, () =>
        //    {
        //        var model = _purchaseOrderDetailService.GetAll();
        //        var responseData = Mapper.Map<IEnumerable<PurchaseOrderDetail>, IEnumerable<PurchaseOrderDetailViewModel>>(model);
        //        var response = request.CreateResponse(HttpStatusCode.OK, responseData);
        //        return response;
        //    });
        //}

        [Route("getdetail/{id}")]
        [HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                var model = _purchaseOrderDetailService.GetAll(id);
                var responseData = Mapper.Map<IEnumerable<PurchaseOrderDetail>, IEnumerable<PurchaseOrderDetailViewModel>>(model);
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
                var model = _purchaseOrderDetailService.GetById(id);
                var responseData = Mapper.Map<PurchaseOrderDetail, PurchaseOrderDetailViewModel>(model);
                var response = request.CreateResponse(HttpStatusCode.OK, responseData);
                return response;
            });
        }

        //[Route("create")]
        //[HttpPost]
        //public HttpResponseMessage Create(HttpRequestMessage request, PurchaseOrderDetailViewModel purchaseOrderDetailVM)
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
        //            var newPurchaseOrderDetail = new PurchaseOrderDetail();
        //            newPurchaseOrderDetail.UpdatePurchaseOrderDetail(purchaseOrderDetailVM);
        //            _purchaseOrderDetailService.Add(newPurchaseOrderDetail);
        //            _purchaseOrderDetailService.SaveChanges();

        //            var responseData = Mapper.Map<PurchaseOrderDetail, PurchaseOrderDetailViewModel>(newPurchaseOrderDetail);
        //            response = request.CreateResponse(HttpStatusCode.Created, responseData);
        //        }

        //        return response;
        //    });
        //}

        [Route("update")]
        [HttpPut]
        public HttpResponseMessage Update(HttpRequestMessage request, PurchaseOrderDetailViewModel purchaseOrderDetailVM)
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
                    var dbPurchaseOrderDetail = _purchaseOrderDetailService.GetById(purchaseOrderDetailVM.ID);

                    dbPurchaseOrderDetail.UpdatePurchaseOrderDetail(purchaseOrderDetailVM);

                    _purchaseOrderDetailService.Update(dbPurchaseOrderDetail);
                    _purchaseOrderDetailService.SaveChanges();

                    var responseData = Mapper.Map<PurchaseOrderDetail, PurchaseOrderDetailViewModel>(dbPurchaseOrderDetail);
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
                    var oldPurchaseOrderDetail = _purchaseOrderDetailService.Delete(id);
                    _purchaseOrderDetailService.SaveChanges();

                    var responseData = Mapper.Map<PurchaseOrderDetail, PurchaseOrderDetailViewModel>(oldPurchaseOrderDetail);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }

                return response;
            });
        }

        [Route("create")]
        [HttpPost]
        public HttpResponseMessage GetRevenueStatistic(HttpRequestMessage request, int productID, int purchaseOrderID, decimal price, int quantity)
        {

            return CreateHttpResponse(request, () =>
            {
                _purchaseOrderDetailService.CreatePurchaseOrderDetail(productID, purchaseOrderID, price, quantity);
                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK);
                return response;
            });
        }
    }
}