using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace OemosProto1
{
  
  public class ActorData
  {
    public delegate void PaintHandler();
    public BrainType BrainType = BrainType.None;
    public ProcessType ActorProcess = ProcessType.None;
    public PaintHandler ActorPainter;
    public int[] PainterArgs1 = new int[10];
    public string[] PainterArgs2 = new string[10];
    public PointF ActorLocation = new PointF();
    public float ActorSize = 3;
    public double MoneyInBank = 1500;
    public double MonthlySalary = 1500;
    public double LastSalaryPay = 0;
    public double[] ProcessSkill = new double[(int)ProcessType.TotalProcessCount];
    public object[] BrainArgs = new object[10];
    public object[] ProcessArgs = new object[10];
    public int layer = 10;
  }

}
