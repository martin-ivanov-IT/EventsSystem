using System;
using System.Collections.Generic;
using System.Text;

namespace EventsSystem.Web.ViewModels.Home
{
    public class IndexViewModel
    {
        public IEnumerable<IndexEventViewModel> Events { get; set; }

        public IEnumerable<IndexEventViewModel> EventsByCity { get; set; }
    }
}
