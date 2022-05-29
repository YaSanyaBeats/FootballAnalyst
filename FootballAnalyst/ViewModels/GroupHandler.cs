using System.Collections.Generic;
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
    public class GroupHandler : Handler
    {
        public GroupHandler(QueryManagerViewModel _QueryManager)
        {
            QM = _QueryManager;
        }

        public override void Try()
        {
            if (QM != null)
            {
                if (QM.GroupingColumn != null)
                {
                    try
                    {
                        var result = QM.ResultTable.GroupBy(item => item[QM.GroupingColumn]).ToList();
                        QM.ResultTable.Clear();
                        foreach (var group in result)
                        {
                            foreach (Dictionary<string, object?> row in group)
                            {
                                QM.ResultTable.Add(row);
                            }
                        }
                        if (QM.ResultTable.Count != 0)
                        {
                            QM.IsRequestSuccess = true;

                            if (NextHope != null)
                            {
                                NextHope.Try();
                            }
                        }

                        else
                        {
                            QM.IsRequestSuccess = false;
                            return;
                        }
                    }
                    catch
                    {
                        QM.IsRequestSuccess = false;
                        return;
                    }
                }

                else if (QM.GroupFilters.Count == 1 && QM.GroupFilters[0].FilterVal == ""
                        && QM.GroupFilters[0].Operator == "" && QM.GroupFilters[0].Column == "")
                {
                    QM.IsRequestSuccess = true;
                    return;
                }
                else
                {
                    QM.IsRequestSuccess = false;
                    return;
                }
            }
            else
            {
                return;
            }
        }
    }
}
