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
    [RoutePrefix("api/humantype")]
    public class HumanTypeController : ApiControllerBase
    {
        #region Initialize

        private IHumanTypeService _humanTypeService;

        public HumanTypeController(IErrorService errorService, IHumanTypeService humanTypeService)
            : base(errorService)
        {
            this._humanTypeService = humanTypeService;
        }

        #endregion Initialize

        [Route("getallparents")]
        [HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                var model = _humanTypeService.GetAll();
                var responseData = Mapper.Map<IEnumerable<HumanType>, IEnumerable<HumanTypeViewModel>>(model);
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
                var model = _humanTypeService.GetAll();

                totalRow = model.Count();
                var query = model.OrderByDescending(x => x.ID).Skip(page * pageSize).Take(pageSize);

                var responseData = Mapper.Map<IEnumerable<HumanType>, IEnumerable<HumanTypeViewModel>>(query);

                var paginationSet = new PaginationSet<HumanTypeViewModel>()
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
                var model = _humanTypeService.GetById(id);
                var responseData = Mapper.Map<HumanType, HumanTypeViewModel>(model);
                var response = request.CreateResponse(HttpStatusCode.OK, responseData);
                return response;
            });
        }

        [Route("create")]
        [HttpPost]
        public HttpResponseMessage Create(HttpRequestMessage request, HumanTypeViewModel humanTypeVM)
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
                    var newHumanType = new HumanType();
                    newHumanType.UpdateHumanType(humanTypeVM);
                    _humanTypeService.Add(newHumanType);
                    _humanTypeService.SaveChanges();

                    var responseData = Mapper.Map<HumanType, HumanTypeViewModel>(newHumanType);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }

                return response;
            });
        }

        [Route("update")]
        [HttpPut]
        public HttpResponseMessage Update(HttpRequestMessage request, HumanTypeViewModel humanTypeVM)
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
                    var dbHumanType = _humanTypeService.GetById(humanTypeVM.ID);

                    dbHumanType.UpdateHumanType(humanTypeVM);

                    _humanTypeService.Update(dbHumanType);
                    _humanTypeService.SaveChanges();

                    var responseData = Mapper.Map<HumanType, HumanTypeViewModel>(dbHumanType);
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
                    var oldHumanType = _humanTypeService.Delete(id);
                    _humanTypeService.SaveChanges();

                    var responseData = Mapper.Map<HumanType, HumanTypeViewModel>(oldHumanType);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }

                return response;
            });
        }
    }
}
