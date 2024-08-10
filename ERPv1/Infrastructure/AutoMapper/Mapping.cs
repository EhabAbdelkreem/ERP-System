using AutoMapper;
using ERP.PurchasesModule.ViewModel;
using ERPv1.CRM.Model;
using ERPv1.CRM.ViewModel;
using ERPv1.ERP.CurrentAssetModules.Inventory.Model.Main;
using ERPv1.ERP.CurrentAssetModules.Inventory.ViewModel;
using ERPv1.ERP.CurrentLiabilitiesModules.NotesPayableModule.Model;
using ERPv1.ERP.CurrentLiabilitiesModules.NotesPayableModule.ViewModel;
using ERPv1.ERP.GeneralLedgerModule.AccountCharts.Model;
using ERPv1.ERP.GeneralLedgerModule.AccountCharts.ViewModel;
using ERPv1.ERP.PurchasesModule.Model;
using ERPv1.ERP.PurchasesModule.ViewModel.Expenses;
using Infrastructure.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPv1.Infrastructure.AutoMapper
{
    public class Mapping: Profile
    {
        public Mapping()
        {
            CreateMap<CreateAccountVM, AccountChart>();
            CreateMap<AccountChart, AccountListVM>()
                .ForMember(dest => dest.AccTypeName, src => src.MapFrom(x => x.AccType.AccountType))
                .ForMember(dest => dest.CurrencyAbbr, src => src.MapFrom(x => x.Currency.CurrencyAbbrev))
                .ForMember(dest => dest.BranchName, src => src.MapFrom(x => x.Branch.BranchName));
            CreateMap<AccountChart, UpdateAccountVM>();

            CreateMap<ContactCreatingViewModel, Contacts>()
               .ForMember(dest => dest.SupplierAccNum, src => src.MapFrom(x => x.AccNum));

            CreateMap<StoreItemCreationVM, StoreItem>();


            CreateMap<PurchaseSummary, Purchase>()
                .ForMember(dest=>dest.PurchaseDate,src=>src.MapFrom(y=>y.PurchaseDate.ToEgyptionDate()));

            CreateMap<NotesPayableCreationVM, NotesPayable>()
                .ForMember(dest => dest.WritingDate,src => src.MapFrom(y => y.WritingDate.ToEgyptionDate()))
                .ForMember(dest => dest.DueDate, src => src.MapFrom(y => y.DueDate.ToEgyptionDate()));
            CreateMap<NotesPayable, NPDetails>()
                .ForMember(dest => dest.CurrencyAbbrev, src => src.MapFrom(y => y.Currency.CurrencyAbbrev))
                .ForMember(dest => dest.SupplierName, src => src.MapFrom(y => y.Supplier.Name))
                .ForMember(dest => dest.DueDate, src => src.MapFrom(y => y.DueDate.Value.ToString("dd/MM/yyyy")))
                .ForMember(dest => dest.WritingDate, src => src.MapFrom(y => y.WritingDate.Value.ToString("dd/MM/yyyy")))
                .ForMember(dest => dest.BankAccountName, src => src.MapFrom(y => y.BankAccount.AccountName));

            CreateMap<ExpenseDetailsVM, ExpenseSummary>()
                .ForMember(dest => dest.ExpenseDate, src => src.MapFrom(y => y.ExpenseDate.ToEgyptionDate()));
        }
    }
}
