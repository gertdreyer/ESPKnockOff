using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ESPKnockOff.Models;
using ESPKnockOff.Services.Updaters;

namespace ESPKnockOff.Services
{
    public class DatabaseService
    {
        private readonly Updater _updaterChain;
        private readonly ApplicationContext _context;

        public DatabaseService(ApplicationContext context)
        {
            _context = context;
            _updaterChain = new ProvinceUpdater();
            _updaterChain.SetNextUpdater(new MunicipalityUpdater()).SetNextUpdater(new SuburbUpdater());
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
    }
}
