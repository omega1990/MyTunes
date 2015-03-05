using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTunes.Data.EntityModel
{
    public class PagedModel<T>
    {
        public IList<T> PagedData { get; set; }
        public IList<int> Pages { get; set; }
        public int CurrentPage { get; set; }
        public bool NextActive { get; set; }
        public bool PreviousActive { get; set; }
        
        public PagedModel(IList<T> pagedData, IList<int> pages, int currentPage, bool nextActive, bool previousActive)
        {
            PagedData = pagedData;
            Pages = pages;
            CurrentPage = currentPage;
            NextActive = nextActive;
            PreviousActive = previousActive;
        }
    }
}
