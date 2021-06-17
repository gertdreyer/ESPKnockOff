using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ESPKnockOff.Models;
using ESPKnockOff.Data;

namespace ESPKnockOff.Services.Updaters {
	public enum UpdateType {
		Insert,
		Update,
		Remove
	}

	public abstract class Updater {
		protected Updater _nextUpdater;

		public Updater() {
			_nextUpdater = NullUpdater.Instance;
		}

		public Updater SetNextUpdater(Updater inserter) {
			_nextUpdater = inserter;
			return inserter;
		}

		public void NextUpdaterHandle(object obj, ApplicationContext context, UpdateType type) {
			_nextUpdater.HandleUpdate(obj, context, type);
		}

		public abstract void HandleUpdate(object obj, ApplicationContext context, UpdateType type);
	}

	public class NullUpdater : Updater {
		private static readonly NullUpdater _instance = new NullUpdater();

		private NullUpdater() { }

		public static NullUpdater Instance {
			get { return _instance; }
		}

		public override void HandleUpdate(object obj, ApplicationContext context, UpdateType type) {
			throw new Exception($"No updater able to handle object of type {obj.GetType()}");
		}
	}

	public class ProvinceUpdater : Updater {
		public override void HandleUpdate(object obj, ApplicationContext context, UpdateType type) {
			if (obj is Province) {
				switch (type) {
					case UpdateType.Insert:
						context.Province.Add((Province)obj);
						break;
					case UpdateType.Update:
						context.Province.Update((Province)obj);
						break;
					case UpdateType.Remove:
						context.Province.Remove((Province)obj);
						break;
				}
			} else {
				NextUpdaterHandle(obj, context, type);
			}
		}
	}

	public class MunicipalityUpdater : Updater {
		public override void HandleUpdate(object obj, ApplicationContext context, UpdateType type) {
			if (obj is Municipality) {
				switch (type) {
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
			} else {
				NextUpdaterHandle(obj, context, type);
			}
		}
	}

	public class SuburbUpdater : Updater {
		public override void HandleUpdate(object obj, ApplicationContext context, UpdateType type) {
			if (obj is Suburb) {
				switch (type) {
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
			} else {
				NextUpdaterHandle(obj, context, type);
			}
		}
	}

	public class ScheduleUpdater : Updater {
		public override void HandleUpdate(object obj, ApplicationContext context, UpdateType type) {
			if (obj is Suburb) {
				switch (type) {
					case UpdateType.Insert:
						context.LoadSheddingSlot.Add((Schedule)obj);
						break;
					case UpdateType.Update:
						context.LoadSheddingSlot.Update((Schedule)obj);
						break;
					case UpdateType.Remove:
						context.LoadSheddingSlot.Remove((Schedule)obj);
						break;
				}
			} else {
				NextUpdaterHandle(obj, context, type);
			}
		}
	}
}
