﻿using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using ReactiveUI;

using System.Reactive;
using FootballAnalyst.Models;
using Microsoft.Data.Sqlite;
using System.IO;
using System;

namespace FootballAnalyst.ViewModels
{
    public abstract class Handler
    {
        protected QueryManagerViewModel? QM { get; set; } = null;
        public Handler? NextHope { get; set; }
        public abstract void Try();
    }
}
