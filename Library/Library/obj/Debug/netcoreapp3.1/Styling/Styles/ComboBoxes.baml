   M S B A M L   `   `   `  ����B  >Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:LibraryLibrary  5      e aPresentationFramework.AeroLite, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35kNclr-namespace:Microsoft.Windows.Themes;assembly=PresentationFramework.AeroLiteMicrosoft.Windows.Themes 69   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089   ComboBoxToggleButton  �� 
TargetType � ColumnDefinitions ��Name �� CornerRadius  MediumLightGreyBrush ��Name  FontAwesomeSolid  ComboBoxTextBox �� 
TargetType �� Property �� Value  CustomGreyBrush �� 	ClickMode	 d� 	IsChecked
 �� Path �� Mode �� RelativeSource �� Orientation J� IsHitTestVisible  FontSizeSmall  CustomLightGreyBrush �� TextAlignment �� SelectionBoxItem �� SelectionBoxItemTemplate ��Name �� 
IsReadOnly �� 
IsReadOnly �� IsDropDownOpen � AllowsTransparency J� SnapsToDevicePixels � ActualWidth �� MaxDropDownHeight K� IsItemsHost-   'System.Windows.Input.KeyboardNavigation    DirectionalNavigation �� Triggers P� Property P� Value �� 
TargetName  P� 
SourceName! �� Triggers" �� Padding�� 5      F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     :x,http://schemas.microsoft.com/winfx/2006/xaml     5      localclr-namespace:Library  5      XthemeNclr-namespace:Microsoft.Windows.Themes;assembly=PresentationFramework.AeroLite  69   %c  &	        7  7  7  7  &	 �    '�� J    7  7   7  7  7  7  7  7  7  '�� �    7  7  7  �� 5      "  d�63   .R�5   
   �  5      �� 5	      �� 5
      $��20 �6#   .+�5      �� $ Border��5      $��2��5      $ 0��5      
������ 8��  $��2q�5      �� 5      $��0��5      $ 0��5      $��1q�5      
������ 8�� $��0,0,2,0q�5      �� 5      $ Arrow��6   $��1��6'   $��0 0 4 0q�5      $	����6)   8�� $��Center=�5      $��Center=�65   $��16(�5      8�� 6&   5       5"      �� 5$      "  ��6.   .R�5%   
   �� $ PART_ContentHost��6   $|�False��6+   #��z��6=   5&      �� 5(      " ��6&   .�5)   
   ��  �@J�SnapsToDevicePixels$
 true��60   �� 5*   
    �@�OverridesDefaultStyle$
 true��62   �� 5+   
    � ��$
 Auto=�6G   �� 5,   
    � ��$
 Auto=�6E   �� 5-   
    � ��$
 true��6:   �� 5.   
    � ��$	 120��6%   �� 5/   
    � ��$ 20��6&   �� 50   
    � ��8   6'   �� 51   
    � �� 52      �� 53      "  ��6"   .R�54      � .+�56      d� $��ToggleButton��57      8�� $��2��59      $|�false��5:      $ Press=�5<      	 5;      �� $
 IsDropDownOpen�$ TwoWay=� �� *TemplatedParent+5=      � 5?       5@      �� 5A   "   $
��Auto �63   �� 5B   "   $��* �63   .+�5D      �� $ 
Horizontal=�6)   $��0��6B   .+�5F   "   �� $ False��6,   $��Center=�5G   ,   $��27��5H   ,   8�� $	����5J   ,   8�� $��Center=�5L   ,   8�� $��10q�5N   ,   $ Center=�5O   ,   �� 5R   "   8�� $��1q�6]   $��
0 10 10 10q�6q   �� 5T   "   $��ContentSite��63   $ False��6F   #��z 6`   #��z 5U      #��z��5V      $��3,3,23,3q�5W      $��Center=�5X      $
��Left=�5Y      5\      �� 5a      $ PART_EditableTextBox��6&   8�� $
��Left=�5d      $��Center=�5e      $��3,3,23,3q�5f      $
|�True��5g      
������ 
��� � �$y�Hidden=�5j      # z 5k      ��5b      Z� 5k      � 5n      $��Popup��5o      $��Bottom=�5p      #��z 5q      $
 True��5r      $|�False��5s      $��Slide=�5t      .��5v   "   � $��DropDown��6'   $
 True��5w      #��z 5x      #��z 5y      .+�5z   &   �� $ DropDownBorder��5{   !   
�������$��2q�5~   !   8�� 5   !   �� 5�   &   $��4,6,4,6q�63   $
 True��6D   .��5�   *   �� $
 True��65   $ 	Contained=�6H   5�   '   5�   #   5�      5�      5�       5�      P� 5�       �@��HasItems$ false��66   . �5�      �� $ DropDownBorder��6%    � ��$ 95��6V   5�      P� 5�       � {�$ false��67   . �5�      �� 8   � ��6;   5�      P� 5�       �@��
IsGrouping$
 true��68   . �5�      ��  � ��$ false��6N   5�      P� 5�      $  Popup��6"    �@�AllowsTransparency$
 true��6Y   . �5�      �� $ DropDownBorder��6%    �@��CornerRadius$ 0��6Y   �� 5�      $ DropDownBorder��6%    � ��$ 0,2,0,0q�6S   5�      P� 5�       �@��
IsEditable$
 true��69   . �5�      ��  � ��$ false��6:   �� 5�      $ PART_EditableTextBox��6%    � y�$ Visible=�6]   �� 5�      $ ContentSite��6%    � y�$ Hidden=�6T   5�      5�      5�      ! 5�   
   5�      �� 5�      " ��6*   .�5�   
   ��  �@J�SnapsToDevicePixels$
 true��60   �� 5�   
    � ��8   6'   �� 5�   
    �@�OverridesDefaultStyle$
 true��62   �� 5�   
    � �� 5�      �� 5�      "  ��6"   .R�5�      �� $��Border��6   $" 2q�5�      $
 true��5�      ._�5�      �� 5�       5�      P� 5�       �@��IsHighlighted$
 true��6;   . �5�      �� $ Border��6%   8   � ��6O   5�      P� 5�       � {�$ false��67   . �5�      �� 8   � ��6;   5�      5�      5�      5�      5�      