using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ESPKnockOff.Data;
using ESPKnockOff.Models;

namespace ESPKnockOff.Services.Getters {
	public class FilteringCoditions {
		public int Day { get; set; } = 0;
		public string FromTime { get; set; } = null;
		public string ToTime { get; set; } = null;
		public int Stage { get; set; } = 0;
	}

	public abstract class Getter {
		protected Getter _nextGetter;

		public Getter() {
			_nextGetter = NullGetter.Instance;
		}

		public Getter SetNextGetter(Getter getter) {
			_nextGetter = getter;
			return getter;
		}

		public abstract Task<List<T>> GetObjects<T>(FilteringCoditions filteringConditions, ApplicationContext context);
		public abstract Task<T> GetObjectById<T>(int id, ApplicationContext context);
		public abstract Task<List<Y>> GetObjectSubObjects<T, Y>(int id, FilteringCoditions filteringConditions, ApplicationContext context);
	}

	public class NullGetter : Getter {
		private static readonly NullGetter _instance = new NullGetter();

		private NullGetter() { }

		public static NullGetter Instance {
			get { return _instance; }
		}

		public override async Task<List<T>> GetObjects<T>(FilteringCoditions filteringConditions, ApplicationContext context) {
			throw new Exception($"No Getter able to get objects of type {typeof(T)}");
		}

		public override async Task<T> GetObjectById<T>(int id, ApplicationContext context) {
			throw new Exception($"No Getter able to get object of type {typeof(T)}");
		}

		public override async Task<List<Y>> GetObjectSubObjects<T, Y>(int id, FilteringCoditions filteringConditions, ApplicationContext context) {
			throw new Exception($"No Getter able to get object's sub objects of types {typeof(T)} and {typeof(Y)}");
		}
	}

	public class ProvinceGetter : Getter {
		public override async Task<List<T>> GetObjects<T>(FilteringCoditions filteringConditions, ApplicationContext context) {
			if (typeof(T) == typeof(Province)) {
				var provinces = context.Province.ToList();
				return (List<T>)Convert.ChangeType(provinces, typeof(List<T>));
			} else {
				return await _nextGetter.GetObjects<T>(filteringConditions, context);
			}
		}

		public override async Task<T> GetObjectById<T>(int id, ApplicationContext context) {
			if (typeof(T) == typeof(Province)) {
				var province = await context.Province.FindAsync(id);
				return (T)Convert.ChangeType(province, typeof(T));
			} else {
				return await _nextGetter.GetObjectById<T>(id, context);
			}
		}

		public override async Task<List<Y>> GetObjectSubObjects<T, Y>(int id, FilteringCoditions filteringConditions, ApplicationContext context) {
			if (typeof(T) == typeof(Province) && typeof(Y) == typeof(Municipality)) {
				var municipalities = context.Municipality.Where(c => c.ProvinceID == id).ToList();
				return (List<Y>)Convert.ChangeType(municipalities, typeof(List<Y>));
			} else {
				return await _nextGetter.GetObjectSubObjects<T, Y>(id, filteringConditions, context);
			}
		}
	}

	public class MunicipalityGetter : Getter {
		public override async Task<List<T>> GetObjects<T>(FilteringCoditions filteringConditions, ApplicationContext context) {
			if (typeof(T) == typeof(Municipality)) {
				var municipalities = context.Municipality.ToList();
				return (List<T>)Convert.ChangeType(municipalities, typeof(List<T>));
			} else {
				return await _nextGetter.GetObjects<T>(filteringConditions, context);
			}
		}

		public override async Task<T> GetObjectById<T>(int id, ApplicationContext context) {
			if (typeof(T) == typeof(Municipality)) {
				var municipalities = await context.Municipality.FindAsync(id);
				return (T)Convert.ChangeType(municipalities, typeof(T));
			} else {
				return await _nextGetter.GetObjectById<T>(id, context);
			}
		}

		public override async Task<List<Y>> GetObjectSubObjects<T, Y>(int id, FilteringCoditions filteringConditions, ApplicationContext context) {
			if (typeof(T) == typeof(Municipality) && typeof(Y) == typeof(Suburb)) {
				var suburbs = context.Suburb.Where(c => c.MunicipalityID == id).ToList();
				return (List<Y>)Convert.ChangeType(suburbs, typeof(List<Y>));
			} else {
				return await _nextGetter.GetObjectSubObjects<T, Y>(id, filteringConditions, context);
			}
		}
	}

	public class SuburbGetter : Getter {
		public override async Task<List<T>> GetObjects<T>(FilteringCoditions filteringConditions, ApplicationContext context) {
			if (typeof(T) == typeof(Suburb)) {
				var suburbs = context.Suburb.ToList();
				return (List<T>)Convert.ChangeType(suburbs, typeof(List<T>));
			} else {
				return await _nextGetter.GetObjects<T>(filteringConditions, context);
			}
		}

		public override async Task<T> GetObjectById<T>(int id, ApplicationContext context) {
			if (typeof(T) == typeof(Suburb)) {
				var suburbs = await context.Suburb.FindAsync(id);
				return (T)Convert.ChangeType(suburbs, typeof(T));
			} else {
				return await _nextGetter.GetObjectById<T>(id, context);
			}
		}

		public override async Task<List<Y>> GetObjectSubObjects<T, Y>(int id, FilteringCoditions filteringConditions, ApplicationContext context) {
			if (typeof(T) == typeof(Suburb) && typeof(Y) == typeof(Schedule)) {
				var suburb = await context.Suburb.FindAsync(id);

				var schedules = context.LoadSheddingSlot.Join(
						context.TimeCode,
						slot => slot.TimeCodeID,
						timecode => timecode.TimeCodeID,
						(slot, timecode) => new Schedule
						{
							ScheduleID = slot.LoadSheddingSlotID,
							Day = slot.DayOfMonthID,
							Stage = slot.StageID,
							StartTime = timecode.StartTime,
							EndTime = timecode.EndTime,
							SuburbClusterID = slot.SuburbClusterID
						}
					).Where(schedule => schedule.SuburbClusterID == suburb.SuburbClusterID);

				if (filteringConditions != null) {
					if (filteringConditions.FromTime != null) {
						schedules = schedules.Where(c => c.StartTime >= TimeSpan.Parse(filteringConditions.FromTime));
					}

					if (filteringConditions.ToTime != null) {
						schedules = schedules.Where(c => c.EndTime <= TimeSpan.Parse(filteringConditions.ToTime));
					}

					if (filteringConditions.Day != 0) {
						schedules = schedules.Where(c => c.Day == filteringConditions.Day);
					}

					if (filteringConditions.Stage != 0) {
						schedules = schedules.Where(c => c.Stage <= filteringConditions.Stage);
					}
				}

				return (List<Y>)Convert.ChangeType(schedules.ToList(), typeof(List<Y>));
			} else {
				return await _nextGetter.GetObjectSubObjects<T, Y>(id, filteringConditions, context);
			}
		}
	}

	public class ScheduleGetter : Getter
	{
		public override async Task<List<T>> GetObjects<T>(FilteringCoditions filteringConditions, ApplicationContext context)
		{
			if (typeof(T) == typeof(Schedule))
			{
				var schedules = context.LoadSheddingSlot.Join(
					context.TimeCode,
					slot => slot.TimeCodeID,
					timecode => timecode.TimeCodeID,
					(slot, timecode) => new Schedule
					{
						ScheduleID = slot.LoadSheddingSlotID,
						Day = slot.DayOfMonthID,
						Stage = slot.StageID,
						StartTime = timecode.StartTime,
						EndTime = timecode.EndTime,
						SuburbClusterID = slot.SuburbClusterID
					}
				);

				if (filteringConditions != null)
				{
					if (filteringConditions.FromTime != null)
					{
						schedules = schedules.Where(c => c.StartTime >= TimeSpan.Parse(filteringConditions.FromTime));
					}

					if (filteringConditions.ToTime != null)
					{
						schedules = schedules.Where(c => c.EndTime <= TimeSpan.Parse(filteringConditions.ToTime));
					}

					if (filteringConditions.Day != 0)
					{
						schedules = schedules.Where(c => c.Day == filteringConditions.Day);
					}

					if (filteringConditions.Stage != 0)
					{
						schedules = schedules.Where(c => c.Stage <= filteringConditions.Stage);
					}
				}

				return (List<T>)Convert.ChangeType(schedules.ToList(), typeof(List<T>));
			}
			else
			{
				return await _nextGetter.GetObjects<T>(filteringConditions, context);
			}
		}

		public override async Task<T> GetObjectById<T>(int id, ApplicationContext context)
		{
			if (typeof(T) == typeof(Schedule))
			{
				var loadSheddingSlot = await context.LoadSheddingSlot.FindAsync(id);

				if (loadSheddingSlot == null)
                {
					return (T)Convert.ChangeType(null, typeof(T));
				}

				var schedule = new Schedule()
				{
					ScheduleID = loadSheddingSlot.LoadSheddingSlotID,
					Day = loadSheddingSlot.DayOfMonthID,
					Stage = loadSheddingSlot.StageID,
					SuburbClusterID = loadSheddingSlot.SuburbClusterID,
				};

				var timeCode = await context.TimeCode.FindAsync(loadSheddingSlot.TimeCodeID);

				schedule.StartTime = timeCode.StartTime;
				schedule.EndTime = timeCode.EndTime;

				return (T)Convert.ChangeType(schedule, typeof(T));
			}
			else
			{
				return await _nextGetter.GetObjectById<T>(id, context);
			}
		}

		public override async Task<List<Y>> GetObjectSubObjects<T, Y>(int id, FilteringCoditions filteringConditions, ApplicationContext context)
		{
			return await _nextGetter.GetObjectSubObjects<T, Y>(id, filteringConditions, context);
		}
	}
}
