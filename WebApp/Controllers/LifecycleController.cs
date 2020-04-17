﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using CouponDatabase.Lifecycle;
using CouponDatabase.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApp.Data;
using WebApp.Models;
using WebApp.Services;
using WebApp.ViewModels;
using static CouponDatabase.Models.Coupon;

namespace WebApp.Controllers
{

    //[Authorize]
    [Route("Lifecycle/[action]")]
    public class LifecycleController : Controller
    {
        private readonly RepositoryServices _repo;
        public LifecycleController(ApplicationDbContext context)
        {      
             _repo = new RepositoryServices(context);
           
        }

        /// <summary>
        /// Show initial Lifecycle search page or after Back from list of coupons
        /// </summary>
        /// <returns>Opens Lifecycle search page</returns>
        [HttpGet]
        public IActionResult Index()
        {
            LifecycleSearchViewModel initModel = new LifecycleSearchViewModel();
            initModel.PromotionFilter = new PromotionFilter() { ShowActive = true, ShowInactive = false, ValidFrom = DateTime.Today, ValidTo = DateTime.Today.AddMonths(1) };
            initModel.PromotionFilter.Properties = setModelProperties(_repo.GetAllProperties(), new List<Property>());
            initModel.CouponFilter = new CouponFilters() { ShowActive = true, ShowInactive = false, ValidFrom = DateTime.Today, ValidTo = DateTime.Today.AddMonths(1) };

            initModel.CouponFilter.AwardChannels = setModelAwardChannels(_repo.GetAllAwardChannels(), new List<AwardChannel>());
            initModel.CouponFilter.IssuerChannels = setModelIssuerChannels(_repo.GetAllIssuerChannels(), new List<IssuerChannel>());
            initModel.CouponFilter.CurrentStatus = setModelCurrentStatus(_repo.GetCouponStatuses(), new List<CurrentStatus>());
            initModel.CouponFilter.Properties = setModelProperties(_repo.GetAllProperties(), new List<Property>());

            return View("LifecycleSearch", initModel);
        }

        // <summary>
        /// View List of promotions
        /// </summary>
        /// <returns>Opens list of filtered promotions</returns>
        [HttpPost]
        public IActionResult Search(PromotionFilter promotionFilter, CouponFilters couponFilter)
        {
            /*
             * TO DO: 
             *  - optimize promotion filtering
             *  - add coupon filtering
             *  - store filtered coupons into model
             *  - store filtered promotions and coupons into session before returning view
             *  - figure out session storage (WIP)
             */

            LifecycleUpdateViewModel model = new LifecycleUpdateViewModel();

            List<Promotion> f_ListOfPromotions = new List<Promotion>();
            List<Coupon> f_ListOfCoupons = new List<Coupon>();

            if (promotionFilter.ShowActive)
                f_ListOfPromotions.AddRange(_repo.GetAllPromotions().Where(x => x.Active == true).ToList<Promotion>());
            if (promotionFilter.ShowInactive)
                f_ListOfPromotions.AddRange(_repo.GetAllPromotions().Where(x => x.Active == false).ToList<Promotion>());

            if (promotionFilter.ValidFrom != null && promotionFilter.ValidTo != null)
            {
                f_ListOfPromotions.AddRange(_repo.GetAllPromotions().Where(x => x.ValidFrom >= promotionFilter.ValidFrom && x.ValidTo <= promotionFilter.ValidTo).ToList<Promotion>());
            }
            else if (promotionFilter.ValidFrom != null || promotionFilter.ValidTo != null)
            {
                if (promotionFilter.ValidFrom != null)
                    f_ListOfPromotions.AddRange(_repo.GetAllPromotions().Where(x => x.ValidFrom >= promotionFilter.ValidFrom).ToList<Promotion>());
                if (promotionFilter.ValidTo != null)
                    f_ListOfPromotions.AddRange(_repo.GetAllPromotions().Where(x => x.ValidTo <= promotionFilter.ValidTo).ToList<Promotion>());
            }

            if (promotionFilter.Code != null)
            {
                if (f_ListOfPromotions.Count > 0)
                {
                    f_ListOfPromotions = f_ListOfPromotions.Where(x => x.Code.Contains(promotionFilter.Code)).ToList<Promotion>();
                }
                else
                {
                    f_ListOfPromotions.AddRange(_repo.GetAllPromotions().Where(x => x.Code.Contains(promotionFilter.Code)).ToList<Promotion>());
                }
            }


            List<Promotion> f_PromoWithCoupons = new List<Promotion>();

            foreach(Promotion p in f_ListOfPromotions)
            {
                if (p.HasCoupons)
                {
                    List<Coupon> promotionCoupons = _repo.GetPromotionCoupons(p);
                    // continue filtering ...
                    f_ListOfCoupons.AddRange(promotionCoupons);
                }
            }

            /*
             * TODO:
             * - Implement coupon filtering based on filtered promotions and coupon filters
             * - Implement method getFilteredPromotions() : filteredListOfPromotions, promotionFilters
             * - Implement getFilteredCoupons(): filteredListOfPromotions, filteredListOfCoupons, couponFilters
             */

            model.PromotionCodes = getSelectListPromotions(f_ListOfPromotions);
            model.CouponSeries = getSelectListSeries(f_ListOfCoupons); // implement method for mapping Coupon to SelectListItem
            model.Coupons = f_ListOfCoupons;

            /*
             * TODO:
             * Store PromotionCodes, CouponSeries and filtered coupons into session
             */

            model.Coupons.Add(new Coupon() { Code = "EASTER12343566", Id = 1, AquireFrom = DateTime.Today, AquireTo = DateTime.Today.AddMonths(1), CouponSeries = 1, PromotionId = 1, User = "38640440480", Status = (int)CouponStatus.Created });

            /*
             * TODO:
             * If no coupons found, return error / no results view
             * If coupons found, return LifecycleCoupons view
             */

            return View("LifecycleCoupons", model);
        }

        [HttpPost]
        public IActionResult Update(LifecycleUpdateViewModel model)
        {
            /*
             * TODO: 
             */
            foreach(CouponCommand command in model.CouponsSelected)
            {
                command.Status = CommandStatus.Valid;
            }
            return View("LifecycleCoupons", model);
        }

        [HttpPost]
        public IActionResult UpdateSearchFilter(LifecycleUpdateViewModel model)
        {
            /*
             * TODO:
             * - get from session everything required for the update (selected promotion code and selected coupon series, coupons ...)
             * - apply model filters of SelectedPromoCode and SelectedCouponSeries if possible
             * - return new coupon list
             * - return list of promotion codes from session and new coupon series (after filtering)
             * - store new list into session
             */
            return View("LifecycleCoupons", model);
        }

        public List<SelectListItem> getSelectListPromotions(List<Promotion> promotions)
        {
            List<SelectListItem> selectList = new List<SelectListItem>();
            foreach(Promotion p in promotions)
            {
                selectList.Add(new SelectListItem { 
                    Text = p.Code,
                    Value = p.Code
                });
            }
            return selectList;
        }

        /*
         * Returns select list 
         */
        public List<SelectListItem> getSelectListSeries(List<Coupon> coupons)
        {
            List<SelectListItem> seriesList = new List<SelectListItem>();
            List<int> availableSeries = new List<int>();
            foreach(Coupon c in coupons)
            {
                if(!availableSeries.Contains(c.CouponSeries))
                {
                    availableSeries.Add(c.CouponSeries);
                    seriesList.Add(new SelectListItem
                    {
                        Text = String.Format("Series #{0}", c.CouponSeries),
                        Value = c.CouponSeries.ToString()
                    });
                }
            }
            return seriesList;
        }
        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private List<CheckedItem> setModelIssuerChannels(List<IssuerChannel> allIssuerChannels, List<IssuerChannel> promotionIssuerChannels)
        {
            List<CheckedItem> checkedItems = new List<CheckedItem>();
            foreach (IssuerChannel issuerChannel in allIssuerChannels)
            {
                if (promotionIssuerChannels.Contains(issuerChannel))
                {
                    checkedItems.Add(new CheckedItem { Checked = true, Label = issuerChannel.Name, Id = issuerChannel.Id });
                }
                else
                {
                    checkedItems.Add(new CheckedItem { Checked = false, Label = issuerChannel.Name, Id = issuerChannel.Id });
                }
            }
            return checkedItems;
        }

        private List<CheckedItem> setModelAwardChannels(List<AwardChannel> allAwardChannels, List<AwardChannel> promotionAwardChannels)
        {
            List<CheckedItem> checkedItems = new List<CheckedItem>();
            foreach (AwardChannel awardChannels in allAwardChannels)
            {
                if (promotionAwardChannels.Contains(awardChannels))
                {
                    checkedItems.Add(new CheckedItem { Checked = true, Label = awardChannels.Name, Id = awardChannels.Id });
                }
                else
                {
                    checkedItems.Add(new CheckedItem { Checked = false, Label = awardChannels.Name, Id = awardChannels.Id });
                }
            }
            return checkedItems;
        }

        private List<CheckedItem> setModelProperties(List<Property> allProperties, List<Property> promotionProperties)
        {
            List<CheckedItem> checkedItems = new List<CheckedItem>();
            foreach (Property property in allProperties)
            {
                if (promotionProperties.Contains(property))
                {
                    checkedItems.Add(new CheckedItem { Checked = true, Label = property.Name, Id = property.Id });
                }
                else
                {
                    checkedItems.Add(new CheckedItem { Checked = false, Label = property.Name, Id = property.Id });
                }
            }
            return checkedItems;
        }

        private List<CheckedItem> setModelCurrentStatus(List<CurrentStatus> allProperties, List<CurrentStatus> couponStatuses)
        {
            List<CheckedItem> checkedItems = new List<CheckedItem>();
            foreach (CurrentStatus property in allProperties)
            {
                if (couponStatuses.Contains(property))
                {
                    checkedItems.Add(new CheckedItem { Checked = true, Label = property.Name, Id = property.Id });
                }
                else
                {
                    checkedItems.Add(new CheckedItem { Checked = false, Label = property.Name, Id = property.Id });
                }
            }
            return checkedItems;
        }

        private bool updatePromotionFields(PromotionDetailsViewModel viewModel, long Id)
        {
            List<PromotionProperty> promotionProperties = new List<PromotionProperty>();
            List<PromotionAwardChannel> promotionAwardChannels = new List<PromotionAwardChannel>();
            List<PromotionIssuerChannel> promotionIssuerChannels = new List<PromotionIssuerChannel>();

            foreach (var item in viewModel.Properties.Where(x => x.Checked == true).ToList<CheckedItem>())
            {
                promotionProperties.Add(new PromotionProperty() { PromotionId = Id, PropertyId = item.Id });
            }
            foreach (var item in viewModel.AwardChannels.Where(x => x.Checked == true).ToList<CheckedItem>())
            {
                promotionAwardChannels.Add(new PromotionAwardChannel() { PromotionId = Id, AwardChannelId = item.Id });
            }
            foreach (var item in viewModel.IssuerChannels.Where(x => x.Checked == true).ToList<CheckedItem>())
            {
                promotionIssuerChannels.Add(new PromotionIssuerChannel() { PromotionId = Id, IssuerChannelId = item.Id });
            }

            return _repo.updatePromotionFields(viewModel.Promotion.Id, promotionProperties, promotionAwardChannels, promotionIssuerChannels);
        }

    }
}
