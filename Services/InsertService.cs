﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ESPKnockOff.Models;
using ESPKnockOff.Services.Inserters;

namespace ESPKnockOff.Services
{
    public class InsertService
    {
        private readonly Inserter _inserterChain;
        private readonly ApplicationContext _context;

        public InsertService(ApplicationContext context)
        {
            _context = context;
            _inserterChain = new ProvinceInserter();
            _inserterChain.SetNextInserter(new MunicipalityInserter()).SetNextInserter(new SuburbInserter());
        }

        public void Insert(object obj)
        {
            _inserterChain.HandleInsert(obj, _context);
        }

        public void Test()
        {
            // Test Code
            Insert(new Province());
            Insert(new Municipality());
            Insert(new Suburb());

            // This line should throw an error
            //Insert(new Object());
        }
    }
}
