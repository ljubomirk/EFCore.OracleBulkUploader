﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using WebApp.Data;
using CouponDatabase.Models;
using System.Collections.Generic;
using System.Linq;
using CouponDatabase.Services;
using static CouponDatabase.Models.Coupon;
using System;
using CouponDatabase.Lifecycle;
using WebApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace WebApp.Services
{
    public class RepositoryServices
    {
        readonly ApplicationDbContext Context;
        private readonly ILogger _logger;

        public RepositoryServices(ApplicationDbContext context)
        {
            Context = context;
            _logger = new LoggerFactory().CreateLogger("RepositoryService");
        }
        public RepositoryServices(ApplicationDbContext context, ILogger logger)
        {
            Context = context;
            _logger = logger;
        }
        public Command Add(Coupon coupon)
        {
            Command result = new Command(CommandStatus.Valid);
            try {
                Context.Database.BeginTransaction();
                Context.Coupon.Add(coupon);
                int saved = Context.SaveChanges();
                if (saved == 1)
                    result.Status = CommandStatus.Valid;
                else
                    result.Status = CommandStatus.ErrorSystem;
                Context.Database.CommitTransaction();
            }
            catch(DbUpdateException update)
            {
                Context.Database.RollbackTransaction();
                result.Status = CommandStatus.ErrorSystem;
                result.Message = update.Message;
            }
            catch(Exception exc)
            {
                Context.Database.RollbackTransaction();
                result.Status = CommandStatus.ErrorSystem;
                result.Message = exc.Message;
            }
            return result;
        }

        internal List<AccessLog> GetAccessLogs(DateTime? accessFrom, DateTime? accessTo, List<CheckedItem> grantedItems, List<CheckedItem> accessTypes)
        {
            List<long> grantFilter = new List<long>();
            foreach(var granted in grantedItems)
            {
                if(granted.Checked)
                    grantFilter.Add(granted.Id);
            }
            List<int> accessFilter = new List<int>();
            foreach (var access in accessTypes)
            {
                if (access.Checked)
                    grantFilter.Add(access.Id);
            }
            return Context.AccessLog.Where(item => item.IssuedDate >= accessFrom)
                    .Where(item => item.IssuedDate <= accessTo)
                    .Where(item => grantFilter.Contains(item.Granted ? 0 : 1))
                    .Where(item => accessFilter.Contains((int)item.ApplicationType))
                    .ToList();
        }

        public Coupon GetCoupon(string PromotionCode, string CouponCode)
        {
            List<Promotion> promList = new List<Promotion>();
            if (PromotionCode.Length > 0)
                promList.Add(Context.Promotion.Where(p => p.Code == PromotionCode).FirstOrDefault());
            else
                promList.AddRange(Context.Promotion.Where(p => p.Active==true).ToList());
            List<Coupon> coupons = Context.Coupon.Where(c => promList.Contains(c.Promotion)).ToList<Coupon>();
            return coupons.Where(c => c.Code == CouponCode).FirstOrDefault();
        }

        internal List<User> GetAllUsers()
        {
            return Context.User.ToList<User>();
        }

        public Coupon GetCouponById(long id)
        {
            return Context.Coupon.Where(c => c.Id == id).First();
        }

        public Command UpdateCoupon(Coupon coupon)
        {
            Command result = null;
            try
            {
                Context.Database.BeginTransaction();
                Context.Coupon.Update(coupon);
                foreach(CouponHistory ch in coupon.CouponHistories)
                {
                    if (Context.CouponHistory.Find(ch.Id) == null)
                        Context.CouponHistory.Add(ch);
                }
                int saved = Context.SaveChanges();
                Context.Database.CommitTransaction();
                result = new Command(CommandStatus.Valid);
            }
            catch (DbUpdateException update)
            {
                Context.Database.RollbackTransaction();
                result = new Command(CommandStatus.ErrorSystem);
                //store to log update.Message;
                _logger.LogError("Exception DBUpdate:{0}", update.Message);
               
            }
            catch (Exception exc)
            {
                Context.Database.RollbackTransaction();
                result = new Command(CommandStatus.ErrorSystem);
                //store to log result.Message = exc.Message;
                _logger.LogError("Exception DBUpdate:{0}", exc.Message);
            }
            return result;
        }

        internal List<CouponDatabase.Models.System> getAllSystems()
        {
            return Context.System.ToList<CouponDatabase.Models.System>();
        }

        internal int GetCouponSeriesVal(long id)
        {
            return GetPromotionWithId(id).CouponSeries;
        }

        #region PromotionCommands
        public List<Promotion> GetAllPromotions()
        {
            _logger.LogDebug(Utils.GetLogFormat() + "GetAllPromotions");
            List<Promotion> allPromotions = Context.Promotion.ToList<Promotion>();
            allPromotions.ForEach(p => 
                {
                    //_logger.LogDebug(Utils.GetLogFormat() + "Loading hasCoupons {0}", p.Id);
                    p.HasCoupons = getHasCouponsForPromotion(p.Id);
                    GetPromotionData(p);
                }
            );
            _logger.LogDebug(Utils.GetLogFormat() + "Return promotion");
            return allPromotions;
        }
        /*
         * Returns promotions with HasCoupons property true.
         */
        public List<Promotion> GetPromotionsWithCoupons()
        {
            return Context.Promotion.Where(p => p.HasCoupons).ToList();
        }

        internal bool UpdateCouponSeriesNum(long promotionId)
        {
            Promotion promotion = GetPromotionWithId(promotionId);
            promotion.CouponSeries += 1;
            return UpdatePromotion(promotion);
        }


        /*
         * Get coupons for target promotions.
         */
        public List<Coupon> GetCouponsForPromotions(List<Promotion> promotions)
        {
            List<Coupon> promotionCoupons = new List<Coupon>();
            foreach(Promotion p in promotions)
            {
                if(p.Coupons.Count() > 0){
                    promotionCoupons.AddRange(p.Coupons);
                } else
                {
                    List<Coupon> tmpCoupons = GetPromotionCoupons(p);
                    promotionCoupons.AddRange(tmpCoupons);
                }
            }
            return promotionCoupons;
        }

        /*
         * Get promotion by promotion id.
         */
        public Promotion GetPromotionWithId(long id)
        {
            Promotion promotion = new Promotion();
            promotion = Context.Promotion.Find(id);
            GetPromotionData(promotion);
            
            return promotion;
        }

        /*
         * Get promotion by promotion code.
         */
        public Promotion GetPromotionByCode(String code)
        {
            Promotion promotion = new Promotion();
            promotion = Context.Promotion.Find(code);
            return promotion;
        }

        public Promotion GetPromotionData(Promotion promotion)
        {
            _logger.LogDebug(Utils.GetLogFormat()+"Loading promotion {1}:{2}", promotion.Id,promotion.Code);
            Context.Entry(promotion).Collection(p => p.PromotionProperties).Load();
            //_logger.LogDebug(Utils.GetLogFormat() + "GetPromotionData {1}:props", promotion.Id);
            Context.Entry(promotion).Collection(p => p.PromotionAwardChannels).Load();
            //_logger.LogDebug(Utils.GetLogFormat() + "GetPromotionData {1}:awardCh", promotion.Id);
            Context.Entry(promotion).Collection(p => p.PromotionIssuerChannels).Load();
            //_logger.LogDebug(Utils.GetLogFormat() + "GetPromotionData {1}:issuerCh", promotion.Id);
            //Context.Entry(promotion).Collection(p => p.Coupons).Load();
            _logger.LogDebug(Utils.GetLogFormat() + "GetPromotionData {1}:has&Load", promotion.Id);
            foreach (var promProp in promotion.PromotionProperties)
            {
                promProp.Property = Context.Property.Find(promProp.PropertyId);
            }
            //_logger.LogDebug(Utils.GetLogFormat() + "GetPromotionData {1}:props&Load", promotion.Id);
            foreach (var awardChannel in promotion.PromotionAwardChannels)
            {
                awardChannel.AwardChannel = Context.AwardChannel.Find(awardChannel.AwardChannelId);
            }
            //_logger.LogDebug(Utils.GetLogFormat() + "GetPromotionData {1}:award&Load", promotion.Id);
            foreach (var channel in promotion.PromotionIssuerChannels)
            {
                channel.IssuerChannel = Context.IssuerChannel.Find(channel.IssuerChannelId);
            }
            _logger.LogDebug(Utils.GetLogFormat() + "GetPromotionData {1}:issuer&Load", promotion.Id);

            return promotion;
        }

        public bool getHasCouponsForPromotion(long id)
        {
            return Context.Coupon.Any(c => c.PromotionId == id);
        }
        #endregion

        public List<Property> GetAllProperties()
        {
            return Context.Property.ToList<Property>();
        }

        internal List<Coupon> getCoupons()
        {
            return Context.Coupon.ToList<Coupon>();
        }

        /*
         * Generate list of CurrentStatus class instances from CouponStatus.
         * Assign CouponStatus enum's values and names to instances.
         * Used for populating CouponFilter view.
         */
        public List<CurrentStatus> GetCouponStatuses()
        {
            List<string> enumNames = GetCouponStatusList();
            List<CurrentStatus> statusList = new List<CurrentStatus>();
            for (var i = 0; i < enumNames.Count(); i++)
            {
                statusList.Add(new CurrentStatus()
                {
                    Id = i+1,
                    Name = enumNames[i]
                });
            }
            return statusList;
        }

        /*
         * Get list of coupon status names. Generated from enum.
         */
        public List<string> GetCouponStatusList()
        {
            return Enum.GetNames(typeof(CouponStatus)).ToList();
        }

        public List<Property> GetPromotionProperties(long idPromotion)
        {
            List<Property> properties = GetAllProperties();
            List<PromotionProperty> promotionPropertiesMap = Context.PromotionProperty.Where(x => x.PromotionId == idPromotion).ToList<PromotionProperty>();
            List<Property> promotionProperties = new List<Property>();
            if (promotionPropertiesMap != null && promotionPropertiesMap.Count > 0)
            {
                foreach (var promotionProperty in promotionPropertiesMap)
                    {
                     promotionProperties.Add(properties.Where(x => x.Id == promotionProperty.PropertyId).First<Property>());
                    }
            }
            return promotionProperties;
        }
        public List<AwardChannel> GetAllAwardChannels()
        {
            return Context.AwardChannel.ToList<AwardChannel>();
        }
        public List<AwardChannel> GetPromotionAwardChannels(long idPromotion)
        {
            List<AwardChannel> awardChannels = GetAllAwardChannels();
            List<PromotionAwardChannel> awardChannelsMap = Context.PromotionAwardChannel.Where(x => x.PromotionId == idPromotion).ToList<PromotionAwardChannel>();

            List<AwardChannel> promotionAwardChannels = new List<AwardChannel>();
            if (awardChannelsMap != null && awardChannelsMap.Count > 0)
            {
                foreach (var awardChannel in awardChannelsMap)
                {
                    promotionAwardChannels.Add(awardChannels.Where(x => x.Id == awardChannel.AwardChannelId).First<AwardChannel>());
                }
            }
            return promotionAwardChannels;

        }

        public List<CouponAwardChannel> GetCouponAwardChannels(long idCoupon)
        {
            return Context.CouponAwardChannel.Where(c => c.CouponId == idCoupon).ToList();
        }

        public List<IssuerChannel> GetAllIssuerChannels()
        {
            return Context.IssuerChannel.ToList<IssuerChannel>();
        }
        public List<IssuerChannel> GetPromotionIssuerChannels(long idPromotion)
        {
            List<IssuerChannel> issuerChannels = GetAllIssuerChannels();
            List<PromotionIssuerChannel> issuerChannelsMap = Context.PromotionIssuerChannel.Where(x => x.PromotionId == idPromotion).ToList<PromotionIssuerChannel>();

            List<IssuerChannel> promotionIssuerChannels = new List<IssuerChannel>();
            if (issuerChannelsMap != null && issuerChannelsMap.Count > 0)
            {
                foreach (var issuerChannel in issuerChannelsMap)
                {
                    promotionIssuerChannels.Add(issuerChannels.Where(x => x.Id == issuerChannel.IssuerChannelId).First<IssuerChannel>());
                }
            }
            return promotionIssuerChannels;
        }

        public List<CouponIssuerChannel> GetCouponIssuerChannels(long idCoupon)
        {
            return Context.CouponIssuerChannel.Where(c => c.CouponId == idCoupon).ToList();
        }

        public long CreatePromotion(Promotion promotion)
        {
            Context.Promotion.Add(promotion);
            Context.SaveChanges();
            return promotion.Id;
        }
        public bool UpdatePromotion(Promotion promotion)
        {
            Promotion selectedPromotion = Context.Promotion.First(x => x.Id == promotion.Id);
            PromotionFactory pf = new PromotionFactory(selectedPromotion);
            pf.UpdateDetails(promotion);
            int returnValue = Context.SaveChanges();
            return returnValue > 0 ? true : false;
        }

        /// <summary>
        /// Returns found User coupons that Promotion is Active
        /// </summary>
        /// <param name="user">User assigned for Promotion</param>
        /// <returns></returns>
        public IList<Coupon> GetUserCoupons(string user)
        {
            List<Coupon> coupons = Context.Coupon
                .Include(c => c.Promotion)
                    .Where(p => p.Promotion.Active == true)
                .Where(item => item.User == user || item.Holder == user)
                .ToList();
            return coupons;
        }

        public List<Coupon> GetPromotionCoupons(Promotion promotion)
        {
            return Context.Coupon.Where(c => c.PromotionId == promotion.Id).ToList();
        }

        /*
         * Returns coupons for promotion.
         */
        public List<Coupon> GetPromotionCoupons(Coupon coupon)
        {
            return Context.Coupon.Where(p => p.PromotionId == coupon.PromotionId).ToList();
        }

        public bool updatePromotionFields(long id, List<PromotionProperty> promotionProperties, List<PromotionAwardChannel> awardChannels, List<PromotionIssuerChannel> issuerChannels)
        {
            foreach (PromotionProperty item in Context.PromotionProperty.Where(x => x.PromotionId == id).ToList<PromotionProperty>())
            {
                Context.PromotionProperty.Remove(item);
            }
            foreach (PromotionProperty item in promotionProperties)
            {
                PromotionProperty prop = new PromotionProperty() { PromotionId = id, PropertyId = item.PropertyId };
                Context.PromotionProperty.Add(prop);
            }

            foreach (PromotionAwardChannel item in Context.PromotionAwardChannel.Where(x => x.PromotionId == id).ToList<PromotionAwardChannel>())
            {
               Context.PromotionAwardChannel.Remove(item);
            }
            foreach (PromotionAwardChannel item in awardChannels)
            {
                PromotionAwardChannel awardChannel = new PromotionAwardChannel() { PromotionId = id, AwardChannelId = item.AwardChannelId };
                Context.PromotionAwardChannel.Add(awardChannel);
            }

            foreach (PromotionIssuerChannel item in Context.PromotionIssuerChannel.Where(x => x.PromotionId == id).ToList<PromotionIssuerChannel>())
            {
               Context.PromotionIssuerChannel.Remove(item) ;
            }
            foreach (PromotionIssuerChannel item in issuerChannels)
            {
                PromotionIssuerChannel issuerChannel = new PromotionIssuerChannel() { PromotionId = id, IssuerChannelId = item.IssuerChannelId };
                Context.PromotionIssuerChannel.Add(issuerChannel);
            }

            int returnValue = Context.SaveChanges();
            return returnValue > 0 ? true : false;
        }

        internal bool insertCoupons(List<Coupon> listOfCoupons)
        {
            foreach (Coupon coupon in listOfCoupons)
            {
                Context.Coupon.Add(coupon);
            }
            /*TODO add handling dupl */
            int returnValue = Context.SaveChanges();
            return returnValue > 0 ? true : false;
        }

        public bool LogAppAccess (string action, string username, bool granted)
        {
            AccessLog log = new AccessLog(ApplicationEnum.WebApp, "", action, username, granted);
            Context.AccessLog.Add(log);
            int returnValue = Context.SaveChanges();
            return returnValue > 0 ? true : false;
        }
        public bool LogAPIAccess(string action, string channel, string username, bool granted)
        {
            AccessLog log = new AccessLog(ApplicationEnum.API, channel, action, username, granted);
            Context.AccessLog.Add(log);
            int returnValue = Context.SaveChanges();
            return returnValue > 0 ? true : false;
        }

        internal Command AddSystem(CouponDatabase.Models.System model)
        {
            Command result = new Command(CommandStatus.Valid);
            try
            {
                Context.Database.BeginTransaction();
                Context.System.Add(model);
                int saved = Context.SaveChanges();
                if (saved == 1)
                    result.Status = CommandStatus.Valid;
                else
                    result.Status = CommandStatus.ErrorSystem;
                Context.Database.CommitTransaction();
            }
            catch (DbUpdateException update)
            {
                Context.Database.RollbackTransaction();
                result.Status = CommandStatus.ErrorSystem;
                result.Message = update.Message;
            }
            catch (Exception exc)
            {
                Context.Database.RollbackTransaction();
                result.Status = CommandStatus.ErrorSystem;
                result.Message = exc.Message;
            }
            return result;
        }
        internal dynamic UpdateSystem(CouponDatabase.Models.System model)
        {
            Command result = null;
            try
            {
                Context.Database.BeginTransaction();
                Context.System.Update(model);
                int saved = Context.SaveChanges();
                Context.Database.CommitTransaction();
                result = new Command(CommandStatus.Valid);
            }
            catch (DbUpdateException update)
            {
                Context.Database.RollbackTransaction();
                result = new Command(CommandStatus.ErrorSystem);
                //store to log update.Message;
                _logger.LogError("Exception DBUpdate:{0}", update.Message);

            }
            catch (Exception exc)
            {
                Context.Database.RollbackTransaction();
                result = new Command(CommandStatus.ErrorSystem);
                //store to log result.Message = exc.Message;
                _logger.LogError("Exception DBUpdate:{0}", exc.Message);
            }
            return result;
        }

        internal dynamic DeleteSystem(long id)
        {
            Command result = null;
            
            try
            {
                Context.Database.BeginTransaction();
                Context.System.Remove(Context.System.Find(id));
                int saved = Context.SaveChanges();
                Context.Database.CommitTransaction();
                result = new Command(CommandStatus.Valid);
            }
            catch (DbUpdateException update)
            {
                Context.Database.RollbackTransaction();
                result = new Command(CommandStatus.ErrorSystem);
                //store to log update.Message;
                _logger.LogError("Exception DBUpdate:{0}", update.Message);

            }
            catch (Exception exc)
            {
                Context.Database.RollbackTransaction();
                result = new Command(CommandStatus.ErrorSystem);
                //store to log result.Message = exc.Message;
                _logger.LogError("Exception DBUpdate:{0}", exc.Message);
            }
            return result;
        }

        internal dynamic AddNotifyList(ExternalSystemsViewModel model)
        {
            Command result = new Command(CommandStatus.Valid);

            try
            {
                Context.Database.BeginTransaction();
                Context.NotifyList.Add(new NotifyList() {ChannelId = Int32.Parse(model.ChannelId), SystemId = Int32.Parse(model.SystemId), Url = model.Url });
                int saved = Context.SaveChanges();
                if (saved == 1)
                    result.Status = CommandStatus.Valid;
                else
                    result.Status = CommandStatus.ErrorSystem;
                Context.Database.CommitTransaction();
            }
            catch (DbUpdateException update)
            {
                Context.Database.RollbackTransaction();
                result.Status = CommandStatus.ErrorSystem;
                result.Message = update.Message;
            }
            catch (Exception exc)
            {
                Context.Database.RollbackTransaction();
                result.Status = CommandStatus.ErrorSystem;
                result.Message = exc.Message;
            }
            return result;
        }

        internal dynamic UpdateNotifyList(ExternalSystemsViewModel model)
        {
            Command result = null;
            NotifyList notifyList = new NotifyList() { ChannelId = Int32.Parse(model.ChannelId), SystemId = Int32.Parse(model.SystemId), Url = model.Url };
            NotifyList targetNotifyList = new NotifyList();
            try
            {
                Context.Database.BeginTransaction();
                targetNotifyList = Context.NotifyList.Where(x=> x.ChannelId == notifyList.ChannelId && x.SystemId == notifyList.SystemId).First();
                targetNotifyList.Url = notifyList.Url;
                int saved = Context.SaveChanges();
                Context.Database.CommitTransaction();
                result = new Command(CommandStatus.Valid);
            }
            catch (DbUpdateException update)
            {
                Context.Database.RollbackTransaction();
                result = new Command(CommandStatus.ErrorSystem);
                //store to log update.Message;
                _logger.LogError("Exception DBUpdate:{0}", update.Message);

            }
            catch (Exception exc)
            {
                Context.Database.RollbackTransaction();
                result = new Command(CommandStatus.ErrorSystem);
                //store to log result.Message = exc.Message;
                _logger.LogError("Exception DBUpdate:{0}", exc.Message);
            }
            return result;
        }

        internal dynamic deleteNotifyList(long channelId, long systemId)
        {
            Command result = null;

            try
            {
                Context.Database.BeginTransaction();
                Context.NotifyList.Remove(Context.NotifyList.Where(x => x.ChannelId == channelId && x.SystemId == systemId).First());
                int saved = Context.SaveChanges();
                Context.Database.CommitTransaction();
                result = new Command(CommandStatus.Valid);
            }
            catch (DbUpdateException update)
            {
                Context.Database.RollbackTransaction();
                result = new Command(CommandStatus.ErrorSystem);
                //store to log update.Message;
                _logger.LogError("Exception DBUpdate:{0}", update.Message);

            }
            catch (Exception exc)
            {
                Context.Database.RollbackTransaction();
                result = new Command(CommandStatus.ErrorSystem);
                //store to log result.Message = exc.Message;
                _logger.LogError("Exception DBUpdate:{0}", exc.Message);
            }
            return result;
        }


        internal dynamic AddUser(User user)
        {
            Command result = new Command(CommandStatus.Valid);
            try
            {
                Context.Database.BeginTransaction();
                Context.User.Add(user);
                int saved = Context.SaveChanges();
                if (saved == 1)
                    result.Status = CommandStatus.Valid;
                else
                    result.Status = CommandStatus.ErrorSystem;
                Context.Database.CommitTransaction();
            }
            catch (DbUpdateException update)
            {
                Context.Database.RollbackTransaction();
                result.Status = CommandStatus.ErrorSystem;
                result.Message = update.Message;
            }
            catch (Exception exc)
            {
                Context.Database.RollbackTransaction();
                result.Status = CommandStatus.ErrorSystem;
                result.Message = exc.Message;
            }
            return result;
        }

    }

    public class PromotionFactory : IPromotion
    {
        public PromotionFactory(Promotion promo) : base(promo)
        {

        }
    }
}
