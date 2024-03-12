using System;
using System.Collections.Generic;
using System.Text;

namespace MyPushNotify.Classes
{
    public class NotifyEventArgs:EventArgs
    {
        public string Action { get; set; }
    }
}
