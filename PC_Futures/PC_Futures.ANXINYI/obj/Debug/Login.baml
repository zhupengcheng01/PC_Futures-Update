   M S B A M L   `   `   `  ����M  IPC_Futures.ANXINYI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:PC_Futures
PC_Futures  5   	   D @Utilities, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null8*clr-namespace:Utilities;assembly=Utilities	Utilities 5   
       PC_Futures.ANXINYI.Login   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   *localclr-namespace:PC_Futures     5   	   9pw*clr-namespace:Utilities;assembly=Utilities     5   
     � Title$  Login��5	   	   $	��390��6   $	��700��6$    � 
ResizeMode$ NoResize=�60    � WindowStartupLocation$ CenterScreen=�6G    � WindowStyle$
 None=�6l   ��5
      �� 5   
    �� ImageSource$K E/PC_Futures.ANXINYI;component/Resources/Images/TitleLog/denglu_bg.png��6    � 	Resources 5       �� MergedDictionaries �� Source�� 5   
    5      �� 5      $&  ControlStyles/ControlStyles.xaml?�6%   6Q   �� 5      $% ControlStyles/ButtonStyles.xaml?�6%   6P   %    5      .��5      �  � RowDefinitions 5   
   �� 5      $��23 �6   6*   �� 5      6   .+�5   
   �� $��0,0,5,2q�6   	 �� Orientation$	 
Horizontal=�6'   $��Right=�6@   .+�5      �� -   5        $	��Min��5      $��4,0,0,0q�6    $��Center=�61      WindowMaxButtonStyle#��[  6M   .��5      �� $L��F/PC_Futures.ANXINYI;component/Resources/Images/ico_system_minimize.png��6   5      �� 5!      -   5        $��Close��5!      $��4,0,0,0q�6#   $��Center=�64     WindowCloseButtonStyle#��[ 6P   .��5"      �� $I��C/PC_Futures.ANXINYI;component/Resources/Images/ico_system_close.png��6   5#      5$      � 5%   
   $��1��6   
 � ColumnDefinitions
 5&      �� 5'      6%   �� 5(      $	��380 �6#   61   .+�5*      �� $��1��6   ._�5+      �  5,      �� 5-      $
��0.5* �6(   68   �� 5.      6*   �� 5/      $
��0.8* �6(   68   .+�51      �� $��行情登录��6    $��16(�6,   $
��Bold"�6:   $��Bottom=�6L   $��Center=�6g   $��0,0,0,30q�6�   6�   �� 52      $��1��6   $��2��6*   ._�53      �  54      �� 55   "   62   �� 56   "   62   �� 57   "   62   �� 58   "   62   
 5:      �� 5;   "   $��1* �63   6@   �� 5<   "   $��2* �63   6@   .+�5>      �� $��账户名：��6(   $
��Bold"�65   $��Center=�6G   $��Right=�6b   6�   �� 5?      $��密    码：��6(   $
��Bold"�68   $��1��6J   $��Center=�6W   $��Right=�6r   6�   �� 5@      $��1��6&   $
��Bold"�69   $��Center=�6e   $��30��6�   $��0��6�   $
��Left=�6�   $	��197��6�   $��Center=�6�   $��10,0,0,0q�6�   ��6K   �� *
UserName+6�   G� 5A      $��1��6*   $��1��6:   $
��Bold"�6H   $��30��6Z   $��Center=�6f   $
��Left=�6�   $	��197��6�   $��10,0,0,0q�6�   $��Center=�6�   !  Utilities.PasswordBoxHelper   Attach .    Password 6   ��  �� Path$ Password� �� Mode$ TwoWay=��� 5B      $��1��6E   $��2��6U   $
��Left=�6b   $��Center=�6}   $��保存账号及密码��6�   $��10,0,0,0q�6�    d� 	IsChecked 6(   �� *IsSave+6�   5C      5D      �� 5E      -   5        $��login��5E   {   $	��Top=�6     LoginButton#��[ 67   $��Center=�6]   $��2��6�   $��登录��6�   $��2��6�   $��134,0.652,50,0q�6�   $	��196��6�   ��6�   �� *LoginCommand+6�   5G      5H      5I      5J      5K      