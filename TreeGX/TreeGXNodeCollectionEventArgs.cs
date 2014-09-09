using System;

namespace DevComponents.Tree
{
	/// <summary>
	/// Represents event arguments for NodeCollection based events, like BeforeNodeInsert, AfterNodeInsert etc.
	/// </summary>
	public class TreeGXNodeCollectionEventArgs : TreeGXNodeEventArgs
	{
		/// <summary>
		/// Creates new instance of the class.
		/// </summary>
		/// <param name="action">Source action</param>
		/// <param name="node">Affected node</param>
		/// <param name="parentNode">Parent of the node if any</param>
		public TreeGXNodeCollectionEventArgs(eTreeAction action, Node node, Node parentNode):base(action, node)
		{
			this.ParentNode = parentNode;
		}
		
		/// <summary>
		/// Indicates parent node of the affected node. For example if event handled is BeforeNodeInsert parent of the Node is has
		/// not been set yet so this property provides information on the node that will become parent. If this property returns null
		/// then node is being added or removed from the main TreeGX.Nodes collection.
		/// </summary>
		public DevComponents.Tree.Node ParentNode=null;
	}
}
