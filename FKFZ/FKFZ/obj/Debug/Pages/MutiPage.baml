   M S B A M L   `   `   `  ����?  ;FKFZ, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:FKFZ.ControlsFKFZ.Controls  5          FKFZ.Pages.MutiPage   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      -localclr-namespace:FKFZ.Controls     5        N� Title$  MutiPage��5	       N� 
Background �#F6F6F6.��5      � $��40,50,40,50q�6    � ColumnDefinitions 5   
   �� 5      $��60 �6   �� 5      $��2* �6   �� 5      $��50 �6   �� 5      $��3* �6   �� 5      $��60 �6    � RowDefinitions 5   
   �� 5      $��* �6   �� 5      $��30 �6   �� 5      $��* �6   .+�5   
   � 
�������$��0��6"   $��1��6/   .+�5      �� $��/Styles/skins/audio1.png��6   $��UniformToFill=�66    J� SnapsToDevicePixels . $��5q�6i    �� BitmapScalingMode$ 
LowQuality=�6t   5      � 5   
   
�������$��2��6#   $��1��60   .+�5      �� $��/Styles/skins/audio2.png��6   $��UniformToFill=�66    . $��5q�6i   $ 
LowQuality=�6t   5      � 5!   
   -   5        $��Grid_Lrc��5!      $��0��6   $��3��6,   $��3��6<   .+�5"      �� $��/Styles/skins/audiobg.png��6   $
��Fill=�67    . $ 
LowQuality=�6a   r� 5#      -   5        $��AudioPlayer��5#      $��50��6.   $��40��5$       r� LoadedBehavior$ Manual=�6    r� UnloadedBehavior$
 Stop=�64   $y�	Collapsed=�5%   S   5&      �� 5'      -   5        $	��RTB��5'      $��Center=�6%   $��Hidden=�6I   $��25(�5(      
������    DefaultRichTextBox#���   6A    �� 
IsReadOnly . 5*      �� 5+      -   5        $��Button��5+      $
��Left=�6&   $��136,123,0,0q�6A   $	��Top=�6V   $��75��6n   6y   5,      �� 5.      $��1��6   $��1��6(   ��5/      �� 50      	 �� EndPoint$	 0.5,1&�6*   
 �� 
StartPoint$
 0.5,0&�6;   .��51      �  � Color$ White��6'    � Offset$ 0Q�65   � 52      $ White��6'   $ 1Q�65   � 53      $ Transparent��6'   $ 0.475Q�6;   � 54      $ 	#C8FEFEFE��6'   $ 0.375Q�69   � 55      $ 	#C8FFFFFF��6'   $ 0.575Q�69   56      58      5:      5;      