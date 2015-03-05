using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyTunes.Web.Api.ViewModels
{
    public class PagedViewModel<T>
    {
        public IList<T> PagedData { get; set; }
        public IList<int> Pages { get; set; }
        public int CurrentPage { get; set; }
        public bool NextActive { get; set; }
        public bool PreviousActive { get; set; }
    }
}