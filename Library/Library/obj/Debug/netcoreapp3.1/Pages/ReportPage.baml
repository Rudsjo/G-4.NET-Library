   M S B A M L   `   `   `  ����B  >Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:LibraryLibrary  5	      G CLibrary.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA0clr-namespace:Library.Core;assembly=Library.CoreLibrary.Core 5
          Library.ReportPage   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      'localclr-namespace:Library     5	      Acore0clr-namespace:Library.Core;assembly=Library.Core     5
        N� Title$  
ReportPage��5      .��5      �  � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6   .+�5   
   � $��0��6    � ColumnDefinitions 5      �� 5      $��* �6#   �� 5      $��* �6#   .+�5      �� $��0��6      MainHeaderText#��[  5      $��	Rapporter��5       $
��Left=�5!      $��
50 80 0 10q�5"      �� 5&       �� Orientation$ 
Horizontal=�6   $��1��5'      $��Right=�6)   $��-200 80 50 10q�5(      .+�5+         Library.ButtonWithIcon  $��Ladda ner CSV��6'      IconContent$	 ��5,   '     TableActionButton#��[ 5-   '   $��0 0 5 0q�5/   '   ��5.   '   �� *DownloadCSV+5/   '     53      $��Lånade böcker��6'   $	 ��54   '   #��[ 55   '   $��0 0 5 0q�5:   '   ��56   '   �� *AllLoanedBooksFilter+ �� UpdateSourceTrigger$ PropertyChanged=� �� 	Converter #   Library.StateToStyleConverter   �� ConverterParameter$ LoanedBooksReport����59   '   �� *ShowLoanedArticles+5:   '     5>      $��Reserverade böcker��6'   $	 ��5?   '   #��[ 5@   '   $��0 0 5 0q�5E   '   ��5A   '   �� *AllReservedBooksFilter+   $ ReservedBooksReport����5D   '   �� *ShowReservedArticles+5E   '   5G      5H      � 5J   
   $��1��6   $��
50 0 50 55q�6   .+�5L      �� 
���������5M      �� *
CurrentCSV+��5N      �� *
CurrentCSV+ 2   ,Library.DynamicListToHeaderTemplateConverter  ��5O      �� *
CurrentCSV+ 0   *Library.DynamicListToItemTemplateConverter  ��5R      �� 5S       �� 
TargetType" ��6&   .R�5U      �� ._�5V      �� $��Hidden=�6+   $
��Auto=�6R   .��5W   "   �� .+�5X   &   �� $��Header��67   �� 5Y   &   5Z   #   5[      5\      5^      5a      5c      5e      5g      