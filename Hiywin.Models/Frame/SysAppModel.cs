﻿using Hiywin.Entities.Frame;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hiywin.Models.Frame
{
    public class SysAppModel: ISysAppModel
    {
        public int Id { get; set; }
        public string AppNo { get; set; }
        public string AppName { get; set; }
        public string Leader { get; set; }
        public string Deploy { get; set; }
        public string Remark { get; set; }
        public string Creator { get; set; }
        public string CreateName { get; set; }
        public DateTime CreateTime { get; set; }
        public string Updator { get; set; }
        public string UpdateName { get; set; }
        public DateTime? UpdateTime { get; set; }
        public bool IsDelete { get; set; }
    }
}
