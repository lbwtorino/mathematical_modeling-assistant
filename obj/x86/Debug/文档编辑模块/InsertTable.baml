   M S B A M L   `   `   `  ����^  ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35tOclr-namespace:System.Windows.Forms.Integration;assembly=WindowsFormsIntegration System.Windows.Forms.Integration  5   	   [ WSystem.Windows.Forms, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089Y@clr-namespace:System.Windows.Forms;assembly=System.Windows.FormsSystem.Windows.Forms 5   	   Y@clr-namespace:System.Windows.Forms;assembly=System.Windows.FormsSystem.Windows.Forms 5   t   
 MMAWPF+   %MMAWPF.文档编辑模块.InsertTable   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5   	   _wfiOclr-namespace:System.Windows.Forms.Integration;assembly=WindowsFormsIntegration      5   	   Owf@clr-namespace:System.Windows.Forms;assembly=System.Windows.Forms      5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   Omy@clr-namespace:System.Windows.Forms;assembly=System.Windows.Forms     5   t   -   5          � Title$  InsertTable��5   	   $	��375��6   $	��500��6*    � 
ResizeMode$ CanMinimize=�66    � WindowStartupLocation$ CenterScreen=�6O    � Icon$4 ./MMAWPF;component/bin/Debug/images/mma_big.ico��6�   ��5      �� 5   
    �� ImageSource$4 ./MMAWPF;component/bin/Debug/images/samllbg.jpg��6   .��5
      � .+�5   
   �� -   5        $��label3��5   p   $)��#表名(若为空则使用默认名)��6   $��16(�6(   $��38��66   $
��Left=�6B   $��	52,70,0,0q�6]   $	��Top=�6~   �� 5   
   -   5        $��tableTitleTxt��5   M   $��30��6   $
��Left=�6   $��
53,105,0,0q�69    K� ZIndex$ 4��6b   $	��Top=�6s   $	��357��6�   �� 5   
   -   5        $��OKBtn��5   �   ���	#FFA6E3FB$��插入��6)   $��14(�66   $��Normal"�6D   $��23��6X   $
��Left=�6d   $��239,234,0,0q�6   $ 4��6�   $	��Top=�6�   $��75��6�   ��5      �� 5       �� EndPoint$ 0.5,1&�6&    �� 
StartPoint$ 0.5,0&�67   .��5      �  � Color$ White��6#   	 � Offset$	 0Q�61   � 5      $ 	#FF9CE0FA��6#   $
	 0.77Q�65   5      5      �� 5   
   -   5        $��label5��5   h   $��列数��6   $��16(�6   $��36��6+   $��Right=�67   $��0,167,215,0q�6S   $	��Top=�6v   �� 5   
   -   5        $��label4��5   f   $��行数��6   $��16(�6   $��37��6+   $
��Left=�67   $��
54,167,0,0q�6R   $	��Top=�6t   7   1System.Windows.Forms.Integration.WindowsFormsHost  5   
   $��25��6   $��102,170,0,141q�6'   $ 4��6>   $	��100��6O   $
��Left=�6[   $��Center=�6v   
   Child.
 5      (  "System.Windows.Forms.NumericUpDown  -   5        
colNum��   Maximum$
 1000~�5   /      Minimum$ 1~�6>   5        5   
   $��25��6   $��Right=�6'   $��0,170,112,141q�6C   $ 4��6Z   $	��100��6k   $��Center=�6w   .
 5        -   5        
rowNum��$
 1000~�5   /   $ 1~�6>   5      �� 5   
   -	   5        $��	CancelBtn��5   �   ���	#FFA6E3FB$��取消��6)   $��14(�66   $��Normal"�6D   $��23��6X   $
��Left=�6d   $��335,234,0,0q�6   $ 4��6�   $	��Top=�6�   $��75��6�   ��5      �� 5      $ 0.5,1&�6&   $ 0.5,0&�67   .��5       � $ White��6#   $	 0Q�61   � 5!      $ 	#FF9CE0FA��6#   $
	 0.77Q�65   5"      5$      5%      5&      