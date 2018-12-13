using System.Collections.Generic;
using tojitoji.Data.Infrastructure;
using tojitoji.Data.Repositories;
using tojitoji.Model.Models;

namespace tojitoji.Service
{
    public interface ICampaignSKUService
    {
        IEnumerable<CampaignSKU> GetAll();

        CampaignSKU GetById(int id);

        CampaignSKU Add(CampaignSKU campaignSKU);

        void Update(CampaignSKU campaignSKU);

        CampaignSKU Delete(int id);

        void SaveChanges();
    }

    public class CampaignSKUService : ICampaignSKUService
    {
        private ICampaignSKURepository _campaignSKURepository;
        private IUnitOfWork _unitOfWork;

        public CampaignSKUService(ICampaignSKURepository campaignSKURepository, IUnitOfWork unitOfWork)
        {
            this._campaignSKURepository = campaignSKURepository;
            this._unitOfWork = unitOfWork;
        }

        public IEnumerable<CampaignSKU> GetAll()
        {
            return _campaignSKURepository.GetAll();
        }

        public CampaignSKU GetById(int id)
        {
            return _campaignSKURepository.GetSingleById(id);
        }

        public CampaignSKU Add(CampaignSKU campaignSKU)
        {
            return _campaignSKURepository.Add(campaignSKU);
        }

        public void Update(CampaignSKU campaignSKU)
        {
            _campaignSKURepository.Update(campaignSKU);
        }

        public CampaignSKU Delete(int id)
        {
            return _campaignSKURepository.Delete(id);
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }
    }
}