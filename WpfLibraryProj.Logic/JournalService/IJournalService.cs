using System.Collections.Generic;
using WpfLibraryProj.Common;

namespace WpfLibraryProj.Logic.JournalService
{
    public interface IJournalService
    {
        bool AddJournal(Journal journal);
        void DeleteJournal(Journal journal);
        Journal GetById(Journal journal);
        IEnumerable<Journal> GetJournals();
        void UpdateJournal(Journal journal);
    }
}