   M S B A M L   `   `   `  ����G  CMyChatClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:MyChatClientMyChatClient  5   	   !    MyChatClient.PrivateMessage   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ,localclr-namespace:MyChatClient     5   	     � Title$  PrivateMassage��5   	   $	��400��6    $	��800��6-   $	��250��69   $	��350��6H   .��5	      � $��0,0,0,10q�6    � ColumnDefinitions 5
   
   �� 5       � RowDefinitions 5   
   �� 5      $��9* �6   �� 5      $
��216* �6   �� 5      $��0* �6   �� 5      $
��100* �6   �� 5      $	��31* �6   �� 5      $��0* �6   .+�5   
   �� -   5         ��Name$ listBoxPrivateMsgList��5      $��1��61   $��1��6>   $��10q�6N   �� 5   
   -   5         ��Name$ textBoxPrivateMsgWrite��5      $��Wright message here =)��62   $��10q�6P    �� TextWrapping$
 Wrap=�6\   $��3��6p   $��30��6}   �� 5   
   -   5         ��Name$ 
SendButton��5      $
��Send��6%   $��Right=�64   $��	0,0,125,0q�6P   $��4��6c   $��75��6p   $��25��6{   $��Bottom=�6�   6�   �� 5   
   -   5        $ CloseButton��5      $��Close��6&   $��Right=�66   $��0,0,10,0q�6R   $��4��6d   $��75��6q   $��25��6|   $��Bottom=�6�   6�   5      5      