   M S B A M L   `   `   `  ����D  @Group2WPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:Group2WPF	Group2WPF  5   	   %    Group2WPF.BookingPLatformWindow   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   )localclr-namespace:Group2WPF     5   	     � Title$  Booking Platform Window��5   	   $	��450��6)   $	��800��66   ���#F0F0F0.��5      �  � ColumnDefinitions 5   
   �� 5      $
��1.5* �6   �� 5      $��2* �6   �� 5      $��2* �6   .+�5   
   �� $��1��6   ���#623ED0 �� CornerRadius$ 	0 50 0 30��63   ._�5      �� $��0��6   $��10q�6)   .+�5      �� -   5         ��Name$ btnOpenAirlineWindow��5      $��Airline Window��67      NavButtonStyle#��[  6p   �� 5      -   5        $ btnOpenAirportWindow��5      $��Airport Window��67   #��[  6p   �� 5      -   5        $ btnOpenPassengerWindow��5      $��Passenger Window��69   #��[  6v   �� 5      -   5        $ btnOpenBookingWindow��5      $��Booking Window��67   #��[  6p   �� 5      -   5        $" btnOpenBookingPlatformWindow��5      $��BookingPlatform Window��6?   #��[  6�   �� 5      -   5        $ btnOpenFlightWindow��5      $��Flight Window��66   #��[  6m   �� 5      -   5        $ btnOpenBaggageWindow��5      $��Baggage Window��67   #��[  6p   �� 5      -   5        $ 	btnLogout��5      $��Logout��6,   #��[  6R   5      5      � 5#   
   $��1��6   $��10q�6    � RowDefinitions 5$      �� 5%      $
��Auto �6    �� 5&      $
��Auto �6    �� 5'      $��* �6    �� 5(      $��60 �6    .+�5+      ��  �� Orientation$ Vertical=�6   $��0��60   $��0 0 0 10q�6=   .+�5,      �� $��Booking Platform Table��6   $
��Bold"�69   $��18(�6K   $��Center=�6Y   $��0 0 0 10q�6t   5-      &   System.Windows.Controls.DataGrid  51      -	   5          Name$ DataGridBookingPlatform��51         AutoGenerateColumns$ False��68   $��2��6T      AlternatingRowBackground
 �����
�������	   Columns	 52      0  *System.Windows.Controls.DataGridTextColumn  53      ,  &System.Windows.Controls.DataGridColumn
   Header$
 ID��6)   1  +System.Windows.Controls.DataGridBoundColumn   Binding 65   �� *Id+  54      $

 Name��6)    67   �� *Name+  55      $	
 URL��6)    66   �� *Url+57      �� 59      $��3��6   $��Right=�6"   ���#DEE4EC$��1q�6T   $ 5��6h    �� Padding$ 5q�6y   ���#ffffff$��
0 10 10 10q�6�   ._�5:      �� $ 
Horizontal=�6   .+�5;      �� -
   5        $ btnPreviousPage��5;      $	��Pre��66     pagingButton#��[ 6_   �� 5<      $��1��6    ���#DEE4EC$	��3 7q�69   �� 5=      -   5         ��Name$ PaginationItemsControl��5=   #   $��Center=�6C   ��5>      �� 5?      .R�5@   "   �� $ 
Horizontal=�6-   5A      ��5C      �� 5D      .R�5E   "   �� $ 
Horizontal=�6-   .+�5F   &   �� -   5        #��[ 5F   Z   ��6-   �� 6Z   �� 5G   &   $��1��60   ���#DEE4EC$	��3 7q�6I   5H   #   5I      5K      �� 5L      -   5        $ btnNextPage��5L      $
��Next��62   #��[ 6X   5M      5N      5O      � 5R   
   $��2��6   $��10q�6    5S      �� 5T      $
��Auto �6    �� 5U      $
��Auto �6    �� 5V      $
��Auto �6    �� 5W      $
��Auto �6    �� 5X      $
��Auto �6     5Z      �� 5[      $
��Auto �6#   �� 5\      $��* �6#   .+�5_      �� $	��ID:��6   $��0��6"   $��0��6/   $��Center=�6?     FormLabelStyle#��[ 6Z   �� 5`      -   5         ��Name$ txtId��5`      $��0��6k   $��1��6x   ${�False��6�     FormTextBoxStyle#��[ 6�   ��6%   �� *SelectedItem.Id+ �� ElementName$ DataGridBookingPlatform��6�   �� 5b      $��Name:��6   $��1��6$   $��0��61   $��Center=�6A   #��[ 6\   �� 5c      -   5        $ txtName��5c      $��1��6o   $��1��6|   #��[ 6�   ��6'   �� *SelectedItem.Name+$ DataGridBookingPlatform��6�   �� 5e      $
��URL:��6   $��2��6#   $��0��60   $��Center=�6@   #��[ 6[   �� 5f      -   5        $ txtUrl��5f      $��2��6m   $��1��6z   #��[ 6�   ��6&   �� *SelectedItem.Url+$ DataGridBookingPlatform��6�   �� 5h      $ 
Horizontal=�6   $��4��62   $��2��6?   $��5 q�6S   .+�5j      �� -   5        $	��Add��5j      #��[  6?   $��60��6g   $��0 5 5 0q�6r   �� 5k      -   5        $
��Edit��5k      #��[  6A   $��60��6i   $��0 5 5 0q�6t   �� 5l      -   5        $��Delete��5l      #��[  6E   $��60��6m   $��0 5 5 0q�6x   �� 5m      -   5        $��Clear��5m      #��[  6C   $��60��6k   $��0 5 5 0q�6v   5n      5o      5p      5q      