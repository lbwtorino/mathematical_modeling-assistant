   M S B A M L   `   `   `  ���� ^  ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35tOclr-namespace:System.Windows.Forms.Integration;assembly=WindowsFormsIntegration System.Windows.Forms.Integration  [ WSystem.Windows.Forms, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089Y@clr-namespace:System.Windows.Forms;assembly=System.Windows.FormsSystem.Windows.Forms F BGifImageLib, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null>.clr-namespace:GifImageLib;assembly=GifImageLibGifImageLib 
 MMAWPF.   (MMAWPF.文档编辑模块.DocumentEditor   WwfiOclr-namespace:System.Windows.Forms.Integration;assembly=WindowsFormsIntegration  Gwf@clr-namespace:System.Windows.Forms;assembly=System.Windows.Forms  W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     =my.clr-namespace:GifImageLib;assembly=GifImageLib     -     � Title$  文档编辑��$	��600��$	��900�� � Icon$4 ./MMAWPF;component/bin/Debug/images/mma_big.ico�� � WindowStartupLocation$ CenterScreen=�.��� $
��Auto��$
��Auto��.+��� -   $��image1��$��60��$��Stretch=�$
��Fill=�$	��Top=�$B��</MMAWPF;component/bin/Debug/images/documenteditorbkg_top.jpg���� -   $��image2��$��63��$��Stretch=�$��0q�$
��Fill=�$��Bottom=�$E��?/MMAWPF;component/bin/Debug/images/documenteditorbkg_bottom.jpg��� -   $��
globalgrid��$��Stretch=�$��	0,60,0,63q�$��Stretch=�$
��Auto�� K� ZIndex$ 1�� � RowDefinitions �� $	��65* ��� $	��60* ��� $
��282* ��� $��30 � � ColumnDefinitions �� $
��203* �$	��245��$	��120���� $��5 ��� $
��461* ��� $��5 ��� $
��203* �$	��500��$��30��.+��� -   $��rightpicture��$��2��$��4��$
��Auto��$��Right=�$
��Fill=�$��Stretch=�$	��238��$��3��$D��>/MMAWPF;component/bin/Debug/images/documenteditorbkg_right.jpg��$��LeftToRight=��� -   $��leftpicture��$��2��$	��150��$
��Left=�$��0q�$
��Fill=�$��Bottom=�$	��200��$��2��$C��=/MMAWPF;component/bin/Debug/images/documenteditorbkg_left.jpg��]� -   $��leftdockpanel��$
��Auto��$��Stretch=�$��Stretch=�$ 2��$��0,0,1,0q�$��4��.+�� -   $��grid2��$
��Auto��$
��Auto�� �� $
��352* ��� $	��89* �.+��� -	   $��	groupBox1��$��1��$��Stretch=�$��0,18,0,0q�$��Stretch=�$
��Auto��.��� .+��� -
   $��comBtn��$��完成修改��$��23��$��Right=�$��	0,19,6,15q�$��Center=�$��75�����	#FF8BDAF9����  �� EndPoint$ 0.5,1&� �� 
StartPoint$ 0.5,0&�.���  � Color$ 	#FF8FDCF9��	 � Offset$	 0.683Q�� $ White��$	 0.036Q��� -   $��modBtn��$��修改结构��$��23��$
��Left=�$��	6,19,0,16q�$��Center=�$��75�����	#FF8BDAF9���� $ 0.5,1&�$ 0.5,0&�.��� $ 	#FF8FDCF9��$	 0.683Q�� $ White��$	 0.036Q��� -   $��	groupBox2��$��文档结构信息��$��Stretch=�$��Stretch=�$
��Auto��$��0,0,0,70q�$��2��.��� .+��� -   $��scrollViewer1��$
��Auto��$��Stretch=�$��0q�$��Stretch=�$
��Auto��7   1System.Windows.Forms.Integration.WindowsFormsHost  
   Child.
 #  System.Windows.Forms.TreeView  -   treeView��   DrawMode$ Normal=�   
Scrollable . � -   $��topgrid��$��2��$��Stretch=�$��Stretch=�$ 2��$��0q�.+��� -   $��	groupBox3��$��文档记录��$
��Auto��$��Stretch=�$��0q�$��Stretch=�$
��Auto��$��Center=�$��Center=�.��� .+��� -   $��
docHistory��$��23��$
��Left=�$��6,13,0,0q�$	��Top=�$	��183�����	#FF00B0F1���� $ 0.5,1&�$ 0.5,0&�.��� $ 	#FF00B0F1��$	 0Q�� $ White��$	 0.122Q�� $ 	#FF7FD7F8��$	 1Q��� -   $��	selectBtn��$��选择载入��$��23��$
��Left=�$��
195,13,0,0q�$	��Top=�$��75�����	#FF00B0F1���� $ 0.5,1&�$ 0.5,0&�.��� $ 	#FF00B0F1��$	 0Q�� $ White��$	 0.122Q�� $ 	#FF7FD7F8��$	 1Q��� -   $��	deleteBtn��$��删除文档��$��23��$
��Left=�$��
279,13,0,0q�$	��Top=�$��75�����	#FF00B0F1���� $ 0.5,1&�$ 0.5,0&�.��� $ 	#FF00B0F1��$	 0Q�� $ White��$	 0.122Q�� $ 	#FF7FD7F8��$	 1Q��� -   $��newBtn��$��新建��$��23��$
��Left=�$��
362,13,0,0q�$	��Top=�$��75�����	#FF00B0F1���� $ 0.5,1&�$ 0.5,0&�.��� $ 	#FF00B0F1��$	 0Q�� $ White��$	 0.122Q�� $ 	#FF7FD7F8��$	 1Q�� -   $��
middlegrid��$��2��$��1��$��Stretch=�$��Stretch=�$ 2��$��0q�.+��� -   $��	groupBox4��$��元素选择去��$
��Auto��$��Stretch=�$��0q�$��Stretch=�$
��Auto��$ 3��.��� .+�  GifImageLib.GifImage  -   $��	gifImage1��$
��Left=�$��6,3,0,1q�$��Center=�$��35��$��35��{�.    Source$' !/bin/Debug/images/pic_dynamic.gif��  -   $��	gifImage2��$��35��$
��Left=�$��99,2,0,0q�$��Center=�$��35��$) #/bin/Debug/images/table_dynamic.gif��  -   $��	gifImage3��$��35��$
��Left=�$��	188,1,0,1q�$��Center=�$��35��$+ %/bin/Debug/images/formula_dynamic.gif���� -   $��image6��$��35��$
��Left=�$��6,3,0,0q�$
��Fill=�$��Center=�$��35��$0��*/MMAWPF;component/bin/Debug/images/pic.jpg���� -   $��image7��$��35��$
��Left=�$��99,2,0,0q�$
��Fill=�$��Center=�$��35��$2��,/MMAWPF;component/bin/Debug/images/table.jpg���� -   $��image4��$��35��$
��Left=�$��	188,1,0,1q�$
��Fill=�$��Center=�$��35��$4��./MMAWPF;component/bin/Debug/images/formula.jpg���� -   $��inputBtn��$��确认输入��$��23��$��Right=�$��0,8,4,6q�$��Center=�$��75�����	#FF00B0F1$ 6������ $ 0.5,1&�$ 0.5,0&�.��� $ 	#FF00B0F1��$	 0Q�� $ White��$	 0.058Q�� $ 	#FF7FD7F8��$	 1Q��� -   $��picBtn��$��图片��$��25��$
��Left=�$��43,8,0,4q�$��Center=�$��40��
��� ������� $ 0.5,1&�$ 0.5,0&�.��� $ 	#FFA1E2FA��$	 1Q�� $ White��$	 0Q��� -   $��tableBtn��$��表格��$��25��$
��Left=�$��	136,8,0,4q�$��Center=�$��40��
��� ������� $ 0.5,1&�$ 0.5,0&�.��� $ 	#FFA1E2FA��$	 1Q�� $ White��$	 0Q��� -    $��
formulaBtn��$��公式��$��25��$
��Left=�$��	227,8,0,4q�$��Center=�$��40��
��� ������� $ 0.5,1&�$ 0.5,0&�.��� $ 	#FFA1E2FA��$	 1Q�� $ White��$	 0Q�� -!   $��grid6��$��2��$��3��$
��Auto��$��Stretch=�$��Stretch=�$ 2��$��0q�.+��� -"   $��	statusBar��$
��Auto��$��Stretch=�$��0,0,1,0q�$��Stretch=�.@��� -#   $��status��$��当前编辑区：��$��12(��� -$   $��gridSplitter1��$��1��$��4��$
��Auto��$��Stretch=�$��Stretch=�$��5��
��� ����� -%   $��gridSplitter2��$��3��$��4��$
��Auto��$��Stretch=�$��Stretch=�$��5��
��� ���� -&   $��	rightgrid��$��Stretch=�$��Stretch=�$��4��$��4��$��0q� �� $
��119* �$��50���� $��5 ��� $
��312* �$	��100��.+��� -'   $��gridSplitter3��$��Stretch=�$��Stretch=�$
��Auto��
��� ���$��1��$��5��� -(   $��grid9��$��Stretch=�$��0q�$��Stretch=�.+��� -)   $��previewimage��$
��Auto��$��Stretch=�$��0q�$��Uniform=�$��Stretch=�$ 3�� � DataContext �� �� -*   $��
tabControl��$
��Auto��$��Stretch=�$��Stretch=�$ 3�� �� TabStripPlacement$ Bottom=�$��0q�$��2������ .@��� -+   $��tabItem1��$��图片��
��� ������� .��� .+��� -,   $��
picmarkbox�����	#FF8ADBF9$��23��$��Center=�$��14,46,12,182q�$��Bottom=�$	��167������ $ 1,0.5&�$ 0,0.5&�.��� $ 	#FFAEE5FB��$	 0Q�� $ White��$	 0.842Q��� --   $��	picmodBtn�����	#FF8ADBF9$��	重定位��$��23��$��Center=�$��14,85,105,143q�$��Bottom=�$��75������ $ 0.5,1&�$ 0.5,0&�.��� $ 	#FF8ADBF9��$
	 0.64Q�� $ White��$
	 0.05Q��� -.   $��	picdelBtn�����	#FF92DDFA$��删除��$��23��$��Center=�$��106,85,13,143q�$��Bottom=�$��75������ $ 0.5,1&�$ 0.5,0&�.��� $ 	#FF8ADBF9��$
	 0.64Q�� $ White��$	 0Q��� -/   $��picrenameBtn�����	#FF90DCF9$��	重命名��$��23��$��Center=�$��107,158,12,70q�$��Bottom=�$��75������ $ 0.5,1&�$ 0.5,0&�.��� $ 	#FFAEE5FB��$
	 0.59Q�� $ White��$	 0.029Q��� -0   $��
picnewname��
��� ���$��23��$��Center=�$��14,123,12,105q�$��Bottom=�$	��168���� 
��� ���$��表格������ .��� $��Stretch=�$��Stretch=�.+��� -1   $��tablemarkbox�����	#FF8BDBF9$��23��$��Center=�$��15,0,13,201q�$��Bottom=�$	��165�� �� 
IsReadOnly . ���� $ 1,0.5&�$ 0,0.5&�.��� $ White��$	 0.892Q�� $ 	#FF97DFF9��$	 0Q��� -2   $��tablemodBtn�����	#FF91DDF9$��修改��$��23��$��Center=�$��18,0,100,159q�$��Bottom=�$��75������ $ 0.5,1&�$ 0.5,0&�.��� $ 	#FF8BDBF9��$	 0.777Q�� $ White��$	 0.058Q��� -3   $��tabledelBtn�����	#FF91DDF9$��删除��$��23��$��Center=�$��105,0,13,158q�$��Bottom=�$��75������ $ 0.5,1&�$ 0.5,0&�.��� $ 	#FF91DDF9��$	 0.755Q�� $ White��$	 0.036Q��� -4   $��	tablename��
��� ���$��23��$��Center=�$��18,0,10,111q�$��Bottom=�$	��165���� -5   $��label6��$��行数��$��27��$��Center=�$��18,0,142,77q�$��Bottom=��� -6   $��label7��$��列数��$��28��$��Center=�$��124,0,36,76q�$��Bottom=�  $��25��$��120,0,10,55q�$��63��$��Center=�$��Bottom=�.
 (  "System.Windows.Forms.NumericUpDown  -7   	oldcolNum��   Maximum$
 1000~�   Minimum$ 1~�  $��25��$��Center=�$��18,0,112,56q�$��63��$��Bottom=�.
   -8   	oldrowNum��$
 1000~�$ 1~��� -9   $��label1��$��表名��$��28��$��Center=�$��18,0,142,128q�$��Bottom=��� 
��� ���$��公式������ .��� $��Stretch=�$��Stretch=�.+��� -:   $��
formarkbox�����	#FF8ADBF9$��23��$��Center=�$��
6,56,6,172q�$��Bottom=�$	��182������ $ 1,0.5&�$ 0,0.5&�.��� $ 	#FFAEE5FB��$	 0Q�� $ White��$	 0.842Q��� -;   $��	fordelBtn�����	#FF92DDFA$��删除��$��23��$��Center=�$��113,107,6,121q�$��Bottom=�$��75������ $ 0.5,1&�$ 0.5,0&�.��� $ 	#FF8ADBF9��$
	 0.64Q�� $ White��$	 0Q��� -<   $��	formodBtn�����	#FF92DDFA$��重新编辑��$��23��$��Center=�$��6,107,113,121q�$��Bottom=�$��75������ $ 0.5,1&�$ 0.5,0&�.��� $ 	#FF8ADBF9��$
	 0.64Q�� $ White��$	 0Q�� -=   $��textboxgrid��$��2��$��2��$
��Auto��$��Stretch=�$��Stretch=�$ 1��$��0q� �� $
��432* ��� $	��30* �.+�  .
 &   System.Windows.Forms.RichTextBox  ->   contentTextBox��"  System.Windows.Forms.Control   Dock$
 Fill=�&   System.Windows.Forms.TextBoxBase   
AcceptsTab . � -?   $��
signalgrid��$
��Auto��$��Stretch=�$��Stretch=�$��0q�$��1��.+�9� -@   $��signal_expander��$	��250��$��Right=�$	��Top=� 9� ExpandDirection$
 Left=�$	��206��$��0,11,0,0q�$ 2��.��� .+��� -A   $	��jia��$��+��$��25��$
��Left=�$��10,8,0,0q�$	��Top=�$��25�����	#FFA6E3FA$ 1������ $ 0.5,1&�$ 0.5,0&�.��� $ 	#FFA0E2FA��$	 0.022Q�� $ White��$	 0.647Q��� -B   $
��jian��$��-��$��25��$
��Left=�$��36,8,0,0q�$	��Top=�$��25�����	#FFA6E3FA$ 1������ $ 0.5,1&�$ 0.5,0&�.��� $ 	#FFA0E2FA��$	 0.022Q�� $ White��$	 0.647Q��� -C   $��cheng��$��×��$��25��$
��Left=�$��62,8,0,0q�$	��Top=�$��25�����	#FFA6E3FA$ 1������ $ 0.5,1&�$ 0.5,0&�.��� $ 	#FFA0E2FA��$	 0.022Q�� $ White��$	 0.647Q��� -D   $	��chu��$��÷��$��25��$
��Left=�$��88,8,0,0q�$	��Top=�$��25�����	#FFA6E3FA$ 1������ $ 0.5,1&�$ 0.5,0&�.��� $ 	#FFA0E2FA��$	 0.022Q�� $ White��$	 0.647Q��� -E   $��budengyu��$	��≠��$��25��$
��Left=�$��	114,8,0,0q�$	��Top=�$��25�����	#FFA6E3FA$ 1������ $ 0.5,1&�$ 0.5,0&�.��� $ 	#FFA0E2FA��$	 0.022Q�� $ White��$	 0.647Q��� -F   $��jifen��$	��∫��$��25��$
��Left=�$��	10,34,0,0q�$	��Top=�$��25�����	#FFA6E3FA$ 1������ $ 0.5,1&�$ 0.5,0&�.��� $ 	#FFA0E2FA��$	 0.022Q�� $ White��$	 0.647Q��� -G   $��	wuqiongda��$	��∞��$��25��$
��Left=�$��	36,34,0,0q�$	��Top=�$��25�����	#FFA6E3FA$ 1������ $ 0.5,1&�$ 0.5,0&�.��� $ 	#FFA0E2FA��$	 0.022Q�� $ White��$	 0.647Q��� -H   $��xiaoyudengyu��$	��≤��$��25��$
��Left=�$��	62,34,0,0q�$	��Top=�$��25�����	#FFA6E3FA$ 1������ $ 0.5,1&�$ 0.5,0&�.��� $ 	#FFA0E2FA��$	 0.022Q�� $ White��$	 0.647Q��� -I   $��
dayudengyu��$	��≥��$��25��$
��Left=�$��	88,34,0,0q�$	��Top=�$��25�����	#FFA6E3FA$ 1������ $ 0.5,1&�$ 0.5,0&�.��� $ 	#FFA0E2FA��$	 0.022Q�� $ White��$	 0.647Q��� -J   $��pi��$	��∏��$��25��$
��Left=�$��
114,34,0,0q�$	��Top=�$��25�����	#FFA6E3FA$ 1������ $ 0.5,1&�$ 0.5,0&�.��� $ 	#FFA0E2FA��$	 0.022Q�� $ White��$	 0.647Q��� -K   $	��log��$	��log��$��25��$
��Left=�$��	10,60,0,0q�$	��Top=�$��25�����	#FFA6E3FA$ 1������ $ 0.5,1&�$ 0.5,0&�.��� $ 	#FFA0E2FA��$	 0.022Q�� $ White��$	 0.647Q��� -L   $��lg��$��lg��$��25��$
��Left=�$��	36,60,0,0q�$	��Top=�$��25�����	#FFA6E3FA$ 1������ $ 0.5,1&�$ 0.5,0&�.��� $ 	#FFA0E2FA��$	 0.022Q�� $ White��$	 0.647Q��� -M   $��ln��$��ln��$��25��$
��Left=�$��	62,60,0,0q�$	��Top=�$��25�����	#FFA6E3FA$ 1������ $ 0.5,1&�$ 0.5,0&�.��� $ 	#FFA0E2FA��$	 0.022Q�� $ White��$	 0.647Q��� -N   $��shuyu��$	��∈��$��25��$
��Left=�$��	88,60,0,0q�$	��Top=�$��25�����	#FFA6E3FA$ 1������ $ 0.5,1&�$ 0.5,0&�.��� $ 	#FFA0E2FA��$	 0.022Q�� $ White��$	 0.647Q��� -O   $
��jiao��$	��∩��$��25��$��114,60,44,0q�$	��Top=�$��25�����	#FFA6E3FA$ 1������ $ 0.5,1&�$ 0.5,0&�.��� $ 	#FFA0E2FA��$	 0.022Q�� $ White��$	 0.647Q��� -P   $��	luojijiao��$	��∧��$��25��$
��Left=�$��	10,86,0,0q�$	��Top=�$��25�����	#FFA6E3FA$ 1������ $ 0.5,1&�$ 0.5,0&�.��� $ 	#FFA0E2FA��$	 0.022Q�� $ White��$	 0.647Q��� -Q   $��	luojibing��$	��∨��$��25��$
��Left=�$��	36,86,0,0q�$	��Top=�$��25�����	#FFA6E3FA$ 1������ $ 0.5,1&�$ 0.5,0&�.��� $ 	#FFA0E2FA��$	 0.022Q�� $ White��$	 0.647Q��� -R   $��α��$��α��$��25��$
��Left=�$��	62,86,0,0q�$	��Top=�$��25�����	#FFA6E3FA$ 1������ $ 0.5,1&�$ 0.5,0&�.��� $ 	#FFA0E2FA��$	 0.022Q�� $ White��$	 0.647Q��� -S   $��β��$��β��$��25��$
��Left=�$��	88,86,0,0q�$	��Top=�$��25�����	#FFA6E3FA$ 1������ $ 0.5,1&�$ 0.5,0&�.��� $ 	#FFA0E2FA��$	 0.022Q�� $ White��$	 0.647Q��� -T   $��γ��$��γ��$��25��$��
114,86,0,0q�$	��Top=�$
��Left=�$��25�����	#FFA6E3FA$ 1������ $ 0.5,1&�$ 0.5,0&�.��� $ 	#FFA0E2FA��$	 0.022Q�� $ White��$	 0.647Q��� -U   $��ε��$��ε��$��25��$
��Left=�$��
10,112,0,0q�$	��Top=�$��25�����	#FFA6E3FA$ 1������ $ 0.5,1&�$ 0.5,0&�.��� $ 	#FFA0E2FA��$	 0.022Q�� $ White��$	 0.647Q��� -V   $��ζ��$��ζ��$��25��$
��Left=�$��
36,112,0,0q�$	��Top=�$��25�����	#FFA6E3FA$ 1������ $ 0.5,1&�$ 0.5,0&�.��� $ 	#FFA0E2FA��$	 0.022Q�� $ White��$	 0.647Q��� -W   $��η��$��η��$��25��$
��Left=�$��
62,112,0,0q�$	��Top=�$��25�����	#FFA6E3FA$ 1������ $ 0.5,1&�$ 0.5,0&�.��� $ 	#FFA0E2FA��$	 0.022Q�� $ White��$	 0.647Q��� -X   $��θ��$��θ��$��25��$
��Left=�$��
88,112,0,0q�$	��Top=�$��25�����	#FFA6E3FA$ 1������ $ 0.5,1&�$ 0.5,0&�.��� $ 	#FFA0E2FA��$	 0.022Q�� $ White��$	 0.647Q��� -Y   $��l��$	�� ι��$��25��$
��Left=�$��114,112,0,0q�$	��Top=�$��25�����	#FFA6E3FA$ 1������ $ 0.5,1&�$ 0.5,0&�.��� $ 	#FFA0E2FA��$	 0.022Q�� $ White��$	 0.647Q��� -Z   $��λ��$��λ��$��25��$
��Left=�$��
10,138,0,0q�$	��Top=�$��25�����	#FFA6E3FA$ 1������ $ 0.5,1&�$ 0.5,0&�.��� $ 	#FFA0E2FA��$	 0.022Q�� $ White��$	 0.647Q��� -[   $��μ��$��μ��$��25��$
��Left=�$��
36,138,0,0q�$	��Top=�$��25�����	#FFA6E3FA$ 1������ $ 0.5,1&�$ 0.5,0&�.��� $ 	#FFA0E2FA��$	 0.022Q�� $ White��$	 0.647Q��� -\   $��ν��$��ν��$��25��$
��Left=�$��
62,138,0,0q�$	��Top=�$��25�����	#FFA6E3FA$ 1������ $ 0.5,1&�$ 0.5,0&�.��� $ 	#FFA0E2FA��$	 0.022Q�� $ White��$	 0.647Q��� -]   $��ξ��$��ξ��$��25��$
��Left=�$��
88,138,0,0q�$	��Top=�$��25�����	#FFA6E3FA$ 1������ $ 0.5,1&�$ 0.5,0&�.��� $ 	#FFA0E2FA��$	 0.022Q�� $ White��$	 0.647Q��� -^   $��ο��$��ο��$��25��$��114,138,0,0q�$	��Top=����	#FFA6E3FA$ 1��$
��Left=�$��25������ $ 0.5,1&�$ 0.5,0&�.��� $ 	#FFA0E2FA��$	 0.022Q�� $ White��$	 0.647Q��� -_   $��ρ��$��ρ��$��25��$
��Left=�$��
10,162,0,0q�$	��Top=�$��25�����	#FFA6E3FA$ 1������ $ 0.5,1&�$ 0.5,0&�.��� $ 	#FFA0E2FA��$	 0.022Q�� $ White��$	 0.647Q��� -`   $��σ��$��σ��$��25��$
��Left=�$��
36,162,0,0q�$	��Top=�$��25�����	#FFA6E3FA$ 1������ $ 0.5,1&�$ 0.5,0&�.��� $ 	#FFA0E2FA��$	 0.022Q�� $ White��$	 0.647Q��� -a   $��τ��$��τ��$��25��$
��Left=�$��
62,162,0,0q�$	��Top=�$��25�����	#FFA6E3FA$ 1������ $ 0.5,1&�$ 0.5,0&�.��� $ 	#FFA0E2FA��$	 0.022Q�� $ White��$	 0.647Q��� -b   $��υ��$��υ��$��25��$
��Left=�$��
88,162,0,0q�$	��Top=�$��25�����	#FFA6E3FA$ 1������ $ 0.5,1&�$ 0.5,0&�.��� $ 	#FFA0E2FA��$	 0.022Q�� $ White��$	 0.647Q��� -c   $��φ��$��φ��$��25��$
��Left=�$��114,162,0,0q�$	��Top=�$��25�����	#FFA6E3FA$ 1������ $ 0.5,1&�$ 0.5,0&�.��� $ 	#FFA0E2FA��$	 0.022Q�� $ White��$	 0.647Q��� -d   $��genhao��$	��√��$��25��$
��Left=�$��	141,8,0,0q�$	��Top=�$��25�����	#FFA6E3FA$ 1������ $ 0.5,1&�$ 0.5,0&�.��� $ 	#FFA0E2FA��$	 0.022Q�� $ White��$	 0.647Q��� -e   $��qiuhe��$	��∑��$��25��$
��Left=�$��
141,34,0,0q�$	��Top=�$��25�����	#FFA6E3FA$ 1������ $ 0.5,1&�$ 0.5,0&�.��� $ 	#FFA0E2FA��$	 0.022Q�� $ White��$	 0.647Q��� -f   $
��bing��$	��∪��$��25��$
��Left=�$��
141,60,0,0q�$	��Top=�$��25�����	#FFA6E3FA$ 1������ $ 0.5,1&�$ 0.5,0&�.��� $ 	#FFA0E2FA��$	 0.022Q�� $ White��$	 0.647Q��� -g   $��δ��$��δ��$��25��$
��Left=�$��141,0,0,137q�$��Bottom=�$��25�����	#FFA6E3FA$ 1������ $ 0.5,1&�$ 0.5,0&�.��� $ 	#FFA0E2FA��$	 0.022Q�� $ White��$	 0.647Q��� -h   $��κ��$��κ��$��25��$
��Left=�$��141,112,0,0q�$	��Top=�$��25�����	#FFA6E3FA$ 1������ $ 0.5,1&�$ 0.5,0&�.��� $ 	#FFA0E2FA��$	 0.022Q�� $ White��$	 0.647Q��� -i   $��π��$��π��$��25��$
��Left=�$��141,138,0,0q�$	��Top=�$��25�����	#FFA6E3FA$ 1������ $ 0.5,1&�$ 0.5,0&�.��� $ 	#FFA0E2FA��$	 0.022Q�� $ White��$	 0.647Q��� -j   $��χ��$��χ��$��25��$
��Left=�$��141,162,0,0q�$	��Top=�$��25�����	#FFA6E3FA$ 1������ $ 0.5,1&�$ 0.5,0&�.��� $ 	#FFA0E2FA��$	 0.022Q�� $ White��$	 0.647Q��� -k   $��ψ��$��ψ��$��25��$
��Left=�$��
10,188,0,0q�$	��Top=�$��25�����	#FFA6E3FA$ 1������ $ 0.5,1&�$ 0.5,0&�.��� $ 	#FFA0E2FA��$	 0.022Q�� $ White��$	 0.647Q��� -l   $��ω��$��ω��$��25��$
��Left=�$��
36,188,0,0q�$	��Top=�$��25�����	#FFA6E3FA$ 1������ $ 0.5,1&�$ 0.5,0&�.��� $ 	#FFA0E2FA��$	 0.022Q�� $ White��$	 0.647Q��� -m   $��Γ��$��Γ��$��25��$
��Left=�$��
62,188,0,0q�$	��Top=�$��25�����	#FFA6E3FA$ 1������ $ 0.5,1&�$ 0.5,0&�.��� $ 	#FFA0E2FA��$	 0.022Q�� $ White��$	 0.647Q��� -n   $��Δ��$��Δ��$��25��$��
88,188,0,0q�$	��Top=����	#FFA6E3FA$ 1��$
��Left=�$��25������ $ 0.5,1&�$ 0.5,0&�.��� $ 	#FFA0E2FA��$	 0.022Q�� $ White��$	 0.647Q��� -o   $��Θ��$��Θ��$��25��$
��Left=�$��114,188,0,0q�$	��Top=�$��25�����	#FFA6E3FA$ 1������ $ 0.5,1&�$ 0.5,0&�.��� $ 	#FFA0E2FA��$	 0.022Q�� $ White��$	 0.647Q��� -p   $��Λ��$��Λ��$��25��$
��Left=�$��141,188,0,0q�$	��Top=�$��25�����	#FFA6E3FA$ 1������ $ 0.5,1&�$ 0.5,0&�.��� $ 	#FFA0E2FA��$	 0.022Q�� $ White��$	 0.647Q��� -q   $��Ξ��$��Ξ��$��25��$
��Left=�$��
10,215,0,0q�$	��Top=�$��25�����	#FFA6E3FA$ 1������ $ 0.5,1&�$ 0.5,0&�.��� $ 	#FFA0E2FA��$	 0.022Q�� $ White��$	 0.647Q��� -r   $��Ο��$��Ο��$��25��$
��Left=�$��
36,215,0,0q�$	��Top=�$��25�����	#FFA6E3FA$ 1������ $ 0.5,1&�$ 0.5,0&�.��� $ 	#FFA0E2FA��$	 0.022Q�� $ White��$	 0.647Q��� -s   $��Φ��$��Φ��$��25��$
��Left=�$��
62,215,0,0q�$	��Top=�$��25�����	#FFA6E3FA$ 1������ $ 0.5,1&�$ 0.5,0&�.��� $ 	#FFA0E2FA��$	 0.022Q�� $ White��$	 0.647Q��� -t   $��Χ��$��Χ��$��25��$
��Left=�$��
88,215,0,0q�$	��Top=�$��25�����	#FFA6E3FA$ 1������ $ 0.5,1&�$ 0.5,0&�.��� $ 	#FFA0E2FA��$	 0.022Q�� $ White��$	 0.647Q��� -u   $��Ψ��$��Ψ��$��25��$
��Left=�$��114,215,0,0q�$	��Top=�$��25�����	#FFA6E3FA$ 1������ $ 0.5,1&�$ 0.5,0&�.��� $ 	#FFA0E2FA��$	 0.022Q�� $ White��$	 0.647Q��� -v   $��Ω��$��Ω��$��25��$
��Left=�$��141,215,0,0q�$	��Top=�$��25�����	#FFA6E3FA$ 1������ $ 0.5,1&�$ 0.5,0&�.��� $ 	#FFA0E2FA��$	 0.022Q�� $ White��$	 0.647Q��� -w   $��border1��
��� ���$��2q�$��Stretch=�$��0q�$��Stretch=�  -x   $��	wordimage��$��Right=�$��0,2,22,0q�$	��Top=�$��48��$��48��$  /bin/debug/images/word.gif��   Stretch$ Uniform=� � Cursor$
 Hand�� � ToolTip$ 生成word文档��