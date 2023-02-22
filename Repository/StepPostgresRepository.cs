using ChessTaskEFSignalR.Models;
using ChessTaskEFSignalR.Repository.DataBaseContext;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChessTaskEFSignalR
{
    public class StepPostgresRepository : IRepository<Step>
    {
        private string _dbConnectionString;
        private IConfiguration _configuration;

        public string DbConnectionString
        {
            get { return _dbConnectionString; }
        }
        public ChessDbContext Db { get; set; }

        public StepPostgresRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _dbConnectionString = _configuration.GetConnectionString("PostgresDataBaseConnection");
            Db = new ChessDbContext(DbConnectionString);
        }
#nullable enable
        public Step? GetElemByNumber(int number)
        {
            try
            {
                var step = Db.Steps.Where(step => step.StepNumber == number).First();
                return step;
            }
            catch(Exception ex)
            {
                return null;
            }
        }
        public Step? GetElemFirst()
        {
            return Db.Steps?.First();
        }
        public IEnumerable<Step> GetListOfElems()
        {
            return Db.Steps.ToList();
        }
        public int Count()
        {
            return Db.Steps.Count();
        }
        public void Save()
        {
            Db.SaveChanges();
        }

        public int? GetFirstElemX()
        {
            return GetElemByNumber(0)?.X;
        }

        public int? GetFirstElemY()
        {
            return GetElemByNumber(0)?.Y;
        }

        public void UpdateItemStepNumber(int prevNumber, int curNumber)
        {
            var item = GetElemByNumber(prevNumber);
            if (item != null)
            {
                item.StepNumber = curNumber;
                Save();
            }
        }

        public void CorrectDataBase(int coordX, int coordY)
        {
            int curStepNumber = 0;
            foreach (var step in Db.Steps)
            {
                if (step.X == coordX && step.Y == coordY)
                {
                    curStepNumber = step.StepNumber;
                }
            }
            if (curStepNumber == 0)
                return;
            for (int i = 0; i < curStepNumber; i++)
            {
                UpdateItemStepNumber(i, this.Count() + i) ;
            }
            for (int i = curStepNumber; i< this.Count() + curStepNumber; i++)
            {
                UpdateItemStepNumber(i, i - curStepNumber);
            }
        }
        public async Task Initialize()
        {
            await DbInitializer.InitializeDefault(Db);
        }
    }
}
