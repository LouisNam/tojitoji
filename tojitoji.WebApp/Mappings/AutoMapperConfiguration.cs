using AutoMapper;
using tojitoji.Model.Models;
using tojitoji.WebApp.Models;

namespace tojitoji.WebApp.Mappings
{
    public class AutoMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Account, AccountViewModel>();
                cfg.CreateMap<CompanyInformation, CompanyInformationViewModel>();
                cfg.CreateMap<Human, HumanViewModel>();
                cfg.CreateMap<HumanType, HumanTypeViewModel>();
                cfg.CreateMap<Category, CategoryViewModel>();
                cfg.CreateMap<Product, ProductViewModel>();
                cfg.CreateMap<Bundle, BundleViewModel>();
                cfg.CreateMap<SKU, SKUViewModel>();
                cfg.CreateMap<SKULazada, SKULazadaViewModel>();
                cfg.CreateMap<CampaignSKU, CampaignSKUViewModel>();
                cfg.CreateMap<Campaign, CampaignViewModel>();
                cfg.CreateMap<Warehouse, WarehouseViewModel>();
                cfg.CreateMap<InventoryTransaction, InventoryTransactionViewModel>();
                cfg.CreateMap<DocumentType, DocumentTypeViewModel>();
                cfg.CreateMap<PurchaseOrder, PurchaseOrderViewModel>();
                cfg.CreateMap<PurchaseOrderDetail, PurchaseOrderDetailViewModel>();
                cfg.CreateMap<PurchaseOrderDetailReturn, PurchaseOrderDetailReturnViewModel>();
                cfg.CreateMap<SalesOrder, SalesOrderViewModel>();
                cfg.CreateMap<SalesOrderDetail, SalesOrderDetailViewModel>();
                cfg.CreateMap<SalesOrderDetailReturn, SalesOrderDetailReturnViewModel>();
                cfg.CreateMap<Document, DocumentViewModel>();
                cfg.CreateMap<Transaction, TransactionViewModel>();
                cfg.CreateMap<TrialBalance, TrialBalanceViewModel>();
                cfg.CreateMap<Bible, BibleViewModel>();
            });
        }
    }
}