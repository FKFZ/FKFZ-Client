   M S B A M L   `   `   `  ����   FKFZ    FKFZ.Window1   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     -     � Title$  Window1��$	��300��$	��300�� � 	Resources    
TextBrush1  
TextBrush2  Storyboard1 �� 
TargetName �� TargetProperty t� From	 t� To g� FillBehavior g� Duration W� From		 W� To	
 �� To �� Source �� Path R� KeyTime R� Value��%�  &	        &	      &	 !     7  7  7   7   ��	White����DarkGray���� -   .��t� $ GridPrev��$ Margin�$ 0,0,0,0X�$ 	0,-80,0,0X�$
 Stop=�$ 0:0:2D�W� $ GridPrev��$ Opacity�$ 1X�$	 0X�$
 Stop=�$ 0:0:2D��� $ TextBlockNow1��$4 .(TextBlock.Foreground).(SolidColorBrush.Color)�$
 Stop=�$ 0:0:2D�
 �� 8   $ Color��� $ TextBlockNow2��$4 .(TextBlock.Foreground).(SolidColorBrush.Color)�$
 Stop=�$ 0:0:2D�
 �� 8  $ Color��� $ TextBlockNext1��$4 .(TextBlock.Foreground).(SolidColorBrush.Color)�$
 Stop=�$ 0:0:2D�
 �� 8  $ Color��� $ TextBlockNext2��$4 .(TextBlock.Foreground).(SolidColorBrush.Color)�$
 Stop=�$ 0:0:2D�
 �� 8  $ Color�T� $ TextBlockNow1��$ (TextElement.FontWeight)�$
 Stop=�$ 0:0:2D�..�i� $ 0�� #�
Normal"�T� $ TextBlockNow2��$ (TextElement.FontWeight)�$
 Stop=�$ 0:0:2D�..�i� $ 0�� #�
Normal"�T� $ TextBlockNext1��$ (TextElement.FontWeight)�$
 Stop=�$ 0:0:2D�..�i� $ 0�� #�Bold"�T� $ TextBlockNext2��$ (TextElement.FontWeight)�$
 Stop=�$ 0:0:2D�..�i� $ 0�� #�Bold"�.��� 
���   � � RowDefinitions �� $��20 ��� $
��Auto �.+��� $��0�� �� Orientation$ 
Horizontal=�.+��� -   $��ButtonRepeat��$��
  重来  ��{�.  �� $��1��.+�� -   $��GridPrev��$��80�� �� $��10 ��� $��30 ��� $��30 ��� $��10 �.+��� $��1��$��Center=�$��Center=�
�������$��Normal"����� *ent+�� $��2��$��Center=�$��Center=�
�������$��Normal"����� *chs+� -   $��GridNow��$��80�� �� $��10 ��� $��30 ��� $��30 ��� $��10 �.+��� -   $��TextBlockNow1��$��1��$��Center=�$��Center=�
�������$
��Bold"����� *ent+�� -   $��TextBlockNow2��$��2��$��Center=�$��Center=�
�������$
��Bold"����� *chs+� -   $��GridNext��$��80�� �� $��10 ��� $��30 ��� $��30 ��� $��10 �.+��� -	   $��TextBlockNext1��$��1��$��Center=�$��Center=�
�������$��Normal"����� *ent+�� -
   $��TextBlockNext2��$��2��$��Center=�$��Center=�
�������$��Normal"����� *chs+�� -   $��ItemsControl1������ .R�� $��80�� �� $��10 ��� $��30 ��� $��30 ��� $��10 �.+��� $��1��$��Center=�$��Center=�
����������� *ent+�� $��2��$��Center=�$��Center=�
����������� *chs+