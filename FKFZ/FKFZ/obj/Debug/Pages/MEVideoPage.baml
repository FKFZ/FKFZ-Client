   M S B A M L   `   `   `  ����?  ;FKFZ, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:FKFZ.ControlsFKFZ.Controls  5          FKFZ.Pages.MEVideoPage   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      -localclr-namespace:FKFZ.Controls     5      -   5          N� 
Background
  �   � N� Title$ 	VideoPage��5      .��5
      � -   5        $��vGrid��5
      .+�5   
   r� -   5        $��Player��5       r� LoadedBehavior$ Manual=�6%    r� UnloadedBehavior$
 Stop=�6=    r� Stretch$
 Fill=�6U   $y�Visible=�5   S    � FocusVisualStyle 6h   Z� 5      �� 5   
   -   5        $��btnPlay��5         ImgBtnStyle#���   6    $y�Visible=�6\   $��90��6�   $��Center=�6�   � 5   
   -   5        $��Status��5      $��Center=�6   $��Bottom=�6:   ���	#A0000000��6U   ��  �� ElementName$ vGrid�� �� Path$ ActualWidth� � RowDefinitions 5      �� 5       $��10 �6    �� 5!      $��25 �6    �� 5"      $��20 �6    �� 5#      $��10 �6    .+�5%      �� -   5        $��
TBProgress��5%      $��2��6*   $
��Left=�68   
�������$��00:00:00��6f   $��Center=�6v   $��10,0,0,0q�6�   �� 5&      -   5        $��TBDur��5&      $��2��6%   $��Right=�63   
�������$��00:00:00��6b   $��Center=�6r   $��0,0,10,0q�6�   � 5'      -   5        $��Progress��5'      $��1��6*   $��6��67   $��Center=�6B   $��	10,0,10,0q�6]     SimpleProgressBar#���  6p   $y�	Collapsed=�6�   %   FKFZ.Controls.SliderProgressBar  5)      -	   5        	  Name$		 spb��5)   &   %   FKFZ.Controls.SliderProgressBar
   ValueChange$
 Slider_PercentChange��5*   1   $��1��5)   3   $��20��6@   $��Center=�6L   $��	10,0,10,0q�6g    5*      Z� 61   5+      �� 5,   
   $��Center=�6   $��Bottom=�62    �� Orientation$ 
Horizontal=�5-      $y�	Collapsed=�6.   ��5,   M   �� $ vGrid��$ ActualWidth�.+�5/      �� -
   5        $��/Styles/skins/play.png��5/      $��30��64   $��30,5,0,5q�6`   �� 50      -   5        $��/Styles/skins/play.png��50      $��30��64   $��5q�6a   $y�	Collapsed=�6l   �� 51      -   5        $��/Styles/skins/play.png��51      $��30��64   $��5q�6`   $y�	Collapsed=�6k   �� 54      
�������$��Center=�6+   $��5q�6F   $y�	Collapsed=�6R   Volume6q   �� 55      -   5        $��volumeSlider��55      $��Center=�6)    � Minimum$ 0Q�56       � Maximum$ 1Q�6)    � Value$	 0.5Q�65   $��70��6A   $y�	Collapsed=�6L   �� 59      
�������$��5q�6+   $��Center=�67   $y�	Collapsed=�6R   Speed6p   �� 5:      -   5        $��speedRatioSlider��5:      $��Center=�6-   $ 1Q�5;      $��70��6   $y�	Collapsed=�6*   �� 5>      
�������$��5q�6+   $��Center=�67   $y�	Collapsed=�6R   	Seek To6r   �� 5?      -   5        $��timelineSlider��5?      $��5q�6+   $��70��6Y   $y�	Collapsed=�6d   5A      5B      5C      