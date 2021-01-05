using System;
using System.Collections.Generic;
using System.Text;

namespace MyPortal.Logic.Models.Data
{
    public class TreeNode
    {
        public TreeNode()
        {
            Children = new HashSet<TreeNode>();
        }

        public string Id { get; set; }
        public string Text { get; set; }
        public string Icon { get; set; }
        public TreeNodeState State { get; set; }
        public ICollection<TreeNode> Children { get; set; }

        public static TreeNode CreateRoot(string name)
        {
            return new TreeNode
            {
                Id = "#",
                Text = name,
                State = new TreeNodeState
                {
                    Opened = true,
                    Disabled = false,
                    Selected = false
                }
            };
        }

        public void SetEnabled(bool enabled)
        {
            State.Disabled = !enabled;

            foreach (var treeNode in Children)
            {
                treeNode.SetEnabled(enabled);
            }
        }
    }
}
