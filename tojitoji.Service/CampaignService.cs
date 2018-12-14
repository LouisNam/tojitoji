using System.Collections.Generic;
using tojitoji.Data.Infrastructure;
using tojitoji.Data.Repositories;
using tojitoji.Model.Models;

namespace tojitoji.Service
{
    public interface ICampaignService
    {
        IEnumerable<Campaign> GetAll();

        Campaign GetById(int id);

        Campaign Add(Campaign campaign);

        void Update(Campaign campaign);

        Campaign Delete(int id);

        void SaveChanges();
    }

    public class CampaignService : ICampaignService
    {
        private ICampaignRepository _campaignRepository;
        private IUnitOfWork _unitOfWork;

        public CampaignService(ICampaignRepository campaignRepository, IUnitOfWork unitOfWork)
        {
            this._campaignRepository = campaignRepository;
            this._unitOfWork = unitOfWork;
        }

        public IEnumerable<Campaign> GetAll()
        {
            return _campaignRepository.GetAll();
        }

        public Campaign GetById(int id)
        {
            return _campaignRepository.GetSingleById(id);
        }

        public Campaign Add(Campaign campaign)
        {
            return _campaignRepository.Add(campaign);
        }

        public void Update(Campaign campaign)
        {
            _campaignRepository.Update(campaign);
        }

        public Campaign Delete(int id)
        {
            return _campaignRepository.Delete(id);
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }
    }
}