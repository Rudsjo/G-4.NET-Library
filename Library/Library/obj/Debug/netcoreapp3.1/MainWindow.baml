   M S B A M L   `   `   `  ����B  >Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:LibraryLibrary  5   	   G CLibrary.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA0clr-namespace:Library.Core;assembly=Library.CoreLibrary.Core 5   	       Library.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   'localclr-namespace:Library     5   	   Acore0clr-namespace:Library.Core;assembly=Library.Core     5   	     � WindowStartupLocation$  CenterScreen=�5	   	    � Title$  ��5
   	   $	��800��6   $
��1200��6"   $	��800��62   $
��1200��6B    J� RenderTransformOrigin$ 0.5 0.5&�5   	   z�5      �� 5   
    �� ScaleX$ 1Q�6    �� ScaleY$ 1Q�6$   .��5      �  � ColumnDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6    � RowDefinitions 5!   
   �� 5#      $��* �6   �� 5&      $	��10* �6   ��5*   
   �� 5+       �� ImageSource$) #Styling\Images\LibsysBackground.png��6   .+�5/   
      Library.HeaderBarControl  $��1��6!   y�50      �� *&$ApplicationViewModel.SideMenuVisible+   Library.ViewModelLocator   Instance	 �� Source#	 Z 
 �� 	Converter
 *   $Library.BooleanToVisibilityConverter   �� ConverterParameter$ 	ErrorText��,   &Library.MainContentUserControlSkeleton  56   
   $��1��6/   $��1��6?   y�57   	   �� *(&ApplicationViewModel.IsSkeletonVisible+#	 Z 
   $ 	ErrorText��   Library.PageHost  5<   
   $��1��5=      $��1��5>       � DataContext 5@      �� *" ApplicationViewModel.CurrentPage+#	 Z 
 +   %Library.ApplicationPageValueConverter  $
 Main��   CurrentPage 5D      ��    Library.SideMenuControl  5I   
   $��2��6    $	��250��5J      y�5K      �� *&$ApplicationViewModel.SideMenuVisible+#	 Z 
   $ 	ErrorText��   Library.PopUpControl  5P   
   $��2��5S      $��2��61   y�5P      �� *#!ApplicationViewModel.PopUpVisible+#	 Z 
   5S   1   5V      5W      