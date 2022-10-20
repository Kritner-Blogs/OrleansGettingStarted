using System;
using System.Collections.Generic;
using System.Text;

namespace Kritner.OrleansGettingStarted.Client.OrleansFunctionExamples;

public interface IOrleansFunctionProvider
{
    IList<IOrleansFunction> GetOrleansFunctions();
}
