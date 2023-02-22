using ChessTaskEFSignalR.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChessTaskEFSignalR
{
    public interface IRepository<T> where T : class
    {
        public string DbConnectionString { get; }
        public T GetElemByNumber(int number);
        public T GetElemFirst();
        public int Count();
        public void Save();
        public IEnumerable<Step> GetListOfElems();
        public int? GetFirstElemX();
        public int? GetFirstElemY();
        public void CorrectDataBase(int coordX, int coordY);
        public Task Initialize();
    }
}
