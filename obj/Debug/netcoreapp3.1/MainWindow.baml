   M S B A M L   `   `   `  ����G  CCard Creator, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:Card_CreatorCard_Creator  5   	       Card_Creator.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ,localclr-namespace:Card_Creator     5   	     � Title$  Card Creator��5   	   $	��720��6   $
��1280��6.   $	��720��6>   $
��1280��6K   .��5
      � -   5         �Name$ MainGrid��5
      $��0q�6   ���#3b3b3b$
|�True��6R    � RowDefinitions 5      �� 5      $��1* �6   �� 5      $��1* �6   �� 5      $��1* �6   �� 5      $��1* �6   �� 5      $	��21* �6    � ColumnDefinitions 5      �� 5      $��4* �6   �� 5      $��4* �6   �� 5      $��4* �6   �� 5      $��4* �6   �� 5      $	��36* �6   .+�5      �� -   5        $��0��5   #   $��0��60   $��4��6@   �� 5      -   5         ��Name$ 
CardButton��5       K� ZIndex$ 3��6S   $��0q�6d   $��14(�6o   $��Light"�6}   
����������#3b3b3b$��0q�6�   $��0��6�   $��0��6�   .��5      ��  �� Orientation$ 
Horizontal=�6   .+�5      �� -   5         ��Name$ CardButtonText��5      $
��Card��6(   �� 5      -   5         ��Name$ CardButtonImage��5      $��10��6%   $��/Assets/DropDownIcon.png��61   5      5       �� 5!      -   5        $ NewCardButton��5!      $��New Card��6"   $ 3��6c   $��0q�6t   $��14(�6   $��Light"�6�   
����������#3b3b3b$��0q�6�   $��0��6�   $��1��6�   �� 5"      -   5        $ SaveCardButton��5"      $��	Save Card��6#   $ 3��6f   $��0q�6w   $��14(�6�   $��Light"�6�   
����������#3b3b3b$��0q�6�   $��0��6�   $��2��6�   �� 5#      -   5        $ OpenCardButton��5#      $��	Open Card��6#   $ 3��6f   $��0q�6w   $��14(�6�   $��Light"�6�   
����������#3b3b3b$��0q�6�   $��0��6�   $��3��6�   �� 5%      -	   5        $��0��5%   #   $��0��60   $��4��6@   $��2��6Q   �� 5&      -
   5        $ 
TypeButton��5&      $ 3��6S   $��0q�6d   $��14(�6o   $��Light"�6}   
����������#3b3b3b$��0q�6�   $��1��6�   $��0��6�   .��5'      �� $ 
Horizontal=�6   .+�5(      �� -   5        $ TypeButtonText��5(      $
��Type��6(   �� 5)      -   5        $ TypeButtonImage��5)      $��10��6%   $��/Assets/DropDownIcon.png��61   5*      5+      �� 5,      -   5        $ NewTypeButton��5,      $��New Type��6"   $ 3��6c   $��0q�6t   $��14(�6   $��Light"�6�   
����������#3b3b3b$��0q�6�   $��1��6�   $��1��6�   �� 5-      -   5        $ SaveTypeButton��5-      $��	Save Type��6#   $ 3��6f   $��0q�6w   $��14(�6�   $��Light"�6�   
����������#3b3b3b$��0q�6�   $��1��6�   $��2��6�   �� 5/      -   5        $ CollectionButton��5/      $��
Collection��6%   $ 3��6T   $��0q�6e   $��14(�6p   $��Light"�6~   
����������#3b3b3b$��0q�6�   $��2��6�   $��0��6�   �� 50      -   5        $ TypesButton��50      $��Types��6    $ 4��6E   $��0q�6V   $��14(�6a   $��Light"�6o   
����������#3b3b3b$��0q�6�   $��3��6�   $��0��6�    � 52      -   5        	  �Name$
	 Main��52   
   $ 0��6   $��0q�6)   
  � NavigationUIVisibility$
 Hidden=�64   ���#242424$��1��6i   $��0��6v   $��4��6�   $��7��6�   53      54      