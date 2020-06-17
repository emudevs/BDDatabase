// Decompiled with JetBrains decompiler
// Type: BDDatabase.Helper.CustomWebClient
// Assembly: BDDatabase, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 13430B3E-0681-4043-8C46-F9A424DA21B6
// Assembly location: C:\Users\Admin\Music\Новая папка (4)\BDDatabase\BDDatabase.exe

using System;
using System.Net;

namespace BDDatabase.Helper
{
  internal class CustomWebClient : WebClient
  {
    protected override WebRequest GetWebRequest(Uri address)
    {
      WebRequest webRequest = base.GetWebRequest(address);
      if (webRequest is HttpWebRequest)
        (webRequest as HttpWebRequest).KeepAlive = false;
      return webRequest;
    }
  }
}
