using System.Collections.Generic;

namespace WpfLibraryProj.DAL
{
    public interface IRepository<T> where T : class
    {
        void Delete(int id);
        IEnumerable<T> GetAll();
        T GetById(int id);
        void Insert(T obj);
        void Reload();
        bool Save();
        void Update(T obj);
    }
}