﻿using CouponDatabase.Models;
using CouponDatabase.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Data;
using WebApp.Services;

namespace WebApp.ViewModels
{

    public class AccessHistoryFilters
    {
        private readonly RepositoryServices _repo;

        public AccessHistoryFilters(ApplicationDbContext context)
        {
            _repo = new RepositoryServices(context);
        }

        public List<CheckedItem> AccessTypes{ get; set; }
        public List<CheckedItem> AccessGrants {get; set;}
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy HH:mm}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        [Display(Name = "Date_AccessFrom", ResourceType = typeof(Resources))]
        public Nullable<DateTime> AccessFrom { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy HH:mm}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        [Display(Name = "Date_AccessTo", ResourceType = typeof(Resources))]
        public Nullable<DateTime> AccessTo { get; set; }

        public AccessHistoryFilters()
        {
            AccessTypes = new List<CheckedItem>();
            foreach(var type in User.GetAccessTypes())
            {
                AccessTypes.Add(new CheckedItem() { Id = type.Id, Checked = true, Label = type.Name });
            }
            AccessGrants = new List<CheckedItem>
            {
                new CheckedItem() { Id = 0, Checked = true, Label = "Yes" },
                new CheckedItem() { Id = 1, Checked = false, Label = "No" }
            };
            AccessFrom = DateTime.Now.AddMonths(-1);
            AccessTo = DateTime.Now;
        }

        public List<AccessLog> GetFilteredAccessHistory(AccessHistoryFilters accessHistoryFilter)
        {
            List<AccessLog> allAccessLogs = _repo.GetAllAccessLogs();
            List<AccessLog> f_ListOfAccessLog = new List<AccessLog>();

            if (accessHistoryFilter.AccessTypes[0].Checked)
                f_ListOfAccessLog.AddRange(allAccessLogs.Where(x => x.Username == accessHistoryFilter.AccessTypes[0].Label).ToList<AccessLog>());
            if (accessHistoryFilter.AccessTypes[1].Checked)
                f_ListOfAccessLog.AddRange(allAccessLogs.Where(x => x.Username == accessHistoryFilter.AccessTypes[1].Label).ToList<AccessLog>());

            if (accessHistoryFilter.AccessFrom != null && accessHistoryFilter.AccessTo != null)
            {
                f_ListOfAccessLog = allAccessLogs.Where(x => x.IssuedDate >= accessHistoryFilter.AccessFrom && x.IssuedDate <= accessHistoryFilter.AccessTo).ToList<AccessLog>();
            }
            else if (accessHistoryFilter.AccessFrom != null || accessHistoryFilter.AccessTo != null)
            {
                if (accessHistoryFilter.AccessFrom != null)
                    f_ListOfAccessLog = allAccessLogs.Where(x => x.IssuedDate >= accessHistoryFilter.AccessFrom).ToList<AccessLog>();
                if (accessHistoryFilter.AccessTo != null)
                    f_ListOfAccessLog = allAccessLogs.Where(x => x.IssuedDate <= accessHistoryFilter.AccessTo).ToList<AccessLog>();
            }


            return f_ListOfAccessLog;
        }
    }
}
