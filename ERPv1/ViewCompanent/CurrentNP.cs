using ERPv1.Data;
using ERPv1.ERP.CurrentLiabilitiesModules.NotesPayableModule.Model;
using ERPv1.ERP.CurrentLiabilitiesModules.NotesPayableModule.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPv1.ViewCompanent
{
    public class CurrentNP : ViewComponent
    {
        
        private readonly NotesPayableManager _notesPayableManager;

        public CurrentNP(NotesPayableManager notesPayableManager)
        {
            _notesPayableManager = notesPayableManager;
        }

        public IViewComponentResult Invoke()
        {
            var vm = _notesPayableManager.GetNPWithStatus(NotesPayableStatusEnum.UnderCollection);
            return View(vm);
        }
    }
}
