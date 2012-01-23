using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OemosProto1
{


  public enum ProcessType
  {
    None = 0,
    GoingToWorkX,
    GoingToEat,
    TotalProcessCount
  }

  public class ActorProcesses
  {
    string[] ProcessNames = 
    {
      "GoingToWorkX",
      "GoingToEat"
    };

    public static void GoingToWorkX()
    {
    }
    public static void GoingToEat()
    {
    }
  }
}
