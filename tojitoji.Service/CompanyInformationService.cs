using System.Collections.Generic;
using tojitoji.Data.Infrastructure;
using tojitoji.Data.Repositories;
using tojitoji.Model.Models;

namespace tojitoji.Service
{
    public interface ICompanyInformationService
    {
        CompanyInformation Add(CompanyInformation companyInformation);

        void Update(CompanyInformation companyInformation);

        CompanyInformation Delete(int id);

        IEnumerable<CompanyInformation> GetAll();

        CompanyInformation GetById(int id);

        void SaveChanges();
    }

    internal class CompanyInformationService : ICompanyInformationService
    {
        private ICompanyInformationRepository _companyInformationRepository;
        private IUnitOfWork _unitOfWork;

        public CompanyInformationService(ICompanyInformationRepository companyInformationRepository, IUnitOfWork unitOfWork)
        {
            this._companyInformationRepository = companyInformationRepository;
            this._unitOfWork = unitOfWork;
        }

        public CompanyInformation Add(CompanyInformation companyInformation)
        {
            return _companyInformationRepository.Add(companyInformation);
        }

        public CompanyInformation Delete(int id)
        {
            return _companyInformationRepository.Delete(id);
        }

        public IEnumerable<CompanyInformation> GetAll()
        {
            return _companyInformationRepository.GetAll();
        }

        public CompanyInformation GetById(int id)
        {
            return _companyInformationRepository.GetSingleById(id);
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public void Update(CompanyInformation companyInformation)
        {
            _companyInformationRepository.Update(companyInformation);
        }
    }
}