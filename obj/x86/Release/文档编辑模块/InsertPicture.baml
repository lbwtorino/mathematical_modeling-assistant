   M S B A M L   `   `   `  ���� 
  MMAWPF-    'MMAWPF.文档编辑模块.InsertPicture   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     -     � Title$  图片插入��$	��375��$	��500�� � WindowStartupLocation$ CenterScreen=� � Topmost .   � 
ResizeMode$ CanMinimize=� � Icon$4 ./MMAWPF;component/bin/Debug/images/mma_big.ico������  �� ImageSource$4 ./MMAWPF;component/bin/Debug/images/samllbg.jpg��.��� .+��� -   $��label1��$��图片地址��$��12(�$��28��$
��Left=�$��	21,50,0,0q�$	��Top=��� -   $��
picaddress��$��12(�$��23��$
��Left=� �� 
IsReadOnly . $��	27,84,0,0q�$	��Top=�$	��311��
��� ����� -   $��	liulanBtn�����	#FF97DEF9$��浏览��$��14(�$��Normal"�$��23��$
��Left=�$��
379,84,0,0q� K� ZIndex$ 3��$	��Top=�$��64������  �� EndPoint$ 0.5,1&�	 �� 
StartPoint$	 0.5,0&�.��� 
 � Color$
 White�� � Offset$ 0Q�� $
 	#FFAEE6FB��$ 0.777Q��� -   $��
pictureTxt��$��23��$
��Left=�$��235,160,0,0q�$ 3��$	��Top=�$	��208��
��� ����� -   $��OKBtn�����	#FF97DEF9$��插入��$��14(�$��Normal"�$��23��$
��Left=�$��300,201,0,0q�$ 3��$	��Top=�$��62������ $ 0.5,1&�$	 0.5,0&�.��� $
 	#FFA2E2FA��$ 0.568Q�� $
 White��$ 0Q��� -   $��label2��$,��&图片名(若为空则使用默认名)��$��SimHei)�$��12(�$��28��$
��Left=�$��235,126,0,0q�$	��Top=��� -   $��	groupBox1��$��预览窗口��$	��174��$
��Left=�$��
21,126,0,0q�$	��Top=�$	��208��.��� .+��� -	   $��previewimage��$
��Auto��$��Stretch=�$��0q�$��Uniform=�$��Stretch=�$
��Auto���� -
   $��	CancelBtn�����	#FF97DEF9$��取消��$��14(�$��Normal"�$��23��$��Right=�$��
0,201,35,0q�$ 3��$	��Top=�$��62������ $ 0.5,1&�$	 0.5,0&�.��� $
 	#FFA2E2FA��$ 0.568Q�� $
 White��$ 0Q�