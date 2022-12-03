using System.Collections.ObjectModel;

namespace UI.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class TreeViewNode
    {
        /// <summary>
        /// Коллекция потомков
        /// </summary>
        public ObservableCollection<TreeViewNode> Childrens { get; }

        /// <summary>
        /// Значение
        /// </summary>
        public string Value { get; }

        public TreeViewNode(string value)
        {
            Value = value;
            Childrens = new ObservableCollection<TreeViewNode>();
        }

    }
}