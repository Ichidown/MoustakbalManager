   M S B A M L   `   `   `  ����  MoustakbalManager'    !MoustakbalManager.Pages.AdminPage   5      V RMaterialDesignThemes.Wpf, Version=2.4.1.1101, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5
      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5         MaterialDesignBody#���   5      $��Regular"�5   
   $��13(�5   
   &   System.Windows.Media.TextOptions    TextFormattingMode$  Ideal=�5   
      TextRenderingMode$
 Auto=�5   
     MaterialDesignPaper N� 
Background# �  5   
     MaterialDesignFont N� 
FontFamily# �  5	   
    N� Title$ 	AdminPage��5      $��Disabled=�6    � 	Resources 5        MenuOpen �� TargetProperty �� 
TargetName9  3System.Windows.Media.Animation.EasingDoubleKeyFrame O� KeyTime	 O� Value  	MenuClose��%�  &	       &	 �     �� 5   
   .��5      U� $ (FrameworkElement.Width)�6,   $ 
BurgerMenu��6a   .\�5        $ 0��6'   $	 54Q�63     5      $ 0:0:0.2��6'   $		 260Q�69   5      5      �� 5   
   .��5      U� $ (FrameworkElement.Width)�6,   $ 
BurgerMenu��6a   .\�5        $ 0��6'   $		 260Q�63     5      $ 0:0:0.2��6'   $	 54Q�69   5      5      5       
 � Triggers
 5#      :� 5$   
    :� RoutedEvent$ CheckBox.Checked��6    :� 
SourceName$ 	NavToggle��66   .[�5%      �� #��[ 6   5&      :� 5'   
   $ CheckBox.Unchecked��6   $ 	NavToggle��68   .[�5(      �� #��[ 6   5)      .��5-      � $��Disabled=�6   ���	#FFF5F4F5.+�5.   
   ]� $��Disabled=�6   $��Stretch=�6H   .+�5/      (  "MaterialDesignThemes.Wpf.ColorZone  $
��Left=�6'      Mode$ PrimaryDark=�6=   +  %MaterialDesignThemes.Wpf.ShadowAssist   ShadowDepth$ Depth5=�6P   $��Disabled=�6�   $��Stretch=�6�   .��50      �� -   5         ��Name$ 
BurgerMenu��50      $��54��61   .+�51      � 
�������.+�52      � $��54��6   $��54��6+   $��Right=�66   $��Center=�6R   .+�53      d� -   5         d�Name$ 	NavToggle��53   +   
������� ' #MaterialDesignHamburgerToggleButton#���  6T   $��40��6�   $��40��6�   $��Center=�6�   $��Center=�6�   54      55      �� 57      -   5        $��	0 20 0 20q�57      $��Disabled=�6U   $��Disabled=�6�   .@�58      ��  �� Orientation$ 
Horizontal=�6%   .+�59      '  !MaterialDesignThemes.Wpf.PackIcon  
��������  �ControlzEx.PackIconBase`1[[MaterialDesignThemes.Wpf.PackIconKind, MaterialDesignThemes.Wpf, Version=2.4.1.1101, Culture=neutral, PublicKeyToken=null]]   Kind$ PlaylistCheck=�6I   $��30��6^   $��30��6j   $��5 0 10 0q�6u   �� 5:      $��Liste des adhérents��6(   
�������   MaterialDesignTitleTextBlock#��[ 6V   $��Center=�6�   5;      �� 5=      $ 
Horizontal=�6%   .+�5>        
�������$ ChartBar=�6I   $��30��6Y   $��30��6e   $��5 0 10 0q�6p   �� 5?      $��Statistiques��6(   
�������#��[ 6O   $��Center=�6�   5@      5A      5C      5D        5F      $	��Top=�6'   $ PrimaryDark=�6<   $��8q�6O   $ Depth0=�6[   $��Disabled=�6�   $��54��6�   .��5G      �� $ 
Horizontal=�6   .+�5H      �� $�� Adherant Manager��6    $��Center=�69   $��16 0 0 0q�6T   5I      5J      7  1MaterialDesignThemes.Wpf.Transitions.Transitioner  5M      -   5          Name$ MainTransitioner��5M   +    �� SelectedIndex$ 0��6E   !   AutoApplyTransitionOrigins .  $��Disabled=�6z   .@�5O      <  6MaterialDesignThemes.Wpf.Transitions.TransitionerSlide     ForwardWipe 5P      4	  .MaterialDesignThemes.Wpf.Transitions.SlideWipe	  5Q       	  	Direction$
 Left=�63      BackwardWipe 5S      	  5T      $
 Left=�63   .��5V       � -   5          �Name$ MainGridFrame��5V        � NavigationUIVisibility$ Hidden=�63   5W        5Y       5Z      	  5[      $
 Left=�63    5]      	  5^      $
 Left=�63   .��5`       � -   5        $ MainStatFrame��5`      $ Hidden=�63   5a      5c      5d      5e      5k      