   M S B A M L   `   `   `  ����?  ;FKFZ, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:FKFZ.TranscationFKFZ.Transcation  5   	       FKFZ.MainWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   :PageTransitionsclr-namespace:FKFZ.Transcation     5   	   -   5          �Name$  myMainWindow��5   A    � Title$ 防空防灾��5   	   $
��1080��6   $
��1920��6$    � 
ResizeMode$ NoResize=�61    � WindowStartupLocation$ CenterScreen=�5   	    � WindowState$ 	Maximized=�6.    � Icon$ Styles/skins/fklogo.png��6F    � WindowStyle$
 None=�6e    � 	Resources 5         MyBrush �� EndPoint	 �� 
StartPoint
 � Color � Offset��%�   &	        �� 5	   
   $ 0.5,1&�6.   $	 0.5,0&�6?   .��5
      � $
 	#CFFFFFFF��6   � 5      $
 	#FF7EBDD8��6   $ 1Q�6-   5      5      .��5      �� 
���   �$	��0.5q�6!    �� CornerRadius$ 2��67   $��0q�6H    J� Effect 5   
   3  -System.Windows.Media.Effects.DropShadowEffect  5         ShadowDepth$ 0Q�6      Opacity$	 0.8Q�6/   ._�5   
   � -   5        $��MainGrid��5       � LayoutTransform 5      �� 5      -   5        ApplicationScaleTransform�� �� CenterX$ 0Q�5       �� CenterY$ 0Q�5       �� ScaleX 5      ��  �� ElementName$ myMainWindow�� �� Path$ 
ScaleValue� �� ScaleY 5      �� $ myMainWindow��$ 
ScaleValue�.+�5       � -   5        $��MainPage��5        � NavigationUIVisibility$ Hidden=�6%   ��.  $��Stretch=�5      $	��Top=�5       5!      5"      5#      5$      