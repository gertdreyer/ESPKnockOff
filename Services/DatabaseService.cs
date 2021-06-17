using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ESPKnockOff.Models;
using ESPKnockOff.Services.Updaters;
using ESPKnockOff.Services.Getters;
using ESPKnockOff.Data;

namespace ESPKnockOff.Services
{
    public class DatabaseService
    {
        private readonly Updater _updaterChain;
        private readonly Getter _getterChain;
        private readonly ApplicationContext _context;

        public DatabaseService(ApplicationContext context)
        {
            _context = context;
            _updaterChain = new ProvinceUpdater();
            _updaterChain.SetNextUpdater(new MunicipalityUpdater()).SetNextUpdater(new SuburbUpdater());

            _getterChain = new ProvinceGetter();
            _getterChain.SetNextGetter(new MunicipalityGetter()).SetNextGetter(new SuburbGetter());
        }

        public void Insert(object obj)
        {
            _updaterChain.HandleUpdate(obj, _context, UpdateType.Insert);
            _context.SaveChanges();  
        }

        public void Update(object obj)
        {
            _updaterChain.HandleUpdate(obj, _context, UpdateType.Update);
            _context.SaveChanges();
        }

        public void Remove(object obj)
        {
            _updaterChain.HandleUpdate(obj, _context, UpdateType.Remove);
            _context.SaveChanges();
        }

        public Task<List<T>> GetObjects<T>(FilteringCoditions filteringConditions = null)
        {
            return _getterChain.GetObjects<T>(filteringConditions, _context);
        }

        public Task<T> GetObjectById<T>(int id)
        {
            return _getterChain.GetObjectById<T>(id, _context);
        }

        public Task<List<Y>> GetObjectSubObjects<T, Y>(int id, FilteringCoditions filteringConditions = null)
        {
            return _getterChain.GetObjectSubObjects<T, Y>(id, filteringConditions, _context);
        }
    }
}
