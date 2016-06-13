using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MMAWPF
{
   static class  TreeViewExpand
   {
      /// <summary>
      /// 
      /// </summary>
      /// <param name="treeView"></param>
      public static void ExpandAll(this System.Windows.Controls.TreeView treeView)
      {
         ExpandInternal(treeView);
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="targetItemContainer"></param>
      private static void ExpandInternal(System.Windows.Controls.ItemsControl targetItemContainer)
      {
         if (targetItemContainer == null) return;
         if (targetItemContainer.Items == null) return;
         for (int i = 0; i < targetItemContainer.Items.Count; i++)
         {
            System.Windows.Controls.TreeViewItem treeItem = targetItemContainer.Items[i] as System.Windows.Controls.TreeViewItem;
            if (treeItem == null) continue;
            if (!treeItem.HasItems) continue;

            treeItem.IsExpanded = true;
            ExpandInternal(treeItem);
         }

      } 

   }
}
