   M S B A M L   `   `   `  ����G  CMyChatClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:MyChatClientMyChatClient  5   	       MyChatClient.MainWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ,localclr-namespace:MyChatClient     5   	   -   5          � Title$  MaxLutovitChat��5   	   $��405.714��6    $��587.857��61   $	��300��6A   $	��199��6P    � WindowStartupLocation$ CenterScreen=�6`   .��5	      �  � ColumnDefinitions 5
   
   �� 5      $
��150* �6   �� 5      $
��150* �6   �� 5      $
��150* �6   �� 5      $
��150* �6    � RowDefinitions 5   
   �� 5      $	��30* �6   �� 5      $
��300* �6   �� 5      $
��100* �6   .+�5   
   �� -   5         ��Name$ listBoxMsgList��5      $��10q�6*   $��1��66   $��3��6C   �� 5   
   -   5         ��Name$ textBoxUsername��5      $
��10,3q�6+   $��1��69    �� TextWrapping$
 Wrap=�6J   $��	User name��6^    J� RenderTransformOrigin$ 0.265,0.348&�6o   �� 5   
   -   5        $ listBoxUserList��5      $��3��6+   $��10q�6;   $��1��6G   $��2��6T   6e   �� 5   
   -   5        $ textBoxWrightMsg��5      $��3��6,   $��10q�6@   $��2��6L   $
 Wrap=�6Y   $��Wright message here =)��6m   6�   �� 5   
   -   5         ��Name$ buttonConDisc��5   7   $��Connect��6N   $��3��6`   $
��10,3q�6p   $��Center=�6~   �� 5   
   -   5        $ textBoxUserPassword��5      $��1��6/   $��20��6?   $
��10,3q�6K   $
 Wrap=�6Y   $��User password��6m   $ 
-0.725,0.5&�6�   �� 5   
   -   5        $ registationButton��5      $��Registration��6,   $��2��6C   $
��10,3q�6S   $ -0.187,-0.188&�6a   $��Center=�6�   6�   5      5      