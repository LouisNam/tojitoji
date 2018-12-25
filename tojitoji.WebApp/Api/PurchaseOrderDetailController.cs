using AutoMapper;
using System;
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

        [Route("getdetail/{id}")]
        [HttpGet]
        public HttpResponseMessage GetDetailById(HttpRequestMessage request, int id)
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
                    dbPurchaseOrderDetail.UpdatedDate = DateTime.Now;

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
        public HttpResponseMessage Create(HttpRequestMessage request, [FromBody] PurchaseOrderDetailViewModel model)
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
                    int productID = model.ProductID;
                    int purchaseOrderID = model.PurchaseOrderID;
                    decimal purchasingPrice = model.PurchasingPrice;
                    int quantity = model.Quantity;
                    string status = model.Status;
                    decimal? discountPercent = model.DiscountPercent ?? 0;
                    decimal? discountAmount = model.DiscountAmount ?? 0;
                    string discountReason = model.DiscountReason ?? String.Empty;
                    decimal? shippingFeeDistributor = model.ShippingFeeDistributor ?? 0;
                    decimal? shippingFee = model.ShippingFee ?? 0;
                    decimal? subsidize = model.Subsidize ?? 0;
                    decimal? unitCost = model.UnitCost ?? 0;
                    bool statusPayment = model.StatusPayment;
                    int? documentNo = model.DocumentNo ?? 0;
                    bool? paymentMethod = model.PaymentMethod ?? false;
                    DateTime createdDate = model.CreatedDate;
                    DateTime? updatedDate = model.UpdatedDate ?? null;
                    DateTime? shippingTime = model.ShippingTime ?? null;
                    DateTime? canceledTime = model.CanceledTime ?? null;
                    DateTime? deliveriedETA = model.DeliveriedETA ?? null;
                    DateTime? deliveriedTime = model.DeliveriedTime ?? null;
                    DateTime? failedTime = model.FailedTime ?? null;
                    DateTime? paidTime = model.PaidTime ?? null;
                    string shippingParcel = model.ShippingParcel ?? String.Empty;
                    string TKN = "156";
                    string TKC = "331";                    

                    _purchaseOrderDetailService.CreatePurchaseOrderDetail(productID, purchaseOrderID, purchasingPrice, quantity, status, discountPercent, discountAmount, discountReason, shippingFeeDistributor, shippingFee, subsidize, unitCost, statusPayment, documentNo, paymentMethod, createdDate, updatedDate, shippingTime, canceledTime, deliveriedETA, deliveriedTime, failedTime, paidTime, shippingParcel, TKN, TKC);
                    response = request.CreateResponse(HttpStatusCode.Created);
                }

                return response;
            });
        }
    }
}