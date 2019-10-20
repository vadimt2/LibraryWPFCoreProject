using System;
using System.Collections.Generic;
using System.Text;

namespace WpfLibraryProj.DAL
{
    public abstract class BaseRepository
    {
        internal LibraryContext _context;
        public BaseRepository(LibraryContext context)
        {
            _context = context;
        }
    }
}
