   M S B A M L   `   `   `  ���� ^  ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35tOclr-namespace:System.Windows.Forms.Integration;assembly=WindowsFormsIntegration System.Windows.Forms.Integration  [ WSystem.Windows.Forms, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089Y@clr-namespace:System.Windows.Forms;assembly=System.Windows.FormsSystem.Windows.Forms 
 MMAWPF   MMAWPF.Translation   WwfiOclr-namespace:System.Windows.Forms.Integration;assembly=WindowsFormsIntegration  Gwf@clr-namespace:System.Windows.Forms;assembly=System.Windows.Forms  W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     -     � Title$  文档翻译��$	��600��$	��900�� � WindowStartupLocation$ CenterScreen=� � Icon$4 ./MMAWPF;component/bin/Debug/images/mma_big.ico��.��� .+��� -   $��image1��$��60��$��Stretch=�$��0q�$
��Fill=�$	��Top=�$
��Auto��$B��</MMAWPF;component/bin/Debug/images/documenteditorbkg_top.jpg���� -   $��image2��$��63��$��Stretch=�$��0q�$
��Fill=�$��Bottom=�$
��Auto��$E��?/MMAWPF;component/bin/Debug/images/documenteditorbkg_bottom.jpg��� -   $��
globalgrid��$
��Auto��$��Stretch=�$��	0,60,0,63q�$��Stretch=�$
��Auto�� � RowDefinitions �� $��64 �$��64��$��64���� $��5 ��� $
��326* ��� $��30 �$��30��$��30�� � ColumnDefinitions �� $
��240* �$	��300��$��50���� $��5 ��� $
��330* �$��50���� $��5 ��� $
��330* �$��50��.+�� -   $��leftgrid��$��4��$��Stretch=�$��Stretch=�$
��Auto��.+�7   1System.Windows.Forms.Integration.WindowsFormsHost     Child. #  System.Windows.Forms.TreeView  -   treeView��"  System.Windows.Forms.Control   Visible .  �� -   $��gridSplitter1��$��1��$��4��$��Stretch=�$��Stretch=�$��5��
��� ���� -   $��
middlegrid��$��2��$��Stretch=�$��0,1,0,0q�$��Stretch=�$
��Auto��$��2��.+��� -	   $��
chineseTxt��$
��Auto��$��Stretch=�$��0q�$��Stretch=�$
��Auto��� -
   $��	rightgrid��$��4��$��3��$
��Auto��$��Stretch=�$��0q�$��Stretch=�$
��Auto�� �� $
��120* �$	��120���� $��5 ��� $
��296* �$	��200��.+��� -   $��gridSplitter4��$��1��$��5��$��Stretch=�$��0q�$��Stretch=�$
��Auto��
��� ����� -   $��	groupBox1��$��翻译��$��Stretch=�$��Stretch=�$
��Auto��.��� .+��� -   $��translateBtn�����	#FF00B0F1$��翻译��$��23��$
��Left=�$��	223,6,0,0q�$	��Top=�$��75������  �� EndPoint$ 0.5,1&� �� 
StartPoint$ 0.5,0&�.��� 	 � Color$	 	#FF7FD7F8��
 � Offset$
 1Q�� $	 	#FF00B0F1��$
 1Q�� $	 	#FF7FD7F8��$
 1Q�� $	 White��$
 0Q��� -   $��
contentTxt�����	#FF9CE0FA$��23��$
��Left=�$��23,7,0,0q�$	��Top=�$	��188�����	#2438F4F4�� -   $��	resultTxt��$
��Left=�$��	23,36,0,8q�$��Stretch=�$	��275�����	#FF95DEF9���	#2438F4F4�� -   $��
englishTxt��$��2��$
��Auto��$��Stretch=�$��0q�$��Stretch=�$
��Auto��� -   $��
bottomgrid��$��2��$��3��$
��Auto��$��Stretch=�$��Stretch=�$��3��.+��� -   $��
statusBar1��$
��Auto��$��Stretch=�$��0q�$��Stretch=�$
��Auto��
��� ���.@��� -   $��status��$��当前编辑区:��
��� ����� -   $��gridSplitter2��$��3��$��3��$
��Auto��$��Stretch=�$��0q�$��Stretch=�$��5��
��� ���� -   $��topgrid��$��2��$��64��$��Stretch=�$��0q�$��Stretch=�$
��Auto��.+��� -   $��
docHistory�����	#FF00B0F1$��23��$
��Left=�$��6,22,0,0q�$	��Top=�$	��183������ $ 0.5,1&�$ 0.5,0&�.��� $	 	#FF00B0F1��$
 0Q�� $	 White��$
 0.122Q�� $	 	#FF7FD7F8��$
 1Q��� -   $��	selectBtn�����	#FF00B0F1$��选择载入��$��23��$
��Left=�$��
218,22,0,0q�$	��Top=�$��75������ $ 0.5,1&�$ 0.5,0&�.��� $	 	#FF00B0F1��$
 0Q�� $	 White��$
 0.122Q�� $	 	#FF7FD7F8��$
 1Q��� -   $��gridSplitter3��$��2��$��1��$��5��$��Stretch=�$��0q�$��Stretch=�$
��Auto��
��� ���