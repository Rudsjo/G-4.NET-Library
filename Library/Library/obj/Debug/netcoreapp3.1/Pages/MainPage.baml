   M S B A M L   `   `   `  ����B  >Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:LibraryLibrary  5      G CLibrary.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA0clr-namespace:Library.Core;assembly=Library.CoreLibrary.Core 5          Library.MainPage   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      'localclr-namespace:Library     5      Acore0clr-namespace:Library.Core;assembly=Library.Core     5        N� Title$  MainPage��5
      .��5      �  � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   �� $	����6   $��0��6$      FontAwesomeSolid#��[  5        
IconButton#��[ 5      $��Right=�5      $	��Top=�6-   $��
0 -40 40 0q�5       ��5!      �� *+)MainContentUserControlViewModel.MyProfile+   Library.ViewModelLocator   Instance �� Source# Z � 5&   
   $��1��6   $��Center=�6   $��Center=�69   $��
0 -100 0 0q�6T    � ColumnDefinitions 5'      �� 5(      $
��Auto �6#   �� 5)      $��* �6#    5,      �� 5-      $
��Auto �6    �� 5.      $
��Auto �6    �� 5/      $
��Auto �6    .+�53      �� $$��/Styling/Images/LibsysLogo.png��6   $	��250��6<   $	��250��6H   $��2��6U   �� 56      -   5         ��Name$ tbFirstSearch��5;        	SearchBox#��[ 56      
 � Tag$ Sök...��69   $��1��6F   $	��400��57      $��50��6"     CustomLightGreyBrush#��[ 58      $��2q�59      $��Center=�5:      ��5<      �� *FirstSearchText+ �� UpdateSourceTrigger$ PropertyChanged=�.��5=       J� InputBindings 5>      �� 5?      
	 �� Key$	 Return��6!   
 �� Command
 6.   �� *FirstSearch+.��5@   )   5A      �� 5D      $��1��6   $��1��6%   ._�5E      �� $	����6     SearchButton#��[ 5F      $��-12 20q�5G      ��5H      �� *FirstSearch+5J         Library.ButtonWithIcon  5M      $ ��Eller visa alla böcker...��6#   $��2��6G   $	��Top=�5N   #     TextIconButton#��[ 5O   #   $��-140 20q�5P   #   ��5Q   #   �� *ShowAllBooks+5T      5U      5V      