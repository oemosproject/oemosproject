using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace OemosProto1
{
  public class PaintCommands
  {
    public static PointF loc;
    public static int[] args1;
    public static string[] args2;
    public static Dictionary<int, Pen> pens = new Dictionary<int, Pen>();
    public static Dictionary<int, Brush> brushes = new Dictionary<int, Brush>();
    public static Graphics g;
    public static float resultedWidth;

    public static void PaintRedCircle()
    {
      float width = 3;
      Pen pen1 = new Pen(Color.Red, 0.0F);
      g.DrawArc(pen1, loc.X - width / 2, loc.Y - width / 2, width, width, 0, 360);
    }

    public static void PaintCircle()
    {
      if (!pens.ContainsKey(args1[0]))
        pens.Add(args1[0], new Pen(Color.FromArgb(args1[0]), 0.0F));

      resultedWidth = args1[1];
      g.DrawArc(pens[args1[0]], loc.X - resultedWidth / 2, loc.Y - resultedWidth / 2, resultedWidth, resultedWidth, 0, 360);
    }

    public static void PaintFilledCircle()
    {
      if (!brushes.ContainsKey(args1[0]))
        brushes.Add(args1[0], new SolidBrush(Color.FromArgb(args1[0])));

      resultedWidth = args1[1];
      g.FillEllipse(brushes[args1[0]], loc.X - resultedWidth / 2, loc.Y - resultedWidth / 2, resultedWidth, resultedWidth);
    }
  }
}
