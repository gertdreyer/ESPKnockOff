﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ESPKnockOff.Models;

namespace ESPKnockOff.Services.Inserters
{
    public abstract class Inserter
    {
        protected Inserter _nextInserter;

        public Inserter()
        {
            _nextInserter = NullInserter.Instance;
        }

        public Inserter SetNextInserter(Inserter inserter)
        {
            _nextInserter = inserter;
            return inserter;
        }

        public void NextInserterHandle(object obj)
        {
            _nextInserter.HandleInsert(obj);
        }

        public abstract void HandleInsert(object obj);
    }

    public class NullInserter : Inserter
    {
        private static readonly NullInserter _instance = new NullInserter();

        private NullInserter() { }

        public static NullInserter Instance
        {
            get { return _instance; }
        }

        public override void HandleInsert(object obj)
        {
            throw new Exception($"No inserted able to handle object of type {obj.GetType()}");
        }
    }

    public class ProvinceInserter : Inserter
    {
        public override void HandleInsert(object obj)
        {
            if (obj is Province)
            {
                // TODO: Write code to insert province object into the DB.
                Console.WriteLine("Province");
            }
            else
            {
                NextInserterHandle(obj);
            }
        }
    }

    public class MunicipalityInserter : Inserter
    {
        public override void HandleInsert(object obj)
        {
            if (obj is Municipality)
            {
                // TODO: Write code to insert municipality object into the DB.
                Console.WriteLine("Municipality");
            }
            else
            {
                NextInserterHandle(obj);
            }
        }
    }

    public class SuburbInserter : Inserter
    {
        public override void HandleInsert(object obj)
        {
            if (obj is Suburb)
            {
                // TODO: Write code to insert municipality object into the DB.
                Console.WriteLine("Suburb");
            }
            else
            {
                NextInserterHandle(obj);
            }
        }
    }
}
