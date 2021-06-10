using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ESPKnockOff.Models;

namespace ESPKnockOff.Services
{
   public class InsertService
   {
      private Inserter _inserterChain;
      public InsertService()
      {
         _inserterChain = new ProvinceInserter();
         //_inserterChain.SetNextInserter(new MunicplaityInserter())
      }

      public void Insert(Object obj)
      {
         _inserterChain.HandleInsert(obj);
      }
   }

   public abstract class Inserter
   {
      public Inserter()
      {
         _nextInserter = null;
      }

      private Inserter _nextInserter;

      public Inserter SetNextInserter(Inserter inserter) 
      {
         if(_nextInserter == null)
         {
            _nextInserter = inserter;
         }
         else
         {
            _nextInserter.SetNextInserter(inserter);
         }
         return inserter;
      }

      public void NextInserterHandle(Object obj)
      {
         if(_nextInserter != null)
         {
            _nextInserter.HandleInsert(obj);
         }
         else
         {
            //ERROR
         }
      }

      public abstract void HandleInsert(Object obj);
   }

   public class ProvinceInserter : Inserter
   {
      public override void HandleInsert(object obj)
      {
         if(obj is Province)
         {
            //Insert 
         }
         else
         {
            NextInserterHandle(obj);
         }
      }
   }


}
