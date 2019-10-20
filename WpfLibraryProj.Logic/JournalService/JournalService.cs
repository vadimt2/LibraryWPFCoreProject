using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WpfLibraryProj.Common;
using WpfLibraryProj.DAL;

namespace WpfLibraryProj.Logic.JournalService
{
    public class JournalService : IJournalService
    {
        private readonly IRepository<Journal> _journalRepository;

        public JournalService(IRepository<Journal> journalRepository)
        {
            _journalRepository = journalRepository;
        }

        public IEnumerable<Journal> GetJournals()
        {
            return _journalRepository.GetAll();
        }

        public Journal GetById(Journal journal)
        {
            var getJournal = _journalRepository.GetById(journal.Id);

            return getJournal ?? null;
        }

        public bool AddJournal(Journal journal)
        {
            journal.RentedQuantity = 0;

            journal.Id = 0;

            if (string.IsNullOrWhiteSpace(journal.Publisher) || string.IsNullOrWhiteSpace(journal.Description)
                || string.IsNullOrWhiteSpace(journal.Title) || journal.Quantity == 0 || journal.PublishDate == null)
                return false;
            journal.AbstractItemCategory = AbstractItemCategory.Journal;

            journal.Discount((int)journal.Disscount);

            _journalRepository.Insert(journal);

            _journalRepository.Save();

            return true;
        }

        public void DeleteJournal(Journal journal)
        {
            var getJournal = _journalRepository.GetById(journal.Id);

            if (getJournal == null) return;

            _journalRepository.Delete(getJournal.Id);

            _journalRepository.Save();
        }


        public void UpdateJournal(Journal journal)
        {
            var journalInDb = _journalRepository.GetById(journal.Id);

            if (journalInDb == null) return;

            journalInDb.Title = journal.Title;

            journalInDb.Publisher = journal.Publisher;

            journalInDb.Description = journal.Description;

            journalInDb.Disscount = journal.Disscount;

            journalInDb.JournalCategory = journal.JournalCategory;

            _journalRepository.Save();
        }
    }
}
