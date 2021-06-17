using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ESPKnockOff.Models;
using ESPKnockOff.Data;

namespace ESPKnockOff.Services.Updaters
{
    public enum UpdateType {
        Insert,
        Update,
        Remove
    }

    public abstract class Updater
    {
        protected Updater _nextUpdater;

        public Updater()
        {
            _nextUpdater = NullUpdater.Instance;
        }

        public Updater SetNextUpdater(Updater inserter)
        {
            _nextUpdater = inserter;
            return inserter;
        }

        public object NextUpdaterHandle(object obj, ApplicationContext context, UpdateType type)
        {
            return _nextUpdater.HandleUpdate(obj, context, type);
        }

        public abstract object HandleUpdate(object obj, ApplicationContext context, UpdateType type);
    }

    public class NullUpdater : Updater
    {
        private static readonly NullUpdater _instance = new NullUpdater();

        private NullUpdater() { }

        public static NullUpdater Instance
        {
            get { return _instance; }
        }

        public override object HandleUpdate(object obj, ApplicationContext context, UpdateType type)
        {
            throw new Exception($"No updater able to handle object of type {obj.GetType()}");
        }
    }

    public class ProvinceUpdater : Updater
    {
        public override object HandleUpdate(object obj, ApplicationContext context, UpdateType type)
        {
            if (obj is Province)
            {
                switch (type)
                {
                    case UpdateType.Insert:
                        context.Province.Add((Province)obj);
                        break;
                    case UpdateType.Update:
                        context.Province.Update((Province)obj);
                        break;
                    case UpdateType.Remove:
                        context.Province.Remove((Province)obj);
                        return null;
                }

                context.SaveChanges();
                return obj;
            }
            else
            {
                return NextUpdaterHandle(obj, context, type);
            }
        }
    }

    public class MunicipalityUpdater : Updater
    {
        public override object HandleUpdate(object obj, ApplicationContext context, UpdateType type)
        {
            if (obj is Municipality)
            {
                switch (type)
                {
                    case UpdateType.Insert:
                        context.Municipality.Add((Municipality)obj);
                        break;
                    case UpdateType.Update:
                        context.Municipality.Update((Municipality)obj);
                        break;
                    case UpdateType.Remove:
                        context.Municipality.Remove((Municipality)obj);
                        break;
                }

                context.SaveChanges();
                return obj;
            }
            else
            {
                return NextUpdaterHandle(obj, context, type);
            }
        }
    }

    public class SuburbUpdater : Updater
    {
        public override object HandleUpdate(object obj, ApplicationContext context, UpdateType type)
        {
            if (obj is Suburb)
            {
                switch (type)
                {
                    case UpdateType.Insert:
                        context.Suburb.Add((Suburb)obj);
                        break;
                    case UpdateType.Update:
                        context.Suburb.Update((Suburb)obj);
                        break;
                    case UpdateType.Remove:
                        context.Suburb.Remove((Suburb)obj);
                        break;
                }

                context.SaveChanges();
                return obj;
            }
            else
            {
                return NextUpdaterHandle(obj, context, type);
            }
        }
    }

    public class ScheduleUpdater : Updater
    {
        public override object HandleUpdate(object obj, ApplicationContext context, UpdateType type)
        {
            if (obj is Schedule)
            {
                var schedule = (Schedule)obj;
                var timeCodes = context.TimeCode.Where(timeCode => timeCode.StartTime == schedule.StartTime && timeCode.EndTime == schedule.EndTime).ToList();
                var timeCodeID = 0;

                if (timeCodes.Count > 0)
                {
                    timeCodeID = timeCodes[0].TimeCodeID;
                }
                else
                {
                    var timeCode = new TimeCode()
                    {
                        StartTime = schedule.StartTime,
                        EndTime = schedule.EndTime
                    };
                    context.TimeCode.Add(timeCode);
                    context.SaveChanges();
                    timeCodeID = timeCode.TimeCodeID;
                }

                var loadSheddingSlot = new LoadSheddingSlot()
                {
                    DayOfMonthID = schedule.Day,
                    StageID = schedule.Stage,
                    SuburbClusterID = schedule.SuburbClusterID,
                    TimeCodeID = timeCodeID,
                };

                if (type != UpdateType.Insert)
                {
                    loadSheddingSlot.LoadSheddingSlotID = schedule.ScheduleID;
                }

                switch (type)
                {
                    case UpdateType.Insert:
                        context.LoadSheddingSlot.Add(loadSheddingSlot);
                        break;
                    case UpdateType.Update:
                        context.LoadSheddingSlot.Update(loadSheddingSlot);
                        break;
                    case UpdateType.Remove:
                        context.LoadSheddingSlot.Remove(loadSheddingSlot);
                        break;
                }

                context.SaveChanges();

                if (type == UpdateType.Insert)
                {
                    schedule.ScheduleID = loadSheddingSlot.LoadSheddingSlotID;
                }

                return schedule;
            }
            else
            {
                return NextUpdaterHandle(obj, context, type);
            }
        }
    }
}
