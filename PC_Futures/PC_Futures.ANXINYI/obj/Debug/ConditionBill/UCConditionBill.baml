   M S B A M L   `   `   `  ����M  IPC_Futures.ANXINYI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:PC_Futures.ANXINYIPC_Futures.ANXINYI  5      c _System.Windows.Interactivity, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35qPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.InteractivitySystem.Windows.Interactivity 5      D @Utilities, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null8*clr-namespace:Utilities;assembly=Utilities	Utilities 5	      (    "PC_Futures.ANXINYI.UCConditionBill   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      2local clr-namespace:PC_Futures.ANXINYI     5      ^iPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.Interactivity     5      @	Utilities*clr-namespace:Utilities;assembly=Utilities     5	        � 	Resources  5       �� MergedDictionaries �� Source"  Utilities.BSToValueConverter   BSToValueConverter(  "Utilities.StrToColorValueConverter  StrToColorValueConverter)  #Utilities.BillStateToValueConverter  BillStateToValueConverter!  Utilities.OFFSETTypeToValue  OFFSETTypeToValueConvert�� 5   
    5      �� 5      $&  /ControlStyles/ButtonStyles.xaml?�6%   �� 5      $' !/ControlStyles/CommboxStyles.xaml?�6%   �� 5      $' !/ControlStyles/ControlStyles.xaml?�6%   �� 5      $( "/ControlStyles/DataGridStyles.xaml?�6%   %t   &	        &	      &	 &     &	 9       5      6+     5      61     5      62     5      6*   5      .��5      �  � RowDefinitions 5   
   �� 5      $��25 �6   6*   �� 5      6   .+�5   
   �� ._�5       �  � ColumnDefinitions 5!      �� 5"      6)   �� 5#      6)   .+�5%      ��  �� Orientation$ 
Horizontal=�6   .+�5&      �� $��Center=�6   $
��Left=�68   $��修改��6S     OrderCancelButton#��[ 6`   $��10,0,0,0q�6�   $��70��6�   ��6�   �� *UPDateCommand+5(      �� 5*      $��Center=�6   $
��Left=�68   $��删除��6S   #��[ 6`   $��10,0,0,0q�6�   $��70��6�   ��6�   �� *DeleteCommand+5,      5-      �� 5.      $��1��6   $��Center=�6-   $��Right=�6H   $��0,0,20,0q�6d   .+�50      �� $y�	Collapsed=�6@     DaochuButton#��[ 6W   ��6   �� *ExportCommand+6   51      52      53      �� 54   
   $��1��6   $��1q�6   ���#2C8FDD._�55      &   System.Windows.Controls.DataGrid  -   5        $��gridview��56   *      SelectionUnit$ FullRow=�55         AutoGenerateColumns .     CanUserSortColumns .    DataGridStyle#��[ 6g   	   CanUserResizeColumns	 . 
   CanUserResizeRows
 .   �� AlternationCount$ 2��6�      CanUserAddRows .     SelectionMode$ Single=�56   �   $��0,0,0,0q�6�   ��6;   �� *ConditionBillList+ �� SelectedItem 6f   �� *SelectedItem+.  (System.Windows.Interactivity.Interaction   Triggers 57      /  )System.Windows.Interactivity.EventTrigger  58         	EventName$ MouseLeftButtonUp��6%   .  (System.Windows.Interactivity.TriggerBase   Actions. 59      6	  0System.Windows.Interactivity.InvokeCommandAction	   	  Command 60   �� *SelectionChangedCommand+5:         Columns 5<      4
  .System.Windows.Controls.DataGridTemplateColumn
  5=      ,  &System.Windows.Controls.DataGridColumn   Width5  /System.Windows.Controls.DataGridLengthConverter$ * 6/      HeaderTemplate 5>      �� 5?      .R�5@   "   �� -   5        
������ ._�5A   &   �� $��合约��60   
������ $��Center=�6T   $��Center=�6q   5B   '   5C   #   5D       
  CellTemplate 5F      �� 5G      .R�5H   "   �� ._�5I   &   �� $��0,0,0,0q�6O   $��Center=�6`   $��Center=�6{   ��60   �� *ContractCode+6{   5J   #   5K      5M      
  5O      $ * 60    5P      �� 5Q      .R�5R   "   �� -   5        
������ ._�5S   &   �� $��买卖��60   
������ $��Center=�6T   $��Center=�6q   5T   '   5U   #   5V       5X      �� 5Y      .R�5Z   "   �� ._�5[   &   �� $��0,0,0,0q�6�   $��Center=�6�   $��Center=�6�   ��60   �� *	Direction+ �� 	Converter# [  ��6z   �� *	Direction+# [ 6�   5\   #   5]      5_      
  5a      $ * 60    5b      �� 5c      .R�5d   "   �� -   5        
������ ._�5e   &   �� $��开平��60   
������ $��Center=�6T   $��Center=�6q   5f   '   5g   #   5h       5j      �� 5k      .R�5l   "   �� ._�5m   &   �� $��0,0,0,0q�6�   $��Center=�6�   $��Center=�6�   ��61   �� *
OpenOffset+# [ 6�   5n   #   5o      5q      
  5r      $ * 60    5s      �� 5t      .R�5u   "   �� -   5        
������ ._�5v   &   �� $��状态��60   
������ $��Center=�6U   $��Center=�6r   5w   '   5x   #   5y       5{      �� 5|      .R�5}   "   �� ._�5~   &   �� $��0,0,0,0q�6~   $��Center=�6�   $��Center=�6�   ��60   �� *Status+# [ 6�   5   #   5�      5�      
  5�      $ * 6/    5�      �� 5�      .R�5�   "   �� -   5        
������ ._�5�   &   �� $��触发条件��60   
������ $��Center=�6W   $��Center=�6t   5�   '   5�   #   5�       5�      �� 5�      .R�5�   "   �� ._�5�   &   �� $��0,0,0,0q�6N   $��Center=�6_   $��Center=�6z   ��60   �� *TrrigerCond+6z   5�   #   5�      5�      
  5�      $ * 6/    5�      �� 5�      .R�5�   "   �� -   5        
������ ._�5�   &   �� $��下单价格��60   
������ $��Center=�6W   $��Center=�6t   5�   '   5�   #   5�       5�      �� 5�      .R�5�   "   �� ._�5�   &   �� $��0,0,0,0q�6Q   $��Center=�6b   $��Center=�6}   ��60   �� *ShowOrderPrice+6}   5�   #   5�      5�      
  5�      $ * 6/    5�      �� 5�      .R�5�   "   �� -   5        
������ ._�5�   &   �� $��下单手数��60   
������ $��Center=�6W   $��Center=�6t   5�   '   5�   #   5�       5�      �� 5�      .R�5�   "   �� ._�5�   &   �� $��0,0,0,0q�6N   $��Center=�6_   $��Center=�6z   ��60   �� *OrderVolume+6z   5�   #   5�      5�      
  5�      $ * 6/    5�      �� 5�      .R�5�   "   �� -	   5        
������ ._�5�   &   �� $��创建时间��60   
������ $��Center=�6W   $��Center=�6t   5�   '   5�   #   5�       5�      �� 5�      .R�5�   "   �� ._�5�   &   �� $��0,0,0,0q�6M   $��Center=�6^   $��Center=�6y   ��60   �� *
CreateTime+6y   5�   #   5�      5�      
  5�      $ * 6/    5�      �� 5�      .R�5�   "   �� -
   5        
������ ._�5�   &   �� $��触发时间��60   
������ $��Center=�6W   $��Center=�6t   5�   '   5�   #   5�       5�      �� 5�      .R�5�   "   �� ._�5�   &   �� $��0,0,0,0q�6N   $��Center=�6_   $��Center=�6z   ��60   �� *TrrigerTime+6z   5�   #   5�      5�      5�      5�      5�      5�      