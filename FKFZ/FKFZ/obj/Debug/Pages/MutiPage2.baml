   M S B A M L   `   `   `  ����?  ;FKFZ, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:FKFZ.ControlsFKFZ.Controls  5          FKFZ.Pages.MutiPage2   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      -localclr-namespace:FKFZ.Controls     5      -   5          N� Title$  MutiPage��5	       N� 
Background �#F6F6F6.��5      � $��	0,70,0,70q�6    � ColumnDefinitions 5   
   �� 5      $��60 �6   �� 5      $��2* �6   �� 5      $��55 �6   �� 5      $��4.18* �6   �� 5      $��60 �6    � RowDefinitions 5   
   �� 5      $��* �6   �� 5      $��38 �6   �� 5      $��* �6   .+�5   
   �� 
���   �$	��0.2q�6%    �� CornerRadius$ 0��6;   $��0��6L   $��1��6Y   ._�5      � 
�������$��0��6&   $��1��63   .+�5      �� -   5        $��ImgTop��5      $��/Styles/skins/audio1.png��6&   $
��Fill=�6H    J� SnapsToDevicePixels . $��6q�6r    �� BitmapScalingMode$ 
LowQuality=�6}   5      5      �� 5   
   
���   �$	��0.2q�6%   $ 0��6;   $��2��6L   $��1��6Y   ._�5      � 
�������.+�5       �� -   5        $��	ImgBottom��5       $��/Styles/skins/audio2.png��6)   $
��Fill=�6K    . $��6q�6u   $ 
LowQuality=�6�   5!      5"      � 5$   
   -   5        $��Grid_Lrc��5$      $��0��6   $��3��6,   $��3��6<   .+�5%      �� $��/Styles/skins/audiobg.png��6   $
��Fill=�67    . $ 
LowQuality=�6a   r� 5&      -   5        $��AudioPlayer��5&      $��50��6.   $��40��6:    r� LoadedBehavior$ Manual=�6E    r� UnloadedBehavior$
 Stop=�6]   $y�	Collapsed=�5'   S   	 � DataContext	 6j   �� 54      �� 55      ���	#90000000$
��0.08q�6-   $ 0��6D   ._�56      �� -   5        $	��RTB��56      $
��Left=�6)   $��Hidden=�6K   $��30(�57      
������    DefaultRichTextBox#���   6A   ���#007acc
 �� 
IsReadOnly
 . $��55,35,50,35q�58   ,   &   System.Windows.Media.TextOptions   TextRenderingMode$ Aliased=�6B   5:      5;      5<      �� 5>      $��1��6   $��1��6(   $y�	Collapsed=�65   ��5?      �� 5@       �� EndPoint$ 0.5,1&�6*    �� 
StartPoint$ 0.5,0&�6;   .��5A      �  � Color$ White��6'    � Offset$ 0Q�65   � 5B      $ White��6'   $ 1Q�65   � 5C      $ Transparent��6'   $ 0.475Q�6;   � 5D      $ 	#C8FEFEFE��6'   $ 0.375Q�69   � 5E      $ 	#C8FFFFFF��6'   $ 0.575Q�69   5F      5H      5J      5K      