   M S B A M L   `   `   `  ����G  CMyChatClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:MyChatClientMyChatClient  5   	       MyChatClient.UserProfile   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ,localclr-namespace:MyChatClient     5   	     � Title$  UserProfile��5   	   $	��290��6   $	��600��6-   $	��600��6<   $	��300��6H   $	��600��6X   $	��290��6g    � 
ResizeMode$ NoResize=�6t   .��5	      � $��5,5,5,5q�6    � UseLayoutRounding .   � RowDefinitions 5
   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $��35 �6    � ColumnDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��126* �6   �� 5      $
��125* �6   �� 5      $
��126* �6   �� 5      $
��125* �6   .+�5   
   �� $��Login��6   $��15(�6!   $��5q�6/   �� 5   
   $��Password��6   $��15(�6#   $��5q�62   $��1��6=   �� 5   
   $
��Name��6   $��15(�6   $��5q�6-   $��2��68   �� 5   
   $	��Age��6   $��15(�6   $��5q�6,   $��3��67   �� 5   
   $	��Sex��6   $��15(�6   $��5q�6,   $��4��67   �� 5   
   -   5         ��Name$ textBoxLogin��5      $��1��6(   $��5q�68    �� TextWrapping$
 Wrap=�6C   $��2��6W   �� 5   
   -   5        $ textBoxPass��5      $��1��6'   $��5q�67   $��1��6B   $
 Wrap=�6O   $��2��6c   �� 5    
   -   5        $ textBoxName��5       $��1��6'   $��5q�67   $��2��6B   $
 Wrap=�6O   $��2��6c   �� 5!   
   -   5         ��Name$ comboBoxAge��5!      $��1��6(   $��10,10,12,10q�68   $��3��6M   $��2��6Z   �� 5"   
   -   5        $ comboBoxSex��5"      $��1��6(   $��10q�68   $��4��6D   $��2��6Q   �� 5#   
   -   5         ��Name$ applyButton��5#      $��Apply��6&   $��5,5,10,0q�66   $��5��6H   $��2��6U   6i   �� 5$   
   -   5        $ loadImageButton��5$      $��
Load image��6*   $��3��6?   $��5,5,5,0q�6O   $��5��6`   �� 5%   
   -   5        $ exiteButton��5%      $��Exite��6&   $��4��66   $��5,5,5,0q�6F   $��5��6W   6d   �� 5&   
   -	   5        $ deleteButton��5&      $��Delete��6'   $��2��69   $
��Left=�6I   $��10,5,0,0q�6d   $��5��6v   $	��Top=�6�   $	��110��6�   $��31��6�   6�   �� 5'   
   -
   5        	 ��Name$	 textblockInfo��5'      $��3��6+   $��9,5,5,5q�6;   $
��Wrap=�6L   $Q��KFor deleting a profile enter your Login, Password and press Delete button. ��6`   $��2��6�   5)      5*      