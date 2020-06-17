// Decompiled with JetBrains decompiler
// Type: BDDatabase.Program
// Assembly: BDDatabase, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 13430B3E-0681-4043-8C46-F9A424DA21B6
// Assembly location: C:\Users\Admin\Music\Новая папка (4)\BDDatabase\BDDatabase.exe

using System;
using System.Windows.Forms;

namespace BDDatabase
{
  internal static class Program
  {
    [STAThread]
    private static void Main()
    {
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      Application.Run((Form) new Form1());
    }
  }
}
