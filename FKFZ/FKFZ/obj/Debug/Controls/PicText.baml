   M S B A M L   `   `   `  ����?  ;FKFZ, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:FKFZ.ControlsFKFZ.Controls  5      1clr-namespace:FKFZ.ConvertersFKFZ.Converters  5          FKFZ.Controls.PicText   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      -localclr-namespace:FKFZ.Controls     5      -cvtclr-namespace:FKFZ.Converters     5        � 	Resources  5
      *   $FKFZ.Converters.WidthHeightConverter   WidthHeightConverter��%   &	          5   
   6B   5      .��5      (� .��5      �� 
���   �$	��0.4q�6!    J� Effect 5   
   3  -System.Windows.Media.Effects.DropShadowEffect  5         	Direction$	 270Q�6      ShadowDepth$ 2Q�6/      Opacity$	 0.2Q�6?   ._�5   
   � -   5        $��
GridBorder��5      
������� � RowDefinitions 5      �� 5      $	��10* �6    �� 5      $��3* �6    $��70��6,    � ColumnDefinitions 5      �� 5      $��60 �6#   �� 5      $��* �6#   �� 5      $��60 �6#   .+�5      �� -   5        $��TopImg��5      $��0��6"   $��3��6/   $	��Top=�6C   $��Center=�6[   $��5,5,5,0q�6x   $��/Styles/skins/c_dz.png��5   $   $
��Fill=�6D    �� BitmapScalingMode$
 Fant=�6S   5       $   FKFZ.Controls.RollingImageCtrl  5"      -   5          Name$ rollImg��5"   %   $   FKFZ.Controls.RollingImageCtrl	   Click$	 scrollImg_Click_1��5#   Y   
   TitleChange$
 rollImg_WordChanged_1��6s   $
��Auto��5"   6   $
��Auto��6C   $��0��6Q   $��3��6^   $��5,5,5,0q�6r      	StoryType$ RightToLeft=�5#   %      TimeHold$ 1Q�6=      
TimeChange$ 1Q�6J   6s   �� 5$      -   5        $
��Play��5$      $��70��6"   $��70��6-     ImgBtnStyle#���  69   $��1��6_   $y�	Collapsed=�6o   6�   �� 5&      -   5        $��BtnLeft��5&      $��27��6%   $��54��60     LeftBtnStyle#���  6<   $y�	Collapsed=�6c   6z   �� 5'      -   5        $��BtnRight��5'      $��27��6&   $��54��61     RightBtnStyle#���  6=   $��2��6e   $y�	Collapsed=�6u   6�   �� 5)      -   5        $��TitleTb��5)      $��3��6'   $��1��6;   $��Center=�6H   $��Center=�6c   $
��asdf��6�   $��28(�6�   5*      5+      5,      5-      