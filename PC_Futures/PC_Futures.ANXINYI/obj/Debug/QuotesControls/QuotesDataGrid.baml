   M S B A M L   `   `   `  ����M  IPC_Futures.ANXINYI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:PC_Futures.ANXINYIPC_Futures.ANXINYI  5      D @Utilities, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null8*clr-namespace:Utilities;assembly=Utilities	Utilities 5      '    !PC_Futures.ANXINYI.QuotesDataGrid   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      2local clr-namespace:PC_Futures.ANXINYI     5      @	Utilities*clr-namespace:Utilities;assembly=Utilities     5      -   5          � 	Resources  5
       �� MergedDictionaries �� Source �� 
TargetType   TransparentLabel �� Property �� Value$  Utilities.BoolToValueConverter  AddVisibleConverter   	TrueValue   
FalseValue  DeleteVisibleConverter�� 5   
    5      �� 5      $# /DataConvert/DataConvert.xaml?�6%   �� 5      $' !/ControlStyles/ControlStyles.xaml?�6%   %l  &	        &	 �     &	     �� 5      " ��6   .�5      ��  � ��
 ���� 6F   �� 5       � ��
 � ���6@   �� 5       � ��$ Center=�66   �� 5       � ��$ 0q�6,   �� 5       � ��$ Right=�68   5        5      $ 	Collapsed��6I   $ Visible��6_     5      $ Visible��6L   $ 	Collapsed��6`   5      .��5      � .+�5   
   &   System.Windows.Controls.DataGrid  -   5          Name$ quotesDataGrid��5        QuotesDataGridStyle#��[ 6,     5         ContextMenu	 m� Command
 �� 	Converter��%  &	       7  7  �� 5!      -   5        .@�5"      m� $��加入自选��6   	 6-   �� *
AddCommand+y�6L   �� *IsOptionalStock+8
   m� 5#      $��删除自选��6   	 6-   �� *
DelCommand+y�6L   �� *IsOptionalStock+8
  5$      5%         RowStyle 5&      �� 5'      )  #System.Windows.Controls.DataGridRow"  6   .�5(      ��  � ��
 ���� 63   �� 5*       � ��$ 1q�68   -   5        �� 5,       �@�ContextMenu# [ 64    �� Triggers 5-      P� 5.       P� Property �@ 
IsSelected P� Value . . �5/      ��  � �� �#0d39826<   �� 50       � �� �#03004c6;   51      53         Columns 55      4  .System.Windows.Controls.DataGridTemplateColumn  56      ,  &System.Windows.Controls.DataGridColumn   Header$	 序��6)      Width5  /System.Windows.Controls.DataGridLengthConverter$ 40 64      CellTemplate 57      �� 58      .R�59      �� #��[  6$   ��6N   �� *Seq+5:      5<        5=      $ 名称��6)   $	 150 65    5>      �� 5?      .R�5@      �� #��[  6$   ��6N   �� *ContractName+5A      5C        5D      $ 	最新价��6)   $ 90 66    5E      �� 5F      .R�5G      $   PC_Futures.ANXINYI.StockTicker  $��Center=�60   $��Right=�6K      Precy 6g   �� *
Tick.Precy+   Value 6�   �� *Tick.LastPrice+  NaNConverter#
 [ ��6�   �� *
Tick.Color+5H      5J        5K      $ 买价��6)   $ 90 65    5L      �� 5M      .R�5N      �� #��[  6$   ��6N   �� *
Tick.BidP1+#
 [ ��6�   �� *
Tick.Color+ �� ContentStringFormat 6�   �� *Tick.KeepDigits+5O      5Q        5R      $ 卖价��6)   $ 90 65    5S      �� 5T      .R�5U      �� #��[  6$   ��6N   �� *
Tick.AskP1+#
 [ ��6�   �� *
Tick.Color+ 6�   �� *Tick.KeepDigits+5V      5X        5Y      $ 买量��6)   $ 90 65    5Z      �� 5[      .R�5\      �� #��[  6$   $ 0��6�   
���  ����6N   �� *
Tick.BidV1+#
 [ 6�   5]      5_        5`      $ 卖量��6)   $ 90 65    5a      �� 5b      .R�5c      �� #��[  6$   $ 0��6�   
��� � ���6N   �� *
Tick.AskV1+#
 [ 6�   5d      5f        5g      $ 	成交量��6)   $ 90 66    5h      �� 5i      .R�5j      �� #��[  6$   $ 0��6�   ��6N   �� *Tick.TotalSize+#
 [ 6�   5k      5m        5n      $ 	持仓量��6)   $ 90 66    5o      �� 5p      .R�5q      �� #��[  6$   $ 0��6�   ��6N   �� *Tick.PositionSize+#
 [ 6�   5r      5t        5u      $ 昨收��6)   $ 90 65    5v      �� 5w      .R�5x      �� #��[  6$   ��6N   �� *Tick.PreClosePrice+#
 [  6�   �� *Tick.KeepDigits+5y      5{        5|      $ 涨跌��6)   $ 90 65    5}      �� 5~      .R�5      �� #��[  6$   ��6N   �� *Tick.Change+#
 [  6�   �� *Tick.KeepDigits+5�      5�        5�      $ 幅度%��6)   $ 90 66    5�      �� 5�      .R�5�      �� #��[  6$   $ F2��6�   ��6N   �� *Tick.ChangePct+#
 [ 6�   5�      5�        5�      $ 开盘��6)   $ 90 65    5�      �� 5�      .R�5�      �� #��[  6$   ��6N   �� *Tick.OpenPrice+#
 [ ��6�   �� *
Tick.Color+ 6�   �� *Tick.KeepDigits+5�      5�        5�      $ 最高��6)   $ 90 65    5�      �� 5�      .R�5�      �� #��[  6$   ��6N   �� *Tick.HighPrice+#
 [  6�   �� *Tick.KeepDigits+��6�   �� *
Tick.Color+5�      5�        5�      $ 最低��6)   $ 90 65    5�      �� 5�      .R�5�      �� #��[  6$   ��6N   �� *Tick.LowPrice+#
 [  6�   �� *Tick.KeepDigits+��6�   �� *
Tick.Color+5�      5�        5�      $ 时间��6)   $	 110 65    5�      �� 5�      .R�5�      �� #��[  6$   ��6N   �� *Tick.QuotesTime+5�      5�        5�      $ 代码��6)   $	 150 65    5�      �� 5�      .R�5�      �� #��[  6$   ��6N   �� *ContractCode+5�      5�      5�      5�      5�      