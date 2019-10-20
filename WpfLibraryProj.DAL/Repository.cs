using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;

namespace WpfLibraryProj.DAL
{
    public class Repository<T> : BaseRepository, IRepository<T> where T : class
    {
        private DbSet<T> table = null;

        public Repository(LibraryContext context) : base(context)
        {

            table = _context.Set<T>();
        }

        public IEnumerable<T> GetAll()
        {
            return table;
        }

        public T GetById(int id)
        {
            return table.Find(id);
        }

        public void Insert(T obj)
        {
            table.Add(obj);
        }

        public void Update(T obj)
        {
            table.Attach(obj);
            _context.Entry(obj).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            T existing = table.Find(id);
            table.Remove(existing);
        }

        public void Reload()
        {
       
        }

        public bool Save()
        {
            int result = _context.SaveChanges();
            return result == 0 ? false : true;
        }
    }
}
