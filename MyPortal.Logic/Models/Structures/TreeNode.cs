using System.Collections.Generic;

namespace MyPortal.Logic.Models.Structures
{
    public class TreeNode
    {
        public TreeNode()
        {
            Children = new HashSet<TreeNode>();
            State = TreeNodeState.Default;
        }

        public string Id { get; set; }
        public string Text { get; set; }
        public string Icon { get; set; }
        public TreeNodeState State { get; set; }
        public ICollection<TreeNode> Children { get; set; }

        public static TreeNode CreateRoot(string id, string name)
        {
            return new TreeNode
            {
                Id = id,
                Text = name,
                State = TreeNodeState.Open
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
