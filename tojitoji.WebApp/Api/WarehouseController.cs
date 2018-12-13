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
    [RoutePrefix("api/warehouse")]
    public class WarehouseController : ApiControllerBase
    {
        #region Initialize

        private IWarehouseService _warehouseService;

        public WarehouseController(IErrorService errorService, IWarehouseService warehouseService)
            : base(errorService)
        {
            this._warehouseService = warehouseService;
        }

        #endregion Initialize

        [Route("getallwarehouse")]
        [HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                var model = _warehouseService.GetAll();
                var responseData = Mapper.Map<IEnumerable<Warehouse>, IEnumerable<WarehouseViewModel>>(model);
                var response = request.CreateResponse(HttpStatusCode.OK, responseData);
                return response;
            });
        }

        [Route("getall")]
        [HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage request, string keyword, int page, int pageSize = 20)
        {
            return CreateHttpResponse(request, () =>
            {
                int totalRow = 0;
                var model = _warehouseService.GetAll(keyword);

                totalRow = model.Count();
                var query = model.OrderByDescending(x => x.ID).Skip(page * pageSize).Take(pageSize);

                var responseData = Mapper.Map<IEnumerable<Warehouse>, IEnumerable<WarehouseViewModel>>(query);

                var paginationSet = new PaginationSet<WarehouseViewModel>()
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
                var model = _warehouseService.GetById(id);
                var responseData = Mapper.Map<Warehouse, WarehouseViewModel>(model);
                var response = request.CreateResponse(HttpStatusCode.OK, responseData);
                return response;
            });
        }

        [Route("create")]
        [HttpPost]
        public HttpResponseMessage Create(HttpRequestMessage request, WarehouseViewModel warehouseVM)
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
                    var newWarehouse = new Warehouse();
                    newWarehouse.UpdateWarehouse(warehouseVM);
                    _warehouseService.Add(newWarehouse);
                    _warehouseService.SaveChanges();

                    var responseData = Mapper.Map<Warehouse, WarehouseViewModel>(newWarehouse);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }

                return response;
            });
        }

        [Route("update")]
        [HttpPut]
        public HttpResponseMessage Update(HttpRequestMessage request, WarehouseViewModel warehouseVM)
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
                    var dbWarehouse = _warehouseService.GetById(warehouseVM.ID);

                    dbWarehouse.UpdateWarehouse(warehouseVM);

                    _warehouseService.Update(dbWarehouse);
                    _warehouseService.SaveChanges();

                    var responseData = Mapper.Map<Warehouse, WarehouseViewModel>(dbWarehouse);
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
                    var oldWarehouse = _warehouseService.Delete(id);
                    _warehouseService.SaveChanges();

                    var responseData = Mapper.Map<Warehouse, WarehouseViewModel>(oldWarehouse);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }

                return response;
            });
        }
    }
}