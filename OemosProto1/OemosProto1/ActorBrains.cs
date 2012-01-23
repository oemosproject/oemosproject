using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OemosProto1
{
  public enum BrainType
  {
    None = 0,
    NormalPerson,
    NormalHome,
    ResourceMetal,
    WorkPlaceX,
    MovieTheater,
  }

  public class ActorBrains
  {
    public static ActorData actor;
    public static string[] BrainCombinations = 
    {
      "NormalPerson", 
      "NormalPerson+NormalHome", 
      "NormalHome", 
      "WorkPlaceX",
      "MovieTheater",
    };
    public static void NormalPerson()
    {
      actor.BrainType = BrainType.NormalPerson;
      actor.ProcessSkill[(int)ProcessType.GoingToWorkX] = 1;
      actor.ProcessSkill[(int)ProcessType.GoingToEat] = 1;
      actor.ActorPainter += PaintCommands.PaintFilledCircle;
      uint col = 0xff7777ff;
      actor.PainterArgs1[0] = (int)col;
      actor.PainterArgs1[1] = 3;
      actor.layer = 10;
    }
    public static void NormalHome()
    {
      actor.BrainType = BrainType.NormalHome;
      actor.ActorPainter += PaintCommands.PaintCircle;
      uint col = 0xff000077;
      actor.PainterArgs1[0] = (int)col;
      actor.PainterArgs1[1] = 10;
      actor.layer = 2;
    }
    public static void WorkPlaceX()
    {
      actor.BrainType = BrainType.WorkPlaceX;
      actor.ActorPainter += PaintCommands.PaintCircle;
      uint col = 0xffaaffff;
      actor.PainterArgs1[0] = (int)col;
      actor.PainterArgs1[1] = 10;
      actor.layer = 1;
    }
    public static void MovieTheater()
    {
      actor.BrainType = BrainType.MovieTheater;
      actor.ActorPainter += PaintCommands.PaintFilledCircle;
      uint col = 0xffccccff;
      actor.PainterArgs1[0] = (int)col;
      actor.PainterArgs1[1] = 40;
      actor.layer = 0;
    }
  }
}
