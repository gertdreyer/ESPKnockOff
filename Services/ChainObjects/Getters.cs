using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ESPKnockOff.Models;

namespace ESPKnockOff.Services.Getters
{
    public class FilteringCoditions
    {

    }

    public abstract class Getter
    {
        protected Getter _nextGetter;

        public Getter()
        {
            _nextGetter = NullGetter.Instance;
        }

        public Getter SetNextGetter(Getter getter)
        {
            _nextGetter = getter;
            return getter;
        }

        public abstract Task<List<T>> GetObjects<T>(FilteringCoditions filteringConditions, ApplicationContext context);
        public abstract Task<T> GetObjectById<T>(int id, ApplicationContext context);
        public abstract Task<List<Y>> GetObjectSubObjects<T, Y>(int id,  FilteringCoditions filteringConditions, ApplicationContext context);
    }

    public class NullGetter : Getter
    {
        private static readonly NullGetter _instance = new NullGetter();

        private NullGetter() { }

        public static NullGetter Instance
        {
            get { return _instance; }
        }

        public override async Task<List<T>> GetObjects<T>(FilteringCoditions filteringConditions, ApplicationContext context)
        {
            throw new Exception($"No Getter able to get objects of type {typeof(T)}");
        }

        public override async Task<T> GetObjectById<T>(int id, ApplicationContext context)
        {
            throw new Exception($"No Getter able to get object of type {typeof(T)}");
        }

        public override async Task<List<Y>> GetObjectSubObjects<T, Y>(int id, FilteringCoditions filteringConditions, ApplicationContext context)
        {
            throw new Exception($"No Getter able to get object's sub objects of types {typeof(T)} and {typeof(Y)}");
        }
    }

    public class ProvinceGetter: Getter
    {
        public override async Task<List<T>> GetObjects<T>(FilteringCoditions filteringConditions, ApplicationContext context)
        {
            if (typeof(T) == typeof(Province))
            {
                var provinces = context.Province.ToList();
                return (List<T>)Convert.ChangeType(provinces, typeof(List<T>));
            }
            else
            {
                return await _nextGetter.GetObjects<T>(filteringConditions, context);
            }
        }

        public override async Task<T> GetObjectById<T>(int id, ApplicationContext context)
        {
            if (typeof(T) == typeof(Province))
            {
                var province = await context.Province.FindAsync(id);
                return (T)Convert.ChangeType(province, typeof(T));
            }
            else
            {
                return await _nextGetter.GetObjectById<T>(id, context);
            }
        }

        public override async Task<List<Y>> GetObjectSubObjects<T, Y>(int id, FilteringCoditions filteringConditions, ApplicationContext context)
        {
            if (typeof(T) == typeof(Province) && typeof(Y) == typeof(Municipality))
            {
                // TODO
                return null;
            }
            else
            {
                return await _nextGetter.GetObjectSubObjects<T, Y>(id, filteringConditions, context);
            }
        }
    }
}
