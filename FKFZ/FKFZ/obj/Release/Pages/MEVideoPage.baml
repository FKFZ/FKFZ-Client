   M S B A M L   `   `   `  ���� ?  ;FKFZ, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:FKFZ.ControlsFKFZ.Controls      FKFZ.Pages.MEVideoPage   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     -localclr-namespace:FKFZ.Controls     -     N� 
Background
  �   � N� Title$ 	VideoPage��.��� -   $��vGrid��.+�r� -   $��Player�� r� LoadedBehavior$ Manual=� r� UnloadedBehavior$
 Stop=� r� Stretch$
 Fill=�$y�Visible=� � FocusVisualStyle Z� �� -   $��btnPlay��   ImgBtnStyle#���   $y�Visible=�$��90��$��Center=�� -   $��Status��$��Center=�$��Bottom=����	#A0000000����  �� ElementName$ vGrid�� �� Path$ ActualWidth� � RowDefinitions �� $��10 ��� $��25 ��� $��20 ��� $��10 �.+��� -   $��
TBProgress��$��2��$
��Left=�
�������$��00:00:00��$��Center=�$��10,0,0,0q��� -   $��TBDur��$��2��$��Right=�
�������$��00:00:00��$��Center=�$��0,0,10,0q�� -   $��Progress��$��1��$��6��$��Center=�$��	10,0,10,0q�  SimpleProgressBar#���  $y�	Collapsed=�%   FKFZ.Controls.SliderProgressBar  -	   	  Name$		 spb��%   FKFZ.Controls.SliderProgressBar
   ValueChange$
 Slider_PercentChange��$��1��$��20��$��Center=�$��	10,0,10,0q� Z� �� $��Center=�$��Bottom=� �� Orientation$ 
Horizontal=�$y�	Collapsed=����� $ vGrid��$ ActualWidth�.+��� -
   $��/Styles/skins/play.png��$��30��$��30,5,0,5q��� -   $��/Styles/skins/play.png��$��30��$��5q�$y�	Collapsed=��� -   $��/Styles/skins/play.png��$��30��$��5q�$y�	Collapsed=��� 
�������$��Center=�$��5q�$y�	Collapsed=�Volume�� -   $��volumeSlider��$��Center=� � Minimum$ 0Q� � Maximum$ 1Q� � Value$	 0.5Q�$��70��$y�	Collapsed=��� 
�������$��5q�$��Center=�$y�	Collapsed=�Speed�� -   $��speedRatioSlider��$��Center=�$ 1Q�$��70��$y�	Collapsed=��� 
�������$��5q�$��Center=�$y�	Collapsed=�	Seek To�� -   $��timelineSlider��$��5q�$��70��$y�	Collapsed=�