   M S B A M L   `   `   `  ����^  ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35tOclr-namespace:System.Windows.Forms.Integration;assembly=WindowsFormsIntegration System.Windows.Forms.Integration  5   	   [ WSystem.Windows.Forms, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089Y@clr-namespace:System.Windows.Forms;assembly=System.Windows.FormsSystem.Windows.Forms 5   	   H DMathMLControl, Version=2.1.3.0, Culture=neutral, PublicKeyToken=nullD2clr-namespace:MathMLControl;assembly=MathMLControlMathMLControl 5   	   
 MMAWPF-   'MMAWPF.文档编辑模块.InsertFormula   5   	   WwfiOclr-namespace:System.Windows.Forms.Integration;assembly=WindowsFormsIntegration  5   	   Gwf@clr-namespace:System.Windows.Forms;assembly=System.Windows.Forms  5   	   DmathMLControl2clr-namespace:MathMLControl;assembly=MathMLControl  5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   -   5          � Title$  公式编辑器��5   	   $	��600��6   $	��800��6$    � Icon$# /bin/Debug/images/mma_big.ico��60    � WindowStartupLocation$ CenterScreen=�6�   .��5      � .+�5	   
   � -   5        $��grid1��5	   @   $��Stretch=�6   $��	0,60,0,63q�6-   $��Stretch=�6M   $
��Auto��6i   �� 5
   
   -   5        $��image1��5
   :   $��60��6   $��Stretch=�6   $
��Fill=�6H   $	��Top=�6W   $B��</MMAWPF;component/bin/Debug/images/documenteditorbkg_top.jpg��6o   �� 5   
   -   5        $��image2��5   E   $��63��6   $��Stretch=�6   $��0q�6:   $
��Fill=�6S   $��Bottom=�6b   $
��Auto��6}   $E��?/MMAWPF;component/bin/Debug/images/documenteditorbkg_bottom.jpg��6�   7   1System.Windows.Forms.Integration.WindowsFormsHost  5   
   $��	0,60,0,63q�6      Child. 5      !  MathMLControl.MathMLControl  -   5        mathformula��5   *   5      �� 5   
   -   5        $��OKBtn��5   \   $��确认编辑��6   $��23��6    $��Right=�6,   $��
0,0,124,12q�6H   $��Bottom=�6i   $��75��6�   
��� ���6�   �� 5   
   -   5        $��	CancelBtn��5   [   $��取消编辑��6   $��23��6    $��Right=�6,   $��	0,0,12,12q�6H   $��Bottom=�6l   $��75��6�   
��� ���6�   5      5      