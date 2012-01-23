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
  public partial class OemosForm : Form
  {
    List<ActorData> WorldActors = new List<ActorData>();
    List<int> SelectedActors = new List<int>();
    RectangleF ViewBounds = new RectangleF(-100, -100, 200, 200);
    double WorldTime = 0;
    double GovermentTaxes = 0;
    Point CurrentSelectionFrom;
    Point CurrentSelectionTo;
    bool SimulationEnabled = false;

    public OemosForm()
    {
      InitializeComponent();
      pictureBox1.Paint += new PaintEventHandler(pictureBox1_Paint);
      listBox1.Items.Clear();
      foreach (string n in ActorBrains.BrainCombinations)
        listBox1.Items.Add(n);
      button6_Click(null, null);
      button7_Click(null, null);
    }

    void SimulateWorld()
    {
      int stepCount = 100;
      double timeStep = 1.0; // 1 second
      while (stepCount-- != 0)
      {
        WorldTime += timeStep;

        // Decide next process
        foreach (ActorData actor in WorldActors)
          if (actor.ActorProcess == ProcessType.None)
          { 
          
          }

        // Transfer salaries
        foreach (ActorData actor in WorldActors)
          if (WorldTime - actor.LastSalaryPay > 3600 * 24 * 31)
          {
            actor.MoneyInBank += actor.MonthlySalary * 0.77;
            GovermentTaxes += actor.MonthlySalary * 0.23;
            actor.LastSalaryPay = WorldTime;
          }

      }
    }

    void pictureBox1_Paint(object sender, PaintEventArgs e)
    {
      Graphics g = e.Graphics;
      SimulateWorld();
      float width = 3;
      float hh = ViewBounds.Width * pictureBox1.Height / pictureBox1.Width;
      if (ViewBounds.Height != hh)
      {
        ViewBounds.Y = ViewBounds.Y + ViewBounds.Height / 2 - hh / 2;
        ViewBounds.Height = hh;
      }

      
      
      g.ScaleTransform((float)pictureBox1.Width / ViewBounds.Width, (float)pictureBox1.Height / ViewBounds.Height);      
      g.TranslateTransform(-ViewBounds.X, -ViewBounds.Y);

      float w = 200 / 10;
      float h = 200 / 10;
      Pen pen = new Pen(Color.LightBlue, 0.0F);
      for (int x = 0; x < 11; x++)
        g.DrawLine(pen, -100 + x * w, -100, -100 + x * w, 100);
      for (int y = 0; y < 11; y++)
        g.DrawLine(pen, -100, -100 + y * h, 100, -100 + y * h);

      Pen pen2 = new Pen(Color.Red, 0.0F);
      PaintCommands.g = g;
      for (int i = 0; i < WorldActors.Count; i++)
      {
        ActorData actor = WorldActors[i];
        PaintCommands.loc = actor.ActorLocation;
        PaintCommands.args1 = actor.PainterArgs1;
        PaintCommands.args2 = actor.PainterArgs2;
        actor.ActorPainter();
        if (SelectedActors.Contains(i))
        {
          float width2 = PaintCommands.resultedWidth + 1;
          g.DrawRectangle(pen2, actor.ActorLocation.X - width2 / 2, actor.ActorLocation.Y - width2 / 2, width2, width2);
        }
      }

      PaintCommands.g = null;
      g.ResetTransform();
      if (ShowSelectionRect)
      {
        g.DrawRectangle(pen2, CurrentSelectionFrom.X, CurrentSelectionFrom.Y, CurrentSelectionTo.X - CurrentSelectionFrom.X, CurrentSelectionTo.Y - CurrentSelectionFrom.Y);
      }

      pictureBox1.Invalidate();
    }

    void ResetButtons()
    {
      button1.BackColor = System.Drawing.SystemColors.Control;
      button1.UseVisualStyleBackColor = true;
      button5.BackColor = System.Drawing.SystemColors.Control;
      button5.UseVisualStyleBackColor = true;
      button6.BackColor = System.Drawing.SystemColors.Control;
      button6.UseVisualStyleBackColor = true;
      label1.Text = "";
    }
    enum MouseMode
    { 
      None,
      Move,
      Select,
      Add,
      Delete,
    }
    MouseMode CurrentMouseMode = MouseMode.Select;

    private void button6_Click(object sender, EventArgs e)
    {
      ResetButtons();
      button6.BackColor = System.Drawing.SystemColors.ButtonHighlight;
      label1.Text = "Drag to pan, scroll wheel to zoom...";
      CurrentMouseMode = MouseMode.Move;

    }

    private void button5_Click(object sender, EventArgs e)
    {
      ResetButtons();
      button5.BackColor = System.Drawing.SystemColors.ButtonHighlight;
      label1.Text = "Paint rectangle to select actors...";
      CurrentMouseMode = MouseMode.Select;
    }
    private void button1_Click(object sender, EventArgs e)
    {
      ResetButtons();
      button1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
      label1.Text = "Select actor to add on the right, the click on the left...";
      CurrentMouseMode = MouseMode.Add;
    }

    private void button2_Click(object sender, EventArgs e)
    {
      //ResetButtons();
      
    }


    private void pictureBox1_Click(object sender, EventArgs e)
    {

    }

    private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
    {
      if (e.Button == MouseButtons.None)
        ShowSelectionRect = false;

      if (CurrentMouseMode == MouseMode.Move)
        if (e.Button == MouseButtons.Left)
        {
          ViewBounds.X -= (float)(e.Location.X - mouseMovedAt.X) * ViewBounds.Width / pictureBox1.Width;
          ViewBounds.Y -= (float)(e.Location.Y - mouseMovedAt.Y) * ViewBounds.Height / pictureBox1.Height;
        }
      mouseMovedAt = e.Location;
      CurrentSelectionFrom = mouseDownAt;
      CurrentSelectionTo = e.Location;
      if (CurrentSelectionFrom.X > CurrentSelectionTo.X)
      {
        int tmp = CurrentSelectionFrom.X;
        CurrentSelectionFrom.X = CurrentSelectionTo.X;
        CurrentSelectionTo.X = tmp;
      }
      if (CurrentSelectionFrom.Y > CurrentSelectionTo.Y)
      {
        int tmp = CurrentSelectionFrom.Y;
        CurrentSelectionFrom.Y = CurrentSelectionTo.Y;
        CurrentSelectionTo.Y = tmp;
      }
    }

    private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
    {

    }

    Point mouseDownAt;
    Point mouseMovedAt;
    bool ShowSelectionRect = false;
    private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
    {
      if (CurrentMouseMode == MouseMode.Select)
        ShowSelectionRect = true;
      mouseDownAt = e.Location;
      mouseMovedAt = e.Location;
      CurrentSelectionFrom = e.Location;
      CurrentSelectionTo = e.Location;
    }

    public static int SortByLayer(ActorData a, ActorData b)
    {
      return a.layer.CompareTo(b.layer);
    }
    private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
    {
      ShowSelectionRect = false;
      if (CurrentMouseMode == MouseMode.Add)
      {
        if (listBox1.SelectedIndex == -1)
          listBox1.SelectedIndex = 0;
        string[] createActors = (listBox1.Items[listBox1.SelectedIndex] + "").Split('+');
        foreach (string a in createActors)
        {
          ActorBrains.actor = new ActorData();
          typeof(ActorBrains).InvokeMember(a, System.Reflection.BindingFlags.InvokeMethod, null, null, null);
          float x = e.Location.X * ViewBounds.Width / pictureBox1.Width + ViewBounds.Left;
          float y = e.Location.Y * ViewBounds.Height / pictureBox1.Height + ViewBounds.Top;
          ActorBrains.actor.ActorLocation = new PointF(x, y);
          WorldActors.Add(ActorBrains.actor);
          WorldActors.Sort(SortByLayer);
        }
      }

      if (CurrentMouseMode == MouseMode.Select)
      {
        RectangleF painted = new RectangleF();
        painted.X = Math.Min(mouseDownAt.X, e.Location.X) * ViewBounds.Width / pictureBox1.Width + ViewBounds.Left;
        painted.Y = Math.Min(mouseDownAt.Y, e.Location.Y) * ViewBounds.Height / pictureBox1.Height + ViewBounds.Top;
        painted.Width = Math.Max(mouseDownAt.X, e.Location.X) * ViewBounds.Width / pictureBox1.Width + ViewBounds.Left -
          painted.X;
        painted.Height = Math.Max(mouseDownAt.Y, e.Location.Y) * ViewBounds.Height / pictureBox1.Height + ViewBounds.Top -
          painted.Y;

        SelectedActors.Clear();
        foreach (ActorData actor in WorldActors)
          if (painted.Contains(actor.ActorLocation))
            SelectedActors.Add(WorldActors.IndexOf(actor));
      }
    }

    private void button7_Click(object sender, EventArgs e)
    {
      SimulationEnabled = !SimulationEnabled;
      if (SimulationEnabled)
        button7.BackColor = System.Drawing.SystemColors.ButtonHighlight;
      else
        button7.BackColor = System.Drawing.SystemColors.Control;
      button7.UseVisualStyleBackColor = !SimulationEnabled;
    }

  }
}
