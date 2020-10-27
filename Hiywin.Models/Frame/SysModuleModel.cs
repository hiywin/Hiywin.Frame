using Hiywin.Entities.Frame;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hiywin.Models.Frame
{
    public class SysModuleModel : ISysModuleModel
    {
        public int Id { get; set; }
        public string ModuleNo { get; set; }
        public string ModuleName { get; set; }
        public string ParentNo { get; set; }
        public string Icon { get; set; }
        public string Url { get; set; }
        public string Category { get; set; }
        public string Target { get; set; }
        public string AppNo { get; set; }
        public string Creator { get; set; }
        public string CreateName { get; set; }
        public DateTime CreateTime { get; set; }
        public string Updator { get; set; }
        public string UpdateName { get; set; }
        public DateTime? UpdateTime { get; set; }
        public bool IsDelete { get; set; }
        public int Sort { get; set; }
        public string RouterName { get; set; }
        public int ChildrenCount { get; set; }
        public bool HasChildren
        {
            get
            {
                if (ChildrenCount > 0)
                    return true;
                else
                    return false;
            }
            set
            {
                this.HasChildren = value;
            }
        }
    }
}
