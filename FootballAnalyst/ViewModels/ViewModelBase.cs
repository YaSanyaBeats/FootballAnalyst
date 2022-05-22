using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Text;

namespace FootballAnalyst.ViewModels
{
    public class ViewModelBase : ReactiveObject
    {
        public virtual object GetTable() { return null; }
    }
}
